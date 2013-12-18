using System.Collections.Generic;
using UI.Web.Controllers;

namespace UI.Web.ViewModels.User
{
    public class UserListViewModel
    {
        public List<UserViewModel> UserViewModels { get; set; }
        public int TotalUsers { get; set; }
    }
}