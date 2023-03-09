using CI_platfom.Entity.Data;
using CI_platfom.Entity.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CI_platform.Controllers
{
    public class Landing_pageController : Controller
    {
        public readonly CiPlatformContext _context;

        public Landing_pageController( CiPlatformContext context)
        {
            _context = context;
        }
        public ActionResult Landing_page()
        {
            var mission= _context.Landing_page.ToList();
            if (mission != null)
            {
                List<Landing_page> cardlist = new List<Landing_page>();
                foreach (var card in mission)
                { 
                var Landing_page=new Landing_page();
         
                    { 
                    
                    }
                }
            }
            return View();
        }
     }

       
     
    
}
