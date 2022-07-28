using Sample_MachineTest.DatabaseConnection.DatabaseClasses;

namespace Sample_MachineTest.Models
{
    public class EmployeeViewModel
    {
        public string? LoginName { get; set; }
        public string? FullName { get; set; }
        public int Age { get; set; }
        public int DepartmentId { get; set; }
        public List<Employee>? EmployeeData { get; set; }
    }
}
