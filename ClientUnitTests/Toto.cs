using System;
using System.Threading.Tasks;
using Client;
using NUnit.Framework;

namespace ClientUnitTests
{
    public class TotoTests
    {
        [Test]
        public void TestSayHello()
        {
            var toto = new Toto("Pierre");
            var result = toto.SayHello();
            Assert.AreEqual("Hello Pierre", result);
        }

        [Test]
        public void TestAlwaysFails()
        {
            var toto = new Toto("Toto");
            Assert.Throws<ArgumentException>(toto.AlwaysFails);
        }

        [Test]
        public async Task TestSayHelloAsync()
        {
            var toto = new Toto("Toto");
            var res = await toto.SayHelloAsync();
            Assert.AreEqual("Hello Toto", res);
        }
    }
}
