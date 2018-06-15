namespace DotNetFlicks.Common.Models
{
    public class EmailConfiguration
    {
        public string MailServer { get; set; }

        public int MailServerPort { get; set; }

        public string MailServerUsername { get; set; }

        public string MailServerPassword { get; set; }

        public string SenderName { get; set; }

        public string SenderEmail { get; set; }

        public bool EnableSsl { get; set; }
    }
}
