using System;
using System.Collections.Generic;

namespace Blacksmith.Extensions.Enumerables.Tests
{
    public static class Sources
    {
        public static IEnumerable<User> getUsers()
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

        public static IEnumerable<User> getUsersWithNulls()
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

        public static IEnumerable<int> getInts()
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

        public static IEnumerable<string> getStrings()
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
