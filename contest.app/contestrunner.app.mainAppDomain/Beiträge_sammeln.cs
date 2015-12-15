using contestrunner.app.data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace contestrunner.app.mainAppDomain
{
	public class Beiträge_sammeln
	{
		public event Action<IEnumerable<Auftritt>> Result;
		public void Process(IEnumerable<string> wettbewerbspfade)
		{
			this.Result(wettbewerbspfade.ToList<string>().SelectMany(new Func<string, IEnumerable<Auftritt>>(Beiträge_sammeln.Beitrag_sammeln)));
		}
		internal static IEnumerable<Auftritt> Beitrag_sammeln(string wettbewerbspfad)
		{
			return 
				from beitragspfad in Directory.GetDirectories(wettbewerbspfad).ToList<string>()
				select new Auftritt
				{
					Wettbewerbspfad = wettbewerbspfad, 
					Beitragsverzeichnis = Path.GetFileName(beitragspfad)
				};
		}
	}
}
