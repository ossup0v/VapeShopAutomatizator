using Tesseract;

namespace VapeShopAutomatizator.Interfaces
{
    public interface IImageService
    {
        string GetImageText(string path, bool deleteExtraEnters = false);
        string GetImageText(Pix image, bool deleteExtraEnters = false);
    }
}
