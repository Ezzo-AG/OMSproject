using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OMSproject.Data;
using OMSproject.DTO;
using OMSproject.Models;
using System.Diagnostics;

namespace OMSproject.Controllers
{
    public class HomeController : Controller
    {
        readonly ApplicationDbContext? db;
        
        public HomeController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            
            var home = new Home()
            {
                InProgress = OrderStatus("Inprogress"),
                Canceled = OrderStatus("Canceled"),
                Delivred = OrderStatus("Delivered"),
                New = OrderStatus("New"),

                
            };
            home.order.AddRange(db.Orders.Where(x => x.OrderStatus == "new").OrderByDescending(x => x.OrderId).Include(x => x.Client). ToList());

            return View(home);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public int OrderStatus(string status)
        {
           var sum = db.Orders.Where(x => x.OrderStatus == status).Select(x => x.OrderId).Count();
            return sum;

        }
    }
}