using System.Windows.Media;
using SmartBusWPF.Common.Enums;
using System.Collections.Generic;

namespace SmartBusWPF.Common.Consts
{
    public static class TripStatusConsts
    {
        public const string TRIP_READY = "Ready for facial recognition.";
        public const string TRIP_IN_PROGRESS = "Facial recognition in progress.";
        public const string STUDENT_ADDED = "Successfully added student to trip.";
        public const string STUDENT_REMOVED = "Successfully removed student from trip.";
        public const string STUDENT_UNKNOWN = "Failed to recognize student face.";
        public const string STUDENT_COOLDOWN = "Failed to remove student from trip. Please try again after {0:mm\\:ss}.";
        public const string STUDENT_UPDATE_ERROR = "Failed to update student status.";
        public const string STUDENT_INVALID_BUS = "Student does not belong to this bus. The student should be on bus ID {0}.";

        public static readonly Dictionary<TripStatus, SolidColorBrush> BackgroundColors = new()
        {
            { TripStatus.TRIP_READY, new SolidColorBrush(Colors.Green) },
            { TripStatus.TRIP_IN_PROGRESS, new SolidColorBrush(Colors.Yellow) },
            { TripStatus.STUDENT_ADDED, new SolidColorBrush(Colors.Blue) },
            { TripStatus.STUDENT_REMOVED, new SolidColorBrush(Colors.Red) },
            { TripStatus.STUDENT_UNKNOWN, new SolidColorBrush(Colors.Orange) },
            { TripStatus.STUDENT_COOLDOWN, new SolidColorBrush(Colors.Gray) },
            { TripStatus.STUDENT_UPDATE_ERROR, new SolidColorBrush(Colors.Red) },
            { TripStatus.STUDENT_INVALID_BUS, new SolidColorBrush(Colors.Red) }
        };

        public static readonly Dictionary<TripStatus, SolidColorBrush> ForegroundColors = new()
        {
            { TripStatus.TRIP_READY, new SolidColorBrush(Colors.White) },
            { TripStatus.TRIP_IN_PROGRESS, new SolidColorBrush(Colors.Black) },
            { TripStatus.STUDENT_ADDED, new SolidColorBrush(Colors.White) },
            { TripStatus.STUDENT_REMOVED, new SolidColorBrush(Colors.White) },
            { TripStatus.STUDENT_UNKNOWN, new SolidColorBrush(Colors.Black) },
            { TripStatus.STUDENT_COOLDOWN, new SolidColorBrush(Colors.Black) },
            { TripStatus.STUDENT_UPDATE_ERROR, new SolidColorBrush(Colors.White) },
            { TripStatus.STUDENT_INVALID_BUS, new SolidColorBrush(Colors.White) }
        };
    }
}