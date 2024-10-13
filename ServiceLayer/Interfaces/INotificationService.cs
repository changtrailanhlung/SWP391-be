using FirebaseAdmin.Messaging;
using ServiceLayer.RequestModels;
using ServiceLayer.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
	public interface INotificationService
	{
		Task<IEnumerable<NotificationResponseModel>> GetAllNotificationsAsync();
		Task<NotificationResponseModel> GetNotificationByIdAsync(int id);
		Task CreateNotificationAsync(CreateNotificationRequestModel request);
		Task UpdateNotificationAsync(int id, UpdateNotificationRequestModel request);
		Task DeleteNotificationAsync(int id);
	}
}
