using System.ComponentModel.DataAnnotations;

namespace api.Entities
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Balance { get; set; }
        public int UserId { get; set; }
    }
}