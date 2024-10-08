﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ResponseModels
{
    // PetsResponseModel: Dùng để trả về thông tin cơ bản của Pet
    public class PetResponseModel
    {
        public int PetID { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Breed { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public int Age { get; set; }
        public string Size { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string AdoptionStatus { get; set; } = null!;
        public string? Image { get; set; }
        public int ShelterID { get; set; }

        
        public int UserID { get; set; }
    }

    public class PetDetailResponse : PetResponseModel
    {
        public string Description { get; set; } = null!;
        public int StatusId { get; set; }
        public string? ShelterName { get; set; } 
        public string? UserName { get; set; } 
    }

}
