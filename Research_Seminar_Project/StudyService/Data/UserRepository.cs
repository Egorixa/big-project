namespace StudyService.Data
{
    public class UserRepository
    {
        private static List<Models.User> _users = new List<Models.User> {};

        public List<Models.User> GetAllUsers() => _users;

        public Models.User GetUserByEmail(string email) => _users.FirstOrDefault(u => u.Email == email);

        public void AddUser(Models.User user)
        {
            user.Id = _users.Count + 1; // Простая генерация ID
            _users.Add(user);
        }
    }
}