using apicrud.Models;
using Microsoft.AspNetCore.Mvc;

namespace apicrud.Controllers
{
    public class infoController1 : Controller
    {
        private readonly ClientDbcontext _dbcontext;

        public infoController1(ClientDbcontext context)
        {
            this._dbcontext = context;
        }
        public IActionResult Index()
            
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}
