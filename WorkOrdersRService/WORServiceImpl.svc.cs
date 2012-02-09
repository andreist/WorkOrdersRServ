//----------------------------------------------------
// <copyright file="WORServiceImpl.svc.cs" company="">
// </copyright>
// <author>andrei.stoica</author>
// <date>2012.02.09 03:49</date>
// <summary>
// </summary>
//----------------------------------------------------
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DAL;
using WorkOrder;
using System.IO;
using System.Runtime.Serialization.Json;

namespace WorkOrdersRService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WORServiceImpl" in code, svc and config file together.
    public class WORServiceImpl : IWORServiceImpl
    {
        string connStr = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;

        public string XMLData(string id)
        {
            return "You requested workorder " + id;
        }

        //public string JSONData(string id)
        //{
        //    return "You requested product " + id;
        //}

        public WorkOrder.Workorder JSONData(string ID)
        {
            DALDB dal = new DALDB(connStr);
            int id = int.Parse(ID);
            Workorder wo = dal.GetWorkorder(id);
            //string strEmp = emp.getEmployeeName(id);

            //MemoryStream stream1 = new MemoryStream();
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Employee));
            //ser.WriteObject(stream1, emp);
            //string json = Encoding.Default.GetString(stream1.ToArray());

            return wo;
        }
        public List<WorkOrder.Workorder> JSONDataAll()
        {
            DALDB dal = new DALDB(connStr);

            List<Workorder> lstWO = new List<Workorder>();
            lstWO = dal.GetWorkorders();
            //string json = "";
            //MemoryStream stream1 = new MemoryStream();
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<Employee>));


            //ser.WriteObject(stream1, lstEmps);

            return lstWO;
        }

        public int UpdateWorkorder(Stream stream)
        {
            DALDB dal = new DALDB(connStr);
            WorkOrder.Workorder wo = new Workorder();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(WorkOrder.Workorder));
            wo = (WorkOrder.Workorder)ser.ReadObject(stream);

            if (wo != null)
            {
                if (dal.GetWorkorder(wo.wo_ID) != null)
                {
                    dal.UpdateWorkorder(wo);
                }
                return 1;
            }
            dal.AddWorkorder(wo);
            //string fileName = Server.MapPath("Output.txt");
            // write a text file
            FileStream fs = new FileStream(@"c:\xworkorders.txt", FileMode.Append);
            TextWriter tws = new StreamWriter(fs);


            // write the current datetime to the stream
            tws.WriteLine(DateTime.Now);

            // write test strings to the stream
            tws.WriteLine(wo.wo_ID);
            tws.WriteLine(wo.wo_Number);
            tws.WriteLine(wo.wo_Date);
            tws.WriteLine(wo.wo_Debtor);
            tws.WriteLine(wo.wo_Address);
            tws.WriteLine(wo.wo_StartTime);

            tws.Close();   // close the stream


            return 1;
        }
    }
}
