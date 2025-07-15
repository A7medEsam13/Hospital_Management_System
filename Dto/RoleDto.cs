using System.ComponentModel.DataAnnotations;

namespace Hospital_Management_System.Dto
{
    public class RoleDto
    {
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;
    }
}
