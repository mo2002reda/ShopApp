using BLL.Interfaces;
using BLL.Spacification;
using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Shop_web.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUniteOfWork _uniteOfWork;

        public ProductController(ApplicationDbContext context, IUniteOfWork uniteOfWork)
        {
            _context = context;

            _uniteOfWork = uniteOfWork;
        }

        #region Index Action (Get All Products)
        public async Task<IActionResult> Index()
          => View();

        #endregion

        public async Task<IActionResult> GetAll()
        {
            var spac = new ProductWithCategorySpacification();
            var Products = await _uniteOfWork.Product.GetAllAsync(spac);
            if (Products == null || !Products.Any())
            {
                Console.WriteLine("No products returned from database.");
            }
            else
            {
                Console.WriteLine($"Fetched {Products.Count()} products from database.");
            }
            return Json(new { data = Products });
        }
        #region Create Action
        [HttpGet]
        public async Task<IActionResult> Create()
        => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                //var Result = await _Product.AddCategoryAsync(Product);
                await _uniteOfWork.Product.AddAsync(product);
                var Result = await _uniteOfWork.CompleteAsync();
                if (Result > 0)
                {
                    TempData["Create"] = "Product Has Created Successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(product);
        }
        #endregion


        #region Edit 
        [HttpGet]
        public async Task<IActionResult> Edit(int? IDCategory)
        {
            if (IDCategory is not null)
            {
                var spec = new BaseSpacificatons<Product>(X => X.Id == IDCategory);
                var product = await _uniteOfWork.Product.GetbyIdAsync(spec);
                if (product != null)
                {
                    return View(product);
                }
                return View(nameof(Notfound));
            }
            return View(nameof(Index));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {

            if (ModelState.IsValid)
            {
                _uniteOfWork.Product.UpdateAsync(product);
                var Result = await _uniteOfWork.CompleteAsync();
                if (Result > 0)
                {
                    TempData["Update"] = "Product Has Updated Successfully";
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
                var spec = new BaseSpacificatons<Product>(X => X.Id == id);
                var product = await _uniteOfWork.Product.GetbyIdAsync(spec);
                if (product != null)
                    return View(product);
                return View(nameof(Notfound));
            }
            else
                return View(nameof(Notfound));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Product product)
        {
            _uniteOfWork.Product.Delete(product);
            var Result = await _uniteOfWork.CompleteAsync();
            if (Result > 0)
            {
                _uniteOfWork.Dispose();
                TempData["Delete"] = "Product Has Deleted Successfully";
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
