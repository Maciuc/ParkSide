using Parkside.Models.ViewModels;

namespace Parkside.Services.UsersExtend
{
    public interface IUserExtendService
    {
        public List<BaseViewModel> GetUsers();
    }
}
