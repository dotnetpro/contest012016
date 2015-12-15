using System;
namespace contestrunner.contract.host
{
	public class Prüfungsmeldung
	{
		public DateTime Timestamp
		{
			get;
			private set;
		}
		protected Prüfungsmeldung()
		{
			this.Timestamp = DateTime.Now;
		}
	}
}
