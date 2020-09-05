namespace score.Domain
{
    using System;
    using System.Collections.Generic;
    using score.Midi.Events;
    using static score.Domain.Note;

    public class Sequence
    {
        const byte DRUM_CHANNEL = 9;

        public List<EventData> Events { get; private set; }

        public sbyte Offset { get; set; }

        public Sequence()
        {
            Events = new List<EventData>();
        }

        public void AddMidiNote(Note note)
        {
            switch (note.type)
            {
                case NoteType.Accent:
                    AddNote(note.Key, note.Duration, note.Velocity);
                    break;

                case NoteType.Staccato:
                    {
                        uint totalLength = note.Duration;
                        uint soundLength = totalLength / 4;
                        uint restLength = totalLength - soundLength;

                        AddNote(note.Key, soundLength, note.Velocity);
                        AddNote(note.Key, restLength, 0);
                    }
                    break;

                case NoteType.Breath:
                    {
                        uint totalLength = note.Duration;
                        uint restLength = totalLength / 2;
                        uint soundLength = totalLength - restLength;

                        AddNote(note.Key, soundLength, note.Velocity);
                        AddNote(note.Key, restLength, 0);
                    }
                    break;

                case NoteType.Click:
                    AddClick(note.Key, note.Duration, note.Velocity, DRUM_CHANNEL);
                    break;

                default:
                    AddNote(note.Key, note.Duration, note.Velocity);
                    break;
            }
        }

        private void AddClick(byte Key, uint Duration, byte Velocity, byte Channel = 0)
        {

            var onEvent = new EventData();
            onEvent.EventType = EventTypeList.NoteOn;

            var on = new NoteOnEvent();

            on.Channel = Channel;
            on.Key = (byte)(Key);
            on.Velocity = Velocity;

            onEvent.Event = on;
            onEvent.DeltaTime = 0;

            Events.Add(onEvent);

            var offEvent = new EventData();
            offEvent.EventType = EventTypeList.NoteOff;

            var off = new NoteOffEvent();
            off.Channel = Channel;
            off.Key = (byte)(Key);
            off.Velocity = Velocity;

            offEvent.Event = off;
            offEvent.DeltaTime = Duration;
            Events.Add(offEvent);
        }

        private void AddNote(byte Key, uint Duration, byte Velocity, byte Channel = 0)
        {

            var onEvent = new EventData();
            onEvent.EventType = EventTypeList.NoteOn;

            var on = new NoteOnEvent();

            on.Channel = Channel;
            on.Key = (byte)(Key + Offset);
            on.Velocity = Velocity;

            onEvent.Event = on;
            onEvent.DeltaTime = 0;

            Events.Add(onEvent);

            var offEvent = new EventData();
            offEvent.EventType = EventTypeList.NoteOff;

            var off = new NoteOffEvent();
            off.Channel = Channel;
            off.Key = (byte)(Key + Offset);
            off.Velocity = Velocity;

            offEvent.Event = off;
            offEvent.DeltaTime = Duration;
            Events.Add(offEvent);
        }

        public void AddTempo(uint tempo)
        {
            var n = new SetTempoEvent(tempo);
            var e = new EventData();
            e.EventType = EventTypeList.Tempo;

            e.Event = n;
            e.DeltaTime = 0;
            Events.Add(e);
        }
    }
}