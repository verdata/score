namespace score.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public enum NoteType { Accent, Staccato, Breath, Default, Click }

    public class Note
    {
        private bool isAlto = false;
        private Dictionary<string, byte> NoteList;
        private Dictionary<string, uint> DurationList;
        private Dictionary<string, byte> VelocityList;

        private string source;

        public NoteType type;


        private string lastDuration;

        public byte Key { get; set; }
        public byte Velocity { get; set; }
        public uint Duration { get; set; }

        public Note()
        {
            lastDuration = "4";

            type = NoteType.Default;

            NoteList = new Dictionary<string, byte>();
            DurationList = new Dictionary<string, uint>();
            VelocityList = new Dictionary<string, byte>();

            BuildDictionaries();
        }

        public void NextNote(string note)
        {
            source = note;

            StripVelocity();

            ProcessDuration();

            ProcessNote();
        }

        private void ProcessDuration()
        {
            Duration = 0;

            StripDuration();

            while (source.EndsWith("+"))
            {
                source = source.TrimEnd('+');
                StripDuration();
            }
        }

        private void StripDuration()
        {
            string suffix = "";
            string length = null;

            var noteDuration = lastDuration;

            var tokens = new string[] { ".", "%" };
            foreach (var token in tokens)
            {
                var value = TrimDuration(token);
                if (value != null)
                {
                    suffix = value;
                }
            }

            tokens = new string[] { "32", "16", "8", "4", "2", "1" };

            foreach (var token in tokens)
            {
                var value = TrimDuration(token);

                if (value != null)
                {
                    length = value;
                }
            }

            if (length != null)
            {
                noteDuration = length + suffix;
            }

            Duration += DurationList[noteDuration];

            lastDuration = noteDuration;
        }
        private string TrimDuration(string token)
        {
            if (source.EndsWith(token))
            {
                source = source.TrimEnd(token.ToCharArray());
                return token.ToString();
            }
            else
            {
                return null;
            }
        }

        private void StripVelocity()
        {
            var velocityFlag = "=";

            type = NoteType.Default;

            if (source.EndsWith("^"))
            {
                velocityFlag = "^";
                type = NoteType.Accent;
                source = source.Replace("^", "");
            }

            if (source.EndsWith(";"))
            {
                velocityFlag = ";";
                type = NoteType.Breath;
                source = source.Replace(";", "");
            }

            if (source.EndsWith(":"))
            {
                velocityFlag = ":";
                type = NoteType.Staccato;
                source = source.Replace(":", "");
            }

            Velocity = VelocityList[velocityFlag];
        }

        private void ProcessNote()
        {
            if (source == "r")
            {
                Velocity = 0;
                Key = 52;
                return;
            }
            if (source == "k")
            {
                type = NoteType.Click;
                Key = 60;
                return;
            }


            Key = NoteList[source];

            if (!isAlto)
            {
                Key = (byte)(Key - 7);
            }

        }


        public void SetAlto()
        {
            isAlto = true;

            NoteList = new Dictionary<string, byte>();

            NoteList.Add("f,", 53);
            NoteList.Add("fs,", 54);
            NoteList.Add("gb,", 54);
            NoteList.Add("g,", 55);
            NoteList.Add("gs,", 56);
            NoteList.Add("ab,", 56);
            NoteList.Add("a", 57);
            NoteList.Add("as", 58);
            NoteList.Add("bb", 58);
            NoteList.Add("b", 59);
            NoteList.Add("c", 60);
            NoteList.Add("cs", 61);
            NoteList.Add("db", 61);
            NoteList.Add("d", 62);
            NoteList.Add("ds", 63);
            NoteList.Add("eb", 63);
            NoteList.Add("e", 64);

            NoteList.Add("f", 65);
            NoteList.Add("fs", 66);
            NoteList.Add("gb", 66);
            NoteList.Add("g", 67);
            NoteList.Add("gs", 68);
            NoteList.Add("ab", 68);
            NoteList.Add("a'", 69);
            NoteList.Add("as'", 70);
            NoteList.Add("bb'", 70);
            NoteList.Add("b'", 71);
            NoteList.Add("c'", 72);
            NoteList.Add("cs'", 73);
            NoteList.Add("db'", 73);
            NoteList.Add("d'", 74);
            NoteList.Add("ds'", 75);
            NoteList.Add("eb'", 75);
            NoteList.Add("e'", 76);

            NoteList.Add("f'", 77);
            NoteList.Add("fs'", 78);
            NoteList.Add("gb'", 78);
            NoteList.Add("g'", 79);

            //   NoteList.Add("r", 60);
            //   NoteList.Add("k", 60);

        }

        public void SetSoprano()
        {
            isAlto = false;

            NoteList = new Dictionary<string, byte>();

            NoteList.Add("c,", 60);
            NoteList.Add("cs,", 61);
            NoteList.Add("db,", 61);
            NoteList.Add("d,", 62);
            NoteList.Add("ds,", 63);
            NoteList.Add("eb,", 63);

            NoteList.Add("e", 64);
            NoteList.Add("f", 65);
            NoteList.Add("fs", 66);
            NoteList.Add("gb", 66);
            NoteList.Add("g", 67);
            NoteList.Add("gs", 68);
            NoteList.Add("ab", 68);
            NoteList.Add("a", 69);
            NoteList.Add("as", 70);
            NoteList.Add("bb", 70);
            NoteList.Add("b", 71);
            NoteList.Add("c", 72);
            NoteList.Add("cs", 73);
            NoteList.Add("db", 73);
            NoteList.Add("d", 74);
            NoteList.Add("ds", 75);
            NoteList.Add("eb", 75);

            NoteList.Add("e'", 76);
            NoteList.Add("f'", 77);
            NoteList.Add("fs'", 78);
            NoteList.Add("gb'", 78);
            NoteList.Add("g'", 79);
            NoteList.Add("gs'", 80);
            NoteList.Add("ab'", 80);
            NoteList.Add("a'", 81);
            NoteList.Add("as'", 82);
            NoteList.Add("bb'", 82);
            NoteList.Add("b'", 83);
            NoteList.Add("c'", 84);
            NoteList.Add("cs'", 85);
            NoteList.Add("db'", 85);
            NoteList.Add("d'", 86);


            // NoteList.Add("r", 60);
            // NoteList.Add("k", 60);

        }


        private void BuildDictionaries()
        {

            DurationList.Add("1", 96);
            DurationList.Add("1.", 144);

            DurationList.Add("2", 48);
            DurationList.Add("2%", 32);
            DurationList.Add("2.", 72);

            DurationList.Add("4", 24);
            DurationList.Add("4%", 16);
            DurationList.Add("4.", 36);

            DurationList.Add("8", 12);
            DurationList.Add("8%", 8);
            DurationList.Add("8.", 18);

            DurationList.Add("16", 6);
            DurationList.Add("16%", 4);
            DurationList.Add("16.", 9);

            DurationList.Add("32", 3);

            VelocityList.Add("^", 96); //accent
            VelocityList.Add(":", 88); //stuccato
            VelocityList.Add(";", 80); //breath
            VelocityList.Add("=", 80); //default

        }

    }
}