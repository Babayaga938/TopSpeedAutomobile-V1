using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TopSpeed.Domain.Common;

namespace TopSpeed.Domain.Models
{
    public class VechicleType : BaseModel
    {
        [Required]
        public string Name { get; set; }
    }
}
