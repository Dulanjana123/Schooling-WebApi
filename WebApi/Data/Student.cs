namespace WebApi.Data
{
    public class Student
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string NIC { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; }
        public byte[]? ProfileImage { get; set; } //Store Image as BLOB
    }
}
