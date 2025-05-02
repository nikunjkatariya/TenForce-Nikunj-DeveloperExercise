using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenForce_Nikunj_DeveloperExercise.Utilities
{
    public static class CultureInfoUtility
    {
        public static readonly TextInfo TextInfo = new CultureInfo("nl-BE").TextInfo;
    }
}
