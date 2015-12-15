using contestrunner.app.data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace contestrunner.app.mainAppDomain
{
	public class Auftritte_sammeln
	{
		private readonly Action<string[]> _run;
		public event Action<IEnumerable<Auftritt>> Gefunden;
		public Auftritte_sammeln()
		{
			Wettbewerbe_sammeln wettbewerbe = new Wettbewerbe_sammeln();
			Beiträge_sammeln beiträge_sammeln = new Beiträge_sammeln();
			this._run = delegate(string[] _)
			{
				wettbewerbe.Run(_);
			}
			;
			wettbewerbe.Result += new Action<IEnumerable<string>>(beiträge_sammeln.Process);
			beiträge_sammeln.Result += delegate(IEnumerable<Auftritt> _)
			{
				this.Gefunden(_);
			}
			;
		}
		public void Run(params string[] args)
		{
			Trace.TraceInformation("Auftritte sammeln");
			this._run(args);
		}
	}
}
