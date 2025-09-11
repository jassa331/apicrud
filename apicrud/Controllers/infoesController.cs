using apicrud.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Sitecore.FakeDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apicrud.Controllers
{

    [Authorize]
    public class infoesController : Controller
    {
        private readonly ClientDbcontext _context;

        public infoesController(ClientDbcontext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            string token = Request.Cookies["JWToken"];
            ViewBag.Token = token ?? "Token not found!";
            return View(); 
        }
        public IActionResult infotable()    
        {
            var data = _context.infoo
              .Where(i => !i.IsDeleted)
              .Join(_context.jds,
                    i => i.business,
                    j => j.ID,
                    (i, j) => new info
                    {
                        ID = i.ID,
                        name = i.name,
                        business = i.business,
                        BusinessName = j.business,
                        Gender = i.Gender,
                        age = i.age,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted,
                        CreatedOn = i.CreatedOn
                    })
              .ToList();

            return View(data); 
        }


        // GET: infoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var info = await _context.infoo
                .Join(_context.jds,
                      i => i.business,
                      j => j.ID,
                      (i, j) => new info
                      {
                          ID = i.ID,
                          name = i.name,
                          age = i.age,
                          Gender = i.Gender,
                          business = i.business,
                          BusinessName = j.business,
                          IsActive = i.IsActive,
                          IsDeleted = i.IsDeleted,
                          CreatedOn=i.CreatedOn
                      })
                .FirstOrDefaultAsync(i => i.ID == id);

            if (info == null) return NotFound();

            return View(info);
        }



        public IActionResult Create()
        {
            var nameList = _context.jds.ToList();
            var categoryList = _context.KDS.ToList();

            var model = new InfoViewModel

            {
                
                Info = new info(),
                NameList = nameList.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.business,
                }).ToList(),
                CategoryList = categoryList.Select(c => new SelectListItem
                {
                    Value = c.Gender.ToString(),
                    Text = c.Gender
                }).ToList()

            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InfoViewModel model)
        {
            var nameList = _context.jds.ToList();
            var categoryList = _context.KDS.ToList();
            model.Info.CreatedOn = DateTime.Now;
            model.Info.IsActive = true; 

            //model.NameList = nameList.Select(c => new SelectListItem //{ //    Value = c.ID.ToString(), //    Text = c.business//}).ToList();              
            _context.infoo.Add(model.Info);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        public IActionResult Edit()
        {
            var nameList = _context.jds.ToList();
            var categoryList = _context.KDS.ToList();

            var model = new InfoViewModel
            {
                Info = new info(),
                NameList = nameList.Select(c => new SelectListItem
                {
                    Value = c.business.ToString(),
                    Text = c.business
                }).ToList(),
                CategoryList = categoryList.Select(c => new SelectListItem
                {
                    Value = c.Gender.ToString(),
                    Text = c.Gender
                }).ToList()
            };

            return View(model);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var info = _context.infoo.FirstOrDefault(x => x.ID == id);
            var categoryList = _context.KDS.ToList();

            //if (info == null)
            //{
            //    return NotFound();
            //}

            var model = new InfoViewModel
            {
                Info = info,
                NameList = _context.jds.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.business
                }).ToList(),
                CategoryList = categoryList.Select(c => new SelectListItem
                {
                    Value = c.Gender.ToString(),
                    Text = c.Gender
                }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InfoViewModel model)
        { //if (id != model.Info.ID)//    return NotFound();

            model.Info.CreatedOn = DateTime.Now;
            if (model.Info.IsActive)
            {
                model.Info.IsDeleted = false;
            }
else
            {
                model.Info.IsDeleted = true;
            }
            {
                _context.Update(model.Info);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
        //public async Task<IActionResult> Delete(int? id)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
           

            var info = await _context.infoo
                .Join(_context.jds,
                      i => i.business,
                      j => j.ID,
                      (i, j) => new info
                      {
                          ID = i.ID,
                          name = i.name,
                          age = i.age,
                          Gender = i.Gender,
                          business = i.business,
                          BusinessName = j.business,
                          IsActive = i.IsActive,
                          IsDeleted = i.IsDeleted,
                          CreatedOn = i.CreatedOn,
                          
                      })
                .FirstOrDefaultAsync(i => i.ID == id);

            if (info == null) return NotFound();

            return View(info);
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var info = await _context.infoo.FindAsync(id);
            if (info == null) return NotFound();

            // Soft delete logic
            info.IsDeleted = true;
            info.IsActive = false;
                  


            info.UpdateOn = DateTime.Now;

            if (info.IsDeleted = true)
            {

                info.IsActive = false;
            }
            else
            {
                info.IsActive = true;
            }
            info.IsDeleted = true;


          
            //if (info != null)
            {
                //    _context.info.Remove(info);
                _context.infoo.Update(info);

                await _context.SaveChangesAsync();

                if (info != null)
                {
                    
                    info.IsDeleted = true;
                }
                //    if (info != null)
                //{
                //    info.Date = 
                //}
                //if (info != null)
                //{
                //    info.Date = DateTime.Now;
                //    }
                //else
                //{
                //    info.Date = nu
                //}
                }
                return RedirectToAction(nameof(Index));
            }
        
        //private bool infoExists(int id)
        //{
        //    return _context.info.Any(e => e.ID == id);
        //}
        
            public IActionResult Candidates()
        {
            var data = _context.infoo
                .Where(i => !i.IsActive)
                //.Where(i => !i.Date)
                .Join(_context.jds,
                      i => i.business,
                      j => j.ID,
                      (i, j) => new info
                      {
                          ID = i.ID,
                          name = i.name,
                          business = i.business,
                          BusinessName = j.business,
                          Gender = i.Gender,
                          age = i.age,
                          IsActive = i.IsActive,
                          IsDeleted = i.IsDeleted,
                          CreatedOn=i.CreatedOn,
                          UpdateOn=i.UpdateOn
                          
                      })
                .ToList();

            return View(data);
        }

    }
}


