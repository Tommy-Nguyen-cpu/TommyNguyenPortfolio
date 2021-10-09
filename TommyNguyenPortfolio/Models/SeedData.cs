using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TommyNguyenPortfolio.Data;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace TommyNguyenPortfolio.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TommyNguyenPortfolioContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<TommyNguyenPortfolioContext>>()))
            {
                string hashedPassword = new PasswordHasher<object>().HashPassword("NguyenT68", "TestingNewCode!");
                var initialResultOfHash = new PasswordHasher<object>().VerifyHashedPassword("NguyenT68", hashedPassword, "TestingNewCode");
                if (!context.PasswordTable.Any())
                {
                    context.PasswordTable.Add(new PasswordTable { Username = "NguyenT68", Password = hashedPassword, Email ="tnguyen.techboston@gmail.com", PermissionLevel=2 }) ;
                    context.SaveChanges();

                    var rep = context.PasswordTable.FirstOrDefault(r => r.PasswordTableId == 1);
                    var databaseResult = new PasswordHasher<object>()
                        .VerifyHashedPassword(rep.PasswordTableId, rep.Password, "TestingNewCode!");
                }
                // Look for any recommendations.
                if (context.RecommendationDatabase.Any())
                {
                    return;   // DB has been seeded
                }
                context.RecommendationDatabase.AddRange(
                    new RecommendationDatabase
                    {
                        Recommender = "Johanna Jacobson",
                        CompanyWorkedFor = "Wentworth Institute of Technology",
                        RelationToStudent = "Workstudy Coordinator",
                        Recommendation = "Tommy is wonderful to work with! He communicates effectively and professionally, and his expertise has helped both students and instructors succeed during the pandemic. We are fortunate to have Tommy on our team, and I’m sure he will do wonderful things moving forward.",
                        DateRecommended = new DateTime(2020, 12, 7)
                    },

                    new RecommendationDatabase
                    {
                        Recommender = "Constance Balodimos",
                        CompanyWorkedFor = "State Street",
                        RelationToStudent = "Manager",
                        Recommendation = "	Tommy is a hardworking, persistent, and dedicated employee. Whenever given an assignment that he did not understand, he would often ask the right questions in order to fully comprehend his task. He was able to quickly learn new topics which helped him complete tasks in a timely manner. Tommy did not hesitate to ask for more work. Although this was his first internship, Tommy showed to be capable of completing tasks that normally poses difficulty for students his age. Throughout his internship, Tommy developed positive, professional relationships with the team and built professional skills that will help him in the future. His willingness to learn, quickly understand tasks, manage his work, proficiency in Microsoft Word, Excel, and Office, and his ability to work with a diverse team will make him a very good addition to any team or workplace.",
                        DateRecommended = new DateTime(2019,8,28)
                    }
                );
                context.SaveChanges();


            }
        }
    }
}