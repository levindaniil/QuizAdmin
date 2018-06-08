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

            if (request == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError("Wrong content"));
            }

            Report report = GetReport(created, report_Guid);
            if (report == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, new HttpError("Cannot find report for the date"));
            }



            var jsonResponse = "Priffke kak deliffke";
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(jsonResponse.ToString(), Encoding.UTF8, "application/json");
            return response;
        }

        private Report GetReport(DateTime created, Guid report_guid)
        {
            IRepository<Report> reportList = RepositoryFactory.Default.GetRepository<Report>() as ReportRepository;
            IEnumerable<Report> candidateReports = reportList.FindAll(r => r.Created == created && r.Id == report_guid);
            if (candidateReports.Count() != 1)
            {
                return null;
            }
            return candidateReports.First();            
        }

    }
}
