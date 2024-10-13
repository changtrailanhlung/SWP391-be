using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.RequestModels
{
	public class CreateEventRequestModel
	{
		public int ShelterId { get; set; }
		public string Name { get; set; } = null!;
		public DateTime Date { get; set; }
		public string Description { get; set; } = null!;
		public string Location { get; set; } = null!;
	}
	public class UpdateEventRequestModel
	{
		public int ShelterId { get; set; }

		public string Name { get; set; } = null!;
		public DateTime Date { get; set; }
		public string Description { get; set; } = null!;
		public string Location { get; set; } = null!;
	}

}
