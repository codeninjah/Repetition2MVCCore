using Microsoft.AspNetCore.Identity;
using Repetition2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repetition2.Data
{
    public class DbSeeder
    {
		public static object Products { get; private set; }

		public static void Seeder(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager)
		{
			var product = new Product[]
			{
		new Product{Name="Mostafa is hot", UnitPrice =1000000,
		ReleaseDate = new DateTime(1989,11,02), IsDeleted = false },
				new Product{Name="Gentrit is hot", UnitPrice =2000000,
		ReleaseDate = new DateTime(2009,11,02), IsDeleted = false},
				new Product {Name = "Tragic Endings", UnitPrice =5000000,
				ReleaseDate = new DateTime(2017,12,15), IsDeleted = true
				},
			};

			//foreach (Song s in song)
			//{
			//	context.Songs.Add(s);
			//}
			context.Product.AddRange(product);
			context.SaveChanges();

			if (!context.Roles.Any())
			{
				var admin = new IdentityRole { Name = "Admin" };
				var result = roleManager.CreateAsync(admin).Result;
			}

			if (!context.Users.Any())
			{
				var admin = new ApplicationUser { UserName = "admin@test.com", Email = "admin@test.com" };
				var resultadmin = userManager.CreateAsync(admin, "Test-123").Result;
				var roleResultadmin = userManager.AddToRoleAsync(admin, "Admin").Result;

				var user = new ApplicationUser { UserName = "student@test.com", Email = "student@test.com" };
				var result = userManager.CreateAsync(user, "Test-123").Result;
				//var roleResult = userManager.AddToRoleAsync(user, "Student").Result;

			}
		}
    }
}
