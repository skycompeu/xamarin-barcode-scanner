using System;
using System.Collections.Generic;
using System.Text;

namespace OmniReader.Core.EMDK
{
    public class EMDKDataReceivedArgs
    {
        private string barcodeData;
        public string Data { get { return barcodeData; } }

        private string barcodeType;
        public string BarcodeType { get { return barcodeType; } }

        public EMDKDataReceivedArgs(string dataIn, string barcodeTypeIn)
        {
            barcodeData = dataIn;
            barcodeType = barcodeTypeIn;
        }
    }
}
