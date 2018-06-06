using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAdmin.Logic.Model
{
    public class Report
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public bool? IsOK { get; set; }
        public virtual User User { get; set; }
        public virtual Question Question { get; set; }
        public virtual List<Answer> Answers { get; set; }
        public DateTime? Replied { get; set; }
        [NotMapped]
        public string Correct { get; set; }
    }
}
