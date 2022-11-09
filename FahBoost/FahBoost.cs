using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

namespace FahBoost {
	public partial class FahBoost : ServiceBase {
		private Thread worker;
		public FahBoost() => InitializeComponent();

		private bool run = true;
		private bool running = false;

		private void WorkerLoop() {
			running = true;
			var allowList = 0;
			for(var n = 0; n < Environment.ProcessorCount; n++) {
				if(n < Environment.ProcessorCount - 2)
					allowList |= 1 << n;
			}
			while(run) {
				var plist = Process.GetProcesses();
				foreach(var p in plist) {
					if(p.ProcessName.StartsWith("FahCore", StringComparison.OrdinalIgnoreCase)) {
						try {
							if(p.PriorityClass != ProcessPriorityClass.High) {
								p.PriorityClass = ProcessPriorityClass.High;
								p.ProcessorAffinity = new IntPtr((1 << (Environment.ProcessorCount - 2)) | (1 << (Environment.ProcessorCount - 1)));
							}
						}
						catch(Exception ex) {
							throw ex;
						}
					}
					else {
						try {
							p.ProcessorAffinity = new IntPtr(allowList);
						}
						catch { }
					}
				}
				for(var n = 0; n < 60 * 10 && run; n++)
					Thread.Sleep(100);
			}
			running = false;
		}
		protected override void OnStart(string[] args) {
			run = true;
			worker = new Thread(WorkerLoop);
			worker.Start();
			for(var n = 0; n < 25 && !running; n++)
				Thread.Sleep(50);
			if(!running) {
				Environment.Exit(3);
				throw new InvalidProgramException("Thread not started!");
			}
		}

		protected override void OnStop() {
			run = false;
			worker.Join();
		}
	}
}
