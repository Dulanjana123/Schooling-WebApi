using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IStudentService
    {
        Task<PagedResult<StudentDto>> GetAll(int page, int pageSize, string filter, string sortBy, string sortDirection);
        Task<StudentDto> GetById(int id);
        Task Add(CreateStudentDto student);
        Task Update(UpdateStudentDto student);
        Task Delete(int id);

        Task<bool> StudentExists(CreateStudentDto studentDto);
        Task<bool> StudentExistsInUpdate(UpdateStudentDto studentDto);
    }
}
