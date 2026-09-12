using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TopSpeed.Application.ApplicationConstants;
using TopSpeed.Application.Contracts.Presistence;
using TopSpeed.Domain.ApplicationEnum;
using TopSpeed.Domain.Models;
using TopSpeed.Infrastructure.Common;
using TopSpeed.Infrastructure.UnitOfWork;

namespace TopSpeed.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BrandController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
           _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Brand> posts = await  _unitOfWork.Brand.GetAllAsync();

            return View(posts);
        }

        [HttpGet]
        public IActionResult Create()
        {
      
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create (Brand post)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;

            var file = HttpContext.Request.Form.Files;

            if(file.Count > 0)
            {
                string newFileName = Guid.NewGuid().ToString();

                var upload = Path.Combine(webRootPath, @"images\post");

                var extension = Path.GetExtension(file[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(upload,newFileName + extension), FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                }
                post.BrandLogo = @"\images\post\" + newFileName + extension;
            }
            if (ModelState.IsValid)
            {
                await _unitOfWork.Brand.Create(post);
                await _unitOfWork.SaveAsync();

                TempData["success"] = CommonMessage.RecordCreated;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            Brand post = await _unitOfWork.Brand.GetByIdAsync(id);
            return View(post);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            Brand post = await _unitOfWork.Brand.GetByIdAsync(id);

            return View(post);
        }

        [HttpPost]
        public  async Task <IActionResult> Edit(Brand post)
        {

            string webRootPath = _webHostEnvironment.WebRootPath;

            var file = HttpContext.Request.Form.Files;

            if (file.Count > 0)
            {
                string newFileName = Guid.NewGuid().ToString();

                var upload = Path.Combine(webRootPath, @"images\post");

                var extension = Path.GetExtension(file[0].FileName);

                // delete old image
                

                var objFromDb = await _unitOfWork.Brand.GetByIdAsync(post.Id); 

                if (objFromDb.BrandLogo != null)
                {
                    var oldImagePath = Path.Combine(webRootPath, objFromDb.BrandLogo.Trim('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                }
                post.BrandLogo = @"\images\post\" + newFileName + extension;
            }


            if (ModelState.IsValid)

            {

                await _unitOfWork.Brand.Update(post);
                await _unitOfWork.SaveAsync();

                TempData["warning"] = CommonMessage.RecordUpdated;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public async Task <IActionResult> Delete(Guid id)
        {
            Brand post = await _unitOfWork.Brand.GetByIdAsync(id);

            return View(post);
        }

        [HttpPost]
        public async Task <IActionResult> Delete(Brand post)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;

            if (!string.IsNullOrEmpty(post.BrandLogo))
            {
                // delete old image


                var objFromDb = await _unitOfWork.Brand.GetByIdAsync(post.Id);

                if (objFromDb.BrandLogo != null)
                {
                    var oldImagePath = Path.Combine(webRootPath, objFromDb.BrandLogo.Trim('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
            }

            await _unitOfWork.Brand.Delete(post);
            await _unitOfWork.SaveAsync();

            TempData["error"] = CommonMessage.RecordDeleted;
            return RedirectToAction(nameof(Index));
        }

    }


}
