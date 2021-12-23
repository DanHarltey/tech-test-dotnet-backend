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

        private DateTime _mlt;

        public DespatchDateInteractor(IDbContext dbContext)
            => _dbContext = dbContext;

        public DateTime Get(List<int> productIds, DateTime orderDate)
        {
            _mlt = orderDate; // max lead time
            foreach (var ID in productIds)
            {
                var s = _dbContext.Products.Single(x => x.ProductId == ID).SupplierId;
                var lt = _dbContext.Suppliers.Single(x => x.SupplierId == s).LeadTime;
                if (orderDate.AddDays(lt) > _mlt)
                    _mlt = orderDate.AddDays(lt);
            }
            if (_mlt.DayOfWeek == DayOfWeek.Saturday)
            {
                return _mlt.AddDays(2);
            }
            else if (_mlt.DayOfWeek == DayOfWeek.Sunday) return _mlt.AddDays(1);
            else return _mlt;
        }
    }
}
