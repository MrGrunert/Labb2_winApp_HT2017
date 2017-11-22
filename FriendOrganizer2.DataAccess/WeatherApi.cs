using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FriendOrganizer2.Model;
using Newtonsoft.Json;

namespace FriendOrganizer2.DataAccess
{
    public class WeatherApi : IWeatherApi
    {
        public HttpClient httpClient = new HttpClient { BaseAddress = new Uri("https://www.metaweather.com/api/") };
        public async Task<Weather> RunAsync(DateTime date)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var weather = await GetWeatherAsync($"location/890869/{DateToString(date)}/"); // gothenburg = 890869;

            return weather;
        }

        private async Task<Weather> GetWeatherAsync(string path)
        {
          


            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(path);
                var jsonString = await response.Content.ReadAsStringAsync();
                var weatherList = JsonConvert.DeserializeObject<List<Weather>>(jsonString);

                //check if weatherlist is null or empty
                if (weatherList == null || !weatherList.Any())
                {
                    return new Weather();
                }
                var tempWeather = weatherList[0];
                tempWeather.weatherValid = true;
                return tempWeather;
            }
            catch (HttpRequestException hre)
            {
                Console.WriteLine(hre);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
           


            return new Weather();
        }

        private string DateToString(DateTime date) => $"{date.Year}/{date.Month}/{date.Day}";
    }
}
