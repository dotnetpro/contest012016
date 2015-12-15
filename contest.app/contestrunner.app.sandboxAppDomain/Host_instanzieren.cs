using contestrunner.app.data;
using contestrunner.contract.host;
using System;
using System.Diagnostics;
namespace contestrunner.app.sandboxAppDomain
{
	public class Host_instanzieren
	{
		public event Action<Prüfungsauftrag> Result;
		public void Process(Prüfungsauftrag auftrag)
		{
			Trace.TraceInformation("Host instanzieren: {0}; {1}", new object[]
			{
				auftrag.HostReferenz.AssemblyName, 
				auftrag.HostReferenz.TypeName
			});
			auftrag.Host = (IHost)Activator.CreateInstance(auftrag.HostReferenz.AssemblyName, auftrag.HostReferenz.TypeName).Unwrap();
			this.Result(auftrag);
		}
	}
}
