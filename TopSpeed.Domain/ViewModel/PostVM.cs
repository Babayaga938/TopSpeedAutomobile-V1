using System;
using System.Collections.Generic;
using System.Text;

using TopSpeed.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TopSpeed.Domain.ViewModel
{
    public class PostVM
    {
        public Post Post { get; set; }

        public IEnumerable<SelectListItem> BrandList { get; set; }

        public IEnumerable<SelectListItem> VehicleTypeList { get; set; }

        public IEnumerable<SelectListItem> EngineAndFuelTypeList { get; set; }

        public IEnumerable<SelectListItem> TransmissionList { get; set; }




    }
}
