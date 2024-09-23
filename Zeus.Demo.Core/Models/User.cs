using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Zeus.Demo.Core.Models
{
    public class User : IdentityUser
    {
        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<Order> Orders { get; set; } = [];
    }
}
