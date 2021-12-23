using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Moonpig.PostOffice.IntegrationTests.API.Controllers
{
    public class DespatchDateTests : UsesWebApplicationFactory
    {
        public DespatchDateTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public async Task OneOrderOneDayLeadTime()
        {
            /* 
             * Given an order contains a product from a supplier with a lead time of 1 day
             * And the order is place on a Monday -01 / 01 / 2018
             * When the despatch date is calculated
             * Then the despatch date is Tuesday - 02 / 01 / 2018
             */

            // Arrange
            using var client = CreateClient();

            // Act
            var locationDetail = await client.GetFromJsonAsync<DespatchDate>("/api/DespatchDate?productIds=1&orderDate=2018/01/01");

            // Assert
            Assert.Equal(new DateTime(2018, 1,2), locationDetail.Date);
        }

        [Fact]
        public async Task OneOrderTwoDaysLeadTime()
        {
            /*
             * Given an order contains a product from a supplier with a lead time of 2 days
             * And the order is place on a Monday - 01/01/2018
             * When the despatch date is calculated
             * Then the despatch date is Wednesday - 03/01/2018
             */

            // Arrange
            using var client = CreateClient();

            // Act
            var locationDetail = await client.GetFromJsonAsync<DespatchDate>("/api/DespatchDate?productIds=2&orderDate=2018/01/01");

            // Assert
            Assert.Equal(new DateTime(2018, 1, 3), locationDetail.Date);
        }

        [Fact]
        public async Task TwoOrdersTwoDaysLeadTime()
        {
            /*
             * Given an order contains a product from a supplier with a lead time of 1 day
             * And the order also contains a product from a different supplier with a lead time of 2 days
             * And the order is place on a Monday - 01/01/2018
             * When the despatch date is calculated
             * Then the despatch date is Wednesday - 03/01/2018
             */

            // Arrange
            using var client = CreateClient();

            // Act
            var locationDetail = await client.GetFromJsonAsync<DespatchDate>("/api/DespatchDate?productIds[0]=1&productIds[1]=2&&orderDate=2018/01/01");

            // Assert
            Assert.Equal(new DateTime(2018, 1, 3), locationDetail.Date);
        }

        [Fact]
        public async Task OneOrderPlacedOnFridayWithOneDayLeadTime()
        {
            /*
             * Given an order contains a product from a supplier with a lead time of 1 day
             * And the order is place on a Friday - 05/01/2018
             * When the despatch date is calculated
             * Then the despatch date is Monday - 08/01/2018
             */

            // Arrange
            using var client = CreateClient();

            // Act
            var locationDetail = await client.GetFromJsonAsync<DespatchDate>("/api/DespatchDate?productIds[0]=1&&orderDate=2018/01/05");

            // Assert
            Assert.Equal(new DateTime(2018, 1, 8), locationDetail.Date);
        }

        [Fact(Skip = "Broken")]
        public async Task OneOrderPlacedOnSaturdayWithOneDayLeadTime()
        {
            /*
             * Given an order contains a product from a supplier with a lead time of 1 day
             * And the order is place on a Saturday - 06/01/18
             * When the despatch date is calculated
             * Then the despatch date is Tuesday - 09/01/2018
             */

            // Arrange
            using var client = CreateClient();

            // Act
            var locationDetail = await client.GetFromJsonAsync<DespatchDate>("/api/DespatchDate?productIds[0]=1&&orderDate=2018/01/06");

            // Assert
            Assert.Equal(new DateTime(2018, 1, 9), locationDetail.Date);
        }

        [Fact(Skip = "Broken")]
        public async Task OneOrderPlacedOnSundayWithOneDayLeadTime()
        {
            /*
             * Given an order contains a product from a supplier with a lead time of 1 days
             * And the order is place on a Sunday - 07/01/2018
             * When the despatch date is calculated
             * Then the despatch date is Tuesday - 09/01/2018
             */

            // Arrange
            using var client = CreateClient();

            // Act
            var locationDetail = await client.GetFromJsonAsync<DespatchDate>("/api/DespatchDate?productIds[0]=1&&orderDate=2018/01/07");

            // Assert
            Assert.Equal(new DateTime(2018, 1, 9), locationDetail.Date);
        }

        [Fact(Skip = "Broken")]
        public async Task OneOrderPlacedOnFridayWithSixsDaysLeadTime()
        {
            /*
             * Given an order contains a product from a supplier with a lead time of 6 days
             * And the order is place on a Friday - 05/01/2018
             * When the despatch date is calculated
             * Then the despatch date is Monday - 15/01/2018
             */

            // Arrange
            using var client = CreateClient();

            // Act
            var locationDetail = await client.GetFromJsonAsync<DespatchDate>("/api/DespatchDate?productIds[0]=9&&orderDate=2018/05/01");

            // Assert
            Assert.Equal(new DateTime(2018, 1, 15), locationDetail.Date);
        }


        [Fact( Skip = "Broken")]
        public async Task OneOrderPlacedOnFridayWithElevenDaysLeadTime()
        {
            /*
             * Given an order contains a product from a supplier with a lead time of 11 days
             * And the order is place on a Friday - 05/01/2018
             * When the despatch date is calculated
             * Then the despatch date is Monday - 22/01/2018
             */

            // Arrange
            using var client = CreateClient();

            // Act
            var locationDetail = await client.GetFromJsonAsync<DespatchDate>("/api/DespatchDate?productIds[0]=10&&orderDate=2018/01/05");

            // Assert
            Assert.Equal(new DateTime(2018, 1, 22), locationDetail.Date);
        }

        private class DespatchDate
        {
            public DateTime Date { get; set; }
        }
    }
}
