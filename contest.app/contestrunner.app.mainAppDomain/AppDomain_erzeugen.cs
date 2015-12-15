using contestrunner.app.data;
using System;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
namespace contestrunner.app.mainAppDomain
{
	public class AppDomain_erzeugen
	{
		public event Action<Sandbox> Result;
		public void Process(Sandbox sandbox)
		{
			Trace.TraceInformation("AppDomain erzeugen");
			PermissionSet permissionSet = AppDomain_erzeugen.Create_permission_set_for_sandbox_AppDomain(sandbox.Auftritt.Wettbewerbspfad + "\\" + sandbox.Auftritt.Beitragsverzeichnis);
			AppDomainSetup info = AppDomain_erzeugen.Create_setup_for_AppDomain(sandbox.Auftritt);
			StrongName strongName = AppDomain_erzeugen.Get_strong_name_of_assembly(typeof(AppDomainRunner));
			StrongName strongName2 = AppDomain_erzeugen.Get_strong_name_of_assembly(sandbox.HostTyp);
			sandbox.SandboxedAppDomain = AppDomain.CreateDomain("Sandbox", null, info);
			this.Result(sandbox);
		}
		private static PermissionSet Create_permission_set_for_sandbox_AppDomain(string beitragspfad)
		{
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
			permissionSet.AddPermission(new FileIOPermission(FileIOPermissionAccess.AllAccess, beitragspfad));
			return permissionSet;
		}
		private static StrongName Get_strong_name_of_assembly(Type type)
		{
			return type.Assembly.Evidence.GetHostEvidence<StrongName>();
		}
		private static AppDomainSetup Create_setup_for_AppDomain(Auftritt auftritt)
		{
			string fileName = Path.GetFileName(auftritt.Wettbewerbspfad);
			return new AppDomainSetup
			{
				ApplicationBase = AppDomain.CurrentDomain.BaseDirectory, 
				PrivateBinPath = string.Format("{0};{0}\\{1}", fileName, auftritt.Beitragsverzeichnis), 
				DisallowBindingRedirects = true, 
				DisallowCodeDownload = true, 
				DisallowPublisherPolicy = true
			};
		}
	}
}
