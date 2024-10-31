using BLL.Interfaces;
using BLL.Spacification;
using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Shop_web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUniteOfWork _uniteOfWork;

        public CategoryController(ApplicationDbContext context, IUniteOfWork uniteOfWork)
        {
            _context = context;

            _uniteOfWork = uniteOfWork;
        }

        #region Index Action (Get All Categories)
        public async Task<IActionResult> Index()
        {
            var spac = new BaseSpacificatons<Category>();

            var category = await _uniteOfWork.Category.GetAllAsync(spac);
            return View(category);
        }


        #endregion

        #region Create Action
        [HttpGet]
        public async Task<IActionResult> Create()
        => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                //var Result = await _category.AddCategoryAsync(category);
                await _uniteOfWork.Category.AddAsync(category);
                var Result = await _uniteOfWork.CompleteAsync();
                if (Result > 0)
                {
                    TempData["Create"] = "Category Has Created Successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(category);
        }
        #endregion


        #region Edit 
        [HttpGet]
        public async Task<IActionResult> Edit(int? IDCategory)
        {
            if (IDCategory is not null)
            {
                var spec = new BaseSpacificatons<Category>(X => X.Id == IDCategory);
                var category = await _uniteOfWork.Category.GetbyIdAsync(spec);
                if (category != null)
                {
                    return View(category);
                }
                return View(nameof(Notfound));
            }
            return View(nameof(Index));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {

            if (ModelState.IsValid)
            {
                _uniteOfWork.Category.UpdateAsync(category);
                var Result = await _uniteOfWork.CompleteAsync();
                if (Result > 0)
                {
                    TempData["Update"] = "Category Has Updated Successfully";
                    return RedirectToAction("Index");
                }
                return View(nameof(Notfound));
            }
            return View(nameof(Notfound));
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is not null)
            {
                var spec = new BaseSpacificatons<Category>(X => X.Id == id);
                var category = await _uniteOfWork.Category.GetbyIdAsync(spec);
                if (category != null)
                    return View(category);
                return View(nameof(Notfound));
            }
            else
                return View(nameof(Notfound));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Category category)
        {
            _uniteOfWork.Category.Delete(category);
            var Result = await _uniteOfWork.CompleteAsync();
            if (Result > 0)
            {
                _uniteOfWork.Dispose();
                TempData["Delete"] = "Category Has Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Notfound));
        }


        [HttpGet]
        public IActionResult Notfound()
        {
            return View();
        }
    }
}
