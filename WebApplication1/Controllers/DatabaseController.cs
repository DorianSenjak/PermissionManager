using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Migrations;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class DatabaseController : Controller
    {

        private readonly MyDbContext _context;
        public DatabaseController(MyDbContext context)
        {
            _context = context;
        }
        // GET: DatabaseController
        public ActionResult Index()
        {
            try
            {
                var databaseVMS = _context.Bazas.Select(x => new DatabaseVM
                {
                    BazaID = x.BazaID,
                    BazaName=x.BazaName
                }).ToList();
                return View(databaseVMS);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // GET: DatabaseController/Details/5
        //public ActionResult Details(int id)
        //{
        //    var databaseVMS = _context.Bazas.FirstOrDefault(x => x.BazaID == id);
        //    var bookVM = new DatabaseVM
        //    {
                
        //    };

        //    return View(databaseVMS);

        //}

        // GET: DatabaseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DatabaseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DatabaseVM db)
        {
            try
            {
                var newDB = new Baza
                {
                    BazaName = db.BazaName
                };

                _context.Bazas.Add(newDB);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DatabaseController/Edit/5
        public ActionResult Edit(int id)
        {
            var dbBaza = _context.Bazas.FirstOrDefault(x => x.BazaID == id);

            if (dbBaza == null)
            {
                return NotFound();
            }

            var viewModel = new DatabaseVM
            {
                BazaID = dbBaza.BazaID,
                BazaName = dbBaza.BazaName
            };

            return View(viewModel);
        }

        // POST: DatabaseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DatabaseVM db)
        {
            try
            {
                var dbBaza = _context.Bazas.FirstOrDefault(x => x.BazaID == id);
                dbBaza.BazaName = db.BazaName;

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DatabaseController/Delete/5
        public ActionResult Delete(int id)
        {
            var dbBaza = _context.Bazas.FirstOrDefault(x => x.BazaID == id);
            if (dbBaza == null)
            {
                return NotFound();
            }

            var viewModel = new DatabaseVM
            {
                BazaID = dbBaza.BazaID,
                BazaName = dbBaza.BazaName
            };

            return View(viewModel);
        }

        // POST: DatabaseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, DatabaseVM db)
        {
            try
            {
                var dbBaza = _context.Bazas.FirstOrDefault(x => x.BazaID == id);
                _context.Bazas.Remove(dbBaza);
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
