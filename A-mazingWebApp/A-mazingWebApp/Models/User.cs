using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace MazeModel
{
    public class User
    {
        public string ToJSON()
        {
            JObject userObj = new JObject();
            userObj["Username"] = Username;
            userObj["Email"] = Email;
            userObj["Password"] = Password;
            userObj["Wins"] = Wins;
            userObj["Losses"] = Losses;
            return userObj.ToString();
        }

        // The primary key
        [Key]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
    }
}
