using System.Collections.Generic;
using System.IO;

namespace EntranceProject
{
	public class DataContext
	{
		private const string APP_INFO = "files/info.txt";
		public static List<User> LoadUsers()
		{
			List<User> users = new List<User>();

			using (var reader = new StreamReader(APP_INFO))
			{
				string line = "";

				while ((line = reader.ReadLine()) != null)
				{
					string[] userInfo = line.Split('|');
					User user = null;
					if (userInfo[userInfo.Length - 1] == "user")
					{
						user = new User(userInfo[0], userInfo[1], userInfo[2], userInfo[3], int.Parse(userInfo[4]), double.Parse(userInfo[5]), userInfo[6]);
					}

					else
					{
						user = new User(userInfo[0], userInfo[1], userInfo[2], userInfo[3], userInfo[4]);
					}

					users.Add(user);
				}
			}

			return users;
		}

		public static void SaveUsers(List<User> users)
		{
			using (StreamWriter writer = new StreamWriter(APP_INFO))
			{
				foreach (var user in users)
				{
					if (user.Role == "admin")
					{
						writer.WriteLine($"{user.FirstName}|{user.MiddleName}|{user.LastName}|{user.AdminPassword}|{user.Role}");
					}

					else if (user.Role == "user")
					{
						writer.WriteLine($"{user.FirstName}|{user.MiddleName}|{user.LastName}|{user.Address}|{user.IdentificationCode}|{user.Wage}|{user.Role}");
					}
				}
			}
		}
	}
}
