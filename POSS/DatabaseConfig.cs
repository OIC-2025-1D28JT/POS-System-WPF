using System;
using System.IO;

namespace POSS
{
    internal static class DatabaseConfig
    {
        private static readonly string DatabasePath =
            Path.Combine(AppContext.BaseDirectory, "Database1.mdf");

        public static string ConnectionString =>
            $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={DatabasePath};Integrated Security=True";
    }
}