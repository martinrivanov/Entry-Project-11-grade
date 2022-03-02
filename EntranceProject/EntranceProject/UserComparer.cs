using System;
using System.Collections.Generic;
namespace EntranceProject
{
	public class UserComparer : IComparer<User>
	{
		public string CompareTo { get; set; }

		public int Compare(User x, User y)
		{
			switch (CompareTo)
			{
				default:
				case "first name":
					return x.FirstName.CompareTo(y.FirstName);

				case "last name":
					return x.LastName.CompareTo(y.LastName);

				case "address":
					return x.Address.CompareTo(y.Address);

				case "wage":
					return x.Wage.CompareTo(y.Wage);
			}
		}
	}
}
