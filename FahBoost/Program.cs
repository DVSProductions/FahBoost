using System.ServiceProcess;

namespace FahBoost {
	internal static class Program {
		private static void Main() {
			ServiceBase[] ServicesToRun;
			ServicesToRun = new ServiceBase[]
			{
				new FahBoost()
			};
			ServiceBase.Run(ServicesToRun);
		}
	}
}
