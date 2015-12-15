using contestrunner.app.data;
using contestrunner.contract.host;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace contestrunner.app.viewmodel
{
	public class ViewModelMapper
	{
		private IEnumerable<Auftritt> _auftritte;
		public event Action<ViewModel> ViewModel;
		public event Action<Auftritt> Gewählter_Auftritt;
		public event Action<string> Prüfprotokolleintrag;
		public void Auftritte_nach_ViewModel(IEnumerable<Auftritt> auftritte)
		{
			this._auftritte = auftritte;
			ViewModel viewModel = new ViewModel();
			viewModel.Wahlmöglichkeiten = 
				from a in auftritte.ToList<Auftritt>().Select((Auftritt a, int i) => new
				{
					Index = i + 1, 
					Wettbewerbsname = Path.GetFileName(a.Wettbewerbspfad), 
					Beitragsname = a.Beitragsverzeichnis
				})
				select string.Format("{0}. {1}/{2}", a.Index, a.Wettbewerbsname, a.Beitragsname);
			this.ViewModel(viewModel);
		}
		public void Auftrittsauswahl_nach_Auftritte(string auswahl)
		{
			int index = int.Parse(auswahl) - 1;
			this.Gewählter_Auftritt(this._auftritte.ElementAt(index));
		}
		public void Aufzeichnungsbeginn(Prüfungsanfang anfang)
		{
			this.Prüfprotokolleintrag(string.Format("\n{0}: PRÜFUNGSBEGINN für {1}/{2}", anfang.Timestamp, anfang.Wettbewerb, anfang.Beitrag));
		}
		public void Aufzeichnungsende(Prüfungsende ende)
		{
			this.Prüfprotokolleintrag(string.Format("{0}: PRÜFUNGSENDE, Dauer: {1}", ende.Timestamp, ende.Dauer));
		}
		public void Statusänderung(Prüfungsstatus status)
		{
			this.Prüfprotokolleintrag(string.Format("{0}: {1}", status.Timestamp, status.Statusmeldung));
		}
		public void Fehler(Prüfungsfehler fehler)
		{
			this.Prüfprotokolleintrag(string.Format("{0}: *** {1}\n", fehler.Timestamp, fehler.Fehler.Message, fehler.Fehler.StackTrace));
		}
	}
}
