using CBCC.Areas.Admin.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebMVC.Entities;

namespace CBCC.Areas.Admin.Controllers
{
    public class GopYQuestionsController : Controller
    {
        private UsersContext db = new UsersContext();

        // GET: Admin/GopYQuestions
        public ActionResult Index()
        {
            return View(db.GopYQuestions.ToList());
        }

        // GET: Admin/GopYQuestions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GopYQuestion gopYQuestion = db.GopYQuestions.Find(id);
            if (gopYQuestion == null)
            {
                return HttpNotFound();
            }
            return View(gopYQuestion);
        }

        // GET: Admin/GopYQuestions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/GopYQuestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Question,IDAnswers")] GopYQuestion gopYQuestion)
        {
            if (ModelState.IsValid)
            {
                db.GopYQuestions.Add(gopYQuestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gopYQuestion);
        }

        // GET: Admin/GopYQuestions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GopYQuestion gopYQuestion = db.GopYQuestions.Find(id);
            if (gopYQuestion == null)
            {
                return HttpNotFound();
            }
            return View(gopYQuestion);
        }

        // POST: Admin/GopYQuestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Question,IDAnswers")] GopYQuestion gopYQuestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gopYQuestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gopYQuestion);
        }

        // GET: Admin/GopYQuestions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GopYQuestion gopYQuestion = db.GopYQuestions.Find(id);
            if (gopYQuestion == null)
            {
                return HttpNotFound();
            }
            return View(gopYQuestion);
        }

        // POST: Admin/GopYQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GopYQuestion gopYQuestion = db.GopYQuestions.Find(id);
            db.GopYQuestions.Remove(gopYQuestion);
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
