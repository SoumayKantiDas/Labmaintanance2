using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Labmaintanance2.Models;

namespace Labmaintanance2.Controllers
{
    public class ComplainsController : Controller
    {
        private LabMaintanance2Entities db = new LabMaintanance2Entities();

        // GET: Complains
        public ActionResult Index()
        {
            var complains = db.Complains.Include(c => c.Priority).Include(c => c.Repaired_Staus).Include(c => c.User);
            return View(complains.ToList());
        }

        // GET: Complains/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complain complain = db.Complains.Find(id);
            if (complain == null)
            {
                return HttpNotFound();
            }
            return View(complain);
        }

        // GET: Complains/Create
        public ActionResult Create()
        {
            ViewBag.PriorityId = new SelectList(db.Priorities, "PriorityId", "priority1");
            ViewBag.Repaired_StausId = new SelectList(db.Repaired_Staus, "Repaired_StausId", "Status");
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Complain complain, HttpPostedFileBase imageData)
        {
            if (ModelState.IsValid)
            {
                if (imageData != null && imageData.ContentLength > 0)
                {
                    // Convert the uploaded image to a byte array
                    byte[] imageBytes;
                    using (var binaryReader = new BinaryReader(imageData.InputStream))
                    {
                        imageBytes = binaryReader.ReadBytes(imageData.ContentLength);
                    }

                    complain.image_data = imageBytes;
                }

                db.Complains.Add(complain);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PriorityId = new SelectList(db.Priorities, "PriorityId", "priority1", complain.PriorityId);
            ViewBag.Repaired_StausId = new SelectList(db.Repaired_Staus, "Repaired_StausId", "Status", complain.Repaired_StausId);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", complain.user_id);
            return View(complain);
        }


        // GET: Complains/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complain complain = db.Complains.Find(id);
            if (complain == null)
            {
                return HttpNotFound();
            }
            ViewBag.PriorityId = new SelectList(db.Priorities, "PriorityId", "priority1", complain.PriorityId);
            ViewBag.Repaired_StausId = new SelectList(db.Repaired_Staus, "Repaired_StausId", "Status", complain.Repaired_StausId);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", complain.user_id);
            return View(complain);
        }

        // POST: Complains/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "complain_id,user_id,Name_Of_the_Item,description,location,PriorityId,status,Repaired_StausId,image_data")] Complain complain)
        {
            if (ModelState.IsValid)
            {
                db.Entry(complain).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PriorityId = new SelectList(db.Priorities, "PriorityId", "priority1", complain.PriorityId);
            ViewBag.Repaired_StausId = new SelectList(db.Repaired_Staus, "Repaired_StausId", "Status", complain.Repaired_StausId);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", complain.user_id);
            return View(complain);
        }

        // GET: Complains/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complain complain = db.Complains.Find(id);
            if (complain == null)
            {
                return HttpNotFound();
            }
            return View(complain);
        }

        // POST: Complains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Complain complain = db.Complains.Find(id);
            db.Complains.Remove(complain);
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
