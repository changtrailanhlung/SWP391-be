using ModelLayer.Entities;
using RepositoryLayer.UnitOfWork;
using ServiceLayer.Interfaces;
using ServiceLayer.RequestModels;
using ServiceLayer.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceLayer.Services
{
	public class NotificationService : INotificationService
	{
		private readonly IUnitOfWork _unitOfWork;

		public NotificationService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<NotificationResponseModel>> GetAllNotificationsAsync()
		{
			var notifications = _unitOfWork.Repository<Notification>().GetAll();
			var response = notifications.Select(n => new NotificationResponseModel
			{
				Id = n.Id,
				Message = n.Message,
				Date = n.Date,
				UserId = n.UserId
			});

			return await Task.FromResult(response);
		}

		public async Task<NotificationResponseModel> GetNotificationByIdAsync(int id)
		{
			var notification = await _unitOfWork.Repository<Notification>().GetById(id);

			if (notification == null)
			{
				throw new Exception($"Notification with ID {id} not found.");
			}

			return new NotificationResponseModel
			{
				Id = notification.Id,
				Message = notification.Message,
				Date = notification.Date,
				UserId = notification.UserId
			};
		}

		public async Task CreateNotificationAsync(CreateNotificationRequestModel request)
		{
			var newNotification = new Notification
			{
				Message = request.Message,
				Date = request.Date,
				UserId = request.UserId
			};

			await _unitOfWork.Repository<Notification>().InsertAsync(newNotification);
			await _unitOfWork.CommitAsync();
		}

		public async Task UpdateNotificationAsync(int id, UpdateNotificationRequestModel request)
		{
			var existingNotification = await _unitOfWork.Repository<Notification>().GetById(id);

			if (existingNotification == null)
			{
				throw new Exception($"Notification with ID {id} not found.");
			}

			existingNotification.Message = request.Message;
			existingNotification.Date = request.Date;
			existingNotification.UserId = request.UserId;

			await _unitOfWork.Repository<Notification>().Update(existingNotification, existingNotification.Id);
			await _unitOfWork.CommitAsync();
		}

		public async Task DeleteNotificationAsync(int id)
		{
			var existingNotification = await _unitOfWork.Repository<Notification>().GetById(id);

			if (existingNotification == null)
			{
				throw new Exception($"Notification with ID {id} not found.");
			}

			_unitOfWork.Repository<Notification>().Delete(existingNotification);
			await _unitOfWork.CommitAsync();
		}
	}
}
