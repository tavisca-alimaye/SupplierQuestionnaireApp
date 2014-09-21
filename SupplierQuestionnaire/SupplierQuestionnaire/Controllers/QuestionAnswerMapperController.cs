using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupplierQuestionnaire.Models;
using System.Data.SqlClient;
using System.Dynamic;

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

        public ActionResult SearchByQuestion(string questionString)
        {
            var questionList = new List<string>();
            
            var questions = from q in db.Questions select q.QuestionText;
            
            questionList.AddRange(questions.Distinct());

            ViewBag.questionString = new SelectList(questionList);

            var reqdResult = from t in db.QuestionAnswerMappers select t;


            if (!String.IsNullOrEmpty(questionString))
            {
                int quesId = -1;
                var question = from q in db.Questions where q.QuestionText==questionString select q;
                /* Manipulation on the basis that every question is unique so there will be only one row in question*/
                foreach (var row in question)
                {
                    quesId = row.Id;
                }
                if (quesId != -1)
                {
                    var answers = from rec in db.QuestionAnswerMappers where rec.QuestionId == quesId select rec;//w/o sup name
                    var result = (from a in db.QuestionAnswerMappers
                                 join q in db.Questions on a.QuestionId equals q.Id
                                 join s in db.Suppliers on a.SupplierId equals s.Id
                                 where a.QuestionId == quesId
                                 select new {name = s.Name,question = q.QuestionText,answer = a.Answer }).ToExpando();//can't show on view
                    return View(answers);
                }
            }

            return View(reqdResult);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }

    public static class Extensions
    {
        public static ExpandoObject ToExpando(this object anonymousObject)
        {
            IDictionary<string, object> anonymousDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(anonymousObject);
            IDictionary<string, object> expando = new ExpandoObject();
            foreach (var item in anonymousDictionary)
                expando.Add(item);
            return (ExpandoObject)expando;
        }
    }
}