using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoDataBaseEngine
{
    public class DataBaseInfo
    {
        string _ServerName = string.Empty;
        string _DataBase = string.Empty;
        string _UserName = string.Empty;
        string _Password = string.Empty;

        AthenticationType _Athentication;

        public string ServerName { get => _ServerName; set => _ServerName = value; }
        public string DataBase { get => _DataBase; set => _DataBase = value; }
        public string UserName { get => _UserName; set => _UserName = value; }
        public string Password { get => _Password; set => _Password = value; }
        public AthenticationType Athentication { get => _Athentication; set => _Athentication = value; }
        public string ConnectionString { get => BuildConnectionString(); }
      
        string BuildConnectionString()
        {
            if (_Athentication == AthenticationType.SQLSererAthentication)
                return $"Server={_ServerName};Database={_DataBase};User Id={_UserName};Password={_Password};";
            else
                return $"Server={_ServerName};Database={_DataBase};Trusted_Connection=True;";

            throw new Exception("Invalid Athentication");
        }
    }
}
