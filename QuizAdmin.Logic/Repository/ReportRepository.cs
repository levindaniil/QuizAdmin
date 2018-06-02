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
        public override Report AddItem(Report item)
        {
            using (var context = new Context())
            {
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
                
                context.SaveChanges();
            }
            return report;
        }
    }
}