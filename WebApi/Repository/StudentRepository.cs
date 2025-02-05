using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SMDbContext _dbContext;

        public StudentRepository(SMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Student student)
        {
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();

        }
        public async Task Delete(Student student)
        {
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Student>> GetAll()
        {
            var students = await _dbContext.Students.ToListAsync();
            return students;
        }

        public async Task<Student> GetById(int id)
        {
            var student = await _dbContext.Students.FindAsync(id);
            if (student == null)
            {
                return null;
            }
            return student;
        }

        public async Task<bool> StudentExists(string nic, string mobile, string email, int? excludeStudentId = null)
        {
            //return await _dbContext.Students
            //    .AnyAsync(s => s.NIC == nic
            //                || s.Mobile == mobile
            //                || s.Email == email);

            return await _dbContext.Students
                    .AnyAsync(s =>(s.NIC == nic 
                            || s.Mobile == mobile 
                            || s.Email == email) 
                            &&
                            (!excludeStudentId.HasValue || s.Id != excludeStudentId));
        }


        public IQueryable<Student> GetQueryable()
        {
            return _dbContext.Students.AsQueryable(); 
        }

        public async Task Update(Student student)
        {
            _dbContext.Students.Update(student);
            await _dbContext.SaveChangesAsync();
        }
    }
}
