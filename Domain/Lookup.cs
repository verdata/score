namespace score.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Lookup
    {
        private static Dictionary<byte, string> NoteList;

        public static string GetNote(byte key)
        {

            if (NoteList == null)
            {
                BuildList();
            }

            return NoteList[key];

        }

        private static void BuildList()
        {

            NoteList = new Dictionary<byte, string>();


            NoteList.Add(59, "rest");
            NoteList.Add(60, "c /4");
            NoteList.Add(61, "cs/4");
            NoteList.Add(62, "d /4");
            NoteList.Add(63, "ds/4");
            NoteList.Add(64, "e /4");
            NoteList.Add(65, "f /4");
            NoteList.Add(66, "fs/4");
            NoteList.Add(67, "g /4");
            NoteList.Add(68, "gs/4");
            NoteList.Add(69, "a /4");
            NoteList.Add(70, "as/4");
            NoteList.Add(71, "b /4");

            NoteList.Add(72, "c /5");
            NoteList.Add(73, "cs/5");
            NoteList.Add(74, "d /5");
            NoteList.Add(75, "ds/5");
            NoteList.Add(76, "e /5");
            NoteList.Add(77, "f /5");
            NoteList.Add(78, "fs/5");
            NoteList.Add(79, "g /5");
            NoteList.Add(80, "gs/5");
            NoteList.Add(81, "a /5");
            NoteList.Add(82, "as/5");
            NoteList.Add(83, "b /5");


            NoteList.Add(84, "c /6");
            NoteList.Add(85, "cs/6");
            NoteList.Add(86, "d /6");
            NoteList.Add(87, "ds/6");
            NoteList.Add(88, "e /6");
            NoteList.Add(89, "f /6");
            NoteList.Add(90, "fs/6");
            NoteList.Add(91, "g /6");

        }

    }
}