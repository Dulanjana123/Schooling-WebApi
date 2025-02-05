namespace WebApi.Data
{
    public class DbSeeder
    {
        public static void Seed(SMDbContext context)
        {
            
            if (!context.Students.Any()) // check if there any students in the table
            {
                var students = new List<Student>
                {
                    new Student { FirstName = "John", LastName = "Doe", Mobile = "0711111111", Email = "john.doe@example.com", NIC = "921111111V", DateOfBirth = new DateOnly(1995, 5, 20), Address = "Colombo, Sri Lanka", Active = true },
                    new Student { FirstName = "Alice", LastName = "Smith", Mobile = "0722222222", Email = "alice.smith@example.com", NIC = "921222222V", DateOfBirth = new DateOnly(1997, 3, 14), Address = "Kandy, Sri Lanka", Active = true },
                    new Student { FirstName = "Bob", LastName = "Johnson", Mobile = "0733333333", Email = "bob.johnson@example.com", NIC = "921333333V", DateOfBirth = new DateOnly(1998, 8, 5), Address = "Galle, Sri Lanka", Active = true },
                    new Student { FirstName = "Emma", LastName = "Brown", Mobile = "0744444444", Email = "emma.brown@example.com", NIC = "921444444V", DateOfBirth = new DateOnly(2000, 12, 1), Address = "Jaffna, Sri Lanka", Active = true },
                    new Student { FirstName = "Daniel", LastName = "Wilson", Mobile = "0755555555", Email = "daniel.wilson@example.com", NIC = "921555555V", DateOfBirth = new DateOnly(1996, 6, 18), Address = "Kurunegala, Sri Lanka", Active = true }
                };

                context.Students.AddRange(students);
                context.SaveChanges();
            }

            
        }
    }

}
