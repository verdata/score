﻿namespace score.Midi.Events
{
    using System;
    using System.IO;
    using score.Domain;

    public class NoteOffEvent : IMidiEvent
    {
        private byte Code = 0x80;
        public byte Channel { get; set; }
        public byte Key { get; set; }
        public byte Velocity { get; set; }
        public UInt32 Length { get { return 3; } }

        public NoteOffEvent()
        {
            Channel = 0;
            Key = 0x3C;
            Velocity = 0x40;
        }

        public void WriteBytes(BinaryWriter writer)
        {
            byte statusCode = (byte)(Code + Channel);

            writer.Write(statusCode);
            writer.Write(Key);
            writer.Write(Velocity);
        }
        public override string ToString()
        {
            return string.Format("NoteOff [c: {0,2} k: {1,4} v: {2,2}]", Channel, Lookup.GetNote(Key), Velocity);
        }
    }
}