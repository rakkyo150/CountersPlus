using CountersPlus.ConfigModels;
using CountersPlus.Counters.Interfaces;
using CountersPlus.Counters.Bloom_Font_Asset_Makers;
using TMPro;
using UnityEngine;

namespace CountersPlus.Counters
{
    internal class MissedCounter : Counter<MissedConfigModel>, INoteEventHandler
    {
        private int notesMissed = 0;
        private TMP_Text counter;

        public override void CounterInit()
        {
            GenerateBasicText("Misses", out counter);
            if (Settings.Bloom)
            {
                counter.font = BloomFontAssetMaker.BloomFontAsset();
            }
            counter.color = Settings.GetMissColorFromMiss(notesMissed);
        }

        public void OnNoteCut(NoteData data, NoteCutInfo info)
        {
            if (Settings.CountBadCuts && !info.allIsOK && data.colorType != ColorType.None)
            {
                counter.text = (++notesMissed).ToString();
                counter.color = Settings.CustomMissColors ? Settings.GetMissColorFromMiss(notesMissed) : Color.white;
            }
        }

        public void OnNoteMiss(NoteData data)
        {
            if (data.colorType != ColorType.None) counter.text = (++notesMissed).ToString();
            counter.color = Settings.CustomMissColors ? Settings.GetMissColorFromMiss(notesMissed) : Color.white;
        }
    }
}
