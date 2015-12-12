using InstantDelivery.Model;
using InstantDelivery.Model.Paging;
using InstantDelivery.ViewModel.Dialogs;
using InstantDelivery.ViewModel.Extensions;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel.Proxies
{
    public class UsersServiceProxy : ServiceProxyBase
    {
        public UsersServiceProxy(IDialogManager dialogManager)
            : base("Users", dialogManager)
        {
        }
        public async Task<PagedResult<UserDto>> Page(PageQuery query)
        {
            return await Get<PagedResult<UserDto>>("Page?" + query.ToQueryString());
        }
    }
}