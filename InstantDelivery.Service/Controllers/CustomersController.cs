using AutoMapper;
using AutoMapper.QueryableExtensions;
using InstantDelivery.Common.Enums;
using InstantDelivery.Common.Extensions;
using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Model.Customers;
using InstantDelivery.Model.Packages;
using InstantDelivery.Model.Paging;
using InstantDelivery.Model.Statistics;
using InstantDelivery.Service.Paging;
using InstantDelivery.Service.Pricing;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;

namespace InstantDelivery.Service.Controllers
{
    [Authorize]
    [RoutePrefix("Customers")]
    public class CustomersController : ApiController
    {
        private readonly InstantDeliveryContext context;
        private readonly UserManager<User, string> userManager;
        private readonly IPricingStrategy pricingStrategy;

        public CustomersController(InstantDeliveryContext context, UserManager<User, string> userManager,
            IPricingStrategy pricingStrategy)
        {
            this.context = context;
            this.userManager = userManager;
            this.pricingStrategy = pricingStrategy;
        }

        /// <summary>
        /// Dodaje nowego klienta
        /// </summary>
        /// <param name="newCustomer">Nowy pracownik</param>
        [AllowAnonymous]
        [Route("Register"), HttpPost]
        public IHttpActionResult Register(CustomerRegisterDto newCustomer)
        {
            if (newCustomer == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Customer customer = Mapper.Map<Customer>(newCustomer);
            var user = new User { UserName = newCustomer.UserName };
            if (context.Users.Any(u => user.UserName == u.UserName))
            {
                return BadRequest("Ta nazwa użytkownika jest już zajęta");
            }
            context.Customers.Add(customer);
            customer.User = user;
            var result = userManager.Create(user, newCustomer.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.ToString());
            }
            userManager.AddToRole(user.Id, Role.Customer.ToString());
            context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Zwraca stronę paczek dla zalogowanego klienta
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route("Packages/Page"), HttpGet]
        public IHttpActionResult PackagesPage([FromUri] PageQuery query)
        {
            var customer = context.Customers.FirstOrDefault(c => c.User.UserName == User.Identity.Name);
            if (customer == null)
            {
                return BadRequest();
            }
            var dtos = customer.Packages.AsQueryable().ProjectTo<PackageDto>();
            return Ok(PagingHelper.GetPagedResult(dtos, query));
        }

        /// <summary>
        /// Zwraca dane adresowe zalogowanego klienta
        /// </summary>
        [Route("Address"), HttpGet]
        public IHttpActionResult GetAddress()
        {
            var customer = context.Customers.FirstOrDefault(c => c.User.UserName == User.Identity.Name);
            if (customer == null)
            {
                return BadRequest();
            }
            var dto = Mapper.Map<CustomerAddressDto>(customer);
            return Ok(dto);
        }

        [Route("SendPackage"), HttpPost]
        public IHttpActionResult SendPackage(PackageDto package)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customer = context.Customers.FirstOrDefault(c => c.User.UserName == User.Identity.Name);
            if (customer == null)
            {
                return BadRequest();
            }
            package.Status = PackageStatus.ToPickUp;
            package.Cost = pricingStrategy.GetCost(package);
            var newPackage = Mapper.Map<Package>(package);
            context.Packages.Add(newPackage);
            customer.Packages.Add(newPackage);
            context.PackageEvents.Add(new PackageEvent
            {
                Package = newPackage,
                EventType = PackageEventType.ReadyToPickFromSender
            });
            context.SaveChanges();
            return Ok();
        }

        [Route("Profile"), HttpGet]
        public IHttpActionResult Profile()
        {
            var customer = context.Customers.FirstOrDefault(c => c.User.UserName == User.Identity.Name);
            if (customer == null)
            {
                return BadRequest();
            }
            var dto = Mapper.Map<CustomerProfileDto>(customer);
            dto.RankDescription = customer.Rank.GetDescription();
            return Ok(dto);
        }

        [Route("Packages/Statistics"), HttpGet]
        public IHttpActionResult GetPackageStatistics()
        {
            var customer = context.Customers.FirstOrDefault(c => c.User.UserName == User.Identity.Name);
            if (customer == null)
            {
                return BadRequest();
            }
            var now = DateTime.Now;
            var monthStatistics = from package in customer.Packages
                                  join @event in context.PackageEvents on package.Id equals @event.Package.Id
                                  where @event.Date.Year == now.Year
                                  group package by @event.Date.Month into packages
                                  select new MonthStatisticDto { Month = packages.Key, Cost = packages.Sum(p => p.Cost), Count = packages.Count() };
            DateTime weekStart = now.Date.AddDays(-(int)now.DayOfWeek);
            var weekStatistics = from package in customer.Packages
                                 join @event in context.PackageEvents on package.Id equals @event.Package.Id
                                 where @event.Date >= weekStart
                                 group package by @event.Date.DayOfWeek into packages
                                 select new WeekStatisticDto { Day = packages.Key, Cost = packages.Sum(p => p.Cost), Count = packages.Count() };
            var result = new CustomerPackageStatisticsDto
            {
                MonthStatistics = monthStatistics.ToList(),
                WeekStatistics = weekStatistics.ToList()
            };
            return Ok(result);
        }
    }
}
