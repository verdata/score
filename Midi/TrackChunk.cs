﻿
namespace score.Midi
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using score.Midi.Events;

    public class TrackChunk
    {
        public byte TrackNumber { get; private set; }
        public string Header { get; private set; }
        public UInt32 Length { get; private set; }
        public List<EventData> Events { get; private set; }

        public int EventCount { get { return Events.Count; } }

        public bool Debug { get; set; }

        public TrackChunk(byte trackNumber, bool debug = false)
        {
            Debug = debug;
            Header = "MTrk";
            Length = 0;
            TrackNumber = trackNumber;
            Events = new List<EventData>();
        }

        public byte[] ToBytes()
        {
            foreach (var e in Events)
            {
                Length += e.Length;
            }

            byte[] _bytes = Helper.Combine(new byte[][]{
                    Encoding.ASCII.GetBytes(Header),
                    BitConverter.GetBytes(Helper.ReverseBytes(Length)),
                });

            return _bytes;
        }

        public void WriteBytes(BinaryWriter writer)
        {
            foreach (byte b in ToBytes())
            {
                writer.Write(b);
            }

            if (Debug)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Track #{0} Chunk Event Dump: {1} total ", TrackNumber, Events.Count);
                Console.WriteLine();
                Console.ResetColor();
            }

            foreach (var e in Events)
            {

                if (Debug)
                {
                    if (e.EventType == EventTypeList.NoteOn)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                    if (e.EventType == EventTypeList.NoteOff)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Console.WriteLine(e.ToString());

                    Console.ResetColor();
                }

                e.WriteBytes(writer);
            }
        }
    }
}