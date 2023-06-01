using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace Zadaniator.Models
{
    public class Task1Model
    {
        public int Id { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Opis")]

        public string Description { get; set; }

        public DateTime Data { get; set; }

        [DisplayName("Czy zakończone")]
        public bool IsCompleted { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
