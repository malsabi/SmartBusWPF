using CommunityToolkit.Mvvm.ComponentModel;

namespace SmartBusWPF.Models
{
    public class StudentModel : ObservableObject
    {
        private string image;
        public string Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }

        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
    }
}