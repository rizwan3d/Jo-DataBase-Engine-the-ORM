using JoDataBaseEngine;
using System;
using System.Collections.Generic;
using System.Linq;
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
            public int ID { set; get; }

            [DbColumn("Name")]
            public string Name { set; get; }

            [DbColumn("PhoneNumber")]
            public string PhoneNumber { set; get; }

            [DbColumn("Occupation")]
            public string Occupation { set; get; }

            [DbColumn("Description")]
            public string Description  { set; get; }

            [DbColumn("active")]
            public int Active { set; get; }
        }
        static void Main(string[] args)
        {
            DataBaseInfo dbinfo = new DataBaseInfo()
            {
                ServerName = @"DESKTOP-4BDM3A8\SQLEXPRESS",
                DataBase = @"HWSMST" ,
                Athentication = AthenticationType.WindowsAthentication
            };
            DataBaseEngine.DataBaseInfo = dbinfo;
            PhoneBook p = new PhoneBook() { Name ="Muhammad Rizwan", PhoneNumber = "789456123" , Occupation = "bbb" , Description  = "dew" ,Active = 1};
            DataBaseEngine.Insert(p);

            Console.Read();
        }
    }
}
