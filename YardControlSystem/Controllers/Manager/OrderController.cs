using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YardControlSystem.Data;
using YardControlSystem.Models;
using YardControlSystem.Models.ViewModels;

namespace YardControlSystem.Controllers.Manager
{
    public class OrderController : Controller
    {
        private readonly YardControlSystemIdentityContext _db;

        public OrderController(YardControlSystemIdentityContext db)
        {
            _db = db;
        }

        // GET: OrderController
        public ActionResult OrdersView()
        {
            var orders = _db.Orders.ToList();
            var orderViewModels = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                order.Driver = _db.Users.FirstOrDefault(x => order.DriverId.ToString() == x.Id);
                order.PickUpWarehouse = _db.Warehouses.FirstOrDefault(x => order.PickUpWarehouseId == x.Id);
                order.DropOffWarehouse = _db.Warehouses.FirstOrDefault(x => order.DropOffWarehouseId == x.Id);
                orderViewModels.Add(new OrderViewModel
                {
                    Order = order,
                    HasOperations = _db.Operations.Where(x => x.OrderId == order.OrderNr).Any()
                }) ;
            }

            return View(orderViewModels);
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            

            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order obj)
        {
            return RedirectToAction("OrdersView");
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
    }
}
