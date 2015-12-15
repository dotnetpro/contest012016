using contestrunner.app.data;
using contestrunner.contract.host;
using System;
namespace contestrunner.app.sandboxAppDomain
{
	public class SandboxMainboard : IAppDomainRunnerEntryPoint
	{
		public void Run(object[] args)
		{
			Host_instanzieren host_instanzieren = new Host_instanzieren();
			Beitrag_laden beitrag_laden = new Beitrag_laden();
			Host_prüft_Beitrag host_prüft_Beitrag = new Host_prüft_Beitrag();
			Ergebnis_protokollieren @object = new Ergebnis_protokollieren();
			host_instanzieren.Result += new Action<Prüfungsauftrag>(beitrag_laden.Process);
			beitrag_laden.Result += new Action<Prüfungsauftrag>(host_prüft_Beitrag.Process);
			host_prüft_Beitrag.Anfang += new Action<Prüfungsanfang>(@object.Aufzeichnungsbeginn);
			host_prüft_Beitrag.Ende += new Action<Prüfungsende>(@object.Aufzeichnungsende);
			host_prüft_Beitrag.Status += new Action<Prüfungsstatus>(@object.Statusänderung);
			host_prüft_Beitrag.Fehler += new Action<Prüfungsfehler>(@object.Fehler);
			host_instanzieren.Process((Prüfungsauftrag)args[0]);
		}
	}
}
