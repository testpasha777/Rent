using Microsoft.Owin;
using Rent.App_Start;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Rent
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(DoWork);
            worker.WorkerReportsProgress = false;
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerCompleted +=
                   new RunWorkerCompletedEventHandler(WorkerCompleted);

            // Calling the DoWork Method Asynchronously
            worker.RunWorkerAsync(); //we can also pass parameters to the async method....
        }

        private static void DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(5000000);
        }

        private static void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if (worker != null)
            {
                // sleep for 20 secs and again call DoWork to get FxRates..we can increase the time to sleep and make it configurable to the needs
                System.Threading.Thread.Sleep(50000);
                worker.RunWorkerAsync();
            }
        }
    }
}
