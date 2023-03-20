using System;
using System.Linq;
using MapsterMapper;
using System.Windows.Media;
using SmartBusWPF.Messages;
using System.Threading.Tasks;
using SmartBusWPF.Models.API;
using SmartBusWPF.DTOs.Student;
using SmartBusWPF.Common.Enums;
using SmartBusWPF.Common.Consts;
using SmartBusWPF.Models.Student;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using SmartBusWPF.Models.GeoLocation;
using CommunityToolkit.Mvvm.Messaging;
using SmartBusWPF.Common.Interfaces.Services;
using SmartBusWPF.Common.Interfaces.Services.API;
using Microsoft.Win32;
using System.IO;

namespace SmartBusWPF.ViewModels
{
    public class TripViewModel : BaseViewModel, IRecipient<FaceRecognitionMessage>
    {
        private bool isExecuting;
        private object isExecutingLock;
        private MediaPlayer mediaPlayer;

        private readonly IMapper mapper;
        private readonly ITripService tripService;
        private readonly IStudentService studentService;
        private readonly INavigationService navigationService;
        private readonly IGeoLocationService geoLocationService;

        public TripViewModel(IMapper mapper,
                             ITripService tripService,
                             IStudentService studentService,
                             INavigationService navigationService,
                             IGeoLocationService geoLocationService) : base(navigationService)
        {
            this.mapper = mapper;
            this.tripService = tripService;
            this.studentService = studentService;
            this.navigationService = navigationService;
            this.geoLocationService = geoLocationService;
            Initialize();
        }

        #region "Properties"
        public IRelayCommand StartTripCommand { get; private set; }

        public IRelayCommand StopTripCommand { get; private set; }

        public IRelayCommand CloseModalCommand { get; private set; }

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

        private TripStatus status;
        public TripStatus Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }

        private string statusMessage;
        public string StatusMessage
        {
            get => statusMessage;
            set => SetProperty(ref statusMessage, value);
        }
        #endregion

        private void Initialize()
        {
            isExecuting = false;
            isExecutingLock = new object();
            mediaPlayer = new MediaPlayer();
            StopTripCommand = new RelayCommand(StopTrip, CanStopTrip);
            CloseModalCommand = new RelayCommand(CloseModal);
            ActiveStudents = new ObservableCollection<StudentModel>();
            CurrentStudent = new StudentModel();
            Status = TripStatus.TRIP_READY;
            StatusMessage = TripStatusConsts.TRIP_READY;
            UpdateCurrentLocationWorker();
            WeakReferenceMessenger.Default.Register(this);
        }

        #region "Stop Trip Command"
        private bool CanStopTrip()
        {
            return App.Current.BusDriverSession.IsTripStarted;
        }

