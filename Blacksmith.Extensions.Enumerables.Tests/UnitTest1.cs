using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blacksmith.Extensions.Enumerables.Tests
{
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Registration { get; set; }
        public string SurName { get; set; }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void enumerable_tests()
        {
            User[] sortedUsers, unsortedUsers;

            unsortedUsers = getUsers().ToArray();

            sortedUsers = getUsers()
                .sortBy(u => u.Age, OrderDirection.Descendant)
                .thenBy(u => u.SurName, OrderDirection.Ascendant)
                .ToArray();

            Assert.ReferenceEquals(unsortedUsers[0], sortedUsers[3]);
            Assert.ReferenceEquals(unsortedUsers[1], sortedUsers[1]);
            Assert.ReferenceEquals(unsortedUsers[2], sortedUsers[0]);
            Assert.ReferenceEquals(unsortedUsers[3], sortedUsers[2]);
        }

        [TestMethod]
        public void queryable_tests()
        {
            User[] sortedUsers, unsortedUsers;

            unsortedUsers = getUsers().ToArray();

            sortedUsers = getUsers()
                .AsQueryable()
                .sortBy(u => u.Age, OrderDirection.Descendant)
                .thenBy(u => u.SurName, OrderDirection.Ascendant)
                .ToArray();

            Assert.ReferenceEquals(unsortedUsers[0], sortedUsers[3]);
            Assert.ReferenceEquals(unsortedUsers[1], sortedUsers[1]);
            Assert.ReferenceEquals(unsortedUsers[2], sortedUsers[0]);
            Assert.ReferenceEquals(unsortedUsers[3], sortedUsers[2]);
        }

        [TestMethod]
        public void notNull_tests()
        {
            IEnumerable<User> users;
            IEnumerable<int> ints;

            users = getUsersWithNulls()
                .notNull();

            ints = getInts().notNull();

            Assert.AreEqual(4, users.Count());
            Assert.AreEqual(10, ints.Count());
        }

        [TestMethod]
        public void string_enumerables_tests()
        {
            string[] strings;

            strings = getStrings()
                .notNull()
                .trim()
                .ToArray();

            Assert.AreEqual(6, strings.Length);
            Assert.AreEqual("pepe", strings[2]);
            Assert.AreEqual("tronco", strings[4]);

            strings = strings
                .notEmpty()
                .ToArray();

            Assert.AreEqual(2, strings.Length);
            Assert.AreEqual("pepe", strings[0]);
            Assert.AreEqual("tronco", strings[1]);
        }

        private static IEnumerable<User> getUsers()
        {
            yield return new User //3
            {
                Name = "Narciso",
                SurName = "Robles Olmos",
                Age = 21,
                Registration = new DateTime(2009, 8, 13),
            };

            yield return new User //1
            {
                Name = "Rosa",
                SurName = "De la huerta Castaños",
                Age = 55,
                Registration = new DateTime(2011, 8, 13),
            };

            yield return new User //0
            {
                Name = "Florencio",
                SurName = "Céspedes Perales",
                Age = 55,
                Registration = new DateTime(2015, 8, 13),
            };

            yield return new User //2
            {
                Name = "Florencio",
                SurName = "Campos Huertas",
                Age = 34,
                Registration = new DateTime(2015, 8, 13),
            };
        }

        private static IEnumerable<User> getUsersWithNulls()
        {
            yield return new User
            {
                Name = "Narciso",
                SurName = "Robles Olmos",
                Age = 21,
                Registration = new DateTime(2009, 8, 13),
            };

            yield return null;

            yield return new User
            {
                Name = "Rosa",
                SurName = "De la huerta Castaños",
                Age = 55,
                Registration = new DateTime(2011, 8, 13),
            };

            yield return null;

            yield return new User
            {
                Name = "Florencio",
                SurName = "Céspedes Perales",
                Age = 55,
                Registration = new DateTime(2015, 8, 13),
            };

            yield return new User
            {
                Name = "Florencio",
                SurName = "Campos Huertas",
                Age = 34,
                Registration = new DateTime(2015, 8, 13),
            };

            yield return null;
            yield return null;
            yield return null;
            yield return null;
        }

        private static IEnumerable<int> getInts()
        {
            yield return 1;
            yield return 2;
            yield return 3;
            yield return 5;
            yield return 8;
            yield return 13;
            yield return 21;
            yield return 34;
            yield return 55;
            yield return 89;
        }

        private static IEnumerable<string> getStrings()
        {
            yield return "";
            yield return "";
            yield return "pepe ";
            yield return null;
            yield return "";
            yield return " tronco    ";
            yield return null;
            yield return "";
        }
    }
}
