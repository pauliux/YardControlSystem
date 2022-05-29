using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YardControlSystem.Data;
using YardControlSystem.Models;
using YardControlSystem.Models.ViewModels;


namespace YardControlSystem.Controllers.StoreKeeper
{
    public class RampController : Controller
    {
        private readonly YardControlSystemIdentityContext _db;

        public RampController(YardControlSystemIdentityContext db)
        {
            _db = db;
        }

        // GET: OrderController
        public IActionResult RampManagementView()
        {
            var warehouses = _db.Warehouses.ToList();
            var ramps = _db.Ramps.ToList();
            var operations = _db.Operations.OrderBy(j =>j.ReservedDate).ToList();
            var orders = _db.Orders.ToList();

            var warehouseRampOperation = new List<RampManagementViewModel>();

            foreach (var oneWarehouse in warehouses)
            {
                List<Ramp> rampList = new List<Ramp>();
                List<Operation> operationList = new List<Operation>();
                foreach (var oneRamp in ramps)
                {
                    if (oneWarehouse.Id == oneRamp.WarehouseId)
                    {
                        rampList.Add(oneRamp);
                    }
                   
                    foreach (var oneOperation in operations)
                    {
                        if (oneRamp.Id == oneOperation.RampId)
                        {
                            oneOperation.Order = orders.Find(x => x.OrderNr == oneOperation.OrderId);
                            operationList.Add(oneOperation);
                        }
                    }
                }
                warehouseRampOperation.Add(new RampManagementViewModel
                {
                    Warehouses = oneWarehouse,
                    Ramps = rampList,
                    Operations = operationList

                }) ;
            }
            return View(warehouseRampOperation);
        }

        public IActionResult CheckDone(Operation operation)
        {
            var obj = _db.Operations.FirstOrDefault(x => x.Id == operation.Id);

            var ramps = _db.Ramps.FirstOrDefault(x => x.WarehouseId == operation.WarehouseId && x.Id == operation.RampId);

            if (obj.ArrivalDate == null)
            {
                obj.ArrivalDate = DateTime.Now.ToString();
                _db.SaveChanges();
                
            }
            else
            {
                obj.DepartureDate = DateTime.Now.ToString();
                _db.SaveChanges();
            }
            
            
            return RedirectToAction("RampManagementView");
        }
    }
}
