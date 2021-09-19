using Aspose.Pdf.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REMOVEME
{	public class Program
	{
		public static void Main2(string[] args)
		{
			/*
            var path = @"C:\.dev\ForTests\ForTests";
            var sourceFilePath = @"C:\.dev\images\";
            using (var engine = new TesseractEngine(@"C:\.dev-tools\tessdata", "rus+eng"))
            {
                using (var img1 = Pix.LoadFromFile(sourceFilePath + "1.png"))
                {
                    using (var page = engine.Process(img1))
                    {
                        var text = page.GetText();
                        // text variable contains a string with all words found
                        Console.WriteLine("done1");

                        var result = text.RemoveDuplicates("\n").RemoveDuplicates("\n ");
                        Console.WriteLine(result);
                    }
                    using (var img2 = Pix.LoadFromFile(sourceFilePath + "2.png"))
                    {
                        using (var page = engine.Process(img2))
                        {
							var text = page.GetText();
                            Console.WriteLine("done2");

                            // text variable contains a string with all words found
                            Console.WriteLine(text.RemoveDuplicates(Environment.NewLine));
                        }
                    }
                }
            }
			Console.WriteLine("done");
			using (var doc = new PdfDocument(@"C:\.dev\images\1.pdf"))
			{
				//Console.WriteLine(doc.GetText());
				//var var1 = doc.GetPage(1);
				//var var2 = doc.GetPage(2);
				//Console.WriteLine(var1.GetText());
				//Console.WriteLine(var2.GetText());
			}
			var options = PdfConfigurationOptions.Create();
			options.
			using (var pdf = new PdfDocument(@"C:\.dev\images\1.pdf", PdfConfigurationOptions.Create()))
			{
				for (int i = 0; i < pdf.PageCount; ++i)
				{
					string pageText = pdf.Pages[i].GetText();
					Console.WriteLine(pageText);
				}
			}
            */
			Extract_Table();

			//using (PdfDocument document = PdfReader.Open(filePath, PdfDocumentOpenMode.ReadOnly))
			//{
			//	foreach (var page in document.Pages)
			//	{
			//		Console.WriteLine(page.ExtractText());
			//	}
			//	//Console.WriteLine($"Pages {pdfDocument.Pages}");
			//}
			Console.ReadLine();
		}

		public static void Extract_Table()
		{
			// Load source PDF document
			List<Entity> list = new();
			var filePath = @"C:\.dev\images\1.pdf";
			Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(filePath);
			foreach (var page in pdfDocument.Pages)
			{
				Aspose.Pdf.Text.TableAbsorber absorber = new Aspose.Pdf.Text.TableAbsorber();
				absorber.Visit(page);
				foreach (AbsorbedTable table in absorber.TableList.Where(o => o.RowList.Count > 10))
				{
					Console.WriteLine("Table");
					foreach (AbsorbedRow row in table.RowList)
					{
						var name = row.CellList[2].TextFragments.FirstOrDefault().Segments.FirstOrDefault().Text;
						var amount = row.CellList[3].TextFragments.FirstOrDefault().Segments.FirstOrDefault().Text;
						var prise = row.CellList.Last().TextFragments.FirstOrDefault().Segments.FirstOrDefault().Text;
						list.Add(new Entity(name, amount, prise));
						foreach (AbsorbedCell cell in row.CellList)
						{
							foreach (TextFragment fragment in cell.TextFragments)
							{
								var sb = new StringBuilder();
								foreach (TextSegment seg in fragment.Segments)
									sb.Append(seg.Text);
								Console.Write($"{sb.ToString()}|");
							}
						}
						Console.WriteLine();
					}
				}
			}
			var b = 0;
			list.ForEach(o => 
			{ 
				Console.WriteLine(b++.ToString() + o.ToString()); 
			});
		}

		record Entity(string Name, string Amount, string FinalPrice)
		{
			public override string ToString()
			{
				return $"{Name} -- {Amount} -- {FinalPrice}";
			}
		}

		/*
        public static string GetText(Bitmap imgsource)
        {
            var ocrtext = string.Empty;
            using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            {
                using (var img = PixConverter.ToPix(imgsource))
                {
                    using (var page = engine.Process(img))
                    {
                        ocrtext = page.GetText();
                    }
                }
            }

            return ocrtext;
        }*/
	}
}
/*	}
	public static class PdfSharpExtensions
	{
		public static IEnumerable<string> ExtractText(this PdfPage page)
		{
			var content = ContentReader.ReadContent(page);
			var text = content.ExtractText();
			return text;
		}

		public static IEnumerable<string> ExtractText(this CObject cObject)
		{
			if (cObject is COperator)
			{
				var cOperator = cObject as COperator;
				if (cOperator.OpCode.Name == OpCodeName.Tj.ToString() ||
					cOperator.OpCode.Name == OpCodeName.TJ.ToString())
				{
					foreach (var cOperand in cOperator.Operands)
						foreach (var txt in ExtractText(cOperand))
							yield return txt;
				}
			}
			else if (cObject is CSequence)
			{
				var cSequence = cObject as CSequence;
				foreach (var element in cSequence)
					foreach (var txt in ExtractText(element))
						yield return txt;
			}
			else if (cObject is CString)
			{
				var cString = cObject as CString;
				yield return cString.Value;
			}
		}
	}*/
public static class StringEx
{
	public static string RemoveExtraNewLines(this string str)
	{
		return str.RemoveDuplicates("\n ")
			.RemoveDuplicates("\n")
			.RemoveDuplicates(" \n ")
			.RemoveDuplicates(" \n")
			.RemoveDuplicates(Environment.NewLine);
	}
	public static string RemoveDuplicates(this string str, string toRemove)
	{
		for (int i = 0; i < str.Length; i++)
		{
			if (str[i] == toRemove[0])
			{
				var needToRemove = false;
				for (int j = i; j < i + toRemove.Length && j < str.Length; j++)
				{
					if (str[j] != toRemove[j - i])
					{
						needToRemove = false;
						break;
					}

					if (AreDuplicate(str, toRemove, i))
						needToRemove = true;
				}

				if (needToRemove && i + toRemove.Length < str.Length)
				{
					str = str.Remove(i, toRemove.Length);
					i -= toRemove.Length;
				}
			}
		}

		return str;
	}

	private static bool AreDuplicate(string source, string target, int index)
	{
		for (int i = index - target.Length, j = 0; i < source.Length && j < target.Length; i++, j++)
		{
			if (i < 0)
				return false;

			if (source[i] != target[j])
				return false;
		}

		return true;
	}
}
}
