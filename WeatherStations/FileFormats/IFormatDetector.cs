using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherMonitoringService.WeatherStations.FileFormats
{
    public interface IFormatDetector
    {
        bool IsThisFormat(string input);
    }
}
