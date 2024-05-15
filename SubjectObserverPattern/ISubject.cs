﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherMonitoringService.SubjectObserverPattern
{
    public interface ISubject
    {
        void Attach(IObserver observer);
        void Notify(string PropertyName);
    }
}
