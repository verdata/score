namespace score.Parser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using score.Domain;
    using score.Midi;
    using score.Midi.Events;

    public class ParseScore
    {
        public MidiData MidiFile { get; private set; }
        public Sequence Sequence { get; private set; }

        public bool Debug { get; set; }

        public ParseScore(string fileName, uint tempo, bool debug = false)
        {
            Debug = debug;

            MidiFile = new MidiData(tempo, debug);
            Sequence = new Sequence();

            ProcessFile(fileName);

        }

        private void ProcessFile(string fileName)
        {
            string[] lines = new ProcessScore(fileName).Score.ToArray();

            MidiFile.Folder = Path.GetDirectoryName(fileName);

            string value = "";

            var note = new Note();

            foreach (string s in lines)
            {

                if (s.StartsWith("@skip"))
                {
                    throw new SkipFileException("Skip Instruction");
                }

                if (s.StartsWith("@soprano"))
                {
                    note.SetSoprano();
                    continue;
                }

                if (s.StartsWith("@alto"))
                {
                    note.SetAlto();
                    continue;
                }


                if (s.StartsWith("t="))
                {
                    value = s.Replace("t=", "");
                    Sequence.AddTempo(uint.Parse(value));

                    continue;
                }

                if (s.StartsWith("f="))
                {
                    value = s.Replace("f=", "");
                    MidiFile.Folder = value;

                    continue;
                }

                string[] notes = TokeniseBar(s);

                foreach (var n in notes)
                {
                    note.NextNote(n);

                    Sequence.AddMidiNote(note);
                }

            }

            MidiFile.AllocateTracks(new TrackBuilder(Sequence, Debug));
        }

        private string[] TokeniseBar(string s)
        {
            if (s.Contains(" "))
            {
                return s.Split(" ");
            }
            else
            {
                return new string[] { s };
            }
        }
    }
}