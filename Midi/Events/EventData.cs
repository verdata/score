namespace score.Midi.Events
{
    using System;
    using System.IO;

    public enum EventTypeList{ NoteOn, NoteOff, ProgramChange, Tempo,TrackEnd}

    public class EventData
    {

        public EventTypeList EventType { get; set; }
   
        public IMidiEvent Event { get; set; }
        public UInt32 DeltaTime { get; set; }
        public UInt32 Length { get { return Event.Length + TimeLength(); } }

        public void WriteBytes(BinaryWriter writer)
        {
            foreach (byte b in Helper.VLQ(DeltaTime))
            {
                writer.Write(b);
            }

            Event.WriteBytes(writer);
        }

        private UInt32 TimeLength()
        {
            UInt32 length = 0;

            foreach (byte b in Helper.VLQ(DeltaTime))
            {
                length++;
            }

            return length;
        }

        public override string ToString()
        {
            return string.Format("d:{0,3} Event: {1} ",  DeltaTime, Event.ToString());
        }
    }
}