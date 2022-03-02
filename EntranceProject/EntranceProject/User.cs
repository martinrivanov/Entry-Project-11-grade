using System;

namespace EntranceProject
{
	public class User
	{
		private string firstName;
		private string middleName;
		private string lastName;
		private string address;
		private int identificationCode;
		private double wage;
		private string adminPassword;
		private string role;

		//For Admins
		public User(string firstName, string middleName, string lastName, string adminPassword, string role)
		{
			this.firstName = firstName;
			this.middleName = middleName;
			this.lastName = lastName;
			this.adminPassword = adminPassword;
			this.role = role;
		}

		//For Users
		public User(string firstName, string middleName, string lastName, string address, int identificationCode, double wage, string role)
		{
			this.firstName = firstName;
			this.middleName = middleName;
			this.lastName = lastName;
			this.address = address;
			this.identificationCode = identificationCode;
			this.wage = wage;
			this.role = role;
		}

		public string FirstName
		{
			get => this.firstName;
		}

		public string MiddleName
		{
			get => this.middleName;
		}

		public string LastName
		{
			get => this.lastName;
		}

		public string FullName
		{
			get => $"{firstName} {middleName} {lastName}";
		}

		public string Address
		{
			get => this.address;
		}

		public int IdentificationCode
		{
			get => this.identificationCode;
		}

		public double Wage
		{
			get => this.wage;
		}

		public string AdminPassword
		{
			get => this.adminPassword;
		}

		public string Role
		{
			get => this.role;
			set => this.role = value;
		}

		public void PrintData(User currentUser)
		{
			if (role == "user")
			{
				Console.WriteLine($"Full name: {FullName}");
				Console.WriteLine($"Address: {address}");
				Console.WriteLine($"Identification number: {identificationCode}");
				if (currentUser.Role == "admin")
				{
					Console.Write($"Wage: {wage}");
				}
				Console.WriteLine("\n");
			}
		}
	}
}
