namespace CountersPlus.Counters.Interfaces
{
    public interface ExpandedINoteEventHandler : IEventHandler
    {
        void ExpandedOnNoteCut(NoteController data, NoteCutInfo info);

        void ExpandedOnNoteMiss(NoteData data);
    }
}
