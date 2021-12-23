using Moonpig.PostOffice.Core.Interfaces;
using Moonpig.PostOffice.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moonpig.PostOffice.Core.Interactors
{
    public class DespatchDateInteractor : IDespatchDateInteractor
    {
        private readonly IDbContext _dbContext;

        public DespatchDateInteractor(IDbContext dbContext)
            => _dbContext = dbContext;

        public DateTime CalculateDespatchDate(List<int> productIds, DateTime orderDate)
        {
            var leadTimeDays = GetMaxLeadTime(productIds);

            var despatchDate = orderDate;

            for (int i = 0; i < leadTimeDays; i++)
            {
                var daysToAdd = 1;
                daysToAdd += WeekendOffset(despatchDate.DayOfWeek);

                despatchDate = despatchDate.AddDays(daysToAdd);
            }

            return despatchDate;
        }

        private int GetMaxLeadTime(List<int> productIds)
            => productIds
                .Select(GetProductLeadTime)
                .Max();

        private int GetProductLeadTime(int productId)
        {
            var supplierId = _dbContext.Products
                .Where(x => x.ProductId == productId)
                .Select(x => x.SupplierId)
                .Single();

            var supplierLeadTime = _dbContext.Suppliers
                .Where(x => x.SupplierId == supplierId)
                .Select(x => x.LeadTime)
                .Single();

            return supplierLeadTime;
        }

        private static int WeekendOffset(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                    return 0;
                case DayOfWeek.Friday:
                case DayOfWeek.Saturday:
                    return 2;
                case DayOfWeek.Sunday:
                    return 1;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
