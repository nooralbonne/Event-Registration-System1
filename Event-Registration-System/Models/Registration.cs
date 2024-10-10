using System.ComponentModel.DataAnnotations;

namespace Event_Registration_System.Models
{
    public class Registration
    {
        public int RegistrationId { get; set; }

        [Required(ErrorMessage = "Participant name is required.")]
        public string ParticipantName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
        public string Email { get; set; }

        [Required]
        public int EventId { get; set; }
        public Event Event { get; set; }
    }

}
