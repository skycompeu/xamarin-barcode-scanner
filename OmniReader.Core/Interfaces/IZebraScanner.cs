using OmniReader.Core.Scanner;
using System;
using System.Collections.Generic;
using System.Text;

namespace OmniReader.Core.EMDK
{
    public interface IZebraScanner
    {
        event EventHandler<DataCollectedEventArgs> OnScanDataCollected;
        event EventHandler<string> OnStatusChanged;

        void Read();
        void Enable();
        void Disable();
        void SetConfig(IEMDKScannerConfig cnf);
    }
}
