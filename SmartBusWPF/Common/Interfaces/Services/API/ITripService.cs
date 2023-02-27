using System;
using System.Threading.Tasks;
using SmartBusWPF.Models.API;
using SmartBusWPF.Common.Enums;

namespace SmartBusWPF.Common.Interfaces.Services.API
{
    public interface ITripService
    {
        Task<HttpResponseModel<string>> StartBusTrip(int busID, DestinationType destinationType, string authToken);
        Task<HttpResponseModel<string>> StopBusTrip(int busID, string authToken);
        Task<HttpResponseModel<string>> UpdateBusLocation(int busID, string currentLocation, string authToken);
        Task<HttpResponseModel<string>> UpdateStudentStatusOnBus(int studentID, int busID, DateTime timestamp, string authToken);
        Task<HttpResponseModel<string>> UpdateStudentStatusOffBus(int studentID, int busID, DateTime timestamp, string authToken);
    }
}