namespace score.Midi.Events
{
    using System;
    using System.IO;
    public class SetTempoEvent : IMidiEvent
    {
        public UInt32 Length { get { return 6; } }
        public uint Tempo { get; set; }

        public SetTempoEvent(uint tempo)
        {
            Tempo = tempo;
        }
        public void WriteBytes(BinaryWriter writer)
        {
            var microseconds = new UInt24((UInt32)((60.0 / Tempo) * 1000000));

            writer.Write((byte)0xFF);
            writer.Write((byte)0x51);
            writer.Write((byte)0x03);
            writer.Write(microseconds.Byte2);
            writer.Write(microseconds.Byte1);
            writer.Write(microseconds.Byte0);
        }
        public override string ToString()
        {
            return string.Format("Tempo   [v: {0,2}]", Tempo);
        }
    }
}