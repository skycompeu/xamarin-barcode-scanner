using System;
using System.Collections.Generic;
using System.Text;

namespace OmniReader.Core.EMDK
{
    public interface IAudioNotify
    {
        void Ok();
        void Error();
        void NoUnique();
    }
}
