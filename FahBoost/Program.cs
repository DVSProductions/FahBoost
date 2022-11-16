using System.ServiceProcess;

namespace FahBoost {
	internal static class Program {
		private static void Main() => ServiceBase.Run(new[] { new FahBoost() });
	}
}
