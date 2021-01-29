using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetAPI.Mapping
{
    public interface IEntityMapper<I,O>
    {
        O Map(I i);
        List<O> Map(List<I> i);
        I MapBack(O o);
        List<I> MapBack(List<O> o);
    }
}
