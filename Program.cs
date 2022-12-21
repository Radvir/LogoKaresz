using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogoKaresz
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
            #region command
            void In(string command = "start msedge.exe https://www.youtube.com/watch?v=dQw4w9WgXcQ")
            {
                int exitCode;
                ProcessStartInfo processInfo;
                Process process;

                processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;
                // *** Redirect the output ***
                processInfo.RedirectStandardError = true;
                processInfo.RedirectStandardOutput = true;

                process = Process.Start(processInfo);
                process.WaitForExit();

                // *** Read the streams ***
                // Warning: This approach can lead to deadlocks, see Edit #2
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                exitCode = process.ExitCode;

                Console.WriteLine("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
                Console.WriteLine("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
                Console.WriteLine("ExitCode: " + exitCode.ToString(), "ExecuteCommand");
                process.Close();
            }
            #endregion
            In("powershell -File volume.ps1");
            var player = new SoundPlayer("./rickroll.wav");
            player.Play();
            Application.Run(new Form1());
		}
	}
}
