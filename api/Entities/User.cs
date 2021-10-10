using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace api.Entities
{
    public class User
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string password { get; set; }
        public Collection<Account> accounts { get; set; }
    }
}