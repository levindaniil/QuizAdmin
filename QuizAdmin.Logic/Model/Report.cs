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
        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }
        public bool? IsOK { get; set; }
        public virtual User User { get; set; }
        public virtual Question Question { get; set; }
        public virtual List<Answer> Answers { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? Replied { get; set; }

        [NotMapped]
        public string Correct
        {
            get
            {
                if (IsOK == true)
                    return "Correct";
                else if (IsOK == false && Replied == DateTime.MinValue)
                    return "Skipped";
                else if (IsOK == false && Replied != DateTime.MinValue)
                    return "Incorrect";
                else
                    return "Unknown";
            }
        }

        [NotMapped]
        public string ShortReplied => (Replied == null || Replied == DateTime.MinValue) ?  "-" : Replied?.ToShortTimeString() ;

    }
}
