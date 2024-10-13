using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ResponseModels
{
	public class NotificationResponseModel
	{
		public int Id { get; set; }
		public string Message { get; set; } = null!;
		public DateTime Date { get; set; }
		public int UserId { get; set; }
	}
}
