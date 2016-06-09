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
        [DataType(DataType.EmailAddress)]
        public string SmtpSenderAddress { get; set; }

        [Range(1, 65535, ErrorMessage = "Port number must be between 1 and 65535")]
        public int? SmtpServerPort { get; set; }

        public bool EnableSsl { get; set; }

        public bool AuthenticationRequried { get; set; }

        public string SmtpUsername { get; set; }

        [DataType(DataType.Password)]
        public string SmtpPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AuthenticationRequried && (string.IsNullOrEmpty(SmtpUsername) | string.IsNullOrEmpty(SmtpPassword)))
                yield return new ValidationResult("SMTP Username and Password is required if authentication is enabled");
        }
    }
}