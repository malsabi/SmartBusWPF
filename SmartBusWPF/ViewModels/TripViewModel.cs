using SmartBusWPF.Messages;
using SmartBusWPF.Models.Student;
using CommunityToolkit.Mvvm.Input;
using SmartBusWPF.Models.HuskyLens;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using SmartBusWPF.Common.Interfaces.Services;
using SmartBusWPF.DTOs.Student;
using SmartBusWPF.Common.Consts;
using SmartBusWPF.Models.API;
using System;
using System.IO;

namespace SmartBusWPF.ViewModels
{
    public class TripViewModel : BaseViewModel, IRecipient<FaceRecognitionMessage>
    {
        private readonly ILoggerService loggerService;
        private readonly INavigationService navigationService;
        private readonly IHttpClientService httpClientService;

        public TripViewModel(ILoggerService loggerService,
                            INavigationService navigationService,
                            IHttpClientService httpClientService) : base(navigationService)
        {
            this.loggerService = loggerService;
            this.navigationService = navigationService;
            this.httpClientService = httpClientService;
            Initialize();
        }

        #region "Properties"
        public IRelayCommand StartTripCommand { get; private set; }

        public IRelayCommand StopTripCommand { get; private set; }

        public ObservableCollection<StudentModel> ActiveStudents { get; private set; }

        private StudentModel currentStudent;
        public StudentModel CurrentStudent
        {
            get => currentStudent;
            set => SetProperty(ref currentStudent, value);
        }

        private bool showWarningModal;
        public bool ShowWarningModal
        {
            get => showWarningModal;
            set => SetProperty(ref showWarningModal, value);
        }
        #endregion

        private void Initialize()
        {
            StartTripCommand = new RelayCommand(StartTrip, CanStartTrip);
            StopTripCommand = new RelayCommand(StartTrip, CanStartTrip);
            ActiveStudents = new ObservableCollection<StudentModel>();
            CurrentStudent = new StudentModel();
            WeakReferenceMessenger.Default.Register(this);
        }

        private void StartTrip()
        {
            ShowWarningModal = true;
        }

        private bool CanStartTrip()
        {
            return true; //ActiveStudents != null && ActiveStudents.Count > 0;
        }

        public async void Receive(FaceRecognitionMessage message)
        {
            FaceRecognitonModel model = message.Value;

            HttpResponseModel<StudentDto> result = await httpClientService.GetAsync<StudentDto>(APIConsts.Student.GetStudent, App.Current.BusDriverSession.AuthToken, model.ID.ToString());
            
            if (result.IsSuccess)
            {
                StudentModel student = new()
                {
                    ID = result.Response.ID,
                    Image = result.Response.Image,
                    FirstName = result.Response.FirstName,
                    LastName = result.Response.LastName,
                    Gender = result.Response.Gender,
                    GradeLevel = result.Response.GradeLevel,
                    Address = result.Response.Address,
                    BelongsToBusID = result.Response.BelongsToBusID,
                    IsAtSchool = result.Response.IsAtSchool,
                    IsAtHome = result.Response.IsAtHome,
                    IsOnBus = result.Response.IsOnBus,
                    ParentID = result.Response.ParentID
                };

                CurrentStudent = student;

                if (!ActiveStudents.Contains(student))
                {
                    ActiveStudents.Add(student);
                }
                //StudentImage = student.Image;
                //FullName = string.Format("{0} {1}", student.FirstName, student.LastName);
                //Address = student.Address;
                //GradeLevel = student.GradeLevel;
                //Status = "The student with the given ID: " + model.ID + " found successfully";
            }
            else
            {
                //StudentImage = "";
                //FullName = "N/A";
                //Address = "N/A";
                //GradeLevel = 0;
                //Status = "No student found with the given ID: " + model.ID;
            }
        }
    }
}