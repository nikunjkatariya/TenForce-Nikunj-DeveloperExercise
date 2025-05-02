using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenForce_Nikunj_DeveloperExercise.Domain.Objects;

namespace TenForce_Nikunj_DeveloperExercise.Domain.Interfaces
{
    ///<summary>
    /// A service that can be used to get data from the Solar System OpenData API<see href="https://api.le-systeme-solaire.net/"/>. 
    ///</summary>
    public interface IMoonService
    {
        IEnumerable<Moon> GetAllMoons();
    }
}
