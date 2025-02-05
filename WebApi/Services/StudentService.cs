using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task Add(CreateStudentDto studentDto)
        {
            var studentEntity = _mapper.Map<Student>(studentDto);

            if (studentDto.Image != null)
            {
                using var memoryStream = new MemoryStream();
                await studentDto.Image.CopyToAsync(memoryStream);
                studentEntity.ProfileImage = memoryStream.ToArray();
            }

            await _studentRepository.Add(studentEntity);
        }

        public async Task Delete(int id)
        {
            var existingStudent = await _studentRepository.GetById(id);

            if (existingStudent == null)
            {
                throw new Exception("Student not found");
            }

            await _studentRepository.Delete(existingStudent);
        }


        public async Task<PagedResult<StudentDto>> GetAll(int page, int pageSize, string filter, string sortBy, string sortDirection)
        {
            var query = _studentRepository.GetQueryable(); 

            //Filtering
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(student => student.FirstName.Contains(filter) 
                                    || student.Email.Contains(filter) 
                                    || student.Mobile.Contains(filter)
                                    || student.LastName.Contains(filter)
                                    || student.NIC.Contains(filter));
            }

            //Sorting
            switch (sortBy)
            {
                case "firstName":
                    query = sortDirection.ToLower() == "desc" ? query.OrderByDescending(s => s.FirstName) : query.OrderBy(s => s.FirstName);
                    break;
                case "lastName":
                    query = sortDirection.ToLower() == "desc" ? query.OrderByDescending(s => s.LastName) : query.OrderBy(s => s.LastName);
                    break;
                case "mobile":
                    query = sortDirection.ToLower() == "desc" ? query.OrderByDescending(s => s.Mobile) : query.OrderBy(s => s.Mobile);
                    break;
                case "email":
                    query = sortDirection.ToLower() == "desc" ? query.OrderByDescending(s => s.Email) : query.OrderBy(s => s.Email);
                    break;
                case "nic":
                    query = sortDirection.ToLower() == "desc" ? query.OrderByDescending(s => s.NIC) : query.OrderBy(s => s.NIC);
                    break;
                default:
                    query = query.OrderBy(s => s.FirstName); // Default sorting
                    break;
            }

            //Pagination
            var totalCount = await query.CountAsync();
            var students = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var studentDtos = _mapper.Map<List<StudentDto>>(students);

            for (int i = 0; i < studentDtos.Count; i++)
            {
                if (students[i].ProfileImage != null)
                {
                    studentDtos[i].ProfileImageBase64 = Convert.ToBase64String(students[i].ProfileImage);
                }
            }

            return new PagedResult<StudentDto>
            {
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
                CurrentPage = page,
                PageSize = pageSize,
                Data = studentDtos
            };
        }

        public async Task<StudentDto> GetById(int id)
        {
            var student = await _studentRepository.GetById(id);
            if (student == null)
            {
                return null;
            }
            var studentDto = _mapper.Map<StudentDto>(student);
            return studentDto;
        }

        public async Task Update(UpdateStudentDto studentDto)
        {
            var existingStudent = await _studentRepository.GetById(studentDto.Id);
            if (existingStudent == null)
            {
                throw new Exception("Student not found");
            }

            var studentEntity = _mapper.Map(studentDto, existingStudent);

            if (studentDto.Image != null)
            {
                using var memoryStream = new MemoryStream();
                await studentDto.Image.CopyToAsync(memoryStream);
                existingStudent.ProfileImage = memoryStream.ToArray();
            }

            await _studentRepository.Update(studentEntity);
        }

        public async Task<bool> StudentExists(CreateStudentDto studentDto)
        {
            var result = await _studentRepository.StudentExists(studentDto.NIC, studentDto.Mobile, studentDto.Email);

            return result;
        }

        public async Task<bool> StudentExistsInUpdate(UpdateStudentDto studentDto)
        {
            var result = await _studentRepository.StudentExists(studentDto.NIC, studentDto.Mobile, studentDto.Email, studentDto.Id);

            return result;
        }

    }
}
