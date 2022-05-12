using System.Linq;
using CountersPlus.ConfigModels;
using CountersPlus.Counters.Interfaces;
using CountersPlus.Counters.NoteCountProcessors;
using CountersPlus.Counters.Bloom_Font_Asset_Makers;
using TMPro;
using UnityEngine;
using Zenject;

namespace CountersPlus.Counters
{
    internal class NotesLeftCounter : Counter<NotesLeftConfigModel>, INoteEventHandler
    {
        [Inject] private GameplayCoreSceneSetupData setupData;
        [Inject] private NoteCountProcessor noteCountProcessor;

        private int notesLeft = 0;
        private TMP_Text counter;
        private int allNotes = 0;

        public override void CounterInit()
        {
            if (setupData.practiceSettings != null && setupData.practiceSettings.startInAdvanceAndClearNotes)
            {
                float startTime = setupData.practiceSettings.startSongTime;
                // This LINQ statement is to ensure compatibility with Practice Mode / Practice Plugin
                notesLeft = noteCountProcessor.Data.Count(x => x.time > startTime);
            }
            else
            {
                notesLeft = noteCountProcessor.NoteCount;
            }

            if (Settings.LabelAboveCount)
            {
                GenerateBasicText("Notes Remaining", out counter);
                counter.text = notesLeft.ToString();
            }
            else
            {
                counter = CanvasUtility.CreateTextFromSettings(Settings);
                if (Settings.Bloom)
                {
                    counter.font = BloomFontAssetMaker.instance.BloomFontAsset();
                }
                counter.text = $"Notes Remaining: {notesLeft}";
                counter.fontSize = 2;
            }
            allNotes = notesLeft;
            counter.color = Settings.CustomLeftColors ? Settings.Left1Color : Color.white;
        }

        public void OnNoteCut(NoteData data, NoteCutInfo info)
        {
            if (ShouldProcessNote(data) && !noteCountProcessor.ShouldIgnoreNote(data)) DecrementCounter();
        }

        public void OnNoteMiss(NoteData data)
        {
            if (ShouldProcessNote(data) && !noteCountProcessor.ShouldIgnoreNote(data)) DecrementCounter();
        }

        private bool ShouldProcessNote(NoteData data)
            => data.gameplayType switch
            {
                NoteData.GameplayType.Normal => true,
                NoteData.GameplayType.BurstSliderHead => true,
                _ => false,
            };

        private void DecrementCounter()
        {
            --notesLeft;
            if (Settings.LabelAboveCount) counter.text = notesLeft.ToString();
            else counter.text = $"Notes Remaining: {notesLeft}";

            counter.color = Settings.CustomLeftColors ? Settings.GetLeftColorFromLeft(((double)notesLeft / allNotes) * 100) : Color.white;
        }
    }
}

