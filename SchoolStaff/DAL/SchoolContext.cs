using System;
using System.Collections;
using System.Collections.Generic;
using ContosoUniversity.Models;
//using SchoolStaff.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics.Eventing.Reader;

namespace SchoolStaff.DAL
{
    public class SchoolContext : DbContext
    {

        public SchoolContext() : base("SchoolContext")
        {
        }

        public DbSet<Individual> Individuals { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public interface IRepository
    {
        void Save(Individual i);
        void Delete(Individual i);
        void SaveChanges();
        IEnumerable<Individual> List();
        Individual Get(int? id);
        void Dispose();
    }

    public class SchoolRepository : IDisposable, IRepository
    {
        private SchoolContext _db = new SchoolContext();

        public void Save(Individual i)
        {
            _db.Individuals.Add(i);
            _db.SaveChanges();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public IEnumerable<Individual> List()
        {
            return _db.Individuals;
        }

        public Individual Get(int? id)
        {
            return _db.Individuals.Find(id);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Delete(Individual individual)
        {
            _db.Individuals.Remove(individual);
            _db.SaveChanges();
        }

        public void Update(Individual individual)
        {
            _db.Individuals.AddOrUpdate(individual);
            _db.SaveChanges();
        }
    }
}