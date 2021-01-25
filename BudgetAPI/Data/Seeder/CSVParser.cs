using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAPI.Data.Seeder
{
    public class CSVParser : IParser
    {
        public IList<T> Parse<T>(string path)
        {
            using var streamReader = File.OpenText(path);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                IgnoreReferences = true
            };

            using var csvReader = new CsvReader(streamReader, config);
            IList<T> parsed = csvReader.GetRecords<T>().ToList();
            return parsed;
        }
    }
}
