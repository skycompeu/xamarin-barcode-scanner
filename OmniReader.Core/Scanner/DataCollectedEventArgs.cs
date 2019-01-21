using System;
using System.Collections.Generic;
using System.Text;

namespace OmniReader.Core.Scanner
{
    public class DataCollectedEventArgs
    {
        private string barcodeData;

        public DataCollectedEventArgs(string dataIn, string barcodeTypeIn)
        {
            barcodeData = dataIn;
            barcodeType = barcodeTypeIn;
        }

        public string Data { get { return barcodeData; } }

        private string barcodeType;
        public string BarcodeType { get { return barcodeType; } }
    }
}
