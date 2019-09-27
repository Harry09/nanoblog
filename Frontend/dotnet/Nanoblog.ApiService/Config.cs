using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.ApiService
{
    public static class Config
    {
        public static readonly string ServerIp = "192.168.0.161";
        public static readonly ushort ServerPort = 53188;

        public static string Address => $"http://{ServerIp}:{ServerPort}";
    }
}
