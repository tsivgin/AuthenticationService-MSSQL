using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication.Core.DTOs
{
    public class ClientLoginDto
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}