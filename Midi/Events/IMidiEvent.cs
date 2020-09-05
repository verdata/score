namespace score.Midi.Events
{
    using System;
    using System.IO;
    
    public interface IMidiEvent
    {
        UInt32 Length { get; }
        void WriteBytes(BinaryWriter writer);
    }
}