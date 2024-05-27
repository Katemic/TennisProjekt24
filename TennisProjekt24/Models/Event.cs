using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TennisProjekt24.Models
{
    public class Event
    {
        [Required(ErrorMessage = "Vælg dato")]
        [FutureDate(ErrorMessage = "Dato må ikke ligge før nuværende tidspunkt")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Tilføj titel")]
        [StringLength(50, ErrorMessage = "Titel må max være 50 karakterer")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Tilføj beskrivelse")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Tilføj Afholdelsessted")]
        [StringLength(50, ErrorMessage = "Lokation må max være 50 karakterer")]
        public string Place { get; set; }
        public Member Member { get; set; }
        public int EventId { get; set; }
        public string? Image { get; set; }
        public class FutureDateAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                // Check if value is null
                if (value == null)
                {
                    return ValidationResult.Success; // Required attribute will handle null case
                }
                //var date = (DateTime)value;

                if (value is DateTime date)
                {
                    // Perform the date comparison
                    if (date.Date < DateTime.Now.Date)
                    {
                        return new ValidationResult(ErrorMessage);
                    }

                    return ValidationResult.Success;
                }
                return new ValidationResult("Invalid date format");
            }
        }

    public Event()
        {
            
        }

        public Event(int eventId, DateTime date, string title, string description, string place, Member member, string? image)
        {
            EventId = eventId;
            Date = date;
            Title = title;
            Description = description;
            Place = place;
            Member = member;
            Image = image;
        }
    }
}
