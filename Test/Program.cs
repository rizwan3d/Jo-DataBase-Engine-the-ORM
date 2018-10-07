using JoDataBaseEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        [DBTable("PhoneBook")]
        class PhoneBook
        {
            [DbColumn("id")]
            [AutoIncrement]
            [PrimaryKey]
            [DataType(SqlDbType.Int)]
            public int ID { set; get; }

            [DbColumn("Name")]
            [DataType(SqlDbType.NVarChar)]
            public string Name { set; get; }

            [DbColumn("PhoneNumber")]
            [DataType(SqlDbType.NVarChar)]
            public string PhoneNumber { set; get; }

            [AllowNull]
            [DbColumn("Occupation")]
            [DataType(SqlDbType.NVarChar)]
            public string Occupation { set; get; }

            [DbColumn("Description")]
            [AllowNull]
            [DataType(SqlDbType.NVarChar)]
            public string Description { set; get; }

            [DbColumn("active")]
            [DataType(SqlDbType.Int)]
            public int Active { set; get; }
        }

        //[DBTable("t")]
        //class qwer
        //{
        //    [DbColumn("id")]
        //    [DataType(SqlDbType.NChar)]
        //    public string id { set; get; }

        //    [DbColumn("a")]
        //    [DataType(SqlDbType.NChar)]
        //    public string a { set; get; }

        //    [DbColumn("b")]
        //    [DataType(SqlDbType.NChar)]
        //    public string b { set; get; }

        //    [DbColumn("c")]
        //    [DataType(SqlDbType.NChar)]
        //    public string c { set; get; }

        //    [DbColumn("d")]
        //    [DataType(SqlDbType.NChar)]
        //    public string d { set; get; }

        //    [DbColumn("e")]
        //    [DataType(SqlDbType.NChar)]
        //    public string e { set; get; }

        //    [DbColumn("f")]
        //    [DataType(SqlDbType.NChar)]
        //    public string f { set; get; }

        //    [DbColumn("g")]
        //    [DataType(SqlDbType.NChar)]
        //    public string g { set; get; }

        //    [DbColumn("h")]
        //    [DataType(SqlDbType.NChar)]
        //    public string h { set; get; }

        //    [DbColumn("i")]
        //    [DataType(SqlDbType.NChar)]
        //    public string i { set; get; }

        //    [DbColumn("j")]
        //    [DataType(SqlDbType.NChar)]
        //    public string j { set; get; }

        //    [DbColumn("k")]
        //    [DataType(SqlDbType.NChar)]
        //    public string k { set; get; }

        //    [DbColumn("l")]
        //    [DataType(SqlDbType.NChar)]
        //    public string l { set; get; }

        //    [DbColumn("m")]
        //    [DataType(SqlDbType.NChar)]
        //    public string m { set; get; }

        //    [DbColumn("n")]
        //    [DataType(SqlDbType.NChar)]
        //    public string n { set; get; }
        //}

        static void Main(string[] args)
        {
            DataBaseInfo dbinfo = new DataBaseInfo()
            {
                ServerName = @"DESKTOP-4BDM3A8\SQLEXPRESS",
                DataBase = "HWSMST",
                Athentication = AthenticationType.WindowsAthentication
            };

            DataBaseEngine.DataBaseInfo = dbinfo;

            PhoneBook p = new PhoneBook()
            {
                ID = 1009,
                Name = "asd",
                PhoneNumber = "345678",
                Occupation = "b'bb",
                Description = "''''''''''''''''",
                Active = 1
            };

            PhoneBook p2 = new PhoneBook()
            {

            };
            
            Type t = p2.GetType();
            PropertyInfo[] pi = t.GetProperties();
            foreach (PropertyInfo pp in pi)
            {
                System.Console.WriteLine(pp.Name + "    " + pp.GetValue(p2, new object[] { }));
            }

            DataBaseEngine.Insert(p2);

            //DataBaseEngine.Update(p,p2, (x) => x.ID == 1014);

            //DataBaseEngine.Delete(new PhoneBook(), (x) => x.ID == 1014);

            //DataBaseEngine.Count(p.ID);
            //List<PhoneBook> l = DataBaseEngine.Select(new PhoneBook(), x => x.ID == 2);

            //l.ForEach(i =>
            //{
            //    Type t = i.GetType();
            //    PropertyInfo[] pi = t.GetProperties();
            //    foreach (PropertyInfo pp in pi)
            //    {
            //        System.Console.WriteLine(pp.Name + "    " + pp.GetValue(i, new object[] { }));
            //    }
            //});

            //qwer a = new qwer()
            //{
            //    id = "bbb",
            //    a = "aaa",
            //    b = "aaa",
            //    c = "aaa",
            //    d = "aaa",
            //    e = "aaa",
            //    f = "aaa",
            //    g = "aaa",
            //    h = "aaa",
            //    i = "aaa",
            //    j = "aaa",
            //    k = "aaa",
            //    l = "aaa",
            //    n = "aaa",
            //    m = "aaa",
            //};

            // DataBaseEngine.Insert(a);


            //List<qwer> l = DataBaseEngine.Select(new qwer(), x => x.id == "bbb       ");

            //l.ForEach(i =>
            //{
            //    Type t = i.GetType();
            //    PropertyInfo[] pi = t.GetProperties();
            //    foreach (PropertyInfo pp in pi)
            //    {
            //        System.Console.WriteLine(pp.Name + "    " + pp.GetValue(i, new object[] { }));
            //    }
            //});

            Console.Write("Press Any Key To Exit..");
            Console.ReadKey();
        }
    }
}