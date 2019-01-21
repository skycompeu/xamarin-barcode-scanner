using System;
using System.Collections.Generic;
using System.Text;

namespace OmniReader.Core.EMDK
{
    public class EMDKStatusChangedArgs
    {
        private string scannerState;
        public string ScannerState { get { return scannerState; } }

        public EMDKStatusChangedArgs(string state)
        {
            scannerState = state;
        }
    }
}
