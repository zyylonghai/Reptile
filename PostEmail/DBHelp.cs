using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PostEmail
{
    public class DBHelp
    {
        private string _dbConnectStr = ConfigurationManager.ConnectionStrings["IndexDB_Connection"].ToString();
        private DbProviderFactory _dbProvierFactory;
        private DbConnection _dbConnect = null;
        private DbCommand _dbcmd;
        private DbDataAdapter _dbAdapter;
        private DbTransaction _dbTrans;

        private DbCommand dbCommand
        {
            get
            {
                if (_dbcmd == null)
                {
                    _dbcmd = _dbProvierFactory.CreateCommand();
                    _dbcmd.Connection = dbConnect;
                    if (_dbTrans != null)
                        _dbcmd.Transaction = _dbTrans;
                }
                else
                {
                    if (_dbcmd.Connection.State == ConnectionState.Closed)
                        _dbcmd.Connection.Open();
                }
                return _dbcmd;
            }
        }

        private DbDataAdapter dbAdapter
        {
            get
            {
                if (_dbAdapter == null)
                {
                    _dbAdapter = _dbProvierFactory.CreateDataAdapter();
                }
                return _dbAdapter;
            }
        }

        private DbConnection dbConnect
        {
            get
            {
                if (_dbConnect == null)
                {
                    _dbConnect = _dbProvierFactory.CreateConnection();
                    _dbConnect.ConnectionString = _dbConnectStr;
                    _dbConnect.Open();
                }
                else if (_dbConnect.State == System.Data.ConnectionState.Broken)
                {
                    _dbConnect.Close();
                    _dbConnect.Open();
                }
                else if (_dbConnect.State == System.Data.ConnectionState.Closed)
                {
                    _dbConnect.Open();
                }
                return _dbConnect;
            }
        }

        public DBHelp()
        {
            _dbProvierFactory = LibDBProviderFactory.GetDbProviderFactory(LibProviderType.SqlServer);
        }

        public DataTable GetDataTable(string commandText)
        {
            DataTable dt = new DataTable();
            dbCommand.CommandText = commandText;
            dbAdapter.SelectCommand = dbCommand;
            dbAdapter.Fill(dt);
            CloseConnect();
            return dt;
        }

        public int ExecuteNonQuery(string commandText)
        {
            try
            {
                dbCommand.CommandText = commandText;
                return dbCommand.ExecuteNonQuery();
            }
            catch (Exception excep)
            {
                return -1;
            }
            finally
            {
                //CloseConnect();
            }
        }

        public void InsertData(object obj)
        {
            try
            {
                StringBuilder commandText = new StringBuilder();
                StringBuilder fields = new StringBuilder();
                StringBuilder values = new StringBuilder();
                Type type = obj.GetType();

                //commandText.Append(string.Format("insert into {0}(",type.Name));
                PropertyInfo[] propertis = obj.GetType().GetProperties();
                foreach (PropertyInfo p in propertis)
                {
                    if (fields.Length > 0)
                    {
                        fields.Append(",");
                        values.Append(",");
                    }
                    fields.Append(p.Name);
                    if (p.PropertyType.Name.Contains("DateTime"))
                    {
                        values.Append(string.Format("'{0}'", ((DateTime)p.GetValue(obj, null)).ToString("yyyy-MM-dd HH:mm:ss.fff")));
                    }
                    else
                        values.Append(string.Format("'{0}'", p.GetValue(obj, null)));
                }
                commandText.Append(string.Format("insert into {0}({1}) values({2})", type.Name, fields.ToString(), values.ToString()));
                int result = ExecuteNonQuery(commandText.ToString());
                if (result < 0)
                {
                    if (_dbTrans != null)
                        _dbTrans.Rollback();
                }
            }
            catch (Exception ex)
            {
                if (_dbTrans != null)
                    _dbTrans.Rollback();
            }
        }

        public void Update(object obj, Dictionary<string, object> keyvalue)
        {
            try
            {
                StringBuilder commandText = new StringBuilder();
                StringBuilder fieldvalue = new StringBuilder();
                StringBuilder where = new StringBuilder();
                Type type = obj.GetType();

                //commandText.Append(string.Format("insert into {0}(",type.Name));
                PropertyInfo[] propertis = obj.GetType().GetProperties();
                foreach (PropertyInfo p in propertis)
                {
                    if (fieldvalue.Length > 0)
                    {
                        fieldvalue.Append(",");
                    }
                    if (p.GetType().Name.Contains("Interger"))
                    {
                        fieldvalue.Append(string.Format("{0}= {1}", p.Name, p.GetValue(obj, null)));
                    }
                    else
                    {
                        string val = p.GetValue(obj, null).ToString();
                        if (val.Contains("'"))
                            val = val.Replace("'", "''");
                        fieldvalue.Append(string.Format("{0}= '{1}'", p.Name, val));
                    }
                }
                foreach (KeyValuePair<string, object> item in keyvalue)
                {
                    if (where.Length > 0)
                    {
                        where.Append(" and ");
                    }
                    where.Append(string.Format("{0}='{1}'", item.Key, item.Value));
                }
                commandText.Append(string.Format("update {0} set {1} where {2}", type.Name, fieldvalue.ToString(), where.ToString()));
                int result = ExecuteNonQuery(commandText.ToString());
                if (result < 0)
                {
                    if (_dbTrans != null)
                        _dbTrans.Rollback();
                }
            }
            catch (Exception ex)
            {
                if (_dbTrans != null)
                    _dbTrans.Rollback();
            }
        }

        public void BeginTrans()
        {
            _dbTrans = dbConnect.BeginTransaction();
        }

        public void Commit()
        {
            if (_dbTrans != null && _dbTrans.Connection != null)
                _dbTrans.Commit();
        }

        private void CloseConnect()
        {
            if (this._dbConnect != null)
                this._dbConnect.Close();
        }

    }

    class LibDBProviderFactory
    {
        private static Dictionary<LibProviderType, string> _providerAssemblyNameDic = new Dictionary<LibProviderType, string>();
        private static Dictionary<LibProviderType, DbProviderFactory> _providerFactoryDic = new Dictionary<LibProviderType, DbProviderFactory>();
        static LibDBProviderFactory()
        {
            _providerAssemblyNameDic.Add(LibProviderType.SqlServer, "System.Data.SqlClient");
            //_providerAssemblyNameDic.Add(LibProviderType.Oracle, "System.Data.OracleClient");
            _providerAssemblyNameDic.Add(LibProviderType.Oracle, "System.Data.OleDb");
        }

        public static DbProviderFactory GetDbProviderFactory(LibProviderType pType)
        {
            DbProviderFactory dbProviderFactory = null;
            if (!_providerFactoryDic.TryGetValue(pType, out dbProviderFactory))
            {
                dbProviderFactory = DoImporProviderFactory(pType);
                _providerFactoryDic.Add(pType, dbProviderFactory);
            }
            return dbProviderFactory;
        }
        private static DbProviderFactory DoImporProviderFactory(LibProviderType pType)
        {
            string providerAssemblyNm = _providerAssemblyNameDic[pType];
            DbProviderFactory provider = null;
            try
            {
                provider = DbProviderFactories.GetFactory(providerAssemblyNm);
            }
            catch (Exception ex)
            {
                provider = null;
            }
            finally
            {

            }
            return provider;
        }

    }

    public enum LibProviderType
    {
        SqlServer = 0,
        Oracle = 1,
        MySQL = 2
    }
}
