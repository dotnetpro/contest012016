using contestrunner.app.data;
using contestrunner.app.sandboxAppDomain;
using System;
using System.Reflection;
namespace contestrunner.app.mainAppDomain
{
	public class Host_in_AppDomain_starten
	{
		private readonly string _sandboxMainboardAssemblyName;
		private readonly string _sandboxMainboardTypeName;
		private AppDomainRunnerFactory _runnerFactory;
		public Host_in_AppDomain_starten() : this(Assembly.GetExecutingAssembly().FullName, typeof(SandboxMainboard).FullName)
		{
		}
		internal Host_in_AppDomain_starten(string sandboxMainboardAssemblyName, string sandboxMainboardTypeName)
		{
			this._sandboxMainboardAssemblyName = sandboxMainboardAssemblyName.Split(new char[]
			{
				','
			})[0];
			this._sandboxMainboardTypeName = sandboxMainboardTypeName;
		}
		public void Inject(AppDomainRunnerFactory independent)
		{
			this._runnerFactory = independent;
		}
		public void Process(Sandbox sandbox)
		{
			try
			{
				this._runnerFactory.Run(sandbox.SandboxedAppDomain, this._sandboxMainboardAssemblyName, this._sandboxMainboardTypeName, new object[]
				{
					new Prüfungsauftrag
					{
						Auftritt = sandbox.Auftritt, 
						HostReferenz = new HostReferenz
						{
							AssemblyName = sandbox.HostTyp.Assembly.FullName.Split(new char[]
							{
								','
							})[0], 
							TypeName = sandbox.HostTyp.FullName
						}
					}
				});
			}
			finally
			{
				AppDomain.Unload(sandbox.SandboxedAppDomain);
			}
		}
	}
}
