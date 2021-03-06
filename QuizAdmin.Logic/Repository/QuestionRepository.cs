﻿using QuizAdmin.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAdmin.Logic.Repository
{
    public class QuestionRepository :Repository<Question>
    {
        public QuestionRepository()
        {
            using (var context = new Context())
            {
                _items = context.Questions.Include("Answers").ToList();
            }
        }
               

        public override Question AddItem(Question question)
        {
            using (var context = new Context())
            {
                context.Questions.Add(question);
                context.SaveChanges();
            }

            _items.Add(question);
            ItemAdded?.Invoke(question);
            return question;
        }

        public override Question EditItem(Question question, object id)
        {
            using (var context = new Context())
            {
                var questionToEdit = context.Questions.Include("Answers").FirstOrDefault(q => q.Id == (int)id);
                
               
                questionToEdit.Date = question.Date;
                questionToEdit.Explanation = question.Explanation;
                questionToEdit.Text = question.Text;

                var oldAnswers = questionToEdit.Answers;
                var newAnswers = question.Answers;

                var ansQuan = oldAnswers.Count();

                for (int i = ansQuan-1; i >= 0; i--)                
                {
                    var temp = newAnswers.FirstOrDefault(a => a.Id == oldAnswers[i].Id);
                    if (temp == null)
                        oldAnswers.Remove(oldAnswers[i]);
                    else
                    {
                        oldAnswers[i].IsCorrect = temp.IsCorrect;
                        oldAnswers[i].Text = temp.Text;
                        newAnswers.Remove(temp);
                    }
                }

                var ansLeft = newAnswers.Count();
                if (ansLeft > 0)
                    foreach (var a in newAnswers)
                    {
                        oldAnswers.Add(a);
                    }
                question.Answers = oldAnswers;
                questionToEdit.Answers = oldAnswers;
                context.SaveChanges();
                ItemAdded?.Invoke(questionToEdit);

                return questionToEdit;
            }
        }

        public override void RemoveItem(Question question)
        {            
            using (var context = new Context())
            {
                var reportsToRemove = context.Reports.Where(r => r.Question.Id == question.Id).ToList();

                foreach (var r in reportsToRemove)
                {
                    context.Answers.RemoveRange(r.Answers);
                }

                context.Set<Report>().RemoveRange(reportsToRemove);
                context.SaveChanges();

                var questionToRemove = context.Questions.Include("Answers").FirstOrDefault(q => q.Id == question.Id);
                context.Answers.RemoveRange(questionToRemove.Answers);
                context.Set<Question>().Remove(questionToRemove);
                context.SaveChanges();
            }

            _items.Remove(question);
        }
    }
}
