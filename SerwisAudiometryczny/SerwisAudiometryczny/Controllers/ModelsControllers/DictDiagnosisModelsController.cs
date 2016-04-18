﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SerwisAudiometryczny.Models;
using SerwisAudiometryczny.Models.Dicts;

namespace SerwisAudiometryczny.Controllers.ModelsControllers
{
    public class DictDiagnosisModelsController : Controller
    {
        private MyBaseModelDBContext db = new MyBaseModelDBContext();

        // GET: DictDiagnosisModels
        public ActionResult Index()
        {
            return View(db.DictDiagnosisObjects.ToList());
        }

        // GET: DictDiagnosisModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DictDiagnosisModel dictDiagnosisModel = db.DictDiagnosisObjects.Find(id);
            if (dictDiagnosisModel == null)
            {
                return HttpNotFound();
            }
            return View(dictDiagnosisModel);
        }

        // GET: DictDiagnosisModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DictDiagnosisModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CreationDate,Type,Code,Description,Name")] DictDiagnosisModel dictDiagnosisModel)
        {
            if (ModelState.IsValid)
            {
                db.MyBaseObjects.Add(dictDiagnosisModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dictDiagnosisModel);
        }

        // GET: DictDiagnosisModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DictDiagnosisModel dictDiagnosisModel = db.DictDiagnosisObjects.Find(id);
            if (dictDiagnosisModel == null)
            {
                return HttpNotFound();
            }
            return View(dictDiagnosisModel);
        }

        // POST: DictDiagnosisModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CreationDate,Type,Code,Description,Name")] DictDiagnosisModel dictDiagnosisModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dictDiagnosisModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dictDiagnosisModel);
        }

        // GET: DictDiagnosisModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DictDiagnosisModel dictDiagnosisModel = db.DictDiagnosisObjects.Find(id);
            if (dictDiagnosisModel == null)
            {
                return HttpNotFound();
            }
            return View(dictDiagnosisModel);
        }

        // POST: DictDiagnosisModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DictDiagnosisModel dictDiagnosisModel = db.DictDiagnosisObjects.Find(id);
            db.MyBaseObjects.Remove(dictDiagnosisModel);
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
