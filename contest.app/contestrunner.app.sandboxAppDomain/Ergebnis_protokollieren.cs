using contestrunner.app.view;
using contestrunner.app.viewmodel;
using contestrunner.contract.host;
using System;
using System.Diagnostics;
namespace contestrunner.app.sandboxAppDomain
{
	public class Ergebnis_protokollieren
	{
		private readonly Action<Prüfungsanfang> _aufzeichnungsbeginn;
		private readonly Action<Prüfungsende> _aufzeichnungsende;
		private readonly Action<Prüfungsstatus> _statusänderung;
		private readonly Action<Prüfungsfehler> _fehler;
		public Ergebnis_protokollieren()
		{
			ViewModelMapper viewModelMapper = new ViewModelMapper();
			ConsoleView @object = new ConsoleView();
			this._aufzeichnungsbeginn = (Action<Prüfungsanfang>)Delegate.Combine(this._aufzeichnungsbeginn, new Action<Prüfungsanfang>(viewModelMapper.Aufzeichnungsbeginn));
			this._aufzeichnungsende = (Action<Prüfungsende>)Delegate.Combine(this._aufzeichnungsende, new Action<Prüfungsende>(viewModelMapper.Aufzeichnungsende));
			this._statusänderung = (Action<Prüfungsstatus>)Delegate.Combine(this._statusänderung, new Action<Prüfungsstatus>(viewModelMapper.Statusänderung));
			this._fehler = (Action<Prüfungsfehler>)Delegate.Combine(this._fehler, new Action<Prüfungsfehler>(viewModelMapper.Fehler));
			viewModelMapper.Prüfprotokolleintrag += new Action<string>(@object.Prüfprotokoll_anzeigen);
		}
		public void Aufzeichnungsbeginn(Prüfungsanfang anfang)
		{
			Trace.TraceInformation("Ergebnis protokollieren - Anfang");
			this._aufzeichnungsbeginn(anfang);
		}
		public void Aufzeichnungsende(Prüfungsende ende)
		{
			Trace.TraceInformation("Ergebnis protokollieren - Ende");
			this._aufzeichnungsende(ende);
		}
		public void Statusänderung(Prüfungsstatus status)
		{
			Trace.TraceInformation("Ergebnis protokollieren - Statusänderung");
			this._statusänderung(status);
		}
		public void Fehler(Prüfungsfehler fehler)
		{
			Trace.TraceInformation("Ergebnis protokollieren - Fehler");
			this._fehler(fehler);
		}
	}
}
