using CountersPlus.ConfigModels;
using CountersPlus.Counters.Interfaces;
using CountersPlus.Counters.Bloom_Font_Asset_Makers;
using TMPro;
using UnityEngine;

namespace CountersPlus.Counters
{
    internal class NotesCounter : Counter<NoteConfigModel>, INoteEventHandler
    {
        private int goodCuts = 0;
        private int allCuts = 0;
        private TMP_Text counter;

        public override void CounterInit()
        {
            GenerateBasicText("Notes", out counter);
            if (Settings.Bloom)
            {
                counter.font = BloomFontAssetMaker.instance.BloomFontAsset();
            }
        }

        public void OnNoteCut(NoteData data, NoteCutInfo info)
        {
            allCuts++;
            if (data.colorType != ColorType.None && info.allIsOK) goodCuts++;
            RefreshText();
        }

        public void OnNoteMiss(NoteData data)
        {
            if (data.colorType == ColorType.None) return;
            allCuts++;
            RefreshText();
        }

        private void RefreshText()
        {
            counter.text = $"{goodCuts} / {allCuts}";
            if (Settings.ShowPercentage)
            {
                float percentage = (float)goodCuts / allCuts * 100.0f;
                counter.text += $" - {percentage.ToString($"F{Settings.DecimalPrecision}")}%";
            }

            counter.color = Settings.CustomNoteColors ? Settings.GetNoteColorFromNote(((double)goodCuts / allCuts) * 100) : Color.white;
        }
    }
}
