namespace Entities.Configuration
{
    public interface IEmailConfiguration
    {
        int SmptPort { get; set; }
        string SmtpPassword { get; set; }
        string SmtpServer { get; set; }
        string SmtpUsername { get; set; }
        bool UseSSL { get; set; }
    }
}