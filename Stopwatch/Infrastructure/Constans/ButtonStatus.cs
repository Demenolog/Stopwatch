using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stopwatch.Infrastructure.Constans
{
    internal static class ButtonStatus
    {
        public enum Status
        {
            Started,
            Stopped,
            Continued,
            Reseted
        }
    }
}
