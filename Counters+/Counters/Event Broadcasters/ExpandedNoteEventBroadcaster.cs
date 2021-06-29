using CountersPlus.Counters.Interfaces;
using Zenject;

namespace CountersPlus.Counters.Event_Broadcasters
{
    /// <summary>
    /// A <see cref="EventBroadcaster{T}"/> that broadcasts events relating to note cutting and missing.
    /// </summary>
    internal class ExpandedNoteEventBroadcaster : EventBroadcaster<ExpandedINoteEventHandler>
    {
        [Inject] private BeatmapObjectManager beatmapObjectManager;

        public override void Initialize()
        {
            beatmapObjectManager.noteWasCutEvent += NoteWasCutEvent;
            beatmapObjectManager.noteWasMissedEvent += NoteWasMissedEvent;
        }

        private void NoteWasCutEvent(NoteController data, in NoteCutInfo noteCutInfo)
        {
            foreach (ExpandedINoteEventHandler noteEventHandler in EventHandlers)
            {
                noteEventHandler?.ExpandedOnNoteCut(data, noteCutInfo);
            }
        }

        private void NoteWasMissedEvent(NoteController data)
        {
            foreach (ExpandedINoteEventHandler noteEventHandler in EventHandlers)
            {
                noteEventHandler?.ExpandedOnNoteMiss(data.noteData);
            }
        }

        public override void Dispose()
        {
            beatmapObjectManager.noteWasCutEvent -= NoteWasCutEvent;
            beatmapObjectManager.noteWasMissedEvent -= NoteWasMissedEvent;
        }
    }
}