using tesla;
using Xunit;

namespace TestApp
{
    public class TeslaTests
    {
        [Fact]
        public void TestThing() {
            Assert.Equal(42, new Thing().Get(42));
        }
    }
}
