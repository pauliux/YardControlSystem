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
        public ActionResult UpdateDriverOrderView(int id, string error = "")
        {
            ViewBag.ErrorMessage = error;

            var order = _db.Orders.Find(id);
            var pickupWarehouse = _db.Warehouses.Find(order.PickUpWarehouseId);
            var dropoffWarehouse = _db.Warehouses.Find(order.DropOffWarehouseId);
            ViewBag.PickUpWarehouseId = pickupWarehouse.Id;
            ViewBag.DropOffWarehouseId = dropoffWarehouse.Id;

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

        [HttpPost]
        public ActionResult UpdateDriverOrderView(AssignOrderViewModel model)
        {
            var dbOrder = _db.Orders.Find(model.Order.OrderNr);

            var pickUpDateTime = model.PickUpOperation.ReservedDate;
            var dropOffDateTime = model.DropOffOperation.ReservedDate;

            if (pickUpDateTime < DateTime.Now)
            {
                return UpdateDriverOrderView(model.Order.OrderNr, "Pakrovimo laikas praėjęs");
            }
            if (dropOffDateTime < DateTime.Now)
            {
                return UpdateDriverOrderView(model.Order.OrderNr, "Iškrovimo laikas praėjęs");
            }
            if (dropOffDateTime < pickUpDateTime)
            {
                return UpdateDriverOrderView(model.Order.OrderNr, "Pakrovimo laikas privalo būti ankstesnis");
            }

            var pickUpWareHouseRamps = _db.Ramps.Where(x => x.WarehouseId == int.Parse(model.PickUpWarehouse) && x.Working).ToList();

            if (!pickUpWareHouseRamps.Any())
            {
                return UpdateDriverOrderView(model.Order.OrderNr, "Pakrovimo sandėlyje nėra veikiančių rampų");
            }

            foreach (var ramp in pickUpWareHouseRamps)
            {
                var pickUpWarehouseOperations = _db.Operations.Where(x => x.WarehouseId == int.Parse(model.PickUpWarehouse)).Where(x => x.RampId == ramp.Id && x.DepartureDate == null).ToList();

                bool overlap = false;
                foreach (var operation in pickUpWarehouseOperations)
                {
                    if (operation.OrderId == model.Order.OrderNr)
                    {
                        continue;
                    }

                    if (operation.ReservedDate < model.PickUpOperation.ReservedDate.AddMinutes(60) && model.PickUpOperation.ReservedDate < operation.ReservedDate.AddMinutes(60))
                    {
                        overlap = true;
                    }
                }

                if (!overlap)
                {
                    model.PickUpOperation.RampId = ramp.Id;
                    goto pickUpTimeAvailable;
                }
            }

            return UpdateDriverOrderView(model.Order.OrderNr, "Pakrovimo laikas užimtas");

        pickUpTimeAvailable:

            var dropOffWareHouseRamps = _db.Ramps.Where(x => x.WarehouseId == int.Parse(model.DropOffWarehouse) && x.Working).ToList();

            if (!dropOffWareHouseRamps.Any())
            {
                return UpdateDriverOrderView(model.Order.OrderNr, "Iškrovimo sandėlyje nėra veikiančių rampų");
            }

            foreach (var ramp in dropOffWareHouseRamps)
            {
                var dropOffWarehouseOperations = _db.Operations.Where(x => x.WarehouseId == int.Parse(model.DropOffWarehouse)).Where(x => x.RampId == ramp.Id && x.DepartureDate == null).ToList();

                bool overlap = false;
                foreach (var operation in dropOffWarehouseOperations)
                {
                    if (operation.OrderId == model.Order.OrderNr)
                    {
                        continue;
                    }

                    if (operation.ReservedDate < model.DropOffOperation.ReservedDate.AddMinutes(60) && model.DropOffOperation.ReservedDate < operation.ReservedDate.AddMinutes(60))
                    {
                        overlap = true;
                    }
                }

                if (!overlap)
                {
                    model.DropOffOperation.RampId = ramp.Id;
                    goto dropOffTimeAvailable;
                }
            }

            return UpdateDriverOrderView(model.Order.OrderNr, "Iškrovimo laikas užimtas");

        dropOffTimeAvailable:

            var pickupOperation = new Operation()
            {
                OrderId = dbOrder.OrderNr,
                WarehouseId = dbOrder.PickUpWarehouseId,
                ReservedDate = model.PickUpOperation.ReservedDate,
                RampId = model.PickUpOperation.RampId,
                Duration = 60
            };
            var dropoffOperation = new Operation()
            {
                OrderId = dbOrder.OrderNr,
                WarehouseId = dbOrder.DropOffWarehouseId,
                ReservedDate = model.DropOffOperation.ReservedDate,
                RampId = model.DropOffOperation.RampId,
                Duration = 60
            };

            Operation pickupOperationUpdated = _db.Operations.Find(dbOrder.PickUpOperationId);
            Operation dropoffOperationUpdated = _db.Operations.Find(dbOrder.DropOffOperationId);
            pickupOperationUpdated.ReservedDate = model.PickUpOperation.ReservedDate;
            dropoffOperationUpdated.ReservedDate = model.DropOffOperation.ReservedDate;

            _db.Orders.Update(dbOrder);
            _db.Operations.Update(pickupOperationUpdated);
            _db.Operations.Update(dropoffOperationUpdated);
            _db.SaveChanges();

            return RedirectToAction("DriverOrdersView");
        }

        // GET: OrderController
        public ActionResult AssignOrderView(int? id, string error = "")
        {
            ViewBag.ErrorMessage = error;

            var order = _db.Orders.Find(id);
            var pickupWarehouse = _db.Warehouses.Find(order.PickUpWarehouseId);
            var dropoffWarehouse = _db.Warehouses.Find(order.DropOffWarehouseId);
            ViewBag.PickUpWarehouseId = pickupWarehouse.Id;
            ViewBag.DropOffWarehouseId = dropoffWarehouse.Id;

            var orderViewModels = new AssignOrderViewModel()
            {
                Order = order,
                PickUpWarehouse = pickupWarehouse.Company + " - " + pickupWarehouse.Address + ", " + pickupWarehouse.City,
                DropOffWarehouse = dropoffWarehouse.Company + " - " + dropoffWarehouse.Address + ", " + dropoffWarehouse.City
            };
            return View(orderViewModels);
        }

        [HttpPost]
        public ActionResult AssignOrderView(AssignOrderViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dbOrder = _db.Orders.Find(model.Order.OrderNr);
            dbOrder.DriverId = userId;

            var pickUpDateTime = model.PickUpOperation.ReservedDate;
            var dropOffDateTime = model.DropOffOperation.ReservedDate;

            if (pickUpDateTime < DateTime.Now)
            {
                return AssignOrderView(model.Order.OrderNr, "Pakrovimo laikas praėjęs");
            }
            if (dropOffDateTime < DateTime.Now)
            {
                return AssignOrderView(model.Order.OrderNr, "Iškrovimo laikas praėjęs");
            }
            if (dropOffDateTime < pickUpDateTime)
            {
                return AssignOrderView(model.Order.OrderNr, "Pakrovimo laikas privalo būti ankstesnis");
            }

            var pickUpWareHouseRamps = _db.Ramps.Where(x => x.WarehouseId == int.Parse(model.PickUpWarehouse) && x.Working).ToList();

            if (!pickUpWareHouseRamps.Any())
            {
                return AssignOrderView(model.Order.OrderNr, "Pakrovimo sandėlyje nėra veikiančių rampų");
            }

            foreach (var ramp in pickUpWareHouseRamps)
            {
                var pickUpWarehouseOperations = _db.Operations.Where(x => x.WarehouseId == int.Parse(model.PickUpWarehouse)).Where(x => x.RampId == ramp.Id && x.DepartureDate == null).ToList();

                bool overlap = false;
                foreach (var operation in pickUpWarehouseOperations)
                {
                    if (operation.ReservedDate < model.PickUpOperation.ReservedDate.AddMinutes(60) && model.PickUpOperation.ReservedDate < operation.ReservedDate.AddMinutes(60))
                    {
                        overlap = true;
                    }
                }

                if (!overlap)
                {
                    model.PickUpOperation.RampId = ramp.Id;
                    goto pickUpTimeAvailable;
                }
            }

            return AssignOrderView(model.Order.OrderNr, "Pakrovimo laikas užimtas");

            pickUpTimeAvailable:

            var dropOffWareHouseRamps = _db.Ramps.Where(x => x.WarehouseId == int.Parse(model.DropOffWarehouse) && x.Working).ToList();

            if (!dropOffWareHouseRamps.Any())
            {
                return AssignOrderView(model.Order.OrderNr, "Iškrovimo sandėlyje nėra veikiančių rampų");
            }

            foreach (var ramp in dropOffWareHouseRamps)
            {
                var dropOffWarehouseOperations = _db.Operations.Where(x => x.WarehouseId == int.Parse(model.DropOffWarehouse)).Where(x => x.RampId == ramp.Id && x.DepartureDate == null).ToList();

                bool overlap = false;
                foreach (var operation in dropOffWarehouseOperations)
                {
                    if (operation.ReservedDate < model.DropOffOperation.ReservedDate.AddMinutes(60) && model.DropOffOperation.ReservedDate < operation.ReservedDate.AddMinutes(60))
                    {
                        overlap = true;
                    }
                }

                if (!overlap)
                {
                    model.DropOffOperation.RampId = ramp.Id;
                    goto dropOffTimeAvailable;
                }
            }

            return AssignOrderView(model.Order.OrderNr, "Iškrovimo laikas užimtas");

            dropOffTimeAvailable:

            var pickupOperation = new Operation()
            {
                OrderId = dbOrder.OrderNr,
                WarehouseId = dbOrder.PickUpWarehouseId,
                ReservedDate = model.PickUpOperation.ReservedDate,
                RampId = model.PickUpOperation.RampId,
                Duration = 60
            };
            var dropoffOperation = new Operation()
            {
                OrderId = dbOrder.OrderNr,
                WarehouseId = dbOrder.DropOffWarehouseId,
                ReservedDate = model.DropOffOperation.ReservedDate,
                RampId = model.DropOffOperation.RampId,
                Duration = 60
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
