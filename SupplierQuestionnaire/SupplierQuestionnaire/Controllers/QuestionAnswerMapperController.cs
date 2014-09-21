using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupplierQuestionnaire.Models;

namespace SupplierQuestionnaire.Controllers
{
    public class QuestionAnswerMapperController : Controller
    {
        private SupplierQuestionnaireDbContext db = new SupplierQuestionnaireDbContext();

        //
        // GET: /QuestionAnswerMapper/

        public ActionResult Index()
        {
            var questionanswermappers = db.QuestionAnswerMappers.Include(q => q.Question).Include(q => q.Supplier);
            return View(questionanswermappers.ToList());
        }

        //
        // GET: /QuestionAnswerMapper/Details/5

        public ActionResult Details(int quesId,int suppId)
        {
            QuestionAnswerMapper questionanswermapper = db.QuestionAnswerMappers.Find(quesId,suppId);
            Question question = db.Questions.Find(quesId);
            ViewBag.questionText = question.QuestionText;
            Supplier supplier = db.Suppliers.Find(suppId);
            ViewBag.supplierName = supplier.Name;
            if (questionanswermapper == null)
            {
                return HttpNotFound();
            }
            return View(questionanswermapper);
        }

        //
        // GET: /QuestionAnswerMapper/Create

        public ActionResult Create()
        {
            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "QuestionText");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name");
            return View();
        }

        //
        // POST: /QuestionAnswerMapper/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionAnswerMapper questionanswermapper)
        {
            if (ModelState.IsValid)
            {
                db.QuestionAnswerMappers.Add(questionanswermapper);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "QuestionText", questionanswermapper.QuestionId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", questionanswermapper.SupplierId);
            return View(questionanswermapper);
        }

        //
        // GET: /QuestionAnswerMapper/Edit/5

        public ActionResult Edit(int quesId, int suppId)
        {
            QuestionAnswerMapper questionanswermapper = db.QuestionAnswerMappers.Find(quesId,suppId);
            Question question = db.Questions.Find(quesId);
            ViewBag.questionText = question.QuestionText;
            Supplier supplier = db.Suppliers.Find(suppId);
            ViewBag.supplierName = supplier.Name;
            if (questionanswermapper == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "QuestionText", questionanswermapper.QuestionId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", questionanswermapper.SupplierId);
            return View(questionanswermapper);
        }

        //
        // POST: /QuestionAnswerMapper/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionAnswerMapper questionanswermapper)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionanswermapper).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "QuestionText", questionanswermapper.QuestionId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", questionanswermapper.SupplierId);
            return View(questionanswermapper);
        }

        //
        // GET: /QuestionAnswerMapper/Delete/5

        public ActionResult Delete(int quesId,int suppId)
        {
            QuestionAnswerMapper questionanswermapper = db.QuestionAnswerMappers.Find(quesId,suppId);
            Question question = db.Questions.Find(quesId);
            ViewBag.questionText = question.QuestionText;
            Supplier supplier = db.Suppliers.Find(suppId);
            ViewBag.supplierName = supplier.Name;
            if (questionanswermapper == null)
            {
                return HttpNotFound();
            }
            return View(questionanswermapper);
        }

        //
        // POST: /QuestionAnswerMapper/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionAnswerMapper questionanswermapper = db.QuestionAnswerMappers.Find(id);
            db.QuestionAnswerMappers.Remove(questionanswermapper);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}