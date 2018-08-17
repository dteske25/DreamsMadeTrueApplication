using System.Collections.Generic;

namespace DreamsMadeTrue.Engines.Client.Dtos
{
    public class EmailDto
    {
        public string Subject { get; set; }
        public string HtmlContent { get; set; }
        public string TextContent { get; set; }
        public string FromAddress { get; set; } = "no-reply@dreams-made-true.org";
        public IEnumerable<string> ToAddresses { get; set; }
        public IEnumerable<string> CcAddresses { get; set; }
        public IEnumerable<string> BccAddresses { get; set; }
    }
}
