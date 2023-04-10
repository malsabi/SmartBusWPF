using System.Threading.Tasks;
using SmartBusWPF.Models.API;
using SmartBusWPF.DTOs.Student;

namespace SmartBusWPF.Common.Interfaces.Services.API
{
    public interface IStudentService
    {
        public Task<HttpResponseModel<StudentDto>> GetStudentByFaceRecognitionID(int id, string authToken);
    }
}