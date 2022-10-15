using Microsoft.AspNetCore.Http;
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
            return View(getEditView(id));
        }



        // GET: OrderController/Create
        public ActionResult Create()
        {

             
            return View(getView(1));
        }

        OrderClientViewModel getView(int id)
        {
            
            OrderClientViewModel view = new OrderClientViewModel();
            view.Items.Add(new ItemDTO { Product_Id = id });
            view.Clients.AddRange(db.Clients.Select(x => new ClientDTO { Client_id = x.Client_id, ClientName = x.ClientName }));

            foreach (var item in view.Items)
            {

                item.Products.AddRange(db.Products.Select(x => new ProductDTO { Product_Id = x.Product_Id, Product_Name = x.Product_Name }));
                item.Colors.AddRange(db.Colors.Where(p => p.Product_Id == item.Product_Id ).Select(x =>  new ColorDTO { ColorName = x.ColorName }));
            }
            view.Status.AddRange(new List<string>
            {
                "New","Inprogress","Canceled","Delivered"
            });
            return view;
        }

        public ActionResult getcolors(int productId)
        {

            var colors = db.Colors.Where(x => x.Product_Id == productId).Select(x => new {x.ColorName });

            return Ok(colors);
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderClientViewModel order)
        {
            try
            {

                order.Items.RemoveAll(x => x.IsHidden == true);
                order.Items.RemoveAll(x => x.Quantity == 0);

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
                            ClrName = item.ColorName,
                            SubQty = item.Quantity
                        });

                        var color = db.Colors.SingleOrDefault(x => x.Product_Id == item.Product_Id && x.ColorName == item.ColorName);
                        var product = db.Products.SingleOrDefault(x => x.Product_Id == item.Product_Id);


                        if (color.Quantity >= item.Quantity)
                        {
                            color.Quantity -= item.Quantity;
                            product.Total_QTY -= item.Quantity;
                        }
                        else
                        {
                            ViewBag.message = "the quantity you ordered don't exist in inventory";
                            return View(getView((int)item.Product_Id));
                        }

                    }

                    
                    db.Orders.Add(clientorder);
                    db.Details.AddRange(ListItems);
                }

                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(getView(1));
            }
        }



        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            

            return View(getEditView(id));
        }


        OrderClientViewModel getEditView(int id)
        {
            Order viewOrder = db.Orders.SingleOrDefault(x => x.OrderId == id);
            List<OrderDetails> viewDetails = db.Details.Where(x => x.OrderId == id).ToList();

            OrderClientViewModel view = new OrderClientViewModel();
            view.OrderId = id;
            view.Clients.AddRange(db.Clients.Select(x => new ClientDTO { Client_id = x.Client_id, ClientName = x.ClientName }));
            view.Client_id = viewOrder.Client_id;
            view.Total_price = viewOrder.Total_price;
            view.Address = viewOrder.Address;
            view.OrderStatus = viewOrder.OrderStatus;
            view.SellPrice = viewOrder.SellPrice;
            view.DateOFOrder = viewOrder.DateOFOrder;
            view.Notes = viewOrder.Notes;
            view.Status.AddRange(new List<string>
            {
                "New","Inprogress","Canceled","Delivered"
            });

            foreach (var item in viewDetails)
            {
                view.Items.Add(new ItemDTO { Product_Id = item.ProductId, ColorName = item.ClrName, Quantity = item.SubQty, Price = item.Price });

            }

            foreach (var item in view.Items)
            {

                item.Products.AddRange(db.Products.Select(x => new ProductDTO { Product_Id = x.Product_Id, Product_Name = x.Product_Name }));
                item.Colors.AddRange(db.Colors.Where(p => p.Product_Id == item.Product_Id).Select(x => new ColorDTO { ColorName = x.ColorName }));



            }

            return view;
        }






        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OrderClientViewModel view)
        {
            try
            {
                view.Items.RemoveAll(x => x.IsHidden == true);
                view.Items.RemoveAll(x => x.Quantity == 0);

                var order = db.Orders.SingleOrDefault(x => x.OrderId == id);


                order.Address = view.Address;
                order.SellPrice = view.SellPrice;
                order.Client_id = view.Client_id;
                order.Total_price = view.Total_price;
                order.DateOFOrder = view.DateOFOrder;
                order.OrderStatus = view.OrderStatus;
                order.Notes = view.Notes;

                List<OrderDetails> orderDetails = db.Details.Where(x => x.OrderId == id).ToList();

                foreach(var item in orderDetails)
                {
                    var productColor = db.Colors.Where(x => x.Product_Id == item.ProductId && x.ColorName == item.ClrName).SingleOrDefault();
                    var product = db.Products.Where(x => x.Product_Id == item.ProductId).SingleOrDefault();


                    product.Total_QTY += item.SubQty;
                    productColor.Quantity += item.SubQty;
                }

                if (view.OrderStatus != "Canceled")
                {
                    db.RemoveRange(orderDetails);
                    db.SaveChanges();

                    List<OrderDetails> ListItems = new List<OrderDetails>();

                    foreach (var item in view.Items)
                    {
                        ListItems.Add(new OrderDetails
                        {
                            OrderId = id,
                            ProductId = (int)item.Product_Id,
                            ClrName = item.ColorName,
                            SubQty = item.Quantity
                        });

                        var color = db.Colors.Where(x => x.Product_Id == item.Product_Id && x.ColorName == item.ColorName).SingleOrDefault();
                        var product = db.Products.Where(x => x.Product_Id == item.Product_Id).SingleOrDefault();


                        if (color.Quantity >= item.Quantity)
                        {
                            color.Quantity -= item.Quantity;
                            product.Total_QTY -= item.Quantity;

                        }
                        else
                        {
                            ViewBag.message = "the quantity you ordered don't exist in inventory";
                            return View(getEditView(id));
                        }

                    }

                    db.Details.AddRange(ListItems);
                    
                }
                db.SaveChanges();



                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(getEditView(id));
            }
        }

    }
   
}

