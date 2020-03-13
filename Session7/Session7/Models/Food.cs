using System.ComponentModel.DataAnnotations;

namespace Session7.Models
{
    public class Food
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

    }
}
