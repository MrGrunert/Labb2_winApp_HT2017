using System;
using System.Threading.Tasks;
using FriendOrganizer2.Model;

namespace FriendOrganizer2.DataAccess
{
    public interface IWeatherApi
    {
        Task<Weather> RunAsync(DateTime date);
    }
}