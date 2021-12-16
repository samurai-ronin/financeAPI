using System.Collections.ObjectModel;

namespace api.Dtos
{
    public class UserOutputModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public Collection<AccountOutputModel> accounts { get; set; }
    }
}