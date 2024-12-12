﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weatherMonitoringService.WeatherData;

namespace weatherMonitoringService.SubjectObserverPattern
{
    public interface IObserver
    {
        void Update(WeatherDataModel weatherData);
    }
}
