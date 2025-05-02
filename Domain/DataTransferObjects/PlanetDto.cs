using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenForce_Nikunj_DeveloperExercise.Domain.DataTransferObjects
{
    public class PlanetDto
    {
        public string Id { get; set; }
        public long SemiMajorAxis { get; set; }
        public ICollection<MoonDto> Moons { get; set; }
    }
}
