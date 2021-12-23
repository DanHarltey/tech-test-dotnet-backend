using System;
using System.Collections.Generic;

namespace Moonpig.PostOffice.Core.Interfaces
{
    public interface IDespatchDateInteractor
    {
        DateTime CalculateDespatchDate(List<int> productIds, DateTime orderDate);
    }
}