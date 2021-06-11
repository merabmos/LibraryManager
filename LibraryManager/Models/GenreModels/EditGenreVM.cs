using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models.GenreModels
{
    public class EditGenreVM
    {
        public int Id { get; set; }
        [Required] [DataType(DataType.Text)] public string Name { get; set; }
    }
}