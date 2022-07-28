using System.ComponentModel.DataAnnotations;

namespace Sample_MachineTest.Models
{
    public class RegisterViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public string? FullName { get; set; }
    }
}
