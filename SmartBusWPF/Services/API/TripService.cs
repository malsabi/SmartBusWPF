using System;
using SmartBusWPF.DTOs.Trip;
using System.Threading.Tasks;
using SmartBusWPF.Models.API;
using SmartBusWPF.Common.Enums;
using SmartBusWPF.Common.Consts;
using SmartBusWPF.Common.Interfaces.Services.API;

namespace SmartBusWPF.Services.API
{
    public class TripService : ITripService
    {
        private readonly IHttpClientService httpClientService;

        public TripService(IHttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public async Task<HttpResponseModel<string>> StartBusTrip(int busID, DestinationType destinationType, string authToken)
        {
            StartTripDto data = new()
            {
                BusID = busID,
                DestinationType = destinationType
            };
            HttpResponseModel<string> result = await httpClientService.PostAsync<StartTripDto, string>(data, APIConsts.Trip.StartBusTrip, authToken);
            return result;
        }

        public async Task<HttpResponseModel<string>> StopBusTrip(int busID, string authToken)
        {
            StopTripDto data = new()
            {
                BusID = busID
            };
            HttpResponseModel<string> result = await httpClientService.PostAsync<StopTripDto, string>(data, APIConsts.Trip.StopBusTrip, authToken);
            return result;
        }

        public async Task<HttpResponseModel<string>> UpdateBusLocation(int busID, string currentLocation, string authToken)
        {
            UpdateBusLocationDto data = new()
            {
                BusID = busID,
                CurrentLocation = currentLocation
            };
            HttpResponseModel<string> result = await httpClientService.PostAsync<UpdateBusLocationDto, string>(data, APIConsts.Trip.UpdateBusLocation, authToken);
            return result;
        }

        public async Task<HttpResponseModel<string>> UpdateStudentStatusOnBus(int studentID, int busID, DateTime timestamp, string authToken)
        {
            UpdateStudentStatusOnBusDto data = new()
            {
                StudentID = studentID,
                BusID = busID,
                Timestamp = timestamp
            };
            HttpResponseModel<string> result = await httpClientService.PostAsync<UpdateStudentStatusOnBusDto, string>(data, APIConsts.Trip.UpdateStudentStatusOnBus, authToken);
            return result;
        }

        public async Task<HttpResponseModel<string>> UpdateStudentStatusOffBus(int studentID, int busID, DateTime timestamp, string authToken)
        {
            UpdateStudentStatusOffBusDto data = new()
            {
                StudentID = studentID,
                BusID = busID,
                Timestamp = timestamp
            };
            HttpResponseModel<string> result = await httpClientService.PostAsync<UpdateStudentStatusOffBusDto, string>(data, APIConsts.Trip.UpdateStudentStatusOffBus, authToken);
            return result;
        }
    }
}