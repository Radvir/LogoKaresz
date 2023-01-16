using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace LogoKaresz
{
	static class Program
	{
        [DllImport("user32.dll")]
        public static extern int SendMessageW(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
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
            // Set the volume to the maximum level
            const int APPCOMMAND_VOLUME_MAX = 0x0a;
            SendMessageW(new IntPtr(0xffff), 0x319, new IntPtr(0), new IntPtr(APPCOMMAND_VOLUME_MAX * 0x10000));


            var player = new SoundPlayer("../../Properties/rickroll.wav");
            player.Play();
            Application.Run(new Form1());
		}
	}
}
