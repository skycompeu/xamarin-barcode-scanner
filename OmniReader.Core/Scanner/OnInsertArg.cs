using OmniReader.Data.Database.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniReader.Core.Scanner
{
    public class OnInsertArg : EventArgs
    {
        private ScanData m_ScanData;

        public ScanData ScanData
        {
            get { return m_ScanData; }
        }
        
        public OnInsertArg(ScanData scanData)
        {
            m_ScanData = scanData;
        }

    }
}
