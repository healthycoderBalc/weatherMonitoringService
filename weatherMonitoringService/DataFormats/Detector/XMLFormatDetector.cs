using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace weatherMonitoringService.DataFormats.Detector
{
    public class XMLFormatDetector : IFormatDetector
    {
        public bool IsThisFormat(string input)
        {
            try
            {
                XElement.Parse(input);
                return true;
            }
            catch (XmlException)
            {
                return false;
            }
        }
    }
}
