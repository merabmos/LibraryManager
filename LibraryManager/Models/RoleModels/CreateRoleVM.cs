using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models.RoleModels
{
    public class CreateRoleVM
    {
        [Required]
        [Display(Name = "Role")]
        public string Name { get; set; }
    }
}