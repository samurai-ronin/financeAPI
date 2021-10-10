using System.ComponentModel.DataAnnotations;

namespace api.Entities
{
    public class Account
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public decimal amount { get; set; }
        public int UserId { get; set; }
    }
}