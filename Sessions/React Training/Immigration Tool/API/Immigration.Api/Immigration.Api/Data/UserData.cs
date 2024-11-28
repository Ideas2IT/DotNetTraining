using Immigration.Api.Models;

namespace Immigration.Api.Data
{
    
    public class UserData
    {
        List<User> _users = new List<User>();
        public UserData()
        {
            _users = new List<User>();
            _users.Add(new User { Name = "User Employee", Id = 1, Password = "test", Role = "Employee", UserName = "Emp" });
            _users.Add(new User { Name = "User HR", Id = 2, Password = "test", Role = "HR", UserName = "HR" });
            _users.Add(new User { Name = "User Admin", Id = 3, Password = "test", Role = "Admin", UserName = "Admin" });
        }

        public List<User> Users { get { return _users; } }
    }
}
