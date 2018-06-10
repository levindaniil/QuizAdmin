using Newtonsoft.Json;
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
    public class UserRequestController : ApiController
    {
        IRepository<Report> reportRepo = RepositoryFactory.Default.GetRepository<Report>() as ReportRepository;

        [HttpPost]
        public HttpResponseMessage GetReport([FromBody] CompleteReportRequest request)
        {            
            DateTime.TryParse(request.Created, out DateTime created);
            DateTime.TryParse(request.Finished, out DateTime finished);
            Guid.TryParse(request.Report_Guid, out Guid report_Guid);
            bool.TryParse(request.IsOK, out bool isOK);
            List<int> answers_Id = new List<int>();
            foreach (var a in request.Answers_Id)
            {
                if (int.TryParse(a, out int i))
                    answers_Id.Add(i);
            }
            var userAnswers = GetAnswers(answers_Id); 

            if (request == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError("Wrong content"));
            }           

            Report report = GetReport(created, report_Guid);
            if (report == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, new HttpError("Cannot find report for the date"));
            }
            else if (report.IsOK != null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, new HttpError("Requested report is already completed"));
            }

            Report userReport = new Report()
            {
                Id = report_Guid,
                Created = created,
                Replied = finished,
                IsOK = isOK,
                User = report.User,
                Question = report.Question,
                Answers = userAnswers
            };

            reportRepo.EditItem(userReport, report_Guid);


            
            var jsonResponse = JsonConvert.SerializeObject(report_Guid);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(jsonResponse.ToString(), Encoding.UTF8, "application/json");
            return response;
        }

        private Report GetReport(DateTime created, Guid report_guid)
        {            
            IEnumerable<Report> candidateReports = reportRepo.FindAll(r => ((r.Created.Subtract(created)).TotalSeconds < 1)&& r.Id == report_guid);
            if (candidateReports.Count() != 1)
            {
                return null;
            }
            return candidateReports.First();            
        }

        private List<Answer> GetAnswers(List<int> answers_Id)
        {
            List<Answer> list = new List<Answer>();
            IRepository<Answer> answerRepo = RepositoryFactory.Default.GetRepository<Answer>() as AnswerRepository;
            foreach (var id in answers_Id)
            {
                var a = answerRepo.Data.FirstOrDefault(x => x.Id == id);
                list.Add(a);
            }
            return list;
        }
    }
}
