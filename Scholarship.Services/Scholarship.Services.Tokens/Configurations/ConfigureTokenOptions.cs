using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholarship.Service.Tokens.Configurations
{
    public class ConfigureTokenOptions : IConfigureNamedOptions<TokenOptions>
    {
        private readonly IConfiguration configuration = default!;
        private readonly string sectionName = default!;
        public ConfigureTokenOptions(IConfiguration configuration, string section) : base() 
        {
            this.configuration = configuration;
            this.sectionName = section;
        }
        public void Configure(string? name, TokenOptions options) => this.Configure(options);
        public void Configure(TokenOptions options)
        {
            var section = this.configuration.GetSection(sectionName);
            if (section["AccessSecret"] != null) options.AccessSecret = section["AccessSecret"]!;
            if (section["RefreshSecret"] != null) options.RefreshSecret = section["RefreshSecret"]!;
            
            if (section["AccessExpires"] != null) options.AccessExpires = int.Parse(section["AccessExpires"]!);
            if (section["RefreshExpires"] != null) options.RefreshExpires = int.Parse(section["RefreshExpires"]!);
        }
    }
    public class TokenOptions : object
    {
        public const int AccessExpiresDefault = 5, RefreshExpiresDefault = 20;

        public string AccessSecret { get; set; } = Guid.NewGuid().ToString();
        public string RefreshSecret { get; set; } = Guid.NewGuid().ToString();

        public int AccessExpires { get; set; } = TokenOptions.AccessExpiresDefault;
        public int RefreshExpires { get; set; } = TokenOptions.RefreshExpiresDefault;
    }
}
