﻿using ModelLayer.Entities;
using RepositoryLayer.UnitOfWork;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.RequestModels;
using System.Net;
using ServiceLayer.ResponseModels;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ServiceLayer.Services
{
    public class PetService : IPetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PawFundContext _content;
        public PetService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        // Lấy tất cả các Pet
        public async Task<IEnumerable<PetResponseModel>> GetAllPetsAsync()
        {
            var pets = await _unitOfWork.Repository<Pet>()
                .AsQueryable()
                .Include(p => p.Statuses)
                    .ThenInclude(ps => ps.Status)
                .ToListAsync();

            var petResponses = new List<PetResponseModel>();

            foreach (var pet in pets)
            {
                var petResponse = new PetResponseModel
                {
                    PetID = pet.Id,
                    ShelterID = pet.ShelterID,
                    UserID = pet.UserID,
                    Name = pet.Name,
                    Type = pet.Type,
                    Breed = pet.Breed,
                    Gender = pet.Gender,
                    Age = pet.Age,
                    Size = pet.Size,
                    Color = pet.Color,
                    Description = pet.Description,
                    AdoptionStatus = pet.AdoptionStatus,
                    Image = pet.Image,
                    Statuses = pet.Statuses?.Select(ps => new StatusResponseModel
                    {
                        StatusId = ps.Status.Id,
                        Disease = ps.Status.Disease,
                        Vaccine = ps.Status.Vaccine
                    }).ToList()
                };

                petResponses.Add(petResponse);
            }

            return petResponses;
        }


        // Lấy Pet theo ID
        public async Task<PetResponseModel> GetPetByIdAsync(int id)
        {
            var pet = await _unitOfWork.Repository<Pet>()
                .AsQueryable()
                .Include(p => p.Statuses)
                    .ThenInclude(ps => ps.Status)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pet == null)
                throw new Exception($"Không tìm thấy Pet với ID {id}.");

            var petResponse = new PetResponseModel
            {
                PetID = pet.Id,
                ShelterID = pet.ShelterID,
                UserID = pet.UserID,
                Name = pet.Name,
                Type = pet.Type,
                Breed = pet.Breed,
                Gender = pet.Gender,
                Age = pet.Age,
                Size = pet.Size,
                Color = pet.Color,
                Description = pet.Description,
                AdoptionStatus = pet.AdoptionStatus,
                Image = pet.Image,
                Statuses = pet.Statuses?.Select(ps => new StatusResponseModel
                {
                    StatusId = ps.Status.Id,
                    Date = DateTime.UtcNow,
                    Disease = ps.Status.Disease,
                    Vaccine = ps.Status.Vaccine
                }).ToList()
            };

            return petResponse;
        }

        // Tạo mới Pet
        public async Task<PetResponseModel> CreatePetAsync(PetCreateRequestModel createPetRequest)
        {
            var pet = new Pet
            {
                ShelterID = createPetRequest.ShelterID,
                UserID = createPetRequest.UserID,
                Name = createPetRequest.Name,
                Type = createPetRequest.Type,
                Breed = createPetRequest.Breed,
                Gender = createPetRequest.Gender,
                Age = createPetRequest.Age,
                Size = createPetRequest.Size,
                Color = createPetRequest.Color,
                Description = createPetRequest.Description,
                AdoptionStatus = createPetRequest.AdoptionStatus,
                Image = createPetRequest.Image
            };

            await _unitOfWork.Repository<Pet>().InsertAsync(pet);
            await _unitOfWork.CommitAsync();

            // Trả về Pet đã được tạo với thông tin mới
            return await GetPetByIdAsync(pet.Id);
        }


        // Cập nhật Pet
        public async Task<PetResponseModel> UpdatePetAsync(int id, PetUpdateRequestModel updatePetRequest)
        {
            var existingPet = await _unitOfWork.Repository<Pet>().GetById(id);
            if (existingPet == null)
                throw new Exception($"Không tìm thấy Pet với ID {id}.");

            // Cập nhật các thuộc tính
            existingPet.ShelterID = updatePetRequest.ShelterID;
            existingPet.UserID = updatePetRequest.UserID;
            existingPet.Name = updatePetRequest.Name;
            existingPet.Type = updatePetRequest.Type;
            existingPet.Breed = updatePetRequest.Breed;
            existingPet.Gender = updatePetRequest.Gender;
            existingPet.Age = updatePetRequest.Age;
            existingPet.Size = updatePetRequest.Size;
            existingPet.Color = updatePetRequest.Color;
            existingPet.Description = updatePetRequest.Description;
            existingPet.AdoptionStatus = updatePetRequest.AdoptionStatus;
            existingPet.Image = updatePetRequest.Image;

            _unitOfWork.Repository<Pet>().Update(existingPet, id);
            await _unitOfWork.CommitAsync();

            // Trả về Pet đã được cập nhật
            return await GetPetByIdAsync(id);
        }

        // Xóa Pet
        public async Task<bool> DeletePetAsync(int id)
        {
            var pet = await _unitOfWork.Repository<Pet>().GetById(id);
            if (pet == null)
                throw new Exception($"Không tìm thấy Pet với ID {id}.");

            _unitOfWork.Repository<Pet>().Delete(pet);
            await _unitOfWork.CommitAsync();

            return true;
        }


        // Cập nhật Status của Pet
        public async Task UpdatePetStatusAsync(int petId, int statusId, StatusUpdateRequestModel updateStatusRequest)
        {
            var petStatus = await _unitOfWork.Repository<PetStatus>()
                .AsQueryable()
                .Include(ps => ps.Status)
                .FirstOrDefaultAsync(ps => ps.PetId == petId && ps.StatusId == statusId);

            if (petStatus == null)
                throw new Exception($"Không tìm thấy Status với ID {statusId} cho Pet với ID {petId}.");

            // Cập nhật thông tin Status
            petStatus.Status.Disease = updateStatusRequest.Disease;
            petStatus.Status.Vaccine = updateStatusRequest.Vaccine;
            petStatus.Status.Date = DateTime.UtcNow;

            _unitOfWork.Repository<Status>().Update(petStatus.Status, petStatus.StatusId);
            await _unitOfWork.CommitAsync();
        }

        // Thêm Status vào Pet
        public async Task AddStatusToPetAsync(int petId, CreatePetStatusRequest createPetStatusRequest)
        {
            var pet = await _unitOfWork.Repository<Pet>()
                .AsQueryable()
                .Include(p => p.Statuses)
                .FirstOrDefaultAsync(p => p.Id == petId);

            if (pet == null)
                throw new Exception($"Không tìm thấy Pet với ID {petId}.");

            // Kiểm tra xem Status đã tồn tại cho Pet này chưa
            if (pet.Statuses.Any(ps => ps.StatusId == createPetStatusRequest.StatusId))
                throw new Exception($"Status với ID {createPetStatusRequest.StatusId} đã tồn tại cho Pet với ID {petId}.");

            var petStatus = new PetStatus
            {
                PetId = petId,
                StatusId = createPetStatusRequest.StatusId
            };

            await _unitOfWork.Repository<PetStatus>().InsertAsync(petStatus);
            await _unitOfWork.CommitAsync();
        }



        // Xóa Status khỏi Pet
        public async Task RemoveStatusFromPetAsync(int petId, int statusId)
        {
            var repository = _unitOfWork.Repository<PetStatus>();

            var petStatus = await repository
                .AsQueryable()
                .FirstOrDefaultAsync(ps => ps.PetId == petId && ps.StatusId == statusId);

            if (petStatus == null)
                throw new Exception($"Không tìm thấy Status với ID {statusId} cho Pet với ID {petId}.");

            repository.Delete(petStatus);
            await _unitOfWork.CommitAsync();
        }
    }
}
