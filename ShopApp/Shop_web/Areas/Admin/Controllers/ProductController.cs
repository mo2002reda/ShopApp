using AutoMapper;
using BLL.Interfaces;
using BLL.Spacification;
using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Shop_web.Helper;
using Shop_web.ViewModels;

namespace Shop_web.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IMapper _mapper;
        public ProductController(ApplicationDbContext context, 
                                    IUniteOfWork uniteOfWork,
                                    IMapper mapper
                                )
        {
            _context = context;
            _uniteOfWork = uniteOfWork;
            _mapper = mapper;
        }

        #region Index Action (Get All Products)
        public async Task<IActionResult> Index()
          => View();

        public async Task<IActionResult> GetAll()
        {
            var spac = new ProductWithCategorySpacification();
            var Products = await _uniteOfWork.Product.GetAllAsync(spac);
            if (Products == null || !Products.Any())
            {
                Console.WriteLine("No products returned from database.");
            }
           
            return Json(new { data = Products });
        }
        #endregion

        #region Create Action
        [HttpGet]
        public async Task<IActionResult> Create()
        =>  View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        //IFormFile variable_name must be the same name of name that exist at the Create view (at img attribute)
        public async Task<IActionResult> Create(ProductViewModel productVm)
        {
            if (ModelState.IsValid)
            {
                productVm.ImgName=DocumentationSettings.Upload(productVm.Image, "Products");
                var mappedProduct=_mapper.Map<ProductViewModel,Product>(productVm);
                await _uniteOfWork.Product.AddAsync(mappedProduct);
                var Result = await _uniteOfWork.CompleteAsync();
                if (Result > 0)
                {
                    TempData["Create"] = "Product Has Created Successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(productVm);
        }
        #endregion


        #region Edit 
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is not null)
            {
                TempData["ID"] = id;
                var spec = new ProductWithCategorySpacification(id);
                var product = await _uniteOfWork.Product.GetbyIdAsync(spec);
                if (product != null)
                {
                     
                    var ProductVM = _mapper.Map<Product, ProductViewModel>(product);
                    //ProductVM.Image = $"/Images/Products/{product.Img}";
                  //  ViewData["Image"] = ProductVM.ImgName; 
                    return View(ProductVM);
                }
                return View(nameof(Notfound));
            }
            return View(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel productVM)
        {

            if (ModelState.IsValid)
            {
                try//use this cause if ther is problems in saving at database
                {
                    productVM.ImgName = DocumentationSettings.Upload(productVM.Image, "Products");
                    var mappedProduct = _mapper.Map<ProductViewModel, Product>(productVM);
                    _uniteOfWork.Product.UpdateAsync(mappedProduct);
                    int result = await _uniteOfWork.CompleteAsync();
                    if (result > 0)
                        return RedirectToAction(nameof(Index));
                    return BadRequest(StatusCodes.Status400BadRequest);
                }
                catch (Exception ex)
                {//1)Log the Execption : save and Send the error to develop Team
                 //2)Form :Send error at form to user
                    ModelState.AddModelError("", ex.Message);
                }
            }  
            return View(nameof(Notfound));
        }
        #endregion

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is not null)
            {
                var spec = new BaseSpacificatons<Product>(X => X.Id == id);
                var product = await _uniteOfWork.Product.GetbyIdAsync(spec);
                if (product != null)
                {
                     _uniteOfWork.Product.Delete(product);
                     DocumentationSettings.DeleteFile(product.Img, "Products");
                     var Result = await _uniteOfWork.CompleteAsync();
                        _uniteOfWork.Dispose();
                       return Json(new { success = true, message = "Product Has been Deleted" });
                    
                }
                    return Json(new {success=false,message="Error While Deleting"});
            }
            else
                return View(nameof(Notfound));
        }


        [HttpGet]
        public IActionResult Notfound()
        {
            return View();
        }
    }
}
