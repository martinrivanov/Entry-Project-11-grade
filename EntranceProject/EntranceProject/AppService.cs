using System;
using System.Collections.Generic;
using System.Linq;

namespace EntranceProject
{
	public class AppService
	{
		private readonly UserComparer comparer = new UserComparer();

		public static List<User> LoadUsers()
		{
			return DataContext.LoadUsers();
		}

		public static void SaveUsers(List<User> users)
		{
			answer:
			Console.WriteLine("Do you want to save all the changes you made? [y/n]");
			Console.Write("Type here: ");
			string answer = Console.ReadLine();

			if (answer == "yes" || answer == "y")
			{
				AppService appService = new AppService();
				appService.comparer.CompareTo = "first name";
				users.Sort(appService.comparer);

				DataContext.SaveUsers(users);
			}

			else if (!(answer == "no" || answer == "n"))
			{
				Console.WriteLine("Invalid answer! Please try again!");
				goto answer;
			}
		}

		public static User LogIn(List<User> users)
		{
			User user = null;
			fullName:
			Console.Write("Enter your three names: ");
			string fullName = Console.ReadLine();

			user = users.FirstOrDefault(u => u.FullName == fullName);

			if (user == null)
			{
				Console.WriteLine("A user with this name does not exist. Please try again.");
				goto fullName;
			}

			if (user.Role == "admin")
			{
				password:
				Console.Write("Enter password: ");
				string password = Console.ReadLine();

				if (!(password == user.AdminPassword))
				{
					Console.WriteLine("Incorrect password! Please try again!");
					goto password;
				}
			}

			else
			{
				id:
				Console.Write("Enter your identification code: ");
				int identificationCode = int.Parse(Console.ReadLine());

				if (!(identificationCode == user.IdentificationCode))
				{
					Console.WriteLine("Incorrect identification code! Please try again!");
					goto id;
				}
			}

			return user;
		}

		public static void PrintUsers(List<User> users, User currentUser)
		{
			foreach (var item in users)
			{
				item.PrintData(currentUser);
			}
		}

		public static void SortUsers(List<User> users, User currentUser)
		{
			Console.WriteLine("What do you want the data to be sorted by?");
			Console.Write("(you can sort by first name, last name, address");
			if (currentUser.Role == "admin")
			{
				Console.Write(" and wage");
			}
			Console.WriteLine(")");

			answer:
			Console.Write("Type here: ");

			string answer = Console.ReadLine().ToLower();
			string[] acceptableAnswers = { "first name", "last name", "address", "wage" };

			while ((currentUser.Role != "admin" && answer == "wage") || !acceptableAnswers.Contains(answer))
			{
				Console.WriteLine("Invalid answer! Please try again.");
				goto answer;
			}

			AppService appService = new AppService();
			appService.comparer.CompareTo = answer;
			users.Sort(appService.comparer);

			ascOrDesc:
			Console.WriteLine("Do you wish to be ordered in ascending or descending order? [asc/desc]");
			Console.Write("Type here: ");
			answer = Console.ReadLine();

			if (answer == "desc" || answer == "descending")
			{
				users.Reverse();
			}

			else if (!(answer == "asc" || answer == "ascending"))
			{
				Console.WriteLine("Invalid answer! Please try again.");
				goto ascOrDesc;
			}
		}

		public static void AddUser(List<User> users)
		{
			User user = null;

			name:
			Console.Write("Full name: ");
			string[] name = Console.ReadLine().Split();

			if (name.Length != 3)
			{
				Console.WriteLine("Invalid name! Please write your full name (first name, middle name, last name).");
				goto name;
			}

			role:
			Console.Write("Role: ");
			string role = Console.ReadLine().ToLower();

			if (role == "user")
			{
				Console.Write("Address: ");
				string address = Console.ReadLine();

				id:
				Console.Write("Identification code: ");
				int id = int.Parse(Console.ReadLine());

				if (CheckIdentificationCode(id, users))
				{
					Console.WriteLine("A user with the same identification code already exists! Try with another one!");
					goto id;
				}

				Console.Write("Wage: ");
				double wage = double.Parse(Console.ReadLine());

				user = new User(name[0], name[1], name[2], address, id, wage, role);
			}

			else if (role == "admin")
			{
				Console.Write("Password: ");
				string password = Console.ReadLine();

				user = new User(name[0], name[1], name[2], password, role);
			}

			else
			{
				Console.WriteLine("Invalid role! Please write a valid role (user/admin).");
				goto role;
			}

			AppService appService = new AppService();
			users.Add(user);
			users.Sort(appService.comparer);
		}

		public static void RemoveUser(List<User> users, User currentUser)
		{
			remove:
			Console.Write("Full name: ");
			string name = Console.ReadLine();
			int index = 0;
			bool isFound = false;

			for (int i = 0; i < users.Count; i++)
			{
				if (users[i].FullName == name)
				{
					isFound = true;
					index = i;
					break;
				}
			}

			if (isFound)
			{
				areYouSure:
				Console.WriteLine("Are you sure you want to remove a user with this data? [y/n]");
				users[index].PrintData(currentUser);
				Console.Write("Type here: ");
				string answer = Console.ReadLine().ToLower();

				if (answer == "y" || answer == "yes")
				{
					AppService appService = new AppService();
					users.RemoveAt(index);
					users.Sort(appService.comparer);

					Console.WriteLine("Successfully removed user!");
				}

				else if (answer == "n" || answer == "no")
				{
					answer:
					Console.WriteLine("Do you want to remove another user? [y/n]");
					Console.Write("Type here: ");
					answer = Console.ReadLine().ToLower();

					if (answer == "y" || answer == "yes")
					{
						goto remove;
					}

					else if (!(answer == "n" || answer == "no"))
					{
						Console.WriteLine("Invalid answer! Please try again!");
						goto answer;
					}
				}

				else
				{
					Console.WriteLine("Invalid answer! Please try again!");
					goto areYouSure;
				}
			}

			else
			{
				Console.WriteLine("A user with this name was not found!");
			}
		}

		private static bool CheckIdentificationCode(int id, List<User> users)
		{
			foreach (var user in users)
			{
				if (user.IdentificationCode == id)
				{
					return true;
				}
			}

			return false;
		}

		private static bool CheckForUser(string fullName, List<User> users)
		{
			foreach (var user in users)
			{
				if (user.FullName == fullName)
				{
					return true;
				}
			}

			return false;
		}
	}
}
