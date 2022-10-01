using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OMSproject.Data;
using OMSproject.Models;
using System.Linq;

namespace OMSproject.Controllers
{
    public class ClientController : Controller
    {

        ApplicationDbContext _db;

        public ClientController(ApplicationDbContext db)
        {
            this._db = db;
        }

        // GET: ClientController
        public ActionResult Index()
        {
            var clients = _db.Clients;
            return View(clients);
        }

        // GET: ClientController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Client model)
        {
            
            var id = _db.Clients.Max(x => x.Client_id );

            if (id == null)
            {
                id = 0;
            }
            id++;

            try
            {
                var client = new Client()
                {
                    Client_id = id,
                    Phone = model.Phone,
                    ClientName = model.ClientName,
                    Claasification = model.Claasification,
                };

                _db.Clients.Add(client);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: ClientController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
