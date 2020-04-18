using System;
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
    public class ClientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Gets all Clients
        public ActionResult Index()
        {
            return View(db.Clients.ToList());
        }


        // Returns an Add/Edit Client modal
        public PartialViewResult CreateAddEditClientModal(int? id)
        {
            if (id == null)
            {
                var newClient = new Client();
                return PartialView("_AddEditClientPartial", newClient);
            }
            else
            {
                var oldClient = db.Clients.Find(id);
                return PartialView("_AddEditClientPartial", oldClient);
            }
        }


        // Saves the Add/Edit Client modal's form
        [HttpPost]
        public ActionResult AddEditClientMethod([Bind(Include = "Cl_Id,Cl_FirstName,Cl_LastName,Cl_Phone,Cl_Notes")] Client model)
        {
            if (ModelState.IsValid)
            {
                if (model.Cl_Id == 0)
                {
                    db.Clients.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View("Error");
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }

            var detailsModel = new ClientDetailsViewModel();
            detailsModel.Client = client;
            detailsModel.ClientDevices = db.Devices.Where(d => d.Dev_Owner.Cl_Id == client.Cl_Id);

            return View(detailsModel);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
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
