using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenForce_Nikunj_DeveloperExercise.Domain.DataTransferObjects;

namespace TenForce_Nikunj_DeveloperExercise.Domain.Objects
{
    public class Moon
    {
        public string Id { get; set; }
        public float MassValue { get; set; }
        public float MassExponent { get; set; }
        public float AverageTemperature { get; set; }
        public Moon(MoonDto moonDto)
        {
            Id = moonDto.Id;
            MassValue = moonDto.MassValue;
            MassExponent = moonDto.MassExponent;
            AverageTemperature = moonDto.AverageTemparature;
        }
    }
}
