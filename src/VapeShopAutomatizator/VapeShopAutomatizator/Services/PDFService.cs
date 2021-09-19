using Aspose.Pdf;
using Aspose.Pdf.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using VapeShopAutomatizator.DTOs;
using VapeShopAutomatizator.Interfaces;

namespace VapeShopAutomatizator.Services
{
	public class PDFService : IPDFService
	{
		public List<PurchaseInfo> GetPurchasedInfoList(string path)
		{
			return GetPurchasedInfoList(new Document(path));
		}

		public List<PurchaseInfo> GetPurchasedInfoList(Document doc)
		{
			return ExtractData(doc);
		}

		public List<PurchaseInfo> ExtractData(Document pdfDocument)
		{
			List<PurchaseInfo> list = new();
			foreach (var page in pdfDocument.Pages)
			{
				TableAbsorber absorber = new TableAbsorber();
				absorber.Visit(page);
				foreach (AbsorbedTable table in absorber.TableList)
				{
					Console.WriteLine("Table");
					foreach (AbsorbedRow row in table.RowList)
					{
						try
						{
							var name = row.CellList[2].TextFragments.FirstOrDefault().Segments.FirstOrDefault().Text;
							var amount = row.CellList[3].TextFragments.FirstOrDefault().Segments.FirstOrDefault().Text;
							var price = row.CellList.Last().TextFragments.FirstOrDefault().Segments.FirstOrDefault().Text;
							list.Add(new PurchaseInfo(name, amount, price));
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message + " " + ex.StackTrace);
							break;
						}
					}
				}
			}

			return list;
		}
	}
}
