namespace SmartBusWPF.Common.Consts
{
    public static class APIConsts
    {
        public const string LocalAPIEndPoint = "https://smartbus-api.azurewebsites.net/";

        public const string ProductionAPIEndPoint = "https://smartbus-api.azurewebsites.net/";

        public const string Scheme = "Bearer";

        public static class Health
        {
            public const string GetHealth = "/api/Health";
        }

        public static class Auth
        {
            public const string LoginParent = "/api/Auth/login/parent";
            public const string LoginBusDriver = "/api/Auth/login/bus-driver";
            public const string RegisterParent = "/api/Auth/register/parent";
            public const string RegisterStudent = "/api/Auth/register/student";
            public const string RegisterBusDriver = "/api/Auth/register/bus-driver";
        }

        public static class Student
        {
            public const string GetStudent = "/api/Student/{0}";
            public const string GetByFaceRecognitionId = "/api/Student/faceRecognition/{0}";
        }

        public static class Trip
        {
            public const string StartBusTrip = "/api/Trip/start-bus-trip";
            public const string StopBusTrip = "/api/Trip/stop-bus-trip";
            public const string UpdateBusLocation = "/api/Trip/update-bus-location";
            public const string UpdateStudentStatusOnBus = "/api/Trip/update-student-status-on-bus";
            public const string UpdateStudentStatusOffBus = "/api/Trip/update-student-status-off-bus";
        }
    }
}