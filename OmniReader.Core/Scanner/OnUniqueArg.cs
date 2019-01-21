using OmniReader.Data.Database.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniReader.Core.Scanner
{
    public class OnUniqueArg : EventArgs
    {
        //private string m_Message;

        //public string Message
        //{
        //    get { return m_Message; }
        //}

        //public OnUniqueArg(string message)
        //{
        //    m_Message = message;
        //}

        private ScanData m_ScanData;

        public ScanData ScanData
        {
            get { return m_ScanData; }
        }

        public OnUniqueArg(ScanData scanData)
        {
            m_ScanData = scanData;
        }
    }
}
