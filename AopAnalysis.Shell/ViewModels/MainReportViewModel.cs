using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using Caliburn.Micro;
using System.Collections.ObjectModel;

namespace AopAnalysis.Shell.ViewModels
{
    [Export(typeof(MainReportViewModel))]
 public  class MainReportViewModel : PropertyChangedBase
    {
    }
}
