using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Email.DiscoPlugin.Models
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType().GetMember(enumValue.ToString()).First().GetCustomAttribute<DisplayAttribute>().GetName();
        }
    }

    public class ConfigurationModel : IValidatableObject
    {
        public int CurrentVersion { get; set; }

        //Email Server Configuration Details
        [DisplayName("SMTP Server Address")]
        [Required(ErrorMessage = "SMTP Email Server address is required")]
        public string SmtpServerAddress { get; set; }

        [DisplayName("SMTP Sender Address")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Sender Address is required")]
        public string SmtpSenderAddress { get; set; }

        [DisplayName("SMTP Server Port")]
        [Range(1, 65535, ErrorMessage = "Port number must be between 1 and 65535")]
        public int? SmtpServerPort { get; set; }

        [DisplayName("SSL Enabled")]
        public bool EnableSsl { get; set; }

        [DisplayName("Authentication Required")]
        public bool AuthenticationRequried { get; set; }

        [DisplayName("SMTP Username")]
        public string SmtpUsername { get; set; }

        [DisplayName("SMTP Password")]
        [DataType(DataType.Password)]
        public string SmtpPassword { get; set; }

        public List<MessageConfig> MessageConfig { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AuthenticationRequried && (string.IsNullOrEmpty(SmtpUsername) | string.IsNullOrEmpty(SmtpPassword)))
                yield return new ValidationResult("SMTP Username and Password is required if authentication is enabled");
        }
    }

    public class MessageConfig
    {
        [DisplayName("Message Type")]
        public MessageType EmailMessageType { get; set; }

        [DisplayName("Email Subject Line")]
        public string EmailSubject { get; set; }

        [DisplayName("Email Body Text")]
        [DataType(DataType.MultilineText)]
        public string EmailBody { get; set; }

        [DisplayName("Email Alert Enabled")]
        public bool EmailAlertEnabled { get; set; }
    }

    public enum MessageType
    {
        [Display(Name = "Plugin Test Email")]
        PluginTestEmail = 1,

        [Display(Name = "Device Ready for Collection")]
        DeviceReadyForCollection = 2
    }
}