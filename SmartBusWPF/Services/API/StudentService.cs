using System.Threading.Tasks;
using SmartBusWPF.Models.API;
using SmartBusWPF.DTOs.Student;
using SmartBusWPF.Common.Consts;
using SmartBusWPF.Common.Interfaces.Services.API;

namespace SmartBusWPF.Services.API
{
    public class StudentService : IStudentService
    {
        private readonly IHttpClientService httpClientService;

        public StudentService(IHttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public async Task<HttpResponseModel<StudentDto>> GetStudent(int studentID, string authToken)
        {
            HttpResponseModel<StudentDto> result = await httpClientService.GetAsync<StudentDto>(APIConsts.Student.GetStudent, authToken, studentID.ToString());
            return result;
        }
    }
}