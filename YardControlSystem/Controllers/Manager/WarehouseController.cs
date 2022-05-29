using YardControlSystem.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YardControlSystem.Models;

namespace YardControlSystem.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly YardControlSystemIdentityContext _db;

        public WarehouseController(YardControlSystemIdentityContext db)
        {
            _db = db;
        }

 
        public IActionResult WarehousesView()
        {
            IEnumerable<Warehouse> objList = _db.Warehouses;
            return View(objList);

        }

        // GET-Create
        public IActionResult Create()
        {
            return View();
        }

        // POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Warehouse obj)
        {
            if (ModelState.IsValid)
            {
                _db.Warehouses.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("WarehousesView");
            }
            return View(obj);

        }


        // GET Delete
        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Warehouses.Find(id);
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
            var obj = _db.Warehouses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Warehouses.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("WarehousesView");

        }

        // GET Update
        public IActionResult Update(int? id, string error = "")
        {
            ViewBag.ErrorMessage = error;
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Warehouses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }

        // POST UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Warehouse obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Warehouses.Update(obj);
                    _db.SaveChanges();
                    return RedirectToAction("WarehousesView");
                }
                return View(obj);
            } catch (Exception exception)
            {
                return Update(obj.Id, exception.Message);
            }
        }
    }
}