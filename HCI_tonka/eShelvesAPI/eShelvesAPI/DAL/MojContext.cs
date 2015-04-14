using eShelvesAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eShelvesAPI.DAL
{
	public class MojContext : DbContext
	{
		public DbSet<Korisnik> Korisnics { get; set; }
		public DbSet<Autor> Autors { get; set; }
		public DbSet<Kategorija> Kategorijas { get; set; }
		public DbSet<Knjiga> Knjigas { get; set; }
		public DbSet<Ocjena> Ocjenas { get; set; }
		public DbSet<Polica> Policas { get; set; }
		public DbSet<Prijateljstvo> Prijateljstvos { get; set; }

		public MojContext()
			: base("Name=MyConnectionString")
		{

		}
	}
}