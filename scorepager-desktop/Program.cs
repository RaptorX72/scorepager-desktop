using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using scorepager_desktop.Forms;
using scorepager_desktop.Classes;

namespace scorepager_desktop {
	static class Program {
		private static FirebaseClient client;
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			client = FirebaseClient.GetInstance();
			Application.Run(new MainForm());
		}
	}
}
