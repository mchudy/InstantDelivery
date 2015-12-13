using InstantDelivery.Common.Enums;
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

        /// <summary>
        /// Zwraca stronę danych użytkowników.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PagedResult<UserDto>> Page(PageQuery query)
        {
            return await Get<PagedResult<UserDto>>("Page?" + query.ToQueryString());
        }

        /// <summary>
        /// Zmienia rolę użytkownika
        /// </summary>
        /// <param name="userName">Nazwa użytkownika</param>
        /// <param name="role">Nowa rola</param>
        /// <returns></returns>
        public async Task ChangeRole(string userName, Role role)
        {
            await PostAsJson<Role>($"ChangeRole/{userName}", role);
        }
    }
}