//-------------------------------------------------
// <copyright file="IWORServiceImpl.cs" company="">
// </copyright>
// <author>andrei.stoica</author>
// <date>2012.02.09 03:49</date>
// <summary>
// </summary>
//-------------------------------------------------
using System.ServiceModel;
using System.ServiceModel.Web;
using WorkOrder;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.IO;

namespace WorkOrdersRService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWORServiceImpl" in both code and config file together.
    [ServiceContract]
    public interface IWORServiceImpl
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "xml/{id}")]
        string XMLData(string id);

        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Wrapped,
        //    UriTemplate = "json/{id}")]
        //string JSONData(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "workorder/{id}")]
        WorkOrder.Workorder JSONData(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "workorders")]
        List<WorkOrder.Workorder> JSONDataAll();

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "workorder")]
        int UpdateWorkorder(Stream json);
    }
  
}
