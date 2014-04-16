using System;
using System.Collections.Generic;
using System.Text;

namespace HotelVp.Common.Utilities.Encryption
{
    [Serializable]
    public enum EncryptionAlgorithm
    {
        Des = 1,
        Rc2,
        Rijndael,
        TripleDes
    }
}
