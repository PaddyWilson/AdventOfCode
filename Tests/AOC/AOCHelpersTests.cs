using AOC;

namespace Tests
{
	public class AOCHelpersTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void Rotate()
		{
			int[,] testIn = new int[2, 2] { 
				{ 1, 2 }, 
				{ 3, 4 } 
			};
			var testOut = Helpers.Rotate(testIn, 2);

			if (testOut[0, 0] == 3 && testOut[0, 1] == 1 && testOut[1, 0] == 4 && testOut[1, 1] == 2)
				Assert.Pass();
			else
				Assert.Fail();
		}

		[Test]
		public void Flip()
		{
			int[,] testIn = new int[2, 2] { 
				{ 1, 2 }, 
				{ 3, 4 } 
			};
			var testOut = Helpers.FlipX(testIn, 2);

			//if ()
			//	Assert.Pass();
			//else
			//	Assert.Fail();

			Assert.That(testOut[0, 0] == 3 && testOut[0, 1] == 4 && testOut[1, 0] == 1 && testOut[1, 1] == 2, Is.True);
		}
	}
}