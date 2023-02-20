using SmartBusWPF.Models.HuskyLens;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace SmartBusWPF.Messages
{
    public class FaceRecognitionMessage : ValueChangedMessage<FaceRecognitonModel>
    {
        public FaceRecognitionMessage(FaceRecognitonModel faceRecognitonModel) : base(faceRecognitonModel)
        {
        }
    }
}