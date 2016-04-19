//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using SerwisAudiometryczny.Models;

//namespace SerwisAudiometryczny.Controllers.ModelsControllers
//{
//    public class MyBaseModelsController : Controller
//    {
//        private MyBaseModelDBContext db = new MyBaseModelDBContext();

//        // GET: MyBaseModels
//        public ActionResult Index()
//        {
//            return View(db.MyBaseObjects.ToList());
//        }

//        // GET: MyBaseModels/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            MyBaseModel myBaseModel = db.MyBaseObjects.Find(id);
//            if (myBaseModel == null)
//            {
//                return HttpNotFound();
//            }
//            return View(myBaseModel);
//        }

//        // GET: MyBaseModels/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: MyBaseModels/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "ID,CreationDate,Type")] MyBaseModel myBaseModel)
//        {
//            if (ModelState.IsValid)
//            {
//                db.MyBaseObjects.Add(myBaseModel);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            return View(myBaseModel);
//        }

//        // GET: MyBaseModels/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            MyBaseModel myBaseModel = db.MyBaseObjects.Find(id);
//            if (myBaseModel == null)
//            {
//                return HttpNotFound();
//            }
//            return View(myBaseModel);
//        }

//        // POST: MyBaseModels/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "ID,CreationDate,Type")] MyBaseModel myBaseModel)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(myBaseModel).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(myBaseModel);
//        }

//        // GET: MyBaseModels/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            MyBaseModel myBaseModel = db.MyBaseObjects.Find(id);
//            if (myBaseModel == null)
//            {
//                return HttpNotFound();
//            }
//            return View(myBaseModel);
//        }

//        // POST: MyBaseModels/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            MyBaseModel myBaseModel = db.MyBaseObjects.Find(id);
//            db.MyBaseObjects.Remove(myBaseModel);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
