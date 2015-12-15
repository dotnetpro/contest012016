using contestrunner.app.data;
using contestrunner.app.view;
using contestrunner.app.viewmodel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
namespace contestrunner.app.mainAppDomain
{
	public class Auftritte_auswählen
	{
		private readonly Action<IEnumerable<Auftritt>> _process;
		public event Action<Auftritt> Ausgewählt;
		public Auftritte_auswählen()
		{
			ViewModelMapper viewModelMapper = new ViewModelMapper();
			ConsoleView consoleView = new ConsoleView();
			this._process = new Action<IEnumerable<Auftritt>>(viewModelMapper.Auftritte_nach_ViewModel);
			viewModelMapper.ViewModel += new Action<ViewModel>(consoleView.Abfragen);
			consoleView.Auswahl += new Action<string>(viewModelMapper.Auftrittsauswahl_nach_Auftritte);
			viewModelMapper.Gewählter_Auftritt += delegate(Auftritt _)
			{
				this.Ausgewählt(_);
			}
			;
		}
		public void Process(IEnumerable<Auftritt> auftritte)
		{
			Trace.TraceInformation("Auftritte auswählen: {0}", new object[]
			{
				auftritte.Count<Auftritt>()
			});
			this._process(auftritte);
		}
	}
}
