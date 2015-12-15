using contestrunner.app.data;
using contestrunner.contract.host;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
namespace contestrunner.app.mainAppDomain
{
	public class Host_Typ_bestimmen
	{
		private const string HOST_STANDARD_ASSEMBLY_FILENAME = "contest.host.dll";
		private readonly string _host_assembly_filename;
		public event Action<Sandbox> Result;
		public Host_Typ_bestimmen() : this("contest.host.dll")
		{
		}
		internal Host_Typ_bestimmen(string host_assembly_filename)
		{
			this._host_assembly_filename = host_assembly_filename;
		}
		public void Process(Auftritt auftritt)
		{
			Trace.TraceInformation("Host Typ bestimmen: {0}", new object[]
			{
				auftritt.Wettbewerbspfad
			});
			string text = auftritt.Wettbewerbspfad + "\\" + this._host_assembly_filename;
			Assembly hostAssm = Assembly.LoadFile(text);
			Type[] array = Host_Typ_bestimmen.Host_Typ_in_Assembly_finden(hostAssm);
			Host_Typ_bestimmen.Host_Typ_validieren(array, text);
			Sandbox obj = new Sandbox
			{
				Auftritt = auftritt, 
				HostTyp = array[0]
			};
			this.Result(obj);
		}
		internal static Type[] Host_Typ_in_Assembly_finden(Assembly hostAssm)
		{
			return (
				from t in hostAssm.GetTypes()
				where (
					from i in t.GetInterfaces()
					where i == typeof(IHost)
					select i).Any<Type>()
				select t).ToArray<Type>();
		}
		internal static void Host_Typ_validieren(IEnumerable<Type> hostTypes, string hostAssemblyName)
		{
			if (hostTypes.Count<Type>() == 0)
			{
				throw new InvalidOperationException("No host type found in " + hostAssemblyName);
			}
			if (hostTypes.Count<Type>() > 1)
			{
				throw new InvalidOperationException("More than one host type found in " + hostAssemblyName);
			}
		}
	}
}
