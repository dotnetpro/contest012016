using contestrunner.app.data;
using System;
using System.Diagnostics;
namespace contestrunner.app.sandboxAppDomain
{
	public class Beitrag_laden
	{
		private const string SUBMISSION_STANDARD_ASSEMBLY_FILENAME = "contest.submission";
		private const string SUBMISSION_STANDARD_TYPENAME = "contest.submission.Solution";
		private readonly string _submission_assembly_filename;
		private readonly string _submission_typename;
		public event Action<Prüfungsauftrag> Result;
		public Beitrag_laden() : this("contest.submission", "contest.submission.Solution")
		{
		}
		internal Beitrag_laden(string submission_assembly_filename, string submission_typename)
		{
			this._submission_assembly_filename = submission_assembly_filename;
			this._submission_typename = submission_typename;
		}
		public void Process(Prüfungsauftrag auftrag)
		{
			Trace.TraceInformation("Beitrag in AppDomain laden: {0}, {1}, {2} in {3}", new object[]
			{
				auftrag.Auftritt.Beitragsverzeichnis, 
				this._submission_assembly_filename, 
				this._submission_typename, 
				AppDomain.CurrentDomain.FriendlyName
			});
			auftrag.Beitrag = Activator.CreateInstance(this._submission_assembly_filename, this._submission_typename).Unwrap();
			this.Result(auftrag);
		}
	}
}
