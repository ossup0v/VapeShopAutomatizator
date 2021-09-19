using System.Collections.Generic;

namespace VapeShopAutomatizator.Models
{
	//public class PdfDbContext : DbContext
	//{
	//	public PdfDbContext(DbContextOptions<PdfDbContext> options) : base(options)
	//	{
	//
	//	}
	//
	//	public DbSet<PdfModel> Pdfs { get; set; }
	//}

	public class PdfDbContext
	{
		public List<PdfModel> Pdfs { get; set; } = new();
	}
}
