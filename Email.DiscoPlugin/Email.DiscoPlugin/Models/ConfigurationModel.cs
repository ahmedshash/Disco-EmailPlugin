using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Email.DiscoPlugin.Models
{
    public class ConfigurationModel : IValidatableObject
    {
        //Alerts
        public bool DeviceReadyAlert { get; set; }

        //Email Server Configuration Details
        [Required(ErrorMessage = "SMTP Email Server address is required")]
        public string SmtpServerAddress { get; set; }

        [Required(ErrorMessage = "Sender Address is required")]
        public string SmtpSenderAddress { get; set; }

        public bool AuthenticationRequried { get; set; }

        public string SmtpUsername { get; set; }

        [DataType(DataType.Password)]
        public string SmtpPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AuthenticationRequried && (string.IsNullOrEmpty(SmtpUsername) | string.IsNullOrEmpty(SmtpPassword)))
                yield return new ValidationResult("SMTP Username and Password is required if authntication is ticked");
        }
    }
}