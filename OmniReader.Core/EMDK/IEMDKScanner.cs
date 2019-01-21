using System;
using System.Collections.Generic;
using System.Text;

namespace OmniReader.Core.EMDK
{
    public interface IEMDKScanner
    {
        event EventHandler<EMDKDataReceivedArgs> OnDataReceived;
        event EventHandler<EMDKStatusChangedArgs> OnStatusChanged;
        
        void EnableScanner();
        void DisableScanner();
        
        void StartScanner();
        void ResumeScanner();
        void PauseScanner();
        void DestroyScanner();

        void NotifyError();
        void NotifyWarning();
    }
}
