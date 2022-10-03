﻿using Microsoft.AspNetCore.Http;
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

        //// GET: ClientController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: ClientController/Create
        public ActionResult Create()
        {
            Client model = new Client();
            return View(GetClasfcnList(model));
        }

        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Client model)
        {

            var id = _db.Clients.Max(x => x.Client_id);

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
            var client = _db.Clients.SingleOrDefault(x => x.Client_id == id);
            return View(GetClasfcnList(client));
        }

        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Client model)
        {
            try
            {

                var client = _db.Clients.SingleOrDefault(x => x.Client_id == id);

                if (client != null)
                {
                    client.ClientName = model.ClientName;
                    client.Phone = model.Phone;
                    client.Claasification = model.Claasification;
                }
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(GetClasfcnList(model));
            }
        }

        //// GET: ClientController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: ClientController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        public ActionResult Search(string term)
        {
            var result = _db.Clients.Where(x => x.ClientName.Contains(term)
                                            || x.Phone.Contains(term)
                                            || x.Claasification.Contains(term));
            return View("index",result);
        }


        // this metod return the classification list of clients
        Client GetClasfcnList(Client model)
        {
            model.ClaasificationList.AddRange(new List<string>
            {
                "New","Vip","BlackList"
            });
            return model;
        }
 
    }
}
