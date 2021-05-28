using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models.RoleModels
{
    public class EditRoleVM
    {
        public string Id { get; set; }
        
        [Required]
        [Display(Name = "Role")]
        public string Name { get; set; }          
    }
}