using contestrunner.app.data;
using System;
namespace contestrunner.app.mainAppDomain
{
	public class Auftritte_prüfen
	{
		private readonly Action<Auftritt> _process;
		public Auftritte_prüfen()
		{
			Host_Typ_bestimmen host_Typ_bestimmen = new Host_Typ_bestimmen();
			AppDomain_erzeugen appDomain_erzeugen = new AppDomain_erzeugen();
			Host_in_AppDomain_starten host_in_AppDomain_starten = new Host_in_AppDomain_starten();
			this._process = new Action<Auftritt>(host_Typ_bestimmen.Process);
			host_Typ_bestimmen.Result += new Action<Sandbox>(appDomain_erzeugen.Process);
			appDomain_erzeugen.Result += new Action<Sandbox>(host_in_AppDomain_starten.Process);
			host_in_AppDomain_starten.Inject(new AppDomainRunnerFactory());
		}
		public void Process(Auftritt input)
		{
			this._process(input);
		}
	}
}
