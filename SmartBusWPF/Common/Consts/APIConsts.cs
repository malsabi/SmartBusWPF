namespace SmartBusWPF.Common.Consts
{
    public static class APIConsts
    {
        public const string LocalAPIEndPoint = "https://localhost:7145";

        public const string ProductionAPIEndPoint = "https://localhost:7145";

        public const string NotificationHubEndPoint = LocalAPIEndPoint + "/notification_hub";

        public const string Scheme = "Bearer";

        public static class Auth
        {
            public const string LoginParent = "/api/Auth/login/parent";
            public const string LoginBusDriver = "/api/Auth/login/bus-driver";
            public const string RegisterParent = "/api/Auth/register/parent";
            public const string RegisterStudent = "/api/Auth/register/student";
            public const string RegisterBusDriver = "/api/Auth/register/bus-driver";
        }
    }
}