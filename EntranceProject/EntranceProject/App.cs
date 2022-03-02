using System;
using System.Collections.Generic;

namespace EntranceProject
{
	public class App
	{
		public static void Run()
		{
			List<User> users = AppService.LoadUsers();
			User currentUser = AppService.LogIn(users);

			command:
			Console.WriteLine();
			Console.WriteLine("Enter the number of what you would like to do:");
			Console.WriteLine("1. Print users");
			Console.WriteLine("2. Sort users");
			Console.WriteLine("3. Exit");

			if (currentUser.Role == "admin")
			{
				Console.WriteLine("4. Add user");
				Console.WriteLine("5. Remove user");
			}

			Console.Write("Type here: ");
			int command = int.Parse(Console.ReadLine());

			while (true)
			{
				if (currentUser.Role == "user")
				{
					switch (command)
					{
						case 1:
							AppService.PrintUsers(users, currentUser);
							break;

						case 2:
							AppService.SortUsers(users, currentUser);
							break;

						case 3:
							Console.WriteLine("Goodbye!");
							break;

						default:
							Console.WriteLine("Invalid command! Please try again!");
							goto command;
					}
				}

				else
				{
					switch (command)
					{
						case 1:
							AppService.PrintUsers(users, currentUser);
							break;

						case 2:
							AppService.SortUsers(users, currentUser);
							break;

						case 3:
							AppService.SaveUsers(users);
							Console.WriteLine("Goodbye!");
							break;

						case 4:
							AppService.AddUser(users);
							break;

						case 5:
							AppService.RemoveUser(users, currentUser);
							break;

						default:
							Console.WriteLine("Invalid command! Please try again!");
							goto command;
					}
				}

				if (command == 3)
				{
					break;
				}

				goto command;
			}
		}
	}
}
