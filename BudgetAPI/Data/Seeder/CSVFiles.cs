using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BudgetAPI.Data.Seeder
{
    public static class CSVFiles
    {
        public static string PATH = Path
            .GetDirectoryName(Assembly
                .GetExecutingAssembly().Location) + @"\Data\Seeder\";

        public static string CLIENTS = PATH + "clients.csv";
        public static string CLIENT_CATEGORIES = PATH + "client_categories.csv";
        public static string CLIENT_ITEMS = PATH + "client_items.csv";
        public static string CLIENT_SOURCES = PATH + "client_sources.csv";
        public static string INCOMES = PATH + "incomes.csv";
        public static string ITEMS = PATH + "items.csv";
        public static string OUTCOMES = PATH + "outcomes.csv";
        public static string SOURCES = PATH + "sources.csv";
        public static string CATEGORIES = PATH + "categories.csv";
    }
}
