#region -   版   权   信   息  -
/*======================================================
 *  
 *      创 建 人：rdc-yangxing
 *      创建时间：2013/05/29 15:47:13
 *      公    司：南京焦点科技股份有限公司
 *      功    能：
 *      修改纪录：
 *        
 *  ======================================================*/
#endregion
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using Caliburn.Micro;
using AopAnalysis.Shell.ViewModels;
namespace AopAnalysis.Shell
{
    class AppBootstrapper : Bootstrapper<MainReportViewModel>
    {
        private CompositionContainer container;

        protected override void Configure()
        {
            container = new CompositionContainer(new AggregateCatalog(AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()));

            CompositionBatch batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(container);

            container.Compose(batch);
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = container.GetExportedValues<object>(contract);



            if (exports.Count() > 0)
            {
                return exports.FirstOrDefault();
            }

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }
    }
}
