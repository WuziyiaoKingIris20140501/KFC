using System;
using System.Collections.Generic;
using System.Text;

using System.Xml.Serialization;

namespace HotelVp.Common.DataConfiguration
{
    [XmlRoot("configFileList")]
    public class ConfigurationFileList
    {
        private List<ConfigurationFile> _configurationList;

        [XmlElement("configFile")]
        public List<ConfigurationFile> ConfigurationList
        {
            get
            {
                return this._configurationList;
            }
            set
            {
                this._configurationList = value;
            }
        }
    }


    public class ConfigurationFile
    {
        private string _kye;
        private string _path;
        private bool _isAbsolute = false;

        [XmlAttribute("key")]
        public string Key
        {
            get
            {
                return this._kye;
            }
            set
            {
                this._kye = value;
            }
        }


        [XmlAttribute("path")]
        public string Path
        {
            get
            {
                return this._path;
            }
            set
            {
                this._path = value;
            }
        }


        [XmlAttribute("isAbsolute")]
        public bool IsAbsolute
        {
            get
            {
                return this._isAbsolute;
            }
            set
            {
                this._isAbsolute = value;
            }
        }
    }
}
