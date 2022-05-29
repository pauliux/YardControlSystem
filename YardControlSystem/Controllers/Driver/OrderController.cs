using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using YardControlSystem.Areas.Identity.Data;
using YardControlSystem.Data;
using YardControlSystem.Models;
using YardControlSystem.Models.ViewModels;

namespace YardControlSystem.Controllers.Driver
{
    public class OrderController : Controller
    {
        private readonly YardControlSystemIdentityContext _db;

        public OrderController(YardControlSystemIdentityContext db)

        {
            _db = db;
        }

        // GET: OrderController
        public ActionResult DriverOrdersView()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = _db.Orders.Where(x => x.DriverId.Equals(userId)).ToList();
            var orderViewModels = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                orderViewModels.Add(new OrderViewModel
                {
                    Order = order,
                    PickUpWarehouse = _db.Warehouses.FirstOrDefault(x => order.PickUpWarehouseId == x.Id),
                    DropOffWarehouse = _db.Warehouses.FirstOrDefault(x => order.DropOffWarehouseId == x.Id),
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
                var hasOperations = _db.Operations.Where(x => x.OrderId == order.OrderNr).Any();

                if (!hasOperations)
                {
                    order.DateOfCreation = order.DateOfCreation.Date;

                    orderViewModels.Add(new OrderViewModel
                    {
                        Order = order,
                        PickUpWarehouse = _db.Warehouses.FirstOrDefault(x => order.PickUpWarehouseId == x.Id),
                        DropOffWarehouse = _db.Warehouses.FirstOrDefault(x => order.DropOffWarehouseId == x.Id),
                        HasOperations = hasOperations
                    });
                }
            }
            return View(orderViewModels);
        }

        // GET: OrderController
        public ActionResult AssignOrderView(int? id)
        {
            var order = _db.Orders.Find(id);
            var pickupWarehouse = _db.Warehouses.Find(order.PickUpWarehouseId);
            var dropoffWarehouse = _db.Warehouses.Find(order.DropOffWarehouseId);

            var orderViewModels = new AssignOrderViewModel()
            {
                Order = order,
                PickUpWarehouse = pickupWarehouse.Company + " - " + pickupWarehouse.Address + ", " + pickupWarehouse.City,
                DropOffWarehouse = dropoffWarehouse.Company + " - " + dropoffWarehouse.Address + ", " + dropoffWarehouse.City
            };
            return View(orderViewModels);
        }

        // GET: OrderController
        public ActionResult UpdateDriverOrderView(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = _db.Orders.FirstOrDefault(x => x.DriverId.Equals(userId));
            var pickupWarehouse = _db.Warehouses.Find(order.PickUpWarehouseId);
            var dropoffWarehouse = _db.Warehouses.Find(order.DropOffWarehouseId);

            var orderViewModels = new AssignOrderViewModel()
            {
                Order = order,
                PickUpWarehouse = pickupWarehouse.Company + " - " + pickupWarehouse.Address + ", " + pickupWarehouse.City,
                DropOffWarehouse = dropoffWarehouse.Company + " - " + dropoffWarehouse.Address + ", " + dropoffWarehouse.City
            };

            return View(orderViewModels);
        }

        public ActionResult AssignOrderToDriver(AssignOrderViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dbOrder = _db.Orders.Find(model.Order.OrderNr);
            dbOrder.DriverId = userId;

            var pickupOperation = new Operation() { OrderId = dbOrder.OrderNr, WarehouseId = dbOrder.PickUpWarehouseId };
            var dropoffOperation = new Operation() { OrderId = dbOrder.OrderNr, WarehouseId = dbOrder.DropOffWarehouseId };

            _db.Orders.Update(dbOrder);
            _db.Operations.Add(pickupOperation);
            _db.Operations.Add(dropoffOperation);
            _db.SaveChanges();

            return RedirectToAction("FreeOrdersView");
        }

       
    }
}
