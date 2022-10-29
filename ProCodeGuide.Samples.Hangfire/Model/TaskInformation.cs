using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProCodeGuide.Samples.Hangfire.Model
{
    public partial class TaskInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ScheduleTime { get; set; }
        public string ServiceUrl { get; set; }
        public string ToMail { get; set; }
    }
}
