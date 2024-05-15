using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.WeatherBots;

namespace weatherMonitoringService.SubjectObserverPattern
{
    public interface IObserver
    {
        void Update(ISubject subject, string property);
    }
}
