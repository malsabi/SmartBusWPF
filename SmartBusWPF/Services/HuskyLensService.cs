using System;
using System.Text.Json;
using SmartBusWPF.Messages;
using SmartBusWPF.Common.Enums;
using SmartBusWPF.Models.HuskyLens;
using CommunityToolkit.Mvvm.Messaging;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.Services
{
    public class HuskyLensService : IHuskyLensService
    {
        private readonly ILoggerService loggerService;

        public HuskyLensService(ILoggerService loggerService)
        {
            this.loggerService = loggerService;
        }

        public void Handle(string value)
        {
            try
            {
                FaceRecognitonModel faceRecognitonModel = JsonSerializer.Deserialize<FaceRecognitonModel>(value);

                if (faceRecognitonModel == null)
                {
                    loggerService.Log(LogLevel.Warning, "HuskyLensService", "The face recognition model was empty after the deserialization.");
                    return;
                }

                FaceRecognitionMessage faceRecognitionMessage = new(faceRecognitonModel);

                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    WeakReferenceMessenger.Default.Send(faceRecognitionMessage);
                }));
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "HuskyLensService", ex.Message);
            }
        }
    }
}