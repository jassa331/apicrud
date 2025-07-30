using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using apicrud.Models;
using Sitecore.FakeDb;

namespace apicrud.Controllers
{
    public class infoesController : Controller
    {
        private readonly ClientDbcontext _context;

        public infoesController(ClientDbcontext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _context.info.ToListAsync());
            }
            catch (Exception ex)
            {

                return Ok(ex.Message.ToString());
            }

        }

        // GET: infoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var info = await _context.info.FirstOrDefaultAsync(m => m.ID == id);
            if (info == null)
            {
                return NotFound();
            }

            return View(info);
        }

        
        public IActionResult Create()
        {
            var nameList = _context.jds.ToList();

            var model = new InfoViewModel
            {
                Info = new info(),
                NameList = nameList.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),  
                    Text = c.business            
                }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InfoViewModel model)
        {
            var nameList = _context.jds.ToList();
            //model.NameList = nameList.Select(c => new SelectListItem //{ //    Value = c.ID.ToString(), //    Text = c.business//}).ToList();              
                _context.info.Add(model.Info);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            return View(model);
        }
        
        public IActionResult Edit()
        {
            var nameList = _context.jds.ToList();

            var model = new InfoViewModel
            {
                Info = new info(),
                NameList = nameList.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.business
                }).ToList()
            };

            return View(model);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var info = _context.info.FirstOrDefault(x => x.ID == id);
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
                }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InfoViewModel model)
        { //if (id != model.Info.ID)//    return NotFound();
            {  _context.Update(model.Info);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
 public async Task<IActionResult> Delete(int? id)
{
    if (id == null)
    {
        return NotFound();
    }
    var info = await _context.info
        .FirstOrDefaultAsync(m => m.ID == id);
    if (info == null)
    {   return NotFound();  }
return View(info);
}



[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)
{
    var info = await _context.info.FindAsync(id);
            //if (info != null)
            {
                _context.info.Remove(info);
            }
            await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
}       
 //private bool infoExists(int id)
 //{
 //    return _context.info.Any(e => e.ID == id);
 //}
    }
}

