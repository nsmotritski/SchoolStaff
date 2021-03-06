﻿using ContosoUniversity.Models;
using SchoolStaff.DAL;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Ninject;

namespace SchoolStaff.Controllers
{
    public class IndividualsController : Controller
    {
        private IRepository repo;

        public IndividualsController()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRepository>().To<SchoolRepository>();
            repo = ninjectKernel.Get<IRepository>();
        }

        public IndividualsController(IRepository r)
        {
            repo = r;
        }

        // GET: Individuals
        public ActionResult Index(string sortOrder)
        {
            ViewBag.FirstNameSortParam = string.IsNullOrEmpty(sortOrder) ? "firstName_desc" : "";
            ViewBag.MiddleNameSortParam = string.IsNullOrEmpty(sortOrder) ? "middleName_desc" : "";
            ViewBag.LastNameSortParam = string.IsNullOrEmpty(sortOrder) ? "lastName_desc" : "";
            ViewBag.DateOfBirthSortParam = sortOrder == "Date" ? "dateOfBirth_desc" : "Date";
            ViewBag.EmailSortParam = string.IsNullOrEmpty(sortOrder) ? "email_desc" : "";
            ViewBag.PhoneSortParam = string.IsNullOrEmpty(sortOrder) ? "phone_desc" : "";
            var individuals = from s in repo.List()
                           select s;
            switch (sortOrder)
            {
                case "firstName_desc":
                    individuals = individuals.OrderByDescending(s => s.FirstName);
                    break;
                case "middleName_desc":
                    individuals = individuals.OrderByDescending(s => s.MiddleName);
                    break;
                case "lastName_desc":
                    individuals = individuals.OrderByDescending(s => s.LastName);
                    break;
                case "dateOfBirth_desc":
                    individuals = individuals.OrderByDescending(s => s.DateOfBirth);
                    break;
                case "Email":
                    individuals = individuals.OrderBy(s => s.Email);
                    break;
                case "contactPhone_desc":
                    individuals = individuals.OrderByDescending(s => s.ContactPhone);
                    break;
                default:
                    individuals = individuals.OrderBy(s => s.LastName);
                    break;
            }
            return View(individuals.ToList());
        }

        // GET: Individuals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Individual individual = repo.Get(id);
            if (individual == null)
            {
                return HttpNotFound();
            }
            return View(individual);
        }

        // GET: Individuals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Individuals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,MiddleName,LastName,DateOfBirth,Email,ContactPhone")] Individual individual)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repo.Save(individual);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(individual);
        }

        // GET: Individuals/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var individualToUpdate = repo.Get(id);
            if (TryUpdateModel(individualToUpdate, "",
               new string[] { "FirstName, MiddleName, LastName, DateOfBirth, Email, ContactPhone" }))
            {
                try
                {
                    repo.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(individualToUpdate);
        }

        // POST: Individuals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,MiddleName,LastName,DateOfBirth,Email,ContactPhone")] Individual individual)
        {
            if (ModelState.IsValid)
            {
                repo.Get(individual.ID).State = EntityState.Modified;
                repo.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(individual);
        }

        // GET: Individuals/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator";
            }
            Individual individual = repo.Get(id);
            if (individual == null)
            {
                return HttpNotFound();
            }
            return View(individual);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Individual individual = repo.Get(id);
                repo.Delete(individual);
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
