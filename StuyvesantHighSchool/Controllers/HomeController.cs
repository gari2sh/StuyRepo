using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StuyvesantHighSchool.Models;
using StuyvesantHighSchool.Models.StuyViewModels;
using StuyvesantHighSchool.Data;
using Microsoft.EntityFrameworkCore;

namespace StuyvesantHighSchool.Controllers
{
    public class HomeController : Controller
    {
        private readonly StuyDbContext _context;

        public HomeController(StuyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult About()
        //{
        //    ViewData["Message"] = "Our Mission Statement:";

        //    return View();
        //}

        public async Task<ActionResult> About()
        {
            IQueryable<EnrollDtGrp> data =
                from student in _context.Students
                group student by student.EnrollmentDate into dateGroup
                select new EnrollDtGrp()
                {
                    EnrollmentDate = dateGroup.Key,
                    StudentCount = dateGroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "";

            return View();
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
    }
}
