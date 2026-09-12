using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TopSpeed.Application.ApplicationConstants;
using TopSpeed.Application.Contracts.Presistence;
using TopSpeed.Domain.ApplicationEnum;
using TopSpeed.Domain.Models;
using TopSpeed.Domain.ViewModel;
using TopSpeed.Infrastructure.Common;
using TopSpeed.Infrastructure.UnitOfWork;

namespace TopSpeed.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PostController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
           _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Post> posts = await _unitOfWork.Post.GetAllAsync();

            return View(posts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> brandList = _unitOfWork.Brand.Query().Select(x => new SelectListItem
            {
                Text = x.Name.ToUpper(),
                Value = x.Id.ToString()

            }
      );

            IEnumerable<SelectListItem> vehicleTypeList = _unitOfWork.VehicleType.Query().Select(x => new SelectListItem
            {
                Text = x.Name.ToUpper(),
                Value = x.Id.ToString()

            }



       );
            IEnumerable<SelectListItem> engineAndFuelType = Enum.GetValues(typeof(EngineandFuelType))
                .Cast<EngineandFuelType>()
                .Select(x => new SelectListItem
                {
                    Text = x.ToString().ToUpper(),
                    Value = ((int)x).ToString()

                });

            IEnumerable<SelectListItem> transmission = Enum.GetValues(typeof(Transmission))
                .Cast<Transmission>()
                .Select(x => new SelectListItem
                {
                    Text = x.ToString().ToUpper(),
                    Value = ((int)x).ToString()

                });

            PostVM postVM = new PostVM()
            {
                Post = new Post(),
                BrandList = brandList,
                VehicleTypeList= vehicleTypeList,
                TransmissionList= transmission

            };

            return View(postVM);
        }

        [HttpPost]

        public async Task<IActionResult> Create (Post post)
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
                post.VehicleImage = @"\images\post\" + newFileName + extension;
            }
            if (ModelState.IsValid)
            {
                await _unitOfWork.Post.Create(post);
                await _unitOfWork.SaveAsync();

                TempData["success"] = CommonMessage.RecordCreated;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            Post post = await _unitOfWork.Post.GetByIdAsync(id);
            return View(post);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            Post post = await _unitOfWork.Post.GetByIdAsync(id);

            return View(post);
        }

        [HttpPost]
        public  async Task <IActionResult> Edit(Post post)
        {

            string webRootPath = _webHostEnvironment.WebRootPath;

            var file = HttpContext.Request.Form.Files;

            if (file.Count > 0)
            {
                string newFileName = Guid.NewGuid().ToString();

                var upload = Path.Combine(webRootPath, @"images\post");

                var extension = Path.GetExtension(file[0].FileName);

                // delete old image
                

                var objFromDb = await _unitOfWork.Post.GetByIdAsync(post.Id); 

                if (objFromDb.VehicleImage != null)
                {
                    var oldImagePath = Path.Combine(webRootPath, objFromDb.VehicleImage.Trim('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                }
                post.VehicleImage = @"\images\post\" + newFileName + extension;
            }


            if (ModelState.IsValid)

            {

                await _unitOfWork.Post.Update(post);
                await _unitOfWork.SaveAsync();

                TempData["warning"] = CommonMessage.RecordUpdated;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public async Task <IActionResult> Delete(Guid id)
        {
            Post post = await _unitOfWork.Post.GetByIdAsync(id);

            return View(post);
        }

        [HttpPost]
        public async Task <IActionResult> Delete(Post post)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;

            if (!string.IsNullOrEmpty(post.VehicleImage))
            {
                // delete old image


                var objFromDb = await _unitOfWork.Post.GetByIdAsync(post.Id);

                if (objFromDb.VehicleImage != null)
                {
                    var oldImagePath = Path.Combine(webRootPath, objFromDb.VehicleImage.Trim('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
            }

            await _unitOfWork.Post.Delete(post);
            await _unitOfWork.SaveAsync();

            TempData["error"] = CommonMessage.RecordDeleted;
            return RedirectToAction(nameof(Index));
        }

    }


}
