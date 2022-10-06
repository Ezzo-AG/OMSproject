using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OMSproject.Data;
using OMSproject.Models;


namespace OMSproject.Controllers
{
    public class ProductController : Controller
    {
        readonly ApplicationDbContext? db;

        public ProductController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: ProductController
        public ActionResult Index()
        {
            var product = db.Products;
            return View(product);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            Product productColor = new Product();
            productColor.colors.Add(new Color() { Product_Id = 1 });
            //productColor.colors.Add(new Color() { Product_Id = 2 });

            return View(productColor);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product model)
        {
            try
            {

               
                model.colors.RemoveAll(x => x.ColorName == null);
                model.colors.RemoveAll(x => x.IsDeleted == true);


                var id = db.Products.Max(x => x.Product_Id);
                if (id == null)
                {
                    id = 0;
                }
                id++;

                if (model != null)
                {

                        var prodcts = new Product {
                           Product_Id = id,   
                           Product_Name = model.Product_Name,
                           Cost = model.Cost,
                           Price = model.Price,
                           Total_QTY = model.Total_QTY,
                           Notes = model.Notes,
                        
                        };

                    List<Color> color = new List<Color>();

                   if(model.colors != null)
                   {
                        foreach (var item in model.colors)
                        {
                            color.Add(new Color
                            {
                                Product_Id = (int)id,
                                ColorName = item.ColorName,
                                Quantity = item.Quantity
                            });
                        }

                        db.Products.Add(prodcts);
                        db.Colors.AddRange(color);

                   }

                }

                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            Product model = db.Products.SingleOrDefault(x => x.Product_Id == id);
            List<Color> colors = new List<Color>();
            colors.AddRange(db.Colors.Where(x=>x.Product_Id == id));

            return View(model);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product model)
        {
            try
            {

                model.colors.RemoveAll(x => x.ColorName == null);
                model.colors.RemoveAll(x => x.IsDeleted == true);

                List<Color> oldcolors = db.Colors.Where(x => x.Product_Id == id).ToList();
                db.Colors.RemoveRange(oldcolors);
                db.SaveChanges();

                var product = db.Products.SingleOrDefault(x => x.Product_Id == id);

                if (product != null)
                {



                    product.Product_Name = model.Product_Name;
                    product.Cost = model.Cost;
                    product.Price = model.Price;
                    product.Total_QTY = model.Total_QTY;
                    product.Notes = model.Notes;

                  

                    List<Color> color = new List<Color>();

                    if (model.colors != null)
                    {
                        foreach (var item in model.colors)
                        {
                            color.Add(new Color
                            {
                                Product_Id = id,
                                ColorName = item.ColorName,
                                Quantity = item.Quantity
                            });
                        }

                        //db.Products.Add(prodcts);
                        db.Colors.AddRange(color);

                    }
                }

                db.SaveChanges();


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
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
