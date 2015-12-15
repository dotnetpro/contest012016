using contestrunner.app.data;
using System;
using System.Collections.Generic;
namespace contestrunner.app.mainAppDomain
{
	public class Mainboard
	{
		public void Run()
		{
			Auftritte_sammeln auftritte_sammeln = new Auftritte_sammeln();
			Auftritte_auswählen auftritte_auswählen = new Auftritte_auswählen();
			Auftritte_prüfen @object = new Auftritte_prüfen();
			auftritte_sammeln.Gefunden += new Action<IEnumerable<Auftritt>>(auftritte_auswählen.Process);
			auftritte_auswählen.Ausgewählt += new Action<Auftritt>(@object.Process);
			auftritte_sammeln.Run(new string[0]);
		}
	}
}
