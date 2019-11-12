using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Blacksmith.Extensions.Enumerables.Tests
{
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            User[] users;

            users = getUsers()
                .OrderBy(u => u.Name)
                .ToArray();

            Assert.AreEqual("Florencio", users[0].Name);

            users = getUsers()
                .sortBy(u => u.Age, OrderDirection.Descendant)
                .ToArray();

            Assert.AreEqual("Rosa", users[2].Name);

            users = getUsers()
                .sortBy(u => u.Age, OrderDirection.Ascendant)
                .ToArray();

            Assert.AreEqual("Rosa", users[0].Name);
        }

        private static IEnumerable<User> getUsers()
        {
            yield return new User
            {
                Name = "Narciso",
                Age = 21,
            };

            yield return new User
            {
                Name = "Rosa",
                Age = 8,
            };

            yield return new User
            {
                Name = "Florencio",
                Age = 55,
            };
        }
    }
}
