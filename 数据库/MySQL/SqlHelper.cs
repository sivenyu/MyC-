using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace MySQL
{
    class SqlHelper : IDisposable
    {
        protected SqlConnection conn;
        protected string conStr;

        public SqlHelper()
        {
            //conStr = "Integrated Security=SSPI; Persist Security Info=False; Data Source=(local); Database=TitansWcs; User Id=sa;Pwd=sa123456!";
            conStr = "Data Source=(local); Initial Catalog=TitansWCS; Integrated Security=true";
        }

        private void Open()
        {
            if (conn == null)
            {
                conn = new SqlConnection(conStr);
                conn.Open();
            }
            else
            {
                if (conn.State.Equals(ConnectionState.Closed))
                    conn.Open();
            }
        }

        public void Close()
        {
            if (conn.State.Equals(ConnectionState.Open))
            {
                conn.Close();
            }
        }

        public void Dispose()
        {
            if (conn != null)
            {
                conn.Dispose();
                conn = null;
            }
        }

        public bool ExceSql(string strSqlCom)
        {
            try
            {
                Open();
                SqlCommand sqlcom = new SqlCommand(strSqlCom, conn);
                sqlcom.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        /// 参数传值方法，防止SQL注入式攻击
        /// </summary>
        public bool Login(string loginName, string loginPwd)
        {
            bool bRet = false;
            try
            {
                Open();
                string strsql = "SELECT count(*) FROM tb_operator WHERE OPERATORNAME=@OperatorName AND PASSWORD=@PassWord";
                SqlCommand sqlcom = new SqlCommand(strsql, conn);
                sqlcom.Parameters.Add(new SqlParameter("@OperatorName", SqlDbType.NVarChar, 50));
                sqlcom.Parameters["@OperatorName"].Value = loginName;
                sqlcom.Parameters.Add(new SqlParameter("@PassWord", SqlDbType.NVarChar, 50));
                sqlcom.Parameters["@PassWord"].Value = loginPwd;

                bRet = (int)sqlcom.ExecuteScalar() > 0 ? true : false;
            }
            catch
            {
                bRet = false;
            }
            finally
            {
                Close();
            }

            return bRet;
        }
    }
}
