using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Context;
using WebApplication1.Migrations;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class AppController : Controller
    {
        private readonly MyDbContext _context;
        public AppController(MyDbContext context)
        {
            _context = context;
        }
        // GET: AppController
        public ActionResult Index()
        {
            try
            {
                
                var appVMS = _context.Apps.Select(x => new AppVM
                {
                    AppID = x.AppID,
                    AppName = x.AppName
                }).ToList();
                return View(appVMS);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // GET: AppController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, AppVM app)
        {
            try
            {
                var newApp = new App
                {
                    AppName = app.AppName
                };

                _context.Apps.Add(newApp);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppController/Edit/5
        public ActionResult Edit(int id)
        {
            var appBaza = _context.Apps.FirstOrDefault(x => x.AppID == id);

            if (appBaza == null)
            {
                return NotFound();
            }

            var viewModel = new AppVM
            {
                AppID = appBaza.AppID,
                AppName = appBaza.AppName
            };

            return View(viewModel);
        }

        // POST: AppController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AppVM app)
        {
            try
            {
                var appBaza = _context.Apps.FirstOrDefault(x => x.AppID == id);
                appBaza.AppName = app.AppName;

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppController/Delete/5
        public ActionResult Delete(int id)
        {
            var appBaza = _context.Apps.FirstOrDefault(x => x.AppID == id);

            if (appBaza == null)
            {
                return NotFound();
            }

            var viewModel = new AppVM
            {
                AppID = appBaza.AppID,
                AppName = appBaza.AppName
            };

            return View(viewModel);
        }

        // POST: AppController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, AppVM app)
        {
            try
            {
                var appBaza = _context.Apps.FirstOrDefault(x => x.AppID == id);
                _context.Apps.Remove(appBaza);
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
