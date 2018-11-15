using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ContosoUniversity.Models;

namespace SchoolStaff.DAL
{
    public class SchoolInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SchoolContext>
    {
        protected override void Seed(SchoolContext context)
        {
            var individuals = new List<Individual>
            {
            new Individual{FirstName="Carson",MiddleName="Paul",LastName="Alexander",DateOfBirth=DateTime.Parse("1995-09-01"),Email="test1@test.com",ContactPhone="8(029)1234567"},
            new Individual{FirstName="Meredith",MiddleName="Paul",LastName="Alonso",DateOfBirth=DateTime.Parse("1992-09-01"),Email="test1@test.com",ContactPhone="8(029)2345678"},
            new Individual{FirstName="Arturo",MiddleName="Paul",LastName="Anand",DateOfBirth=DateTime.Parse("1993-09-01"),Email="test1@test.com",ContactPhone="8(029)3456789"},
            new Individual{FirstName="Gytis",MiddleName="Paul",LastName="Barzdukas",DateOfBirth=DateTime.Parse("1992-09-01"),Email="test1@test.com",ContactPhone="8(029)1233567"},
            new Individual{FirstName="Yan",MiddleName="Paul",LastName="Li",DateOfBirth=DateTime.Parse("1992-09-01"),Email="test1@test.com",ContactPhone="8(029)1234267"},
            new Individual{FirstName="Peggy",MiddleName="Paul",LastName="Justice",DateOfBirth=DateTime.Parse("1991-09-01"),Email="test1@test.com",ContactPhone="8(029)7234567"},
            new Individual{FirstName="Laura",MiddleName="Paul",LastName="Norman",DateOfBirth=DateTime.Parse("1993-09-01"),Email="test1@test.com",ContactPhone="8(029)1264567"},
            new Individual{FirstName="Nino",MiddleName="Paul",LastName="Olivetto",DateOfBirth=DateTime.Parse("1995-09-01"),Email="test1@test.com",ContactPhone="8(029)1134567"}
            };

            individuals.ForEach(s => context.Individuals.Add(s));
            context.SaveChanges();
        }
    }
}