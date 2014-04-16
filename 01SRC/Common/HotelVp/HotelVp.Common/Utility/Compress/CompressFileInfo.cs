using System;
using System.Collections.Generic;
using System.Text;

namespace HotelVp.Common.Utilities.Compress
{
    [Serializable]
    internal class CompressFileInfo
    {
        private string m_FileName;


        private byte[] m_FileBuffer;

        #region properties
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName
        {
            get { return m_FileName; }
            set { m_FileName = value; }
        }
        /// <summary>
        /// 文件包含的Byte数组
        /// </summary>
        public byte[] FileBuffer
        {
            get { return m_FileBuffer; }
            set { m_FileBuffer = value; }
        }
        #endregion 
    }
}
