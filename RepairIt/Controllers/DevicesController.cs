﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RepairIt.Models;

namespace RepairIt.Controllers
{
    public class DevicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult GetAllDevices()
        {
            return View(db.Devices.Include(d => d.Dev_Owner).ToList());
        }

        // GET: Devices
        public ActionResult Index(int? id)
        {
            ViewBag.ClientId = id;
            ViewBag.ClientName = db.Clients.Where(c => c.Cl_Id == id).SingleOrDefault().Cl_LastName;
            var devices = db.Devices.Include(d => d.Dev_Owner).Where(d => d.Dev_Owner.Cl_Id == id);
            return View(devices.ToList());
        }

        // Returns an Add Device modal
        //public PartialViewResult CreateAddDeviceModal(int? id)
        public ActionResult CreateAddDeviceModal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client deviceOwner = db.Clients.Find(id);
            if (deviceOwner == null)
            {
                return HttpNotFound();
            }

            var newDevice = new Device();
            newDevice.Dev_Owner = deviceOwner;

            return PartialView("_AddDevicePartial", newDevice);
        }


        // Saves the Add Device modal's form
        [HttpPost]
        public ActionResult AddDeviceMethod(Device model)
        {
            if (ModelState.IsValid)
            {
                Client deviceOwner = db.Clients.Find(model.Dev_Owner.Cl_Id);
                model.Dev_Owner = deviceOwner;

                db.Devices.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index", "Clients");
            }
            return View("Error");
        }




        // GET: Devices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = db.Devices.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(device);
        }

        // GET: Devices/Create
        public ActionResult Create(int ClientId)
        {
            ViewBag.ClientId = ClientId;
            ViewBag.ClientName = db.Clients.Where(c => c.Cl_Id == ClientId).SingleOrDefault().Cl_LastName;
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeviceId,DeviceName,Notes,ClientId")] Device device)
        {
            if (ModelState.IsValid)
            {
                db.Devices.Add(device);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FirstName", device.Dev_Owner.Cl_Id);
            return View(device);
        }

        // GET: Devices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = db.Devices.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FirstName", device.Dev_Owner.Cl_Id);
            return View(device);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeviceId,DeviceName,Notes,ClientId")] Device device)
        {
            if (ModelState.IsValid)
            {
                db.Entry(device).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FirstName", device.Dev_Owner.Cl_Id);
            return View(device);
        }

        // GET: Devices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = db.Devices.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Device device = db.Devices.Find(id);
            db.Devices.Remove(device);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
