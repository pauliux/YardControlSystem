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
        public IActionResult Create()
        {
            return View();
        }

        // POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ramp obj)
        {
            if (ModelState.IsValid)
            {
                _db.Ramps.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("RampsView", new { id = obj.WarehouseId });
            }
            return View();

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
        public IActionResult Update(int? id)
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

        // POST UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Ramp obj)
        {
            if (ModelState.IsValid)
            {
                _db.Ramps.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("RampsView", new { id = obj.WarehouseId });
            }
            return View(obj);

        }
    }

}
