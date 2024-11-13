using AutoMapper;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_web.ViewModels;

namespace Shop_web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _user;
        private readonly IMapper _mapper;
        private readonly IUniteOfWork _uniteOfWork;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> user, IMapper mapper,IUniteOfWork uniteOfWork)
        {
            _roleManager = roleManager;
            _user = user;
            _mapper = mapper;
            _uniteOfWork = uniteOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var Roles = await _roleManager.Roles.ToListAsync(); 
            var MappedRoles=_mapper.Map<IEnumerable<IdentityRole>,IEnumerable<RoleViewModel>>(Roles);
            return View(MappedRoles);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedRole = _mapper.Map<RoleViewModel, IdentityRole>(model);
                await _roleManager.CreateAsync(mappedRole);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string? id)
        {
            if(id !=null)
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    await _roleManager.DeleteAsync(role);
                    return Json(new { success = true, message = "Role Has been Deleted" });
                }
                return Json(new { success = false, message = "Error While Deleting" });
            }
         return BadRequest();

        }
    }
}
