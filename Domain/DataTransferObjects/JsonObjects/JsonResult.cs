using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenForce_Nikunj_DeveloperExercise.Domain.DataTransferObjects.JsonObjects
{
    public class JsonResult<T>
    {
        public Collection<T> Bodies { get; set; }
    }
}
