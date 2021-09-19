using Tesseract;
using VapeShopAutomatizator.Interfaces;
using VapeShopAutomatizator.Common;

namespace VapeShopAutomatizator.Services
{
	public class ImageSerivce : IImageService
	{
		public string GetImageText(string path, bool deleteExtraEnters = false)
		{
			using (var image = Pix.LoadFromFile(path))
				return GetImageText(image, deleteExtraEnters);
		}

		public string GetImageText(Pix image, bool deleteExtraEnters = false)
		{
			using (var engine = new TesseractEngine(@"C:\.dev-tools\tessdata", "rus+eng", EngineMode.Default))
			{
				using (var page = engine.Process(image))
				{
					return page.GetText().RemoveExtraNewLines();
				}
			}
		}
	}
}
