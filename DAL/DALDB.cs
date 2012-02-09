//---------------------------------------
// <copyright file="DALDB.cs" company="">
// </copyright>
// <author>andrei.stoica</author>
// <date>2012.02.09 02:19</date>
// <summary>
// </summary>
//---------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkOrder;
using System.Data.SqlClient;
using ErrorHandler;

namespace DAL
{
    public class DALDB
    {
        private SqlConnection conn;
        private static string connString;
        private SqlCommand command;
        private ErrorHandler.ErrorHandler err;
        private static List<WorkOrder.Workorder> woList;


        public DALDB(string _connString)
        {
            connString = _connString;
            err = new ErrorHandler.ErrorHandler();
        }

        //insert in db
        public void AddWorkorder(WorkOrder.Workorder wo)
        {
            try
            {
                using (conn)
                {
                    string sqlInsertString = "INSERT INTO Workorders (wo_ID, wo_Number, wo_Date, wo_Debtor, wo_Address, wo_StartTime)" +
                    "VALUES ( @wo_ID, @wo_Number, @wo_Date, @wo_Debtor, @wo_Address, @wo_StartTime)";

                    conn = new SqlConnection(connString);
                    command = new SqlCommand();
                    command.Connection = conn;
                    command.Connection.Open();
                    command.CommandText = sqlInsertString;

                    SqlParameter wo_IDParam = new SqlParameter("@wo_ID", wo.wo_ID);
                    SqlParameter wo_NumberParam = new SqlParameter("@wo_Number", wo.wo_Number);
                    SqlParameter wo_DateParam = new SqlParameter("@wo_Date", wo.wo_Date);
                    SqlParameter wo_DebtorParam = new SqlParameter("@wo_Debtor", wo.wo_Debtor);

                    SqlParameter wo_AddressParam = new SqlParameter("@wo_Address", wo.wo_Address);
                    SqlParameter wo_StartTimeParam = new SqlParameter("@wo_StartTime", wo.wo_StartTime);

                    command.Parameters.AddRange(new SqlParameter[] { wo_IDParam, wo_NumberParam, wo_DateParam, wo_DebtorParam, 
                        wo_AddressParam, wo_StartTimeParam });
                    command.ExecuteNonQuery();
                    command.Connection.Close();

                }
            }
            catch (Exception ex)
            {
                err.ErrorMessage = ex.Message.ToString();
                throw;
            }

        }

        //db update
        public void UpdateWorkorder(WorkOrder.Workorder wo)
        {
            try
            {
                using (conn)
                {
                    string sqlUpdateStr = "UPDATE Employee SET wo_id = @wo_ID, wo_Number = @wo_Number, wo_Date = @wo_Date, wo_Debtor = @wo_Debtor, " +
                                          " wo_Address = @wo_Address, wo_StartTime = @wo_StartTime" +
                        " WHERE wo_ID = @wo_id";
                    conn = new SqlConnection(connString);
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.Connection.Open();
                    command.CommandText = sqlUpdateStr;

                    SqlParameter wo_IDParam = new SqlParameter("@wo_ID", wo.wo_ID);
                    SqlParameter wo_NumberParam = new SqlParameter("@wo_Number", wo.wo_Number);
                    SqlParameter wo_DateParam = new SqlParameter("@wo_Date", wo.wo_Date);
                    SqlParameter wo_DebtorParam = new SqlParameter("@wo_Debtor", wo.wo_Debtor);

                    SqlParameter wo_AddressParam = new SqlParameter("@wo_Address", wo.wo_Address);
                    SqlParameter wo_StartTimeParam = new SqlParameter("@wo_StartTime", wo.wo_StartTime);

                    command.Parameters.AddRange(new SqlParameter[] { wo_IDParam, wo_NumberParam, wo_DateParam, wo_DebtorParam, 
                        wo_AddressParam, wo_StartTimeParam });
                    command.ExecuteNonQuery();
                    command.Connection.Close();

                }
            }
            catch (Exception ex)
            {
                err.ErrorMessage = ex.Message.ToString();
                throw;
            }
        }

        // delete from db
        public void DeleteWorkorder(WorkOrder.Workorder wo)
        {
            try
            {
                using (conn)
                {
                    string sqlDeleteStr = "DELETE Workorders WHERE wo_ID = @wo_ID";
                    conn = new SqlConnection(connString);
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.Connection.Open();
                    command.CommandText = sqlDeleteStr;

                    SqlParameter wo_IDParam = new SqlParameter("@wo_ID", wo.wo_ID);

                    command.Parameters.AddRange(new SqlParameter[] { wo_IDParam });
                    command.ExecuteNonQuery();
                    command.Connection.Close();

                }
            }
            catch (Exception ex)
            {
                err.ErrorMessage = ex.Message.ToString();
                throw;
            }
        }

        public List<WorkOrder.Workorder> GetWorkorders()
        {
            try
            {
                using (conn)
                {
                    woList = new List<WorkOrder.Workorder>();
                    string sqlSelectStr = "SELECT wo_ID, wo_Number, wo_Date, wo_Debtor, wo_Address, wo_StartTime FROM Workorders;";
                    conn = new SqlConnection(connString);
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.Connection.Open();
                    command.CommandText = sqlSelectStr;

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        WorkOrder.Workorder wo = new Workorder();
                        wo.wo_ID = (int)reader[0];
                        wo.wo_Number = (int) reader[1];
                        wo.wo_Date = (DateTime) reader[2];
                        wo.wo_Debtor = reader[3].ToString();
                        wo.wo_Address = reader[4].ToString();
                        wo.wo_StartTime = (DateTime)reader[5];
                        
                        woList.Add(wo);
                    }

                    command.Connection.Close();
                    return woList;
                }

            }
            catch (Exception ex)
            {
                err.ErrorMessage = ex.Message.ToString();
                throw;
            }

        }

        public WorkOrder.Workorder GetWorkorder(int id)
        {
            try
            {
                if (woList == null)
                {
                    woList = GetWorkorders();
                }
                foreach (WorkOrder.Workorder wo in woList)
                {
                    if (wo.wo_ID == id)
                    {
                        return wo;
                    }
                }
                return null;
            }
            catch(Exception ex)
            {
                err.ErrorMessage = ex.Message.ToString();
                throw;
            }
        }

        public string GetException()
        {
            return err.ErrorMessage.ToString();
        }

    }


    
}
