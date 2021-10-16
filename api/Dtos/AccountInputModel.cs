using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public class AccountInputModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        public decimal Balance { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}