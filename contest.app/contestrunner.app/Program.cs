using contestrunner.app.mainAppDomain;
using System;
namespace contestrunner.app
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			Mainboard mainboard = new Mainboard();
			mainboard.Run();
		}
	}
}
