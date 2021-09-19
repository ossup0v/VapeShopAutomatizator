
using System.Collections.Generic;
using VapeShopAutomatizator.DTOs;

namespace VapeShopAutomatizator.Interfaces
{
	public interface IPDFService
    {
        List<PurchaseInfo> GetPurchasedInfoList(string path);
        List<PurchaseInfo> GetPurchasedInfoList(Aspose.Pdf.Document doc);
    }
}
