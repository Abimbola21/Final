using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Models
{
    public class Course
    {
        [Display(Name = "Course Id")]
        public int Id { get; set; }
        [Display(Name = "Course Code")]
        public string Code { get; set; }
        [Display(Name = "Course Title")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Display(Name = "Credit Hours")]
        public int CreditHours { get; set; }
        public string Session { get; set; }
        public string Status { get; set; }
        public string Instructor { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
