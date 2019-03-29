using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CineBase
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Database.SetDb(@"Data Source=UZIVATEL-PC\SqlExpress; Initial Catalog=CineBaseDb; Integrated Security=True; MultipleActiveResultSets=true");    
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .Build();
    }
}
