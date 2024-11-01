using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class OkolinaController : Controller
    {
        private readonly MyDbContext _context;
        public OkolinaController(MyDbContext context)
        {
            _context = context;
        }
        // GET: OkolinaController
        public ActionResult Index()
        {
            try
            {
                var OkolinaVMS = _context.Okolinas.Select(x => new OkolinaVM
                {
                    OKLID = x.OKLID,
                    OKLName = x.OKLName
                }).ToList();
                return View(OkolinaVMS);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // GET: OkolinaController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: OkolinaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OkolinaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OkolinaController/Edit/5
        public ActionResult Edit(int id)
        {
            var dbOkolina = _context.Okolinas.FirstOrDefault(x => x.OKLID == id);

            if (dbOkolina == null)
            {
                return NotFound();
            }

            var viewModel = new OkolinaVM
            {
                OKLID = dbOkolina.OKLID,
                OKLName = dbOkolina.OKLName
            };

            return View(viewModel);
        }

        // POST: OkolinaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OkolinaVM okl)
        {
            try
            {
                var dbOkolina = _context.Okolinas.FirstOrDefault(x => x.OKLID == id);
                dbOkolina.OKLName = okl.OKLName;

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OkolinaController/Delete/5
        public ActionResult Delete(int id)
        {
            var dbOkolina = _context.Okolinas.FirstOrDefault(x => x.OKLID == id);

            if (dbOkolina == null)
            {
                return NotFound();
            }

            var viewModel = new OkolinaVM
            {
                OKLID = dbOkolina.OKLID,
                OKLName = dbOkolina.OKLName
            };

            return View(viewModel);
        }

        // POST: OkolinaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, OkolinaVM okl)
        {
            try
            {
                var dbOkolina = _context.Okolinas.FirstOrDefault(x => x.OKLID == id);
                _context.Okolinas.Remove(dbOkolina);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
