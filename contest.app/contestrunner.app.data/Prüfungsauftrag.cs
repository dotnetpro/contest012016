using contestrunner.contract.host;
using System;
namespace contestrunner.app.data
{
	[Serializable]
	public class Prüfungsauftrag
	{
		public Auftritt Auftritt;
		public HostReferenz HostReferenz;
		public IHost Host;
		public object Beitrag;
	}
}
