using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public class UserInputModel
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string password { get; set; }
    }
}