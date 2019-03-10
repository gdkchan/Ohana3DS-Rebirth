using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Ohana3DS_Rebirth
{
    static class Program
    {
        // we want command line output for batch mode.
        // https://stackoverflow.com/a/7199024
        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;


        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // redirect console output to parent process;
            // must be before any calls to Console.WriteLine()
            AttachConsole(ATTACH_PARENT_PROCESS);

            var cmdArgs = new CommandLineArgs(args);
            if (cmdArgs.batchMode)
            {
                var batch = new BatchMode();

                Console.WriteLine("input Folder: " + cmdArgs.inputFolder);
                Console.WriteLine("output Folder: " + cmdArgs.outputFolder);
                Console.WriteLine("export models? " + cmdArgs.exportModels);
                Console.WriteLine("export textures? " + cmdArgs.exportTextures);
                Console.WriteLine("model format: " + cmdArgs.modelFormat);

                batch.openFolder(cmdArgs.inputFolder);

                if (cmdArgs.exportModels)
                    batch.exportModels(cmdArgs.outputFolder, cmdArgs.modelFormat);
                if (cmdArgs.exportTextures)
                    batch.exportTextures(cmdArgs.outputFolder);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                FrmMain form = new FrmMain();

                if (cmdArgs.hasFile) form.setFileToOpen(cmdArgs.filename);

                Application.Run(form);
            }
        }
    }
}
