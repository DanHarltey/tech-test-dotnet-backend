namespace Moonpig.PostOffice.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Moonpig.PostOffice.Core.Interfaces;
    using System;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    public class DespatchDateController : Controller
    {
        private readonly IDespatchDateInteractor _despatchDate;

        public DespatchDateController(IDespatchDateInteractor despatchDate)
            => _despatchDate = despatchDate;

        [HttpGet]
        public DespatchDate Get(List<int> productIds, DateTime orderDate)
        {
            var despatchDate = _despatchDate.Get(productIds, orderDate);

            return new DespatchDate { Date = despatchDate };
        }
    }
}
