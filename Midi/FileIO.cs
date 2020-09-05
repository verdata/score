namespace score.Midi
{
    using System;
    using System.IO;

    public static class FileIO
    {
        public static void ReadFile(string fileName)
        {

            Console.Clear();

            Console.WriteLine("Midi File Dump: {0}", fileName);
            Console.WriteLine();

            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                while (reader.PeekChar() != -1)
                {
                    var b = reader.ReadBytes(12);

                    Display(b);
                }
            }
        }
        private static void Display(byte[] bytes)
        {
            foreach (var b in bytes)
            {
                Console.Write("{0} ", b.ToString("X").PadLeft(2, '0'));
            }

            Console.SetCursorPosition(40, Console.CursorTop);

            foreach (var b in bytes)
            {
                char chr = '.';

                if (b > 31 & b < 127)
                {
                    chr = (char)b;
                }

                Console.Write("{0} ", chr);
            }

            Console.WriteLine();
        }
        public static void WriteFile(string fileName, MidiData midi)
        {
            if (midi.Track.EventCount <= 2)
            {
                throw new SkipFileException("Insuficent Events");
            }

            string outputFile = Path.Combine(midi.Folder, Path.GetFileName(fileName));

            using (BinaryWriter writer = new BinaryWriter(File.Open(outputFile, FileMode.Create)))
            {
                midi.WriteBytes(writer);
            }


        }
    }
}