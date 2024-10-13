using Microsoft.AspNetCore.Mvc;
using ModelLayer.Entities;
using ServiceLayer.Interfaces;
using ServiceLayer.RequestModels;
using ServiceLayer.ResponseModels;
using ServiceLayer.Services;
using System.Drawing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SWP391_PawFund.Controllers
{

    [Route("api/Donate")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private readonly IDonateService _donateService;
        private readonly IUsersService _usersService;
        private readonly IShelterService _shelterService;
        public DonationController(IDonateService donateService, IUsersService usersService, IShelterService shelterService)
        {
            _donateService = donateService;
            _usersService = usersService;
            _shelterService = shelterService;
        }


        // Lấy danh sách tất cả các donation
        [HttpGet]
        public ActionResult<IEnumerable<DonationResponseModel>> GetAllDonations()
        {
            var donations = _donateService.GetAllDonations()
               .Select(d => new DonationDetailResponseModel
               {
                   Id = d.Id,
                   Amount = d.Amount,
                   Date = d.Date,
                   DonorId = d.DonorId,
                   ShelterId = d.ShelterId,
               });
            //var donations= _donateService.GetAllDonations();
            return Ok(donations);
        }


        // Lấy donation theo Id
        [HttpGet("{id}")]
        public async Task<ActionResult<DonationDetailResponseModel>> GetDonationById(int id)
        {
            var donation = await _donateService.GetDonationsByIdAsync(id);
            if (donation == null)
            {
                return NotFound(new { message = "Donation not found." });
            }

            var donor = await _usersService.GetUserByIdAsync(donation.DonorId);
            var shelter = await _shelterService.GetShelterByID(donation.ShelterId);

            var response = new DonationDetailResponseModel
            {
                Id = donation.Id,
                Amount = donation.Amount,
                Date = donation.Date,
                DonorId = donation.DonorId,
                ShelterId = donation.ShelterId,
                Donor = donor != null ? new UsersResponseModel
                {
                    Id = donor.Id,
                    Username = donor.Username,
                    Email = donor.Email,
                    Location = donor.Location,
                    Phone = donor.Phone,
                    TotalDonation = (decimal)donor.TotalDonation
                } : null,
                Shelter = shelter != null ? new ShelterResponseModel
                {
                    Id = shelter.Id,
                    Name = shelter.Name,
                    Location = shelter.Location,
                    PhoneNumber = shelter.PhoneNumber,
                    Capacity = shelter.Capaxity,
                    Email = shelter.Email,
                    Website = shelter.Website,
                    DonationAmount = (decimal)shelter.DonationAmount
                } : null
            };

            return Ok(response);
        }
        // Lấy danh sách donations theo DonorId
        [HttpGet("by-donor/{donorId}")]
        public async Task<ActionResult<IEnumerable<DonationDetailResponseModel>>> GetAllDonationsByDonorId(int donorId)
        {
            try
            {
                var donations = await _donateService.GetDonationsByDonorId(donorId);

                if (donations == null)
                {
                    return NotFound(new { message = $"No donations found for DonorId {donorId}." });
                }

                var response = donations.Select(d => new DonationDetailResponseModel
                {
                    Id = d.Id,
                    Amount = d.Amount,
                    Date = d.Date,
                    DonorId = d.DonorId,
                    ShelterId = d.ShelterId,
                    Donor = d.User != null ? new UsersResponseModel
                    {
                        Id = d.User.Id,
                        Username = d.User.Username,
                        Email = d.User.Email,
                        Location = d.User.Location,
                        Phone = d.User.Phone,
                        TotalDonation =(decimal) d.User.TotalDonation
                    } : null,
                    Shelter = d.Shelter != null ? new ShelterResponseModel
                    {
                        Id = d.Shelter.Id,
                        Name = d.Shelter.Name,
                        Location = d.Shelter.Location,
                        PhoneNumber = d.Shelter.PhoneNumber,
                        Capacity = d.Shelter.Capaxity, 
                        Email = d.Shelter.Email,
                        Website = d.Shelter.Website,
                        DonationAmount =(decimal) d.Shelter.DonationAmount
                    } : null
                });

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }



        // Thêm donation mới
        [HttpPost("CreateDonate")]
        public async Task<IActionResult> CreateDonation([FromBody] DonationCreateRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var donor = await _usersService.GetUserByIdAsync(request.DonorId);
            var shelter = await _shelterService.GetShelterByID(request.ShelterId);

            if (donor == null)
            {
                return NotFound(new { message = "Donor not found." });
            }

            if (shelter == null)
            {
                return NotFound(new { message = "Shelter not found." });
            }

            var donation = new Donation
            {
                Amount = request.Amount,
                Date = request.Date,
                DonorId = request.DonorId,
                ShelterId = request.ShelterId
            };

            await _donateService.CreateDonationAsync(donation);

            // Kiểm tra nullable trước khi ép kiểu
            decimal totalDonation = donor.TotalDonation ?? 0m; 
            decimal donationAmount = shelter.DonationAmount ?? 0m; 

            var response = new DonationDetailResponseModel
            {
                Id = donation.Id,
                Amount = donation.Amount,
                Date = donation.Date,
                DonorId = donation.DonorId,
                ShelterId = donation.ShelterId,
                Donor = donor != null ? new UsersResponseModel
                {
                    Id = donor.Id,
                    Username = donor.Username,
                    Email = donor.Email,
                    Location = donor.Location,
                    Phone = donor.Phone,
                    TotalDonation = totalDonation
                } : null,
                Shelter = shelter != null ? new ShelterResponseModel
                {
                    Id = shelter.Id,
                    Name = shelter.Name,
                    Location = shelter.Location,
                    PhoneNumber = shelter.PhoneNumber,
                    Capacity = shelter.Capaxity,
                    Email = shelter.Email,
                    Website = shelter.Website,
                    DonationAmount = donationAmount
                } : null
            };

            return CreatedAtAction(nameof(GetDonationById), new { id = donation.Id }, response);
        }




        // Cập nhật donation
        [HttpPut("Update_Donate/{id}")]
        public async Task<IActionResult> UpdateDonation(int id, [FromBody] DonationUpdateRequestModel request)
        {
            var existingDonation = await _donateService.GetDonationsByIdAsync(id);
            if (existingDonation == null)
            {
                return NotFound(new { message = "Donation not found." });
            }

            var donor = await _usersService.GetUserByIdAsync(request.DonorId);
            var shelter = await _shelterService.GetShelterByID(request.ShelterId);

            if (donor == null)
            {
                return NotFound(new { message = "Donor not found." });
            }

            if (shelter == null)
            {
                return NotFound(new { message = "Shelter not found." });
            }

            existingDonation.Amount = request.Amount;
            existingDonation.Date = request.Date;
            existingDonation.DonorId = request.DonorId;
            existingDonation.ShelterId = request.ShelterId;

            await _donateService.UpdateDonationAsync(existingDonation);
            return Ok(new { message = "Donation has been updated successfully." });
        }



        // Xóa donation theo Id
        [HttpDelete("Delete_Donate/{id}")]
        public async Task<IActionResult> DeleteDonation(int id)
        {
            var donation = await _donateService.GetDonationsByIdAsync(id);
            if (donation == null)
            {
                return NotFound(new { message = "Donation not found." });
            }

            await _donateService.DeleteDonationAsync(id);
            return Ok(new { message = "Donation has been deleted successfully." });
        }

        // Lấy tổng donation theo ShelterId
        [HttpGet("Shelter/{shelterId}/Total")]
        public ActionResult<TotalShelterDonationResponseModel> GetTotalDonationByShelter(int shelterId)
        {
            var totalDonation = _donateService.GetTotalDonationByShelter(shelterId);
            return Ok(new TotalShelterDonationResponseModel
            {
                ShelterId = shelterId,
                TotalDonation = totalDonation
            });
        }

        // Lấy tổng donation theo DonorId (accountId)
        [HttpGet("Donor/{donorId}/Total")]
        public ActionResult<TotalDonorDonationResponseModel> GetTotalDonationByDonor(int donorId)
        {
            var totalDonation = _donateService.GetTotalDonationByDonor(donorId);
            return Ok(new TotalDonorDonationResponseModel
            {
                DonorId = donorId,
                TotalDonation = totalDonation
            });
        }
    }
}
