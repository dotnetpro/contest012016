using contestrunner.app.data;
using contestrunner.contract.host;
using System;
using System.Diagnostics;
using System.IO;
namespace contestrunner.app.sandboxAppDomain
{
	public class Host_prüft_Beitrag
	{
		public event Action<Prüfungsanfang> Anfang;
		public event Action<Prüfungsstatus> Status;
		public event Action<Prüfungsende> Ende;
		public event Action<Prüfungsfehler> Fehler;
		public void Process(Prüfungsauftrag auftrag)
		{
			Trace.TraceInformation("Host prüft Beitrag: {0} / {1} in AppDomain {2}", new object[]
			{
				auftrag.Host.GetType().FullName, 
				Path.GetFileName(auftrag.Auftritt.Beitragsverzeichnis), 
				AppDomain.CurrentDomain.FriendlyName
			});
			try
			{
				auftrag.Host.Anfang += this.Anfang;
				auftrag.Host.Ende += this.Ende;
				auftrag.Host.Status += this.Status;
				auftrag.Host.Fehler += this.Fehler;
				auftrag.Host.Prüfen(auftrag.Beitrag, auftrag.Auftritt.Wettbewerbspfad, auftrag.Auftritt.Beitragsverzeichnis);
			}
			catch (Exception fehler)
			{
				this.Fehler(new Prüfungsfehler
				{
					Fehler = fehler
				});
			}
			finally
			{
				auftrag.Host.Anfang -= this.Anfang;
				auftrag.Host.Ende -= this.Ende;
				auftrag.Host.Status -= this.Status;
				auftrag.Host.Fehler -= this.Fehler;
			}
		}
	}
}
