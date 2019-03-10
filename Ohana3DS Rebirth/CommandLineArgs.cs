using System;
using System.IO;

namespace Ohana3DS_Rebirth
{
    class CommandLineArgs
    {
        private const string helpText =
@"Ohana3DS-Rebirth Command line Syntax:

Ohana3DSRebirth
    Start the GUI of Ohana 3DS Rebirth
Ohana3DSRebirth [filename]
    Start the GUI of Ohana 3DS Rebirth with [filename] loaded.
Ohana3DSRebirth --batch [inputfolder] [OPTIONS]
    Start Ohana 3DS Rebirth in BATCH mode. It will export all files contained in [inputfolder].
Ohana3DSRebirth --help
    Show this help

Options for --batch mode:
    --batch / -b: Must be first argument in batch mode
    --output [output] / -o [output]: specify output directory
    --models: export models
    --textures: export textures
    --format {dae|smd|obj|cmdl} / -f {dae|smd|obj|cmdl}: Specify export format for the model
";


        public bool batchMode { get; private set; } = false;

        public bool hasFile { get; private set; } = false;
        public string filename { get; private set; } = "";

        public string outputFolder { get; private set; } = ".";
        public string inputFolder { get; private set; }
        public bool exportModels { get; private set; } = false;
        public bool exportTextures { get; private set; } = false;
        public int modelFormat { get; private set; } = 0;

        public CommandLineArgs(String[] args)
        {
            if (args.Length < 1)
                return;


            if (args[0].Equals("--help") || args[0].Equals("-h"))
            {
                Console.Write(helpText);
                Environment.Exit(0);
            }

            if(args[0].Equals("--batch") || args[0].Equals("-b"))
            {
                batchMode = true;

                if(args.Length < 2)
                {
                    Console.Error.WriteLine("Syntax Error.\n" + helpText);
                    Environment.Exit(-1);
                }

                inputFolder = args[1];

                for(int i = 2; i < args.Length; i++)
                {
                         if (args[i].Equals("--models") || args[i].Equals("-m")) exportModels = true;
                    else if (args[i].Equals("--textures") || args[i].Equals("-t")) exportTextures = true;
                    else if (args[i++].Equals("--output") || args[i].Equals("-o")) outputFolder = args[i];
                    else if (args[i++].Equals("--format") || args[i].Equals("-f"))
                    {
                        switch(args[i])
                        {
                            case "dae":
                            case "DAE":
                                modelFormat = 0;
                                break;
                            case "smd":
                            case "SMD":
                                modelFormat = 1;
                                break;
                            case "obj":
                            case "OBJ":
                                modelFormat = 2;
                                break;
                            case "cmdl":
                            case "CMDL":
                                modelFormat = 3;
                                break;
                            default:
                                Console.Error.WriteLine("format " + args[i] + "is not known. Possible options: dae, smd, obj, cmdl");
                                Environment.Exit(-1);
                                break;
                        }
                    }
                    else
                    {
                        Console.Error.WriteLine("Syntax error: unknown flag " + args[i] + " entered.");
                        Environment.Exit(-1);
                    }
                }
            } else
            {
                hasFile = args.Length > 0 && File.Exists(args[0]);
                if (hasFile) filename = args[0];
            }
        }
    }
}
