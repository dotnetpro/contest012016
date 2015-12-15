using contestrunner.app.viewmodel;
using System;
using System.Linq;
namespace contestrunner.app.view
{
	public class ConsoleView
	{
		public event Action<string> Auswahl;
		public void Abfragen(ViewModel viewmodel)
		{
			Console.WriteLine("dotnetpro Contest Runner V 1.0\n");
			Console.WriteLine("Zu prüfende Beiträge wählen:\n");
			viewmodel.Wahlmöglichkeiten.ToList<string>().ForEach(new Action<string>(Console.WriteLine));
			Console.Write("\nNr des Beitrags: ");
			string obj = Console.ReadLine();
			this.Auswahl(obj);
		}
		public void Prüfprotokoll_anzeigen(string eintrag)
		{
			Console.WriteLine(eintrag);
		}
	}
}
