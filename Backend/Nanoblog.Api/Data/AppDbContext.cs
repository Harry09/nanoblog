using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

using Nanoblog.Api.Data.Models;

namespace Nanoblog.Api.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		public DbSet<Entry> Entries { get; set; }

		public AppDbContext(DbContextOptions options) : base(options)
		{
		}
	}
}
