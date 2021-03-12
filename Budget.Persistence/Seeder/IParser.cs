using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Persistence.Seeder
{
    public interface IParser
    {
        public IList<T> Parse<T>(string path);
    }
}
