using System.ComponentModel.DataAnnotations;

namespace Stopwatch.Models.Database
{
    public class Records
    {
        public int RecordsId { get; set; }

        [Required]
        public string? Time { get; set; }
        [Required]
        public string? TotalTime { get; set; }
    }
}
