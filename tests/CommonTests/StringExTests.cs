using NUnit.Framework;
using VapeShopAutomatizator.Common;

namespace CommonTests
{
	public class StringExTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void Ezzzze_Case_Success()
		{
			//arrange
			var raw = "1231231230123123012312212223123";
			//act
			var result = raw.RemoveDuplicates("123");
			//assert
			Assert.AreEqual(result, "1230123012312212223123");
		}

		[Test]
		public void Ezzzzzzzze_Case_Success()
		{
			//arrange
			var raw = "12301231230123";
			//act
			var result = raw.RemoveDuplicates("123");
			//assert
			Assert.AreEqual(result, "12301230123");
		}

		[Test]
		public void Ezzzzzzzzze_Case_Success()
		{
			//arrange
			var raw = "00110101100";
			//act
			var result = raw.RemoveDuplicates("1");
			//assert
			Assert.AreEqual(result, "001010100");
		}
	}
}