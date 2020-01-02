using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Blacksmith.Extensions.Enumerables.Strings;
using FluentAssertions;

namespace Blacksmith.Extensions.Enumerables.Tests
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void stringify_string_array()
        {
            new string[] { "uno que llega", null, @"lorem ipsum", null, "" }
                .stringify()
                .Should()
                .Be("uno que llega, lorem ipsum");
        }

        [TestMethod]
        public void Stringify_collection()
        {
            int[] ints;
            
            ints = new int[] { 1, 2, 3, 5, 8, 13 };

            ints.stringify(i => (-i * 10).ToString())
                .Should()
                .Be("-10, -20, -30, -50, -80, -130");

            ints.stringify(s => (-s * 10).ToString(), "|")
                .Should()
                .Be("-10|-20|-30|-50|-80|-130");

            ints.stringify(s => (-s * 10).ToString(), "")
                .Should()
                .Be("-10-20-30-50-80-130");
        }

        [TestMethod]
        public void stringify_empty_collection()
        {
            Enumerable
                .Empty<decimal>()
                .stringify(s => (-s * 10m).ToString())
                .Should()
                .Be("");
        }
    }
}
