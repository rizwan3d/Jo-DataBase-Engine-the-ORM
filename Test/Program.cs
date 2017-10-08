using JoDataBaseEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        class PhoneBook
        {
            [DbColumn("id")]
            [AutoIncrement]
            [PrimaryKey]
            public int ID { set; get; }

            [DbColumn("Name")]
            public string Name { set; get; }

            [DbColumn("PhoneNumber")]
            public string PhoneNumber { set; get; }

            [DbColumn("Occupation")]
            public string Occupation { set; get; }

            [DbColumn("Description")]
            public string Description { set; get; }

            [DbColumn("active")]
            public int Active { set; get; }
        }
        static void Main(string[] args)
        {
            DataBaseInfo dbinfo = new DataBaseInfo()
            {
                ServerName = @"DESKTOP-4BDM3A8\SQLEXPRESS",
                DataBase = "HWSMST",
                Athentication = AthenticationType.WindowsAthentication
            };
            DataBaseEngine.DataBaseInfo = dbinfo;
            PhoneBook p = new PhoneBook() { ID = 1003, Name = "Muhammad Rizwan", PhoneNumber = "785421", Occupation = "bbb", Description = "dew", Active = 1 };
            PhoneBook p2 = new PhoneBook() { ID = 1003, Name = "Rizwan", PhoneNumber = "789456123", Occupation = "bbb", Description = "dew", Active = 0 };
            DataBaseEngine.Update(p,p2, (x) => x.ID == p2.ID);           

            Console.Read();
        }
    }
}
