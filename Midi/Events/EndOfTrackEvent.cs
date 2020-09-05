namespace score.Midi.Events
{
    using System;
    using System.IO;
    public class EndOfTrackEvent : IMidiEvent
    {
        public UInt32 Length { get { return 3; } }

        public EndOfTrackEvent() { }

        public void WriteBytes(BinaryWriter writer)
        {
            writer.Write((byte)0xFF);
            writer.Write((byte)0x2F);
            writer.Write((byte)0x00);
        }

        public override string ToString()
        {
            return string.Format("End of Track      ");
        }
    }
}