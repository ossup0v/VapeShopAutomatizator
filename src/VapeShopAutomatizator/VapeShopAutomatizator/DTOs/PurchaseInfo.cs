namespace VapeShopAutomatizator.DTOs
{
	public record PurchaseInfo(string Name, string Amount, string Price)
    {
		public override string ToString()
		{
			return $"{Name} Колво:{Amount} Цена:{Price}";	
		}
	}
}
