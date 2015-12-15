using System;
using System.Diagnostics;
using System.Reflection;
namespace contestrunner.app
{
	public class AppDomainRunner : MarshalByRefObject
	{
		public void Run(string assemblyName, string typeName, params object[] args)
		{
			Trace.TraceInformation("AppDomainRunner: {0}, {1} in {2} / full trust? {3}", new object[]
			{
				assemblyName, 
				typeName, 
				AppDomain.CurrentDomain.FriendlyName, 
				base.GetType().Assembly.IsFullyTrusted
			});
			Assembly assembly = Assembly.Load(assemblyName);
			Type type = assembly.GetType(typeName);
			object obj = Activator.CreateInstance(type);
			IAppDomainRunnerEntryPoint appDomainRunnerEntryPoint = (IAppDomainRunnerEntryPoint)obj;
			appDomainRunnerEntryPoint.Run(args);
		}
	}
}
