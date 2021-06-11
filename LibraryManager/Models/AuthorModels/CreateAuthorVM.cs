using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models.AuthorModels
{
    public class CreateAuthorVM
    {
        [Required]
        [RegularExpression("^[a-zA-Z]+((['][a-zA-Z])?[a-zA-Z]*)*$")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z]+((['][a-zA-Z])?[a-zA-Z]*)*$")]
        public string LastName { get; set; }
        
    }
}