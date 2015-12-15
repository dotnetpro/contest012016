using System;
using System.Diagnostics;
using System.Security;
namespace contestrunner.app
{
	public class AppDomainRunnerFactory
	{
		[SecuritySafeCritical]
		public void Run(AppDomain appDomain, string assemblyName, string typeName, params object[] args)
		{
			Trace.TraceInformation("AppDomainRunnerFactory: {0}, {1}, {2}", new object[]
			{
				appDomain.FriendlyName, 
				assemblyName, 
				typeName
			});
			AppDomainRunner appDomainRunner = (AppDomainRunner)Activator.CreateInstanceFrom(appDomain, typeof(AppDomainRunner).Assembly.FullName.Split(new char[]
			{
				','
			})[0] + ".exe", typeof(AppDomainRunner).FullName).Unwrap();
			appDomainRunner.Run(assemblyName, typeName, args);
		}
	}
}
