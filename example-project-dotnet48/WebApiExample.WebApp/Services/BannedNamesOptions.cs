using System.Collections.Generic;

namespace WebApiExample.WebApp.Services
{
    public class BannedNamesOptions
    {
        public IDictionary<string, string> BannedNames { get; set; } = new Dictionary<string, string>();
    }
}