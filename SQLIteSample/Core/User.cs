using System.Text;

namespace SQLIteSample.Core
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(Id.ToString() + "; ");
            sb.Append(Name + "; ");
            sb.Append(Email + "; ");
            sb.Append(Password + "; ");

            return sb.ToString();
        }
    }
}
