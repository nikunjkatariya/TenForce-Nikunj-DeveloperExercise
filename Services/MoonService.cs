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
using Microsoft.Extensions.Caching.Memory;

namespace TenForce_Nikunj_DeveloperExercise.Services
{
    /// <inheritdoc />
    public class MoonService : IMoonService
    {
        private readonly HttpClientService _httpClientService;

        private readonly IMemoryCache _cache;
        private const string CacheKey = "AllMoons";
        public MoonService(HttpClientService httpClientService, IMemoryCache memoryCache)
        {
            _httpClientService = httpClientService;
            _cache = memoryCache;
        }

        public IEnumerable<Moon> GetAllMoons()
        {
            // Try to get the cached value
            if (_cache.TryGetValue(CacheKey, out IEnumerable<Moon> cachedMoons))
            {
                return cachedMoons; // Return cached value if available
            }
            Console.WriteLine(OutputString.LoadingDataFromAPI);

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

            if (allMoons.Any())
            {
                // Set the cache with a 10-minute expiration
                _cache.Set(CacheKey, allMoons, TimeSpan.FromMinutes(10));
            }
            Console.WriteLine(OutputString.DoneLoadingDataFromAPI);
            return allMoons;
        }
    }
}
