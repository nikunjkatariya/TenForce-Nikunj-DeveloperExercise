using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    public class PlanetService : IPlanetService
    {
        private readonly HttpClientService _httpClientService;
        private readonly IMemoryCache _cache;
        private const string CacheKey = "AllPlanetsWithMoons";

        public PlanetService(HttpClientService httpClientService, IMemoryCache memoryCache)
        {
            _httpClientService = httpClientService;
            _cache = memoryCache;
        }

        public IEnumerable<Planet> GetAllPlanets()
        {
            // Try to get the cached value
            if (_cache.TryGetValue(CacheKey, out IEnumerable<Planet> cachedPlanets))
            {
                return cachedPlanets; // Return cached value if available
            }

            Console.WriteLine(OutputString.LoadingDataFromAPI);
            var allPlanetsWithTheirMoons = new Collection<Planet>();

            var response = _httpClientService.Client
                .GetAsync(UriPath.GetAllPlanetsWithMoonsQueryParameters)
                .Result;

            //If the status code isn't 200-299, then the function returns an empty collection.
            if (!response.IsSuccessStatusCode)
            {
                Logger.Instance.Warn($"{LoggerMessage.GetRequestFailed}{response.StatusCode}");
                return allPlanetsWithTheirMoons;
            }

            var content = response.Content.ReadAsStringAsync().Result;

            //The JSON converter uses DTO's, that can be found in the DataTransferObjects folder, to deserialize the response content.
            var results = JsonConvert.DeserializeObject<JsonResult<PlanetDto>>(content);

            //The JSON converter can return a null object. 
            if (results == null) return allPlanetsWithTheirMoons;

            //If the planet doesn't have any moons, then it isn't added to the collection.
            foreach (var planet in results.Bodies)
            {
                if (planet.Moons != null)
                {
                    var newMoonsCollection = new Collection<MoonDto>();
                    foreach (var moon in planet.Moons)
                    {
                        var moonResponse = _httpClientService.Client
                            .GetAsync(UriPath.GetMoonByIdQueryParameters + moon.URLId)
                            .Result;
                        var moonContent = moonResponse.Content.ReadAsStringAsync().Result;
                        newMoonsCollection.Add(JsonConvert.DeserializeObject<MoonDto>(moonContent));
                    }
                    planet.Moons = newMoonsCollection;

                }
                allPlanetsWithTheirMoons.Add(new Planet(planet));
            }

            if (allPlanetsWithTheirMoons.Any())
            {
                // Set the cache with a 10-minute expiration
                _cache.Set(CacheKey, allPlanetsWithTheirMoons, TimeSpan.FromMinutes(10));
            }
            Console.WriteLine(OutputString.DoneLoadingDataFromAPI);

            return allPlanetsWithTheirMoons;
        }

        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder
                .ToString()
                .Normalize(NormalizationForm.FormC);
        }
    }
}
