using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Labmaintanance2.Models;

namespace Labmaintanance2.Controllers
{
    public class TechActionsController : Controller
    {
        private LabMaintanance2Entities db = new LabMaintanance2Entities();

        // GET: TechActions
        public ActionResult Index()
        {
            var actions = db.Actions.Include(a => a.Complain);
            return View(actions.ToList());
        }

        // GET: TechActions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Labmaintanance2.Models.Action action = db.Actions.Find(id);
            if (action == null)
            {
                return HttpNotFound();
            }
            return View(action);
        }

        // GET: TechActions/Create
        public ActionResult Create()
        {
            ViewBag.complain_id = new SelectList(db.Complains, "complain_id", "Name_Of_the_Item");
            return View();
        }

        // POST: TechActions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "action_id,complain_id,action_description,action_date,TeachName")] Labmaintanance2.Models.Action action)
        {
            if (ModelState.IsValid)
            {
                db.Actions.Add(action);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.complain_id = new SelectList(db.Complains, "complain_id", "Name_Of_the_Item", action.complain_id);
            return View(action);
        }

        // GET: TechActions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Labmaintanance2.Models.Action action = db.Actions.Find(id);
            if (action == null)
            {
                return HttpNotFound();
            }
            ViewBag.complain_id = new SelectList(db.Complains, "complain_id", "Name_Of_the_Item", action.complain_id);
            return View(action);
        }
        // ... continuation of code ...

        // POST: TechActions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "action_id,complain_id,action_description,action_date,TeachName")] Labmaintanance2.Models.Action action)
        {
            if (ModelState.IsValid)
            {
                db.Entry(action).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.complain_id = new SelectList(db.Complains, "complain_id", "Name_Of_the_Item", action.complain_id);
            return View(action);
        }

        // GET: TechActions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Labmaintanance2.Models.Action action = db.Actions.Find(id);
            if (action == null)
            {
                return HttpNotFound();
            }
            return View(action);
        }

        // POST: TechActions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Labmaintanance2.Models.Action action = db.Actions.Find(id);
            db.Actions.Remove(action);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // ... continuation of code ...
    }
}