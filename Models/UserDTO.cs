using Geotik.Entities;
using System.ComponentModel.DataAnnotations;

namespace Geotik.Models
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Name field cannot be empty")]
        [MaxLength(50)]
        public string Name { get; set; }

        public int AccountId { get; set; }

        public UserDTO()
        { }

        public UserDTO(User user)
        {
            Id = user.Id;
            Name = user.Name;
            AccountId = user.Account.Id;
        }
    }
}
