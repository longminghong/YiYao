using System;
using Prism.Modularity;
using Prism.Regions;
using Microsoft.Practices.Unity;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WebService
{
    public class WebServiceModule : IModule
    {
        IUnityContainer mContainer;
        public WebServiceModule(IUnityContainer container)
        {
            mContainer = container;
        }

        public void Initialize()
        {
            mContainer.RegisterType<HealthDataService, HealthDataService>(new ContainerControlledLifetimeManager());
        }
    }
}
