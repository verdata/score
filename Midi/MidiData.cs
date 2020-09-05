namespace score.Midi
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using score.Parser;

    public class MidiData
    {
        public HeaderChunk Header { get; private set; }
        public TrackChunk Track { get; private set; }
        public uint Tempo { get; set; }
        public sbyte Offset { get; set; }
        public string Folder { get; set; }

        public bool Debug { get; set; }

        public MidiData(uint tempo, bool debug = false)
        {
            Debug = debug;
            Tempo = tempo;

            Header = new HeaderChunk();
        }

        public void AllocateTracks(TrackBuilder builder)
        {
            Track = builder.GetTrack();

            Header.Tracks = 1;
        }

        public void WriteBytes(BinaryWriter writer)
        {
            Header.WriteBytes(writer);

            Track.WriteBytes(writer);

        }
    }
}