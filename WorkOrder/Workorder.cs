//-------------------------------------------
// <copyright file="Workorder.cs" company="">
// </copyright>
// <author>andrei.stoica</author>
// <date>2012.02.09 02:10</date>
// <summary>
// </summary>
//-------------------------------------------
//-------------------------------------------
// <copyright file="Workorder.cs" company="">
// </copyright>
// <author>andrei.stoica</author>
// <date>2012.02.09 02:23</date>
// <summary>
// </summary>
//-------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WorkOrder
{
    [DataContract]
    public class Workorder
    {
        [DataMember]
        public int wo_ID { get; set; }
        [DataMember]
        public int wo_Number { get; set; }
        [DataMember]
        public string wo_Address { get; set; }
        [DataMember]
        public DateTime wo_Date { get; set; }
        [DataMember]
        public DateTime wo_StartTime { get; set; }

        public string wo_Debtor { get; set; }

        /*
        public string getEmployeeName(int empCode)
        {
            string str = this.FirstName + " " + this.LastName;
            return str;
        }
         * */
    }
}