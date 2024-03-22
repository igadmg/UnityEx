using MathEx;
using NUnit.Framework;

namespace Assets.UnityEx.Libraries.MathEx.src.Test
{
	[TestFixture]
	public class BVHTest
	{
		[Test]
		public void BVHTest1()
		{
			BVHTree2 bvh = new BVHTree2();

			var c = new circle(vec2.zero, 10);
			bvh.Add(c.bound, c);
			Assert.IsFalse(false, "1 should not be prime");
		}
	}
}
