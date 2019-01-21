using System;
using System.Collections.Generic;
using System.Text;

namespace OmniReader.Core.Scanner
{
    public class OnErrorArg : EventArgs
    {
        private string m_Message;

        public string Message
        {
            get { return m_Message; }
        }

        public OnErrorArg(string message)
        {
            m_Message = message;
        }
    }
}
