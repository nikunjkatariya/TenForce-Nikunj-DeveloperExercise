using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace TenForce_Nikunj_DeveloperExercise.Utilities
{
    public static class Logger
    {
        public static readonly ILog Instance = LogManager.GetLogger(typeof(Program));
    }
}
