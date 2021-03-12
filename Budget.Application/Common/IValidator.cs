using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Application.Common
{
    public interface IValidator<T>
    {
        public void Validate(T t);
    }
}
