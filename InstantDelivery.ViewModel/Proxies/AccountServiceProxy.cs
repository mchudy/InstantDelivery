using InstantDelivery.Common.Enums;
using System.Threading.Tasks;

namespace InstantDelivery.ViewModel.Proxies
{
    public class AccountServiceProxy : ServiceProxyBase
    {
        public AccountServiceProxy() : base("Account")
        {
        }

        public async Task<Role[]> GetRoles()
        {
            return await Get<Role[]>("Roles");
        }
    }
}
