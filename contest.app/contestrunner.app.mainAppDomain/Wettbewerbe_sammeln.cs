using System;
using System.Collections.Generic;
using System.IO;
namespace contestrunner.app.mainAppDomain
{
	public class Wettbewerbe_sammeln
	{
		private string _rootPath;
		public event Action<IEnumerable<string>> Result;
		public Wettbewerbe_sammeln() : this(Environment.CurrentDirectory)
		{
		}
		internal Wettbewerbe_sammeln(string rootPath)
		{
			this._rootPath = rootPath;
		}
		public void Run(params string[] args)
		{
			this.Result(Directory.GetDirectories(this._rootPath));
		}
	}
}
