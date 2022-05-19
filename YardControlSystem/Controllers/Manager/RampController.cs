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

        private readonly ApplicationDbContext _db;

        public RampController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult RampsView()
        {
            IEnumerable<Ramp> objList = _db.Ramps;
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
        public IActionResult Create(Ramp obj)
        {
            if (ModelState.IsValid)
            {
                _db.Ramps.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("RampsView");
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
            return RedirectToAction("RampsView");

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
                return RedirectToAction("RampsView");
            }
            return View(obj);

        }
    }

}
