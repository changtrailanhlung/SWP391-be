using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.RequestModels
{
    public class DonationCreateRequestModel
    {
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public int DonorId { get; set; }
        [Required]
        public int ShelterId { get; set; }
    }
    public class DonationUpdateRequestModel
    {
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public int DonorId { get; set; }
        [Required]
        public int ShelterId { get; set; }
    }
    public class TotalShelterResponseModel
    {
        public int ShelterId { get; set; }
        public decimal TotalDonation { get; set; }
    }

    public class TotalDonorResponseModel
    {
        public int DonorID { get; set; }
        public decimal TotalDonation { get; set; }
    }
}
