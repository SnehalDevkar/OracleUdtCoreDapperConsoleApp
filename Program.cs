using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Dapper.Oracle;
using Fsl.Utilities.DataBaseHelpers;
using Oracle.ManagedDataAccess.Client;

namespace OracleUDTCoreDapperConsoleApp
{
    class ObjectUDT
    {
        static int withoutDapperAge = 10;
        static int withDapperAge = 60;
        static void Main(string[] args)
        {
            string storedProc = "odp_obj1_sample_upd_contacts";

            // Create a new Person object
            Person p1 = new Person();
            p1.Name = "John";
            p1.Address = "Address 1";
            p1.Age = ++withoutDapperAge;

            PersonTable personTable = new PersonTable();
            personTable.Value = new Person[1];
            personTable.Value[0] = p1;

            string constr = "<Your Connection String>";

            // CallingWithoutDapper(storedProc, p1, personTable, constr);

            CallingWithDapper(storedProc, personTable, constr);
        }

        private static void CallingWithDapper(string storedProc, PersonTable personTable, string constr)
        {
            OracleConnectionHelper _oracleConnectionHelper = new OracleConnectionHelper();
            IDbConnection connection = null;
            try
            {

                string walletPath = "<your wallet>";
                connection = _oracleConnectionHelper.CreateOracleDbTlsConnection(constr, walletPath);

                string param2Value = DateTime.Now.ToString();

                var dt = new DataTable();
                dt.Columns.Add("name", typeof(string));
                dt.Columns.Add("address", typeof(string));
                dt.Columns.Add("age", typeof(int));

                for (int i = 0; i < personTable.Value.Length; i++)
                {
                    Person p2 = personTable.Value[i];
                    dt.Rows.Add(p2.Name, p2.Address, p2.Age);
                }

                //p.Add("param3", OracleDbType.Object, ParameterDirection.Input, demoPersonList);
                //p.Add("param3", DbType.Object, ParameterDirection.Input, new { dt = dt.AsTableValuedParameter("ODP_OBJ1_SAMPLE_PERSON_TABLE") });
                //p.Add("param3", OracleDbType.Object, ParameterDirection.Input, personTable);

                //connection.Execute(storedProc, new { param2 = param2Value.Substring(0, 15), param3 = dt.AsTableValuedParameter("ODP_OBJ1_SAMPLE_UPD_CONTACTS") }, commandType: CommandType.StoredProcedure);

                connection.Query(storedProc, new { param2 = param2Value.Substring(0, 15), param3 = dt }, commandType: CommandType.StoredProcedure);


                //connection.Query<dynamic>(storedProc, p, commandType: CommandType.StoredProcedure);

                //connection.Query(storedProc, new { param2 = "From Dapper", param3 = dt.AsTableValuedParameter("ODP_OBJ1_SAMPLE_PERSON_TABLE") }, commandType: CommandType.StoredProcedure);

                //connection.Query(storedProc, new { param2= "From Dapper", param3= personTable, param1 = p }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private static void CallingWithoutDapper(string storedProc, Person p1, PersonTable personTable, string constr)
        {
            OracleConnection con = null;
            OracleCommand cmd = null;

            try
            {
                // Establish a connection to Oracle database
                con = new OracleConnection(constr);
                con.Open();
                cmd = new OracleCommand(storedProc, con);

                try
                {
                    // Insert Person object into a database and update object
                    //  using a stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;


                    OracleParameter param2 = new OracleParameter();
                    param2.OracleDbType = OracleDbType.Varchar2;
                    param2.Direction = ParameterDirection.Input;
                    param2.Value = "1-800-555-4412";

                    cmd.Parameters.Add(param2);

                    OracleParameter param3 = new OracleParameter();

                    param3.OracleDbType = OracleDbType.Object;
                    param3.Direction = ParameterDirection.Input;

                    // Note: The UdtTypeName is case-senstive
                    param3.UdtTypeName = "ODP_OBJ1_SAMPLE_PERSON_TABLE";
                    param3.Value = personTable;

                    cmd.Parameters.Add(param3);

                    OracleParameter param1 = new OracleParameter();

                    param1.OracleDbType = OracleDbType.Object;
                    param1.Direction = ParameterDirection.InputOutput;

                    // Note: The UdtTypeName is case-senstive
                    param1.UdtTypeName = "ODP_OBJ1_SAMPLE_PERSON_TYPE";
                    param1.Value = p1;

                    cmd.Parameters.Add(param1);


                    // Insert the UDT into the table
                    cmd.ExecuteNonQuery();

                    // Print out the updated Person object
                    Console.WriteLine("Updated Person: " + param1.Value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    // Clean up
                    if (cmd != null)
                        cmd.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Clean up
                if (cmd != null)
                    cmd.Dispose();
                if (con != null)
                    con.Dispose();
            }
        }
    }
}
