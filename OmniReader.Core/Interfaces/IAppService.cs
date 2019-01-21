using System;
using System.Collections.Generic;
using System.Text;

namespace OmniReader.Core.EMDK
{
    public interface IAppService
    {
        void CloseApp();
        void FinishAffinity();
    }
}
