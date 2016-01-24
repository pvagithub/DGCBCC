using CBCC.Areas.Admin.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebMVC.Entities;

namespace CBCC.Areas.Admin.Controllers
{
    public class GopYAnswersController : Controller
    {
        private UsersContext db = new UsersContext();

        // GET: Admin/GopYAnswers
        public ActionResult Index()
        {
            return View(db.GopYAnswers.ToList());
        }

        // GET: Admin/GopYAnswers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GopYAnswer gopYAnswer = db.GopYAnswers.Find(id);
            if (gopYAnswer == null)
            {
                return HttpNotFound();
            }
            return View(gopYAnswer);
        }

        // GET: Admin/GopYAnswers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/GopYAnswers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Answer,Type")] GopYAnswer gopYAnswer)
        {
            if (ModelState.IsValid)
            {
                db.GopYAnswers.Add(gopYAnswer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gopYAnswer);
        }

        // GET: Admin/GopYAnswers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GopYAnswer gopYAnswer = db.GopYAnswers.Find(id);
            if (gopYAnswer == null)
            {
                return HttpNotFound();
            }
            return View(gopYAnswer);
        }

        // POST: Admin/GopYAnswers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Answer,Type")] GopYAnswer gopYAnswer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gopYAnswer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gopYAnswer);
        }

        // GET: Admin/GopYAnswers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GopYAnswer gopYAnswer = db.GopYAnswers.Find(id);
            if (gopYAnswer == null)
            {
                return HttpNotFound();
            }
            return View(gopYAnswer);
        }

        // POST: Admin/GopYAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GopYAnswer gopYAnswer = db.GopYAnswers.Find(id);
            db.GopYAnswers.Remove(gopYAnswer);
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
