using YardControlSystem.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YardControlSystem.Models;

namespace YardControlSystem.Controllers.Manager
{
    public class RampController : Controller
    {

        private readonly YardControlSystemIdentityContext _db;

        public RampController(YardControlSystemIdentityContext db)
        {
            _db = db;
        }


        public IActionResult RampsView(int id)
        {
            var objList = _db.Ramps.Where(x => x.WarehouseId == id).ToList();
            var warehouse = _db.Warehouses.FirstOrDefault(x => x.Id == id);
            var rampsViewModel = new RampsViewModel
            {
                Warehouse = warehouse,
                Ramps = objList
            };

            return View(rampsViewModel);

        }

        // GET-Create
        public IActionResult Create(int? warehouseId, string error = "")
        {
            ViewBag.ErrorMessage = error;

            if (!Request.Query["warehouseId"].Any())
            {
                ViewBag.WarehouseId = warehouseId;
            } else
            {
                ViewBag.WarehouseId = Request.Query["warehouseId"];
            }

            return View();
        }

        // POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ramp obj)
        {
            try
            {
                if (_db.Ramps.Where(x => x.Number == obj.Number && x.WarehouseId == obj.WarehouseId).Any())
                {
                    return Create(obj.WarehouseId, "Rampos numeris nėra unikalus");
                }

                if (obj.Number <= 0 || obj.Number >= int.MaxValue)
                {
                    return Create(obj.WarehouseId, "Rampos numeris turi būti teigiamas skaičius");
                }

                _db.Ramps.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("RampsView", new { id = obj.WarehouseId });
            } catch (Exception exception)
            {
                return Create(obj.WarehouseId, exception.Message);
            }

        }


        // GET Delete
        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Ramps.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        // POST Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Ramps.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Ramps.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("RampsView", new { id = obj.WarehouseId });

        }

        // GET Update
        public IActionResult Update(int? warehouseId, int? id, string error = "")
        {
            ViewBag.ErrorMessage = error;

            if (!Request.Query["warehouseId"].Any())
            {
                ViewBag.WarehouseId = warehouseId;
            }
            else
            {
                ViewBag.WarehouseId = Request.Query["warehouseId"];
            }

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Ramps.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        // POST UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Ramp obj)
        {
            try
            {
                _db.Ramps.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("RampsView", new { id = obj.WarehouseId });
            }
            catch (Exception exception)
            {
                return Update(obj.WarehouseId, obj.Id, exception.Message);
            }
        }
    }

}
