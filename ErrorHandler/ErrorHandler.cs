//----------------------------------------------
// <copyright file="ErrorHandler.cs" company="">
// </copyright>
// <author>andrei.stoica</author>
// <date>2012.02.09 02:28</date>
// <summary>
// </summary>
//----------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ErrorHandler

{

    //Class responsible for handling error messages
    public class ErrorHandler
    {
        static StringBuilder errMessage = new StringBuilder();

        //Make class immutable
        static ErrorHandler()
        {
        }
        /// <summary>
        /// Prperty - holds exception messages encountered 
        /// at code execution
        /// </summary>
        public string ErrorMessage
        {
            get { return errMessage.ToString(); }
            set
            {
                errMessage.AppendLine(value);
            }
        }
    }
    
}
