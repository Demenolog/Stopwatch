using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stopwatch.Models.Records
{
    public class Records
    {
        public int RecordsId { get; set; }

        [Required]
        public string Time { get; set; }
        [Required]
        public string TotalTime { get; set; }
    }
}
