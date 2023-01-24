using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using OMSproject.Data;
using OMSproject.DTO;
using OMSproject.Models;
using OMSproject.Models.ViewModels;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeState(List<Order> view)
        {
            foreach(var item in view)
            {
                if(item.IsSelected == true)
                {
                    db.Orders.SingleOrDefault(x => x.OrderId == item.OrderId).OrderStatus = "Delivered";
                }
            }
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {

            Order viewOrder = db.Orders.Include(x => x.OrderDetails).SingleOrDefault(x => x.OrderId == id);
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
            foreach (var i in view.Items)
            {

                i.Products.AddRange(db.Products.Select(x => new ProductDTO { Product_Id = x.Product_Id, Product_Name = x.Product_Name }));
                //i.Colors.AddRange(db.Colors.Where(p => p.Product_Id == i.Product_Id).Select(x => new ColorDTO { ColorName = x.ColorName }));
            }

            return View(view);
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
            view.Clients.AddRange(db.Clients.Where(x => x.Claasification != "blacklist").Select(x => new ClientDTO { Client_id = x.Client_id, ClientName = x.ClientName }));

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


                    var NewID = 0;
                    var CheckClient = db.Clients.SingleOrDefault(x => x.ClientName == order.ClientName);
                    if (CheckClient == null)
                    {
                        var clientid = db.Clients.Max(x => x.Client_id);
                        if(clientid == null)
                        {
                            clientid = 0;
                        }
                        clientid++;
                        Client client = new Client()
                        {
                            Client_id = clientid,
                            ClientName = order.ClientName,
                            Phone = order.Phone,
                            Phone2 = order.Phone2,
                            Claasification = "New"
                        };

                        db.Clients.Add(client);

                        NewID = (int)client.Client_id;

                    }
                    else
                    {
                        NewID = (int)CheckClient.Client_id;
                    }


                    Order clientorder = new Order
                    {
                        OrderId = id,
                        Total_price = order.Total_price,
                        SellPrice = order.SellPrice,
                        Address = order.Address,
                        DateOFOrder = order.DateOFOrder,
                        OrderStatus = order.OrderStatus,
                        Notes = order.Notes,
                        Client_id = NewID
                    };
                    
                 
                    List<OrderDetails> ListItems = new List<OrderDetails>();

                    foreach(var item in order.Items)
                    {
                        ListItems.Add(new OrderDetails
                        {
                            OrderId = (int)id,
                            ProductId = (int)item.Product_Id,
                            ClrName = item.ColorName,
                            SubQty = item.Quantity,
                            Price = item.Price
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
                            ViewBag.message = "the quantity you ordered does not exist in inventory";

                            order.Clients.AddRange(db.Clients.Where(x => x.Claasification != "blacklist").Select(x => new ClientDTO { Client_id = x.Client_id, ClientName = x.ClientName }));
                            order.Status.AddRange(new List<string>
                            {
                                      "New","Inprogress","Canceled","Delivered"
                            });

                            foreach (var i in order.Items)
                            {

                                i.Products.AddRange(db.Products.Select(x => new ProductDTO { Product_Id = x.Product_Id, Product_Name = x.Product_Name }));
                                i.Colors.AddRange(db.Colors.Where(p => p.Product_Id == i.Product_Id).Select(x => new ColorDTO { ColorName = x.ColorName }));
                            }

                            return View(order);
                
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

            for (int i = 0; i < view.Items.Count; i++)
            //foreach(var item in view.Items)
            {
                view.Items[i].ColorName = viewDetails[i].ClrName;
                view.Items[i].Product_Id = viewDetails[i].ProductId;
            }


            //for (int i = 0; i < view.Items.Count; i++)
            //{
            //    view.Items[i].Products.AddRange(db.Products.Select(x => new ProductDTO { Product_Id = x.Product_Id, Product_Name = x.Product_Name }));

            //    view.Items[i].Colors.AddRange(db.Colors.Where(p => p.Product_Id == view.Items[i].Product_Id).Select(x => new ColorDTO { ColorName = view.Items[i].ColorName }));
            //    //item.ColorName = db.Colors.Where();
            //}

            return View(view);
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

                foreach(var i in orderDetails)
                {
                    var productColor = db.Colors.Where(x => x.Product_Id == i.ProductId && x.ColorName == i.ClrName).SingleOrDefault();
                    var product = db.Products.Where(x => x.Product_Id == i.ProductId).SingleOrDefault();


                    product.Total_QTY += i.SubQty;
                    productColor.Quantity += i.SubQty;
                }

                if (view.OrderStatus != "Canceled")
                {
                    db.RemoveRange(orderDetails);
                    

                    List<OrderDetails> ListItems = new List<OrderDetails>();

                    for(var i = 0; i < view.Items.Count;i++)
                    {
                        var color = db.Colors.Where(x => x.Product_Id == view.Items[i].Product_Id && x.ColorName == view.Items[i].ColorName).SingleOrDefault();
                        var product = db.Products.Where(x => x.Product_Id == view.Items[i].Product_Id).SingleOrDefault();

                        if (color == null)
                            color = db.Colors.Where(x => x.Product_Id == orderDetails[i].ProductId && x.ColorName == orderDetails[i].ClrName).SingleOrDefault();


                        ListItems.Add(new OrderDetails
                        {
                            OrderId = id,
                            ProductId = (int)product.Product_Id,
                            ClrName = color.ColorName,
                            SubQty = view.Items[i].Quantity,
                            Price = view.Items[i].Price
                            
                        });

                        if (color.Quantity >= view.Items[i].Quantity)
                        {
                            color.Quantity -= view.Items[i].Quantity;
                            product.Total_QTY -= view.Items[i].Quantity;
                        }
                        else
                        {
                            ViewBag.message = "the quantity you ordered don't exist in inventory";

                            return View();
                        }

                    }

                    db.Details.AddRange(ListItems);
                    
                }
                db.SaveChanges();



                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(view);
            }
        }

        public ActionResult Search(string term)
        {
            var result = db.Orders.Where(x => x.OrderStatus.Contains(term)).Include(x => x.Client).ToList();
            return View("index", result);
        }

        public ActionResult searchNew()
        {
            var result = db.Orders.Where(x => x.OrderStatus == "New").Include(x => x.Client);
            return View("index", result);
        } 
        public ActionResult searchInprogress()
        {
            var result = db.Orders.Where(x => x.OrderStatus == "Inprogress").Include(x => x.Client);
            return View("index", result);
        } 
        public ActionResult searchDelivered()
        {
            var result = db.Orders.Where(x => x.OrderStatus == "Delivered").Include(x => x.Client);
            return View("index", result);
        } 
        public ActionResult searchCanceled()
        {
            var result = db.Orders.Where(x => x.OrderStatus == "Canceled").Include(x => x.Client);
            return View("index", result);
        }

        public ActionResult getProductPrice(int productId)
        {
            var price = db.Products.Where(x => x.Product_Id == productId).Select(x => x.Price);
            return Ok(price);
        }

        public ActionResult ProfitCalculate()
        {
            var orders = db.Orders.Include(x => x.OrderDetails).ToList();

            List<ProfetViewModel> profets = new List<ProfetViewModel>();
            
            foreach (var order in orders)
            {
                var OrderCoast = 0.0;
                for (int i = 0; i< order.OrderDetails.Count; i++)
                {
                     OrderCoast += db.Products.SingleOrDefault(x => x.Product_Id == order.OrderDetails[i].ProductId).Cost * order.OrderDetails[i].SubQty; 
                }
                profets.Add(new ProfetViewModel
                {
                    OrderId = order.OrderId,
                    OrderDate = order.DateOFOrder,
                    OrderCoast = (float)OrderCoast,
                    OrderSellPrice = order.SellPrice,
                    Profet = (float)(order.SellPrice - OrderCoast)
                });
            }

            return View(profets);
        }


        public  ActionResult GetClientData(string client)
        {
            var datalist = new List<string>();
            var clientData = db.Clients.SingleOrDefault(x => x.ClientName == client);
            if (clientData != null)
            {
                datalist.Add(clientData.Phone);
                datalist.Add(clientData.Phone2);
            }
            return Ok(datalist) ;
        }
    }
   
}

