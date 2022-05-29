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

                var pickUpOperation = _db.Operations.Find(order.PickUpOperationId);
                var dropOffOperation = _db.Operations.Find(order.DropOffOperationId);

                orderViewModels.Add(new OrderViewModel
                {
                    Order = order,
                    PickUpWarehouse = _db.Warehouses.FirstOrDefault(x => order.PickUpWarehouseId == x.Id),
                    DropOffWarehouse = _db.Warehouses.FirstOrDefault(x => order.DropOffWarehouseId == x.Id),
                    PickUpOperation = pickUpOperation,
                    DropOffOperation = dropOffOperation,
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
            var order = _db.Orders.Find(id);
            var pickupWarehouse = _db.Warehouses.Find(order.PickUpWarehouseId);
            var dropoffWarehouse = _db.Warehouses.Find(order.DropOffWarehouseId);

            var orderViewModels = new AssignOrderViewModel()
            {
                Order = order,
                PickUpWarehouse = pickupWarehouse.Company + " - " + pickupWarehouse.Address + ", " + pickupWarehouse.City,
                DropOffWarehouse = dropoffWarehouse.Company + " - " + dropoffWarehouse.Address + ", " + dropoffWarehouse.City,
                PickUpOperation = _db.Operations.Find(order.PickUpOperationId),
                DropOffOperation = _db.Operations.Find(order.DropOffOperationId)
            };

            return View(orderViewModels);
        }

        // GET: OrderController
        public ActionResult UpdateDriverOrder(AssignOrderViewModel model)
        {
            var dbOrder = _db.Orders.Find(model.Order.OrderNr);

            Operation pickupOperation = _db.Operations.Find(dbOrder.PickUpOperationId);
            Operation dropoffOperation = _db.Operations.Find(dbOrder.DropOffOperationId);
            pickupOperation.ReservedDate = model.PickUpOperation.ReservedDate;
            dropoffOperation.ReservedDate = model.DropOffOperation.ReservedDate;

            _db.Orders.Update(dbOrder);
            _db.Operations.Update(pickupOperation);
            _db.Operations.Update(dropoffOperation);
            _db.SaveChanges();

            return RedirectToAction("DriverOrdersView");
        }

        public ActionResult AssignOrderToDriver(AssignOrderViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dbOrder = _db.Orders.Find(model.Order.OrderNr);
            dbOrder.DriverId = userId;

            var pickupOperation = new Operation()
            {
                OrderId = dbOrder.OrderNr,
                WarehouseId = dbOrder.PickUpWarehouseId,
                ReservedDate = model.PickUpOperation.ReservedDate

            };
            var dropoffOperation = new Operation()
            {
                OrderId = dbOrder.OrderNr,
                WarehouseId = dbOrder.DropOffWarehouseId,
                ReservedDate = model.DropOffOperation.ReservedDate
            };

            _db.Operations.Add(pickupOperation);
            _db.Operations.Add(dropoffOperation);
            _db.SaveChanges();

            int lastOperationId = _db.Operations.Max(item => item.Id);

            dbOrder.PickUpOperationId = lastOperationId - 1;
            dbOrder.DropOffOperationId = lastOperationId;

            _db.Orders.Update(dbOrder);
            _db.SaveChanges();

            return RedirectToAction("FreeOrdersView");
        }


    }
}
