using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Infrastructure
{
    public class JwtSettings
    {
        public string JwtKey { get; set; }
        public TimeSpan LifeTime { get; set; }
    }
}
