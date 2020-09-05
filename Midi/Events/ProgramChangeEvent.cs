namespace score.Midi.Events
{
    using System;
    using System.IO;
    public class ProgramChangeEvent : IMidiEvent
    {
        private byte Code = 0xC0;
        public byte Channel { get; set; }
        public byte Instrument { get; set; }
        public UInt32 Length { get { return 2; } }

        public ProgramChangeEvent()
        {
            Channel = 0;
            Instrument = 0x00;

        }

        public void WriteBytes(BinaryWriter writer)
        {
            byte statusCode = (byte)(Code + Channel);

            writer.Write(statusCode);
            writer.Write(Instrument);

        }
        public override string ToString()
        {
            return string.Format("Program [c: {0,2} v: {1,2}]",Channel, Instrument);
        }
    }
}