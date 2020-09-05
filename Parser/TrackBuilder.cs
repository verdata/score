namespace score.Parser
{
    using score.Domain;
    using score.Midi;
    using score.Midi.Events;

    public class TrackBuilder
    {
        private TrackChunk track;

        

        public TrackBuilder(Sequence sequence, bool debug = false)
        {
            track = new TrackChunk(0, debug);

            foreach (var e in sequence.Events)
            {
                AddEvent(e);
            }

            CloseTracks();
        }

        public TrackChunk GetTrack()
        {
            return track;
        }

        private void AddEvent(EventData e)
        {
            track.Events.Add(e);
        }

        private void CloseTracks()
        {

            var e = new EventData();
            e.EventType = EventTypeList.TrackEnd;

            var n = new EndOfTrackEvent();

            e.Event = n;
            e.DeltaTime = 0;

            track.Events.Add(e);

        }
    }
}