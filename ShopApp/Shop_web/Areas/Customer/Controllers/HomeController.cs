using AutoMapper;
using BLL.Interfaces;
using BLL.Spacification;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Shop_web.ViewModels;
using System.Collections.Generic;

namespace Shop_web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IMapper _mapper;

        public HomeController(IUniteOfWork uniteOfWork,
                               IMapper mapper)
        {
            _uniteOfWork = uniteOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var spac = new BaseSpacificatons<Product>();
            var Products = await _uniteOfWork.Product.GetAllAsync(spac);
       
            if (Products != null )
            {
                var MappedPrducts = _mapper.Map<IEnumerable<Product>, IEnumerable < ProductViewModel >> (Products);
                return View(MappedPrducts);
            }
            return BadRequest();
        }
        public async Task<IActionResult> Details(int? id)
        {   if (id != null)
            {
                var spac = new ProductWithCategorySpacification(id);
                var Product = await _uniteOfWork.Product.GetbyIdAsync(spac);
                if (Product != null)
                {
                    var mappedProduct = _mapper.Map<Product, ProductCardViewModel>(Product);
                    return View(mappedProduct);
                }
            }
            return BadRequest();
        }
    }
}
