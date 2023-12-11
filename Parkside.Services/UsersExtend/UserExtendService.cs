using Parkside.Infrastructure.Repositories.UsersExtend;
using Parkside.Models.ViewModels;

namespace Parkside.Services.UsersExtend
{
    public class UserExtendService : IUserExtendService
    {
        private readonly IUserExtendRepo _userExtendRepo;
        public UserExtendService(IUserExtendRepo userExtendRepo)
        {
            _userExtendRepo = userExtendRepo;
        }

        public List<BaseViewModel> GetUsers()
        {
            var users = _userExtendRepo.GetAllQuerable();

            if (users.Count() == 0)
            {
                throw new Exception("Users not found!");
            }

            var usersList = users.Select(x => new BaseViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return usersList;
        }
    }
}
