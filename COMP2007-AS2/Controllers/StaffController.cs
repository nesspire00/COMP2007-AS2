﻿using System;
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
    public class StaffController : Controller
    {
        private IStaffRepository db;

        public StaffController()
        {
            this.db = new EFStaffRepository();
        }

        public StaffController(IStaffRepository repo)
        {
            this.db = repo;
        }

        // GET: Staff
        public ActionResult Index()
        {
            var staffs = db.Staffs.Include(s => s.Position);
            return View(staffs.ToList());
        }

        // GET: Staff/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Staff staff = db.Staffs.SingleOrDefault(a => a.staffId == id);
            if (staff == null)
            {
                return View("Error");
            }
            return View(staff);
        }

        // GET: Staff/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.positionId = new SelectList(db.Positions, "positionId", "positionName");
            return View("Create");
        }

        // POST: Staff/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "staffId,positionId,firstName,lastName,shiftHours")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Save(staff);
                return RedirectToAction("Index");
            }

            ViewBag.positionId = new SelectList(db.Positions, "positionId", "positionName", staff.positionId);
            return View(staff);
        }

        // GET: Staff/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Staff staff = db.Staffs.SingleOrDefault(a => a.staffId == id);
            if (staff == null)
            {
                return View("Error");
            }
            ViewBag.positionId = new SelectList(db.Positions, "positionId", "positionName", staff.positionId);
            return View(staff);
        }

        // POST: Staff/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "staffId,positionId,firstName,lastName,shiftHours")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Save(staff);
                return RedirectToAction("Index");
            }
            ViewBag.positionId = new SelectList(db.Positions, "positionId", "positionName", staff.positionId);
            return View("Edit", staff);
        }

        // GET: Staff/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Staff staff = db.Staffs.SingleOrDefault(a => a.staffId == id);
            if (staff == null)
            {
                return View("Error");
            }
            return View(staff);
        }

        // POST: Staff/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Staff staff = db.Staffs.SingleOrDefault(a => a.staffId == id);
            db.Delete(staff);
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
