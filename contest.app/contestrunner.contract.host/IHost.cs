using System;
namespace contestrunner.contract.host
{
	public interface IHost
	{
		event Action<Prüfungsanfang> Anfang;
		event Action<Prüfungsstatus> Status;
		event Action<Prüfungsende> Ende;
		event Action<Prüfungsfehler> Fehler;
		void Prüfen(object beitrag, string wettbewerbspfad, string beitragsverzeichnis);
	}
}
