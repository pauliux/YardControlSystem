using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                order.DateOfCreation = order.DateOfCreation.Date;
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
            var warehouses = _db.Warehouses.ToList();

            var items = new List<SelectListItem>{};

            foreach (var warehouse in warehouses)
            {
                items.Add(new SelectListItem { Text = warehouse.Address, Value = warehouse.Id.ToString() });
            }

            var createOrderViewModel = new CreateOrderViewModel
            {
                Warehouses = items
            };

            return View(createOrderViewModel);
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateOrderViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var pickUp = int.Parse(obj.PickUpWarehouse);
                var dropoff = int.Parse(obj.DropOffWarehouse);
                obj.Order.PickUpWarehouseId = pickUp;
                obj.Order.DropOffWarehouseId = dropoff;
                obj.Order.DateOfCreation = DateTime.Today;

                _db.Orders.Add(obj.Order);

                _db.SaveChanges();
                return RedirectToAction("OrdersView");
            }
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
