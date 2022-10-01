﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Packaging;
using OMSproject.Data;
using OMSproject.DTO;
using OMSproject.Models;
using OMSproject.Models.ViewModels;
using System.Linq;

namespace OMSproject.Controllers
{
    public class OrderController : Controller
    {
        //Client clients;
        readonly ApplicationDbContext? db ;

        public OrderController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: OrderController
        public ActionResult Index()
        {

            var order = db.Orders.Include(a => a.Client).ToList();
            return View(order);

        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }



        // GET: OrderController/Create
        public ActionResult Create()
        {

             
            return View(getView());
        }

        OrderClientViewModel getView()
        {
            
            OrderClientViewModel view = new OrderClientViewModel();
            view.Items.Add(new ItemDTO { OrderId = 0 });
            view.Clients.AddRange(db.Clients.Select(x => new ClientDTO { Client_id = x.Client_id, ClientName = x.ClientName }));

            foreach (var item in view.Items)
            {

                item.Products.AddRange(db.Products.Select(x => new ProductDTO { Product_Id = x.Product_Id, Product_Name = x.Product_Name }));
                item.Colors.AddRange(db.Colors.Select(x => new ColorDTO{ Product_Id = x.Product_Id , ColorName = x.ColorName }));
            }
            view.Status.AddRange(new List<string>
            {
                "New","Inprogress","Canceled","Delivered"
            });
            return view;
        }

        public ActionResult getcolors(int productId)
        {

            var colors = db.Colors.Where(x => x.Product_Id == productId).Select(x => new { x.Product_Id,x.ColorName });

            return Ok(colors);
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderClientViewModel order)
        {
            try
            {
                //foreach (var item in order.Items)
                //{
                //    if (item.OrderId == 0)
                //        order.Items.Remove(item);
                //}

                if (ModelState.IsValid)
                {
                     
                    var id = db.Orders.Max(x => x.OrderId);

                    if(id == null)
                    {
                        id = 0;
                    }
                    id++;

                    Order clientorder = new Order
                    {
                        OrderId = id,
                        Total_price = order.Total_price,
                        SellPrice = order.SellPrice,
                        Address = order.Address,
                        DateOFOrder = order.DateOFOrder,
                        OrderStatus = order.OrderStatus,
                        Notes = order.Notes,
                        Client_id = order.Client_id,
                    };

                    List<OrderDetails> ListItems = new List<OrderDetails>();

                    foreach(var item in order.Items)
                    {
                        ListItems.Add(new OrderDetails
                        {
                            OrderId = (int)id,
                            ProductId = (int)item.Product_Id,
                            ClrName = item.ColorName.ToString(),
                            SubQty = item.Quantity,
                            
                        });
                      
                    }

                    
                    db.Orders.Add(clientorder);
                    db.Details.AddRange(ListItems);
                }

                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(getView());
            }
        }



        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
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

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
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


        //List<ClientDTO> fillClientList()
        //{
        //    var clientList = db.Clients(x => x.Client_id, x.ClientName );
        //    return (List<ClientDTO>)clientList;
        //}


        //OrderClientViewModel getOrderClient()
        //{
        //    var order = new OrderClientViewModel
        //    {
        //        clients = fillClientList(),
        //    };
        //    return order;
        //}
    }
}
