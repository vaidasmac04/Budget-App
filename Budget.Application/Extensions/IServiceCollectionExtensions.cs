using Budget.Application.Common;
using Budget.Application.Incomes;
using Budget.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Application.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IValidator<IncomeDTO>, IncomeDTOValidator>();
            services.AddTransient<IIncomeAdder, IncomeAdder>();
            services.AddTransient<IIncomeUpdater, IncomeUpdater>();
            services.AddTransient<ISourceResolver, SourceResolver>();
            services.AddTransient<IClientSourceResolver, ClientSourceResolver>();

            return services;
        }
    }
}
