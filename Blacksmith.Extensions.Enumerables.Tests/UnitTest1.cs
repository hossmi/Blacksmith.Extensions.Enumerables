using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blacksmith.Extensions.Enumerables.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void enumerable_tests()
        {
            User[] sortedUsers, unsortedUsers;

            unsortedUsers = Sources
                .getUsers()
                .ToArray();

            sortedUsers = Sources
                .getUsers()
                .orderBy(u => u.Age, OrderDirection.Descendant)
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

            unsortedUsers = Sources
                .getUsers()
                .ToArray();

            sortedUsers = Sources
                .getUsers()
                .AsQueryable()
                .orderBy(u => u.Age, OrderDirection.Descendant)
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

            users = Sources
                .getUsersWithNulls()
                .notNull();

            ints = Sources
                .getInts()
                .notNull();

            Assert.AreEqual(4, users.Count());
            Assert.AreEqual(10, ints.Count());
        }

        [TestMethod]
        public void string_enumerables_tests()
        {
            string[] strings;

            strings = Sources
                .getStrings()
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

        [TestMethod]
        public void forEach_tests()
        {
            int totalAge;

            totalAge = 0;

            //I know, we can achieve this by using linq Sum<T> extension method,
            //but, this is for to test the forEach method.
            Sources
                .getUsers()
                .forEach(u => totalAge += u.Age);

            Assert.AreEqual(165, totalAge);
        }

        [TestMethod]
        public void enumerable_whereIf_tests()
        {
            IEnumerable<User> users;

            users = Sources.getUsers()
                .whereIf(true, u => u.Age > 30)
                .whereIf(false, u => u.Age < 50);

            Assert.AreEqual(3, users.Count());

            users = users.whereIfStringIsFilled(null, u => u.Age < 50);
            Assert.AreEqual(3, users.Count());

            users = users.whereIfStringIsFilled("", u => u.Age < 50);
            Assert.AreEqual(3, users.Count());

            users = users.whereIfStringIsFilled("pepe", u => u.Age < 50);
            Assert.AreEqual(1, users.Count());
        }

        [TestMethod]
        public void queryable_whereIf_tests()
        {
            IQueryable<User> users;

            users = Sources
                .getUsers()
                .AsQueryable()
                .whereIf(true, u => u.Age > 30)
                .whereIf(false, u => u.Age < 50);

            Assert.AreEqual(3, users.Count());

            users = users.whereIfStringIsFilled(null, u => u.Age < 50);
            Assert.AreEqual(3, users.Count());

            users = users.whereIfStringIsFilled("", u => u.Age < 50);
            Assert.AreEqual(3, users.Count());

            users = users.whereIfStringIsFilled("pepe", u => u.Age < 50);
            Assert.AreEqual(1, users.Count());
        }

        [TestMethod]
        public void push_tests()
        {
            IList<User> users;

            users = Sources.getUsers().ToList();

            users.push(new User
            {
                Age = 89,
                Name = "Pepe",
                SurName = "Fernandez González",
                Registration = new DateTime(2019, 1, 1),
            });

            Assert.AreEqual(5, users.Count);
            Assert.ThrowsException<ArgumentException>(() => Sources.getInts().ToArray().push(4));
        }

        [TestMethod]
        public void enumerable_paginate_tests()
        {
            IEnumerable<User> orderedUsers;
            User[] users;

            orderedUsers = Sources
                .getUsers()
                .orderBy(u => u.Age, OrderDirection.Ascendant)
                .thenBy(u => u.Registration, OrderDirection.Ascendant);

            users = orderedUsers
                .paginate(2, 0)
                .ToArray();

            Assert.AreEqual(2, users.Length);
            Assert.AreEqual("Narciso", users[0].Name);
            Assert.AreEqual("Florencio", users[1].Name);

            users = orderedUsers
                .paginate(2, 1)
                .ToArray();

            Assert.AreEqual(2, users.Length);
            Assert.AreEqual("Rosa", users[0].Name);
            Assert.AreEqual("Florencio", users[1].Name);

            Assert.ThrowsException<ArgumentException>(() => orderedUsers.paginate(0, 2).ToArray());
        }

        [TestMethod]
        public void queryable_paginate_tests()
        {
            IQueryable<User> orderedUsers;
            User[] users;

            orderedUsers = Sources
                .getUsers()
                .AsQueryable()
                .orderBy(u => u.Age, OrderDirection.Ascendant)
                .thenBy(u => u.Registration, OrderDirection.Ascendant);

            users = orderedUsers
                .paginate(2, 0)
                .ToArray();

            Assert.AreEqual(2, users.Length);
            Assert.AreEqual("Narciso", users[0].Name);
            Assert.AreEqual("Florencio", users[1].Name);

            users = orderedUsers
                .paginate(2, 1)
                .ToArray();

            Assert.AreEqual(2, users.Length);
            Assert.AreEqual("Rosa", users[0].Name);
            Assert.AreEqual("Florencio", users[1].Name);

            Assert.ThrowsException<ArgumentException>(() => orderedUsers.paginate(0, 2).ToArray());
        }
    }
}
