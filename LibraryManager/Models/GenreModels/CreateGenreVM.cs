using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Models.GenreModels
{
    public class CreateGenreVM
    {
        [Required] [DataType(DataType.Text)] public string Name { get; set; }
    }
}