using System;
using FriendOrganizer2.Model;

namespace FriendOrganizer2.UI.Wrapper
{
    public class WeatherWrapper : ModelWrapper<Weather>
    {
        public WeatherWrapper(Weather model) : base(model)
        {
        }

        public long id { get { return Model.id; } }
        public string weather_state_name { get { return Model.weather_state_name; } }
        public string weather_state_abbr { get { return Model.weather_state_abbr; } }
        public string applicable_date { get { return Model.applicable_date; } }
        private double NumMin { get { return Math.Round((double)Model.min_temp, 1); } }
        private double NumMax { get { return Math.Round((double)Model.max_temp, 1); } }
        public string min_temp { get { return $"{NumMin.ToString()}°"; } }
        public string max_temp { get { return $"{NumMax.ToString()}°"; } }
    }
}
