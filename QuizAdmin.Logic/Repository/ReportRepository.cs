﻿using QuizAdmin.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAdmin.Logic.Repository
{
    public class ReportRepository : Repository<Report>
    {
        public ReportRepository()
        {
            using (var context = new Context())
            {
                try
                {
                    var reportsToRemove = context.Set<Report>().ToList().FindAll(r => r.Question == null || r.User == null);
                    context.Reports.RemoveRange(reportsToRemove);
                    context.SaveChanges();
                }
                catch { }
                _items = context.Reports.Include("Question").Include("User").Include("Question.Answers").Include("Answers").ToList();
            }
        }

        public override Report AddItem(Report item)
        {
            using (var context = new Context())
            {
                var currentUser = context.Users.FirstOrDefault(u => u.Id == item.User.Id);
                if (currentUser != null) item.User = currentUser;

                var currentQuestion = context.Questions.FirstOrDefault(q => q.Id == item.Question.Id);
                item.Question = currentQuestion;

                context.Set<Report>().Add(item);
                context.SaveChanges();
            }

            _items.Add(item);
            ItemAdded?.Invoke(item);            

            return item;
        }

        public override Report EditItem(Report item, object id)
        {
            Report report;
            using (var context = new Context())
            {
                report = context.Reports.FirstOrDefault(a => a.Id == (Guid)id);
                report.IsOK = item.IsOK;

                List<Answer> userAnswers = new List<Answer>();
                foreach (var a in item.Answers)
                {
                    var ans = context.Answers.FirstOrDefault(x => x.Id == a.Id);
                    userAnswers.Add(ans);
                }

                report.Answers = userAnswers;
                report.Replied = item.Replied;
                
                context.SaveChanges();
            }
            _items.Remove(_items.FirstOrDefault(r => r.Id == report.Id));
            _items.Add(report);
            return report;
        }

        public override void RemoveItem(Report item)
        {
            using (var context = new Context())
            {
                var report = context.Reports.Include("Answers").FirstOrDefault(a => a.Id == item.Id);
                context.SaveChanges();
            }
            _items.Remove(_items.FirstOrDefault(r => r.Id == item.Id));
        }
    }
}
