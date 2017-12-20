using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using COMP2007_AS2.Models;

namespace COMP2007_AS2.Controllers
{
    public class PositionsController : Controller
    {
        // private EventStaffModel db = new EventStaffModel();
        private IPositionsRepository db;

        public PositionsController()
        {
            this.db = new EFPositionsRepository();
        }

        public PositionsController(IPositionsRepository repo)
        {
            this.db = repo;
        }

        // GET: Positions
        public ActionResult Index()
        {
            return View(db.Positions.ToList());
        }

        // GET: Positions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Position position = db.Positions.SingleOrDefault(a => a.positionId == id);
            if (position == null)
            {
                return View("Error");
            }
            return View(position);
        }

        // GET: Positions/Create
        [Authorize]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Positions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "positionId,positionName,hourlyPay,duties")] Position position)
        {
            if (ModelState.IsValid)
            {
                db.Save(position);
                return RedirectToAction("Index");
            }

            return View("Edit", position);
        }

        // GET: Positions/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Position position = db.Positions.SingleOrDefault(a => a.positionId == id);
            if (position == null)
            {
                return View("Error");
            }
            return View(position);
        }

        // POST: Positions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "positionId,positionName,hourlyPay,duties")] Position position)
        {
            if (ModelState.IsValid)
            {
                db.Save(position);
                return RedirectToAction("Index");
            }
            return View("Edit", position);
        }

        // GET: Positions/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Position position = db.Positions.SingleOrDefault(a => a.positionId == id);
            if (position == null)
            {
                return View("Error");
            }
            return View(position);
        }

        // POST: Positions/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Position position = db.Positions.SingleOrDefault(a => a.positionId == id);
            db.Delete(position);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
