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

        private int totalStudents;
        public int TotalStudents
        {
            get => totalStudents;
            set => SetProperty(ref totalStudents, value);
        }

        private string studentImage;
        public string StudentImage
        {
            get => studentImage;
            set => SetProperty(ref studentImage, value);
        }

        private string fullName;
        public string FullName
        {
            get => fullName;
            set => SetProperty(ref fullName, value);
        }

        private string address;
        public string Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }

        private int gradeLevel;
        public int GradeLevel
        {
            get => gradeLevel;
            set => SetProperty(ref gradeLevel, value);
        }

        private string status;
        public string Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }

        private bool showScanningPhase;
        public bool ShowScanningPhase
        {
            get => showScanningPhase;
            set => SetProperty(ref showScanningPhase, value);
        }

        private bool showMap;
        public bool ShowMap
        {
            get => showMap;
            set => SetProperty(ref showMap, value);
        }

        private string mapPath;
        public string MapPath
        {
            get => mapPath;
            set => SetProperty(ref mapPath, value);
        }

        public ObservableCollection<StudentModel> ActiveStudents { get; private set; }
        public IRelayCommand StartTripCommand { get; private set; }

        private void Initialize()
        {
            ActiveStudents = new ObservableCollection<StudentModel>();
            StartTripCommand = new RelayCommand(StartTrip, CanStartTrip);
            TotalStudents = 0;
            StudentImage = "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";
            FullName = "Full Name";
            Address = "Address";
            GradeLevel = 0;
            Status = "No student found";
            WeakReferenceMessenger.Default.Register(this);
            ShowScanningPhase = true;
            ShowMap = false;
            MapPath = "C:\\Users\\Malsabi\\Desktop\\Smart-Bus Project\\SmartBusWPF\\SmartBusWPF\\Assets\\Html\\map.html";
        }

        private void StartTrip()
        {
            ShowScanningPhase = false;
            ShowMap = true;
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

                if (!ActiveStudents.Contains(student))
                {
                    ActiveStudents.Add(student);
                    TotalStudents++;
                }
                StudentImage = student.Image;
                FullName = string.Format("{0} {1}", student.FirstName, student.LastName);
                Address = student.Address;
                GradeLevel = student.GradeLevel;
                Status = "The student with the given ID: " + model.ID + " found successfully";
            }
            else
            {
                StudentImage = "";
                FullName = "N/A";
                Address = "N/A";
                GradeLevel = 0;
                Status = "No student found with the given ID: " + model.ID;
            }
        }
    }
}