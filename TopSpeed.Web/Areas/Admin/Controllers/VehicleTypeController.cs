using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TopSpeed.Application.ApplicationConstants;
using TopSpeed.Application.Contracts.Presistence;
using TopSpeed.Domain.Models;
using TopSpeed.Infrastructure.Common;

using TopSpeed.Infrastructure.UnitOfWork;

namespace TopSpeed.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class VehicleTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VehicleTypeController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
           _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<VechicleType> vehicleType = await  _unitOfWork.VehicleType.GetAllAsync();

            return View(vehicleType);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(VechicleType vechicleType)
        {
            
            if (ModelState.IsValid)
            {
                await _unitOfWork.VehicleType.Create(vechicleType);
                await _unitOfWork.SaveAsync();

                TempData["success"] = CommonMessage.RecordCreated;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            VechicleType vehicleType = await _unitOfWork.VehicleType.GetByIdAsync(id);

            return View(vehicleType);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            VechicleType vehicleType = await _unitOfWork.VehicleType.GetByIdAsync(id);

            return View(vehicleType);
        }

        [HttpPost]
        public  async Task <IActionResult> Edit(VechicleType vechicleType)
        {
            if (ModelState.IsValid)

            {

                await _unitOfWork.VehicleType.Update(vechicleType);
                await _unitOfWork.SaveAsync();

                TempData["warning"] = CommonMessage.RecordUpdated;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public async Task <IActionResult> Delete(Guid id)
        {
            VechicleType vechicleType = await _unitOfWork.VehicleType.GetByIdAsync(id);

            return View(vechicleType);
        }

        [HttpPost]
        public async Task <IActionResult> Delete(VechicleType vehicleType)
        {
           

            await _unitOfWork.VehicleType.Delete(vehicleType);
            await _unitOfWork.SaveAsync();

            TempData["error"] = CommonMessage.RecordDeleted;
            return RedirectToAction(nameof(Index));
        }

    }


}