        private async void StopTrip()
        {
            if (ActiveStudents.Count > 0)
            {
                ShowWarningModal = true;
                mediaPlayer.Open(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Sounds", "CloseTripDenied.mp3")));
                mediaPlayer.Play();
            }
            else
            {
                HttpResponseModel<string> stopTripResult = await tripService.StopBusTrip(App.Current.BusDriverSession.Bus.ID, App.Current.BusDriverSession.AuthToken);

                if (stopTripResult != null && stopTripResult.IsSuccess)
                {
                    App.Current.BusDriverSession.IsTripStarted = false;
                    navigationService.Navigate<HomeViewModel>();
                }
            }
        }
        #endregion

        #region "Close WarningModal Command"
        private void CloseModal()
        {
            ShowWarningModal = false;
        }
        #endregion

        #region "Messaging"
        public async void Receive(FaceRecognitionMessage message)
        {
            lock (isExecutingLock)
            {
                if (isExecuting)
                {
                    return;
                }
                isExecuting = true;
            }
            try
            {
                Status = TripStatus.TRIP_IN_PROGRESS;
                StatusMessage = TripStatusConsts.TRIP_IN_PROGRESS;
                int busID = App.Current.BusDriverSession.Bus.ID;
                string authToken = App.Current.BusDriverSession.AuthToken;
                await AddStudentToTrip(busID, message.Value.ID, authToken);
                await Task.Delay(2000);
            }
            catch (Exception ex)
            {
                App.Current.LoggerService.Log(LogLevel.Error, "TripViewModel", ex.Message);
            }
            finally
            {
                lock (isExecutingLock)
                {
                    CurrentStudent = new();
                    Status = TripStatus.TRIP_READY;
                    StatusMessage = TripStatusConsts.TRIP_READY;
                    isExecuting = false;
                }
            }
        }

        private async Task AddStudentToTrip(int busID, int studentID, string authToken)
        {
            HttpResponseModel<StudentDto> getStudentResult = await studentService.GetStudent(studentID, authToken);

            if (getStudentResult == null || !getStudentResult.IsSuccess)
            {
                Status = TripStatus.STUDENT_UNKNOWN;
                StatusMessage = TripStatusConsts.STUDENT_UNKNOWN;
                await Task.Delay(1000);
                return;
            }

            StudentModel student = mapper.Map<StudentModel>(getStudentResult.Response);

            if (student.BelongsToBusID != busID)
            {
                Status = TripStatus.STUDENT_INVALID_BUS;
                StatusMessage = string.Format(TripStatusConsts.STUDENT_INVALID_BUS, student.BelongsToBusID);
                await Task.Delay(1000);
                return;
            }

            if (ActiveStudents.FirstOrDefault(s => s.ID == student.ID) is StudentModel existingStudent)
            {
                TimeSpan timeDifference = DateTime.Now - existingStudent.LastScanned;
                if (timeDifference.TotalMinutes >= 1)
                {
                    await RemoveStudentFromTrip(busID, existingStudent, authToken);
                    return;
                }
                Status = TripStatus.STUDENT_COOLDOWN;
                StatusMessage = string.Format(TripStatusConsts.STUDENT_COOLDOWN, TimeSpan.FromMinutes(1) - timeDifference);
                mediaPlayer.Open(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Sounds", "StudentCooldown.mp3")));
                mediaPlayer.Play();
                await Task.Delay(1000);
                return;
            }

            HttpResponseModel<string> updateStudentResult = await tripService.UpdateStudentStatusOnBus(student.ID, busID, DateTime.Now, authToken);

            if (updateStudentResult == null || !updateStudentResult.IsSuccess)
            {
                Status = TripStatus.STUDENT_UPDATE_ERROR;
                StatusMessage = TripStatusConsts.STUDENT_UPDATE_ERROR;
                await Task.Delay(1000);
                return;
            }

            CurrentStudent = mapper.Map<StudentModel>(student);
            CurrentStudent.LastScanned = DateTime.Now;
            ActiveStudents.Add(CurrentStudent);

            Status = TripStatus.STUDENT_ADDED;
            StatusMessage = TripStatusConsts.STUDENT_ADDED;
        }

        private async Task RemoveStudentFromTrip(int busID, StudentModel existingStudent, string authToken)
        {
            HttpResponseModel<string> updateStudentResult = await tripService.UpdateStudentStatusOffBus(existingStudent.ID, busID, DateTime.Now, authToken);

            if (updateStudentResult == null || !updateStudentResult.IsSuccess)
            {
                Status = TripStatus.STUDENT_UPDATE_ERROR;
                StatusMessage = TripStatusConsts.STUDENT_UPDATE_ERROR;
                await Task.Delay(1000);
                return;
            }

            CurrentStudent = existingStudent;
            ActiveStudents.Remove(existingStudent);

            Status = TripStatus.STUDENT_REMOVED;
            StatusMessage = TripStatusConsts.STUDENT_REMOVED;
        }
        #endregion

        #region "Live Map Update"
        private async void UpdateCurrentLocationWorker()
        {
            await Task.Run(async ()  => 
            {
                while (App.Current != null && App.Current.BusDriverSession.IsTripStarted)
                {
                    GeoModel response = await geoLocationService.GetCurrentLocation();

                    if (response != null)
                    {
                        int busID = App.Current.BusDriverSession.Bus.ID;
                        string authToken = App.Current.BusDriverSession.AuthToken;
                        string currentLocation = string.Format("{0}|{1}", response.Longitude, response.Latitude);

                        HttpResponseModel<string> updateCurrentLocationResult = await tripService.UpdateBusLocation(busID, currentLocation, authToken);

                        if (updateCurrentLocationResult != null && !updateCurrentLocationResult.IsSuccess)
                        {
                            App.Current.LoggerService.Log(LogLevel.Error, "TripViewModel", updateCurrentLocationResult.ProblemDetails.Detail);
                        }
                        else
                        {
                            App.Current.BusDriverSession.Bus.CurrentLocation = currentLocation;
                            App.Current.LoggerService.Log(LogLevel.Info, "TripViewModel", string.Format("Successfully updated the current bus location [{0}]", currentLocation));
                        }
                    }
                    await Task.Delay(3000);
                }
            });
        }
        #endregion
    }
}