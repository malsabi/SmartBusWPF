using SmartBusWPF.Messages;
using SmartBusWPF.Models.Student;
using CommunityToolkit.Mvvm.Input;
using SmartBusWPF.Models.HuskyLens;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.ViewModels
{
    public class TripViewModel : BaseViewModel, IRecipient<FaceRecognitionMessage>
    {
        private readonly ILoggerService loggerService;
        private readonly INavigationService navigationService;

        public TripViewModel(ILoggerService loggerService,
                            INavigationService navigationService) : base(navigationService)
        {
            this.loggerService = loggerService;
            this.navigationService = navigationService;
            Initialize();
        }

        private int totalStudents;
        public int TotalStudents
        {
            get => totalStudents;
            set => SetProperty(ref totalStudents, value);
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

        private string gradeLevel;
        public string GradeLevel
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

        public ObservableCollection<string> ActiveStudents { get; private set; }
        public IRelayCommand StartTripCommand { get; private set; }

        private void Initialize()
        {
            ActiveStudents = new ObservableCollection<string>();
            StartTripCommand = new RelayCommand(StartTrip, CanStartTrip);
            TotalStudents = 0;
            FullName = "Full Name";
            Address = "Address";
            GradeLevel = "Grade Level";
            Status = "No student found";
            WeakReferenceMessenger.Default.Register(this);
        }

        private void StartTrip()
        {
        }

        private bool CanStartTrip()
        {
            return ActiveStudents != null && ActiveStudents.Count > 0;
        }


        public void Receive(FaceRecognitionMessage message)
        {
            FaceRecognitonModel model = message.Value;


        }
    }
}