namespace SistemaDePonto.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirebaseUid { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public List<TimeEntry> TimeEntries { get; private set; }

        protected User()
        {
            FirebaseUid = "";
            FullName = "";
            Email = "";
            TimeEntries = [];
        }

        public User(string firebaseUid, string fullName, string email)
        {
            FirebaseUid = firebaseUid;
            FullName = fullName;
            Email = email;
            TimeEntries = [];
        }
    }
}
