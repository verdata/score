using System;
using System.Collections.Generic;
using System.IO;
using score.Domain;
using score.Midi;
using score.Parser;

namespace score
{
    class Program
    {
        static void Main(string[] args)
        {
            bool debug = false;

            bool fail = false;

            var suffix = ".score";

            foreach (string a in args)
            {
                if (a == "-debug")
                {
                    debug = true;
                    suffix = ".debug";
                }

                if (a == "-fail")
                {
                    fail = true;
                }
            }

            string targetDirectory = "/home/geraint/score/";

            string[] fileEntries = Directory.GetFiles(targetDirectory, "*" + suffix, SearchOption.AllDirectories);

            Array.Sort(fileEntries);

            foreach (string fileName in fileEntries)
            {
                var midiFileName = fileName.Replace(suffix, ".midi");

                try
                {
                    var midi = new ParseScore(fileName, 60, debug).MidiFile;

                    FileIO.WriteFile(midiFileName, midi);

                    Console.WriteLine("Completed Midi File: {0} ", Path.GetFileName(fileName));

                }
                catch (SkipFileException e)
                {
                    if (fail)
                    {
                        Console.WriteLine("Failed Midi File: {0} : {1}", Path.GetFileName(fileName), e.Message);
                    }
                }
            }
            Console.WriteLine();


        }
    }
}