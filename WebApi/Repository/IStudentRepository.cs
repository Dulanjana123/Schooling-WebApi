using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repository
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAll();
        IQueryable<Student> GetQueryable();
        Task<Student> GetById(int id);
        Task Add(Student student);
        Task Update(Student student);
        Task Delete(Student student);
        Task<bool> StudentExists(string nic, string mobile, string email, int? excludeStudentId = null);
    }
}
