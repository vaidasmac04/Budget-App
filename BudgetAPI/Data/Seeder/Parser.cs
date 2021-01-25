using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAPI.Data.Seeder
{
    public class Parser
    {
        private IParser _parser;
        public Parser(IParser parser)
        {
            _parser = parser;
        }

        public IList<T> Parse<T>(string path)
        {
            return _parser.Parse<T>(path);
        }
    }
}
