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
                });
            }

            return View(orderViewModels);
        }

        // GET: OrderController
        public ActionResult FreeOrdersView()
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
                });
            }
            return View(orderViewModels);
        }

        // GET: OrderController
        public ActionResult AsignOrderView()
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
                });
            }
            return View(orderViewModels);
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            var warehouses = _db.Warehouses.ToList();

            var items = new List<SelectListItem> { };

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

        // GET: OrderController/Create
        public ActionResult AssignOrder()
        {
   
            return View();
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

        // GET: OrderController/Update/5
        public ActionResult Update(int id)
        {
            var warehouses = _db.Warehouses.ToList();

            var items = new List<SelectListItem> { };

            foreach (var warehouse in warehouses)
            {
                items.Add(new SelectListItem { Text = warehouse.Address, Value = warehouse.Id.ToString() });
            }

            ViewBag.Warehouses = items;

            if (id == 0)
            {
                return NotFound();
            }
            var obj = _db.Orders.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            ViewBag.SelectedPickUp = items.Where(x => x.Value == obj.PickUpWarehouseId.ToString()).First();
            ViewBag.SelectedPickUp = items.Where(x => x.Value == obj.DropOffWarehouseId.ToString()).First();

            var createOrderViewModel = new CreateOrderViewModel
            {
                Order = obj
            };

            return View(createOrderViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CreateOrderViewModel obj, IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var pickUp = int.Parse(obj.PickUpWarehouse);
                var dropoff = int.Parse(obj.DropOffWarehouse);
                obj.Order.PickUpWarehouseId = pickUp;
                obj.Order.DropOffWarehouseId = dropoff;
                obj.Order.DateOfCreation = DateTime.Today;

                _db.Orders.Update(obj.Order);
                _db.SaveChanges();
                return RedirectToAction("OrdersView");
            }
            return View(obj);
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var obj = _db.Orders.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            obj.PickUpWarehouse = _db.Warehouses.Find(obj.PickUpWarehouseId);
            obj.DropOffWarehouse = _db.Warehouses.Find(obj.DropOffWarehouseId);
            return View(obj);
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var obj = _db.Orders.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Orders.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("OrdersView");
        }
    }
}
