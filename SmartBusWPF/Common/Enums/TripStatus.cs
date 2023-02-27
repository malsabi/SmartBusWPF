namespace SmartBusWPF.Common.Enums
{
    public enum TripStatus : int
    {
        TRIP_READY,
        TRIP_IN_PROGRESS,
        STUDENT_ADDED,
        STUDENT_REMOVED,
        STUDENT_UNKNOWN,
        STUDENT_COOLDOWN,
        STUDENT_UPDATE_ERROR,
        STUDENT_INVALID_BUS
    }
}