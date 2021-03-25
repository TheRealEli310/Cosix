using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.HAL;
using System.IO;

namespace CosixKernel.Apps.MIV
{
    class MIV
    {
        public static void printMIVStartScreen()
        {
            Terminal.Clear();
            Terminal.WriteLine("~");
            Terminal.WriteLine("~");
            Terminal.WriteLine("~");
            Terminal.WriteLine("~");
            Terminal.WriteLine("~");
            Terminal.WriteLine("~");
            Terminal.WriteLine("~");
            Terminal.WriteLine("~                               MIV - MInimalistic Vi");
            Terminal.WriteLine("~");
            Terminal.WriteLine("~                                  version 1.2");
            Terminal.WriteLine("~                             by Denis Bartashevich");
            Terminal.WriteLine("~                            Minor additions by CaveSponge");
            Terminal.WriteLine("~                    MIV is open source and freely distributable");
            Terminal.WriteLine("~");
            Terminal.WriteLine("~                     type :help<Enter>          for information");
            Terminal.WriteLine("~                     type :q<Enter>             to exit");
            Terminal.WriteLine("~                     type :wq<Enter>            save to file and exit");
            Terminal.WriteLine("~                     press i                    to write");
            Terminal.WriteLine("~");
            Terminal.WriteLine("~");
            Terminal.WriteLine("~");
            Terminal.WriteLine("~");
            Terminal.WriteLine("~");
            Terminal.WriteLine("~");
            Terminal.Write("~");
        }

        public static String stringCopy(String value)
        {
            String newString = String.Empty;

            for (int i = 0; i < value.Length - 1; i++)
            {
                newString += value[i];
            }

            return newString;
        }

        public static void printMIVScreen(char[] chars, int pos, String infoBar, Boolean editMode)
        {
            int countNewLine = 0;
            int countChars = 0;
            delay(10000000);
            Terminal.Clear();

            for (int i = 0; i < pos; i++)
            {
                if (chars[i] == '\n')
                {
                    Terminal.WriteLine("");
                    countNewLine++;
                    countChars = 0;
                }
                else
                {
                    Terminal.Write(chars[i]);
                    countChars++;
                    if (countChars % 80 == 79)
                    {
                        countNewLine++;
                    }
                }
            }

            Terminal.Write("/");

            for (int i = 0; i < 23 - countNewLine; i++)
            {
                Terminal.WriteLine("");
                Terminal.Write("~");
            }

            //PRINT INSTRUCTION
            Terminal.WriteLine();
            for (int i = 0; i < 72; i++)
            {
                if (i < infoBar.Length)
                {
                    Terminal.Write(infoBar[i]);
                }
                else
                {
                    Terminal.Write(" ");
                }
            }

            if (editMode)
            {
                Terminal.Write(countNewLine + 1 + "," + countChars);
            }

        }

        public static String miv(String start)
        {
            Boolean editMode = false;
            int pos = 0;
            char[] chars = new char[2000];
            String infoBar = String.Empty;

            if (start == null)
            {
                printMIVStartScreen();
            }
            else
            {
                pos = start.Length;

                for (int i = 0; i < start.Length; i++)
                {
                    chars[i] = start[i];
                }
                printMIVScreen(chars, pos, infoBar, editMode);
            }

            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Terminal.ReadKey(true);

                if (isForbiddenKey(keyInfo.Key)) continue;

                else if (!editMode && keyInfo.KeyChar == ':')
                {
                    infoBar = ":";
                    printMIVScreen(chars, pos, infoBar, editMode);
                    do
                    {
                        keyInfo = Terminal.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.Enter)
                        {
                            if (infoBar == ":wq")
                            {
                                String returnString = String.Empty;
                                for (int i = 0; i < pos; i++)
                                {
                                    returnString += chars[i];
                                }
                                return returnString;
                            }
                            else if (infoBar == ":q")
                            {
                                return null;

                            }
                            else if (infoBar == ":help")
                            {
                                printMIVStartScreen();
                                break;
                            }
                            else
                            {
                                infoBar = "ERROR: No such command";
                                printMIVScreen(chars, pos, infoBar, editMode);
                                break;
                            }
                        }
                        else if (keyInfo.Key == ConsoleKey.Backspace)
                        {
                            infoBar = stringCopy(infoBar);
                            printMIVScreen(chars, pos, infoBar, editMode);
                        }
                        else if (keyInfo.KeyChar == 'q')
                        {
                            infoBar += "q";
                        }
                        else if (keyInfo.KeyChar == ':')
                        {
                            infoBar += ":";
                        }
                        else if (keyInfo.KeyChar == 'w')
                        {
                            infoBar += "w";
                        }
                        else if (keyInfo.KeyChar == 'h')
                        {
                            infoBar += "h";
                        }
                        else if (keyInfo.KeyChar == 'e')
                        {
                            infoBar += "e";
                        }
                        else if (keyInfo.KeyChar == 'l')
                        {
                            infoBar += "l";
                        }
                        else if (keyInfo.KeyChar == 'p')
                        {
                            infoBar += "p";
                        }
                        else
                        {
                            continue;
                        }
                        printMIVScreen(chars, pos, infoBar, editMode);



                    } while (keyInfo.Key != ConsoleKey.Escape);
                }

                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    editMode = false;
                    infoBar = String.Empty;
                    printMIVScreen(chars, pos, infoBar, editMode);
                    continue;
                }

                else if (keyInfo.Key == ConsoleKey.I && !editMode)
                {
                    editMode = true;
                    infoBar = "-- INSERT --";
                    printMIVScreen(chars, pos, infoBar, editMode);
                    continue;
                }

                else if (keyInfo.Key == ConsoleKey.Enter && editMode && pos >= 0)
                {
                    chars[pos++] = '\n';
                    printMIVScreen(chars, pos, infoBar, editMode);
                    continue;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && editMode && pos >= 0)
                {
                    if (pos > 0) pos--;

                    chars[pos] = '\0';

                    printMIVScreen(chars, pos, infoBar, editMode);
                    continue;
                }

                if (editMode && pos >= 0)
                {
                    chars[pos++] = keyInfo.KeyChar;
                    printMIVScreen(chars, pos, infoBar, editMode);
                }

            } while (true);
        }

        public static bool isForbiddenKey(ConsoleKey key)
        {
            ConsoleKey[] forbiddenKeys = { ConsoleKey.Print, ConsoleKey.PrintScreen, ConsoleKey.Pause, ConsoleKey.Home, ConsoleKey.PageUp, ConsoleKey.PageDown, ConsoleKey.End, ConsoleKey.NumPad0, ConsoleKey.NumPad1, ConsoleKey.NumPad2, ConsoleKey.NumPad3, ConsoleKey.NumPad4, ConsoleKey.NumPad5, ConsoleKey.NumPad6, ConsoleKey.NumPad7, ConsoleKey.NumPad8, ConsoleKey.NumPad9, ConsoleKey.Insert, ConsoleKey.F1, ConsoleKey.F2, ConsoleKey.F3, ConsoleKey.F4, ConsoleKey.F5, ConsoleKey.F6, ConsoleKey.F7, ConsoleKey.F8, ConsoleKey.F9, ConsoleKey.F10, ConsoleKey.F11, ConsoleKey.F12, ConsoleKey.Add, ConsoleKey.Divide, ConsoleKey.Multiply, ConsoleKey.Subtract, ConsoleKey.LeftWindows, ConsoleKey.RightWindows };
            for (int i = 0; i < forbiddenKeys.Length; i++)
            {
                if (key == forbiddenKeys[i]) return true;
            }
            return false;
        }

        public static void delay(int time)
        {
            for (int i = 0; i < time; i++) ;
        }
        public static void StartMIV()
        {
            Terminal.WriteLine("Enter file's filename to open:");
            Terminal.WriteLine("If the specified file does not exist, it will be created.");
            Kernel.file = Terminal.ReadLine();
            try
            {
                if (File.Exists(@"0:\" + Kernel.file))
                {
                    Terminal.WriteLine("Found file!");
                }
                else if (!File.Exists(@"0:\" + Kernel.file))
                {
                    Terminal.WriteLine("Creating file!");
                    File.Create(@"0:\" + Kernel.file);
                }
                Terminal.Clear();
            }
            catch (Exception ex)
            {
                Terminal.WriteLine(ex.Message);
            }

            String text = String.Empty;
            Terminal.WriteLine("Do you want to open " + Kernel.file + " content? (Yes/No)");
            if (Terminal.ReadLine().ToLower() == "yes" || Terminal.ReadLine().ToLower() == "y")
            {
                text = miv(File.ReadAllText(@"0:\" + Kernel.file));
            }
            else
            {
                text = miv(null);
            }

            Terminal.Clear();

            if (text != null)
            {
                File.WriteAllText(@"0:\" + Kernel.file, text);
                Terminal.WriteLine("Content has been saved to " + Kernel.file);
            }
            Terminal.WriteLine("Press any key to continue...");
            Terminal.ReadKey(true);
        }
    }
}
