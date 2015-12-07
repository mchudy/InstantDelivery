using InstantDelivery.Model;
using InstantDelivery.Service.Paging;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace InstantDelivery.Tests
{
    public class PagingHelperTests
    {
        [Fact]
        public void GetPagedResult_WhenSortPropertyIsNotSpecified_ShouldSortById()
        {
            var employees = new List<EmployeeDto>
            {
                new EmployeeDto {Id = 3 },
                new EmployeeDto {Id = 1 },
                new EmployeeDto {Id = 2 }
            };
            PageQuery query = new PageQuery
            {
                PageSize = 10,
                PageIndex = 1
            };

            var result = PagingHelper.GetPagedResult(employees.AsQueryable(), query);

            Assert.Equal(result.PageCollection[0].Id, 1);
            Assert.Equal(result.PageCollection[1].Id, 2);
            Assert.Equal(result.PageCollection[2].Id, 3);
        }

        [Fact]
        public void GetPagedResult_ShouldSortBySpecifiedProperty()
        {
            var employees = new List<EmployeeDto>
            {
                new EmployeeDto {Id = 3, FirstName= "C"},
                new EmployeeDto {Id = 1, FirstName = "A"},
                new EmployeeDto {Id = 2, FirstName = "B"}
            };
            PageQuery query = new PageQuery
            {
                PageSize = 10,
                PageIndex = 1,
                SortProperty = "FirstName"
            };

            var result = PagingHelper.GetPagedResult(employees.AsQueryable(), query);

            Assert.Equal(result.PageCollection[0].FirstName, "A");
            Assert.Equal(result.PageCollection[1].FirstName, "B");
            Assert.Equal(result.PageCollection[2].FirstName, "C");
        }

        [Fact]
        public void GetPagedResult_WhenSortDirectionDescending()
        {
            var employees = new List<EmployeeDto>
            {
                new EmployeeDto {Id = 3, FirstName= "C"},
                new EmployeeDto {Id = 1, FirstName = "A"},
                new EmployeeDto {Id = 2, FirstName = "B"}
            };
            PageQuery query = new PageQuery
            {
                PageSize = 10,
                PageIndex = 1,
                SortProperty = "FirstName",
                SortDirection = System.ComponentModel.ListSortDirection.Descending
            };

            var result = PagingHelper.GetPagedResult(employees.AsQueryable(), query);

            Assert.Equal(result.PageCollection[0].FirstName, "C");
            Assert.Equal(result.PageCollection[1].FirstName, "B");
            Assert.Equal(result.PageCollection[2].FirstName, "A");
        }

        [Fact]
        public void GetPagedResult_ShouldReturnCorrectNumberOfPages()
        {
            var employees = new List<EmployeeDto>
            {
                new EmployeeDto {Id = 1},
                new EmployeeDto {Id = 2},
                new EmployeeDto {Id = 3 },
                new EmployeeDto {Id = 4 },
                new EmployeeDto {Id = 5 },
            };
            PageQuery query = new PageQuery
            {
                PageSize = 2,
                PageIndex = 1
            };

            var result = PagingHelper.GetPagedResult(employees.AsQueryable(), query);

            Assert.Equal(result.PageCount, 3);
        }

        [Fact]
        public void GetPagedResult_ShouldReturnPageCollectionForCorrectPage()
        {
            var employees = new List<EmployeeDto>
            {
                new EmployeeDto {Id = 1},
                new EmployeeDto {Id = 2},
                new EmployeeDto {Id = 3 },
                new EmployeeDto {Id = 4 },
                new EmployeeDto {Id = 5 },
            };
            PageQuery query = new PageQuery
            {
                PageSize = 2,
                PageIndex = 2
            };

            var result = PagingHelper.GetPagedResult(employees.AsQueryable(), query);

            Assert.Equal(result.PageCollection.Count, 2);
            Assert.Equal(result.PageCollection[0].Id, 3);
            Assert.Equal(result.PageCollection[1].Id, 4);
        }

    }
}
