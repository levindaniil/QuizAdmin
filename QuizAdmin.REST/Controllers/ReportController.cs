﻿using Newtonsoft.Json;
using QuizAdmin.Logic.Model;
using QuizAdmin.Logic.Repository;
using QuizAdmin.REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace QuizAdmin.REST.Controllers
{
    public class ReportController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage PutReport([FromBody] ReportRequest request)
        {
            DateTime requestDate;
            DateTime.TryParse(request.Date, out requestDate);

            if (request == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError("Wrong content"));
            }
            User currentUser = GetUser(request.User);
            if (currentUser == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new HttpError("Cannot identify user"));
            }
            Question question = GetQuestion(requestDate);
            if (question == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, new HttpError("Cannot find question for the date"));
            }

            Report existingReport = GetReport(currentUser, question.Date);
            if (existingReport != null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, new HttpError("Report for these user and date already exists"));
            }

            IRepository<Report> reportList = RepositoryFactory.Default.GetRepository<Report>() as ReportRepository;
            Report report = reportList.AddItem(new Report
            {
                User = currentUser,
                Question = question,
                Created = DateTime.Now
            });

            List<AnswerResource> answers = new List<AnswerResource>();
            IRepository<Question> questionList = RepositoryFactory.Default.GetRepository<Question>() as QuestionRepository;
            foreach (var answer in questionList.Data.FirstOrDefault(q => q.Id == question.Id).Answers)
            {
                answers.Add(new AnswerResource
                {
                    Id = answer.Id,
                    IsCorrect = answer.IsCorrect,
                    Text = answer.Text
                });
            }
            ReportResource responseResource = new ReportResource
            {
                Id = report.Id,
                Created = report.Created,
                Question = new QuestionResource
                {
                    Text = report.Question.Text,
                    Date = report.Question.Date,
                    Explanation = report.Question.Explanation
                },
                Answers = answers
            };
            var jsonResponse = JsonConvert.SerializeObject(responseResource);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(jsonResponse.ToString(), Encoding.UTF8, "application/json");
            return response;
        }

        private User GetUser(string item)
        {
            IRepository<User> userList = RepositoryFactory.Default.GetRepository<User>() as UserRepository;
            IEnumerable<User> candidateUsers = userList.FindAll(user => user.Key == item);
            if (candidateUsers.Count() > 1)
            {
                return null;
            }
            else if (candidateUsers.Count() == 0)
            {
                var newUser = new User
                {
                    Key = item
                };
                userList.AddItem(newUser);
                return newUser;
            }
            return candidateUsers.First();
        }
        
        private Question GetQuestion(DateTime item)
        {
            IRepository<Question> questionList = RepositoryFactory.Default.GetRepository<Question>() as QuestionRepository;
            IEnumerable<Question> candidateQuestions = questionList.FindAll(question => question.Date == item);
            if (candidateQuestions.Count() != 1)
            {
                return null;
            }
            return candidateQuestions.First();
        }

        private Report GetReport(User user, DateTime date)
        {
            IRepository<Report> reportList = RepositoryFactory.Default.GetRepository<Report>() as ReportRepository;
            IEnumerable<Report> candidateReports = reportList.FindAll(r => r.User.Id == user.Id && r.Question.Date == date);
            if (candidateReports.Count() != 1)
            {
                return null;
            }
            return candidateReports.First();
        }
    }
}
