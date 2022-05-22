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
        private readonly ApplicationDbContext _db;

        public RampController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: OrderController
        public IActionResult RampManagementView()
        {
            var warehouses = _db.Warehouses.ToList();
            var ramps = _db.Ramps.ToList();
            var operations = _db.Operations.OrderBy(j =>j.ReservedDate).ToList();

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

        public IActionResult CheckDone()
        {

            return View();
        }
    }
}
