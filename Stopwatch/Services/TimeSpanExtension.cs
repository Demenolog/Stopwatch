using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stopwatch.Services
{
    internal static class TimeSpanExtension
    {
        public static string ToStringTime(this TimeSpan value)
        {
            return value.ToString(@"hh\:mm\:ss\:fff");
        }
    }
}
