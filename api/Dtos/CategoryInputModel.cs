using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public class CategoryInputModel
    {
        [Required]
        public string Name{ get; set; }
    }
}