using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TenForce_Nikunj_DeveloperExercise.Constants;
using TenForce_Nikunj_DeveloperExercise.Domain.DataTransferObjects.JsonObjects;
using TenForce_Nikunj_DeveloperExercise.Domain.DataTransferObjects;
using TenForce_Nikunj_DeveloperExercise.Domain.Interfaces;
using TenForce_Nikunj_DeveloperExercise.Domain.Objects;
using TenForce_Nikunj_DeveloperExercise.Utilities;

namespace TenForce_Nikunj_DeveloperExercise.Services
{
    /// <inheritdoc />
    public class MoonService : IMoonService
    {
        private readonly HttpClientService _httpClientService;


        public MoonService(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public IEnumerable<Moon> GetAllMoons()
        {
            var response = _httpClientService.Client
                .GetAsync(UriPath.GetAllMoonsWithMassQueryParameters)
                .Result;

            //If the status code isn't 200-299, then the function returns an empty collection.
            if (!response.IsSuccessStatusCode)
            {
                Logger.Instance.Warn($"{LoggerMessage.GetRequestFailed}{response.StatusCode}");
                return new Collection<Moon>();
            }

            var content = response.Content.ReadAsStringAsync().Result;

            //The JSON converter uses DTO's, that can be found in the DataTransferObjects folder, to deserialize the response content.
            var allMoons = new Collection<Moon>();
            var results = JsonConvert.DeserializeObject<JsonResult<MoonDto>>(content);

            //The JSON converter can return a null object. 
            if (results == null) return new Collection<Moon>();

            foreach (MoonDto moonDto in results.Bodies)
            {
                allMoons.Add(new Moon(moonDto));
            }

            return allMoons;
        }
    }
}
