using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TenForce_Nikunj_DeveloperExercise.Domain.DataTransferObjects.JsonObjects;

namespace TenForce_Nikunj_DeveloperExercise.Domain.DataTransferObjects
{
    //The converter is needed for this DTO. Because of the converter, all the properties need to get the JsonProperty annotation. 
    [JsonConverter(typeof(JsonPathConverter))]
    public class MoonDto
    {
        [JsonProperty("id")] public string Id { get; set; }

        //The property moon is used to set the Id property. 
        [JsonProperty("moon")]
        public string Moon
        {
            get => Id;
            set => Id = value;
        }

        //The path of the specific moon
        [JsonProperty("rel")] public string Rel { get; set; }
        public string URLId { get => Rel.Split('/').Last(); }

        //The path to the nested property is created by using a dot. 
        [JsonProperty("mass.massValue")] public float MassValue { get; set; }
        [JsonProperty("mass.massExponent")] public float MassExponent { get; set; }

        [JsonProperty("avgTemp")] public float AverageTemparature {  get; set; }
    }
}
