using System.ComponentModel.DataAnnotations;

namespace Dtos
{
    public class MessageInDto
    {
        [MaxLength(128)]
        [Required]
        public string Content { get; set; }
        [Required]
        public int SerialNumber { get; set; }
    }
}
