using BeatSaberMarkupLanguage.Attributes;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using UnityEngine;

namespace CountersPlus.ConfigModels
{
    internal class MissedConfigModel : ConfigModel
    {
        [Ignore]
        public override string DisplayName => "Missed";

        public override bool Enabled { get; set; } = true;
        [UseConverter]
        public override CounterPositions Position { get; set; } = CounterPositions.BelowCombo;
        public override int Distance { get; set; } = 0;

        [UIValue(nameof(CountBadCuts))]
        public virtual bool CountBadCuts { get; set; } = true;

        [UIValue(nameof(CustomMissColors))]
        public virtual bool CustomMissColors { get; set; } = true;

        [UIValue(nameof(Miss1Threshold))]
        public int Miss1Threshold { get; set; } = 0;
        [UIValue(nameof(Miss2Threshold))]
        public int Miss2Threshold { get; set; } = 1;
        [UIValue(nameof(Miss3Threshold))]
        public int Miss3Threshold { get; set; } = 2;
        [UIValue(nameof(Miss4Threshold))]
        public int Miss4Threshold { get; set; } = 3;
        [UIValue(nameof(Miss5Threshold))]
        public int Miss5Threshold { get; set; } = 4;
        [UIValue(nameof(Miss6Threshold))]
        public int Miss6Threshold { get; set; } = 5;

        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Miss1Color))]
        public virtual Color Miss1Color { get; set; } = Color.cyan;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Miss2Color))]
        public virtual Color Miss2Color { get; set; } = Color.green;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Miss3Color))]
        public virtual Color Miss3Color { get; set; } = Color.yellow;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Miss4Color))]
        public virtual Color Miss4Color { get; set; } = new Color(1, 0.5f, 0);
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Miss5Color))]
        public virtual Color Miss5Color { get; set; } = Color.red;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Miss6Color))]
        public virtual Color Miss6Color { get; set; } = Color.red;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Miss7Color))]
        public virtual Color Miss7Color { get; set; } = Color.red;

        public Color GetMissColorFromMiss(int miss)
        {
            if (miss <= Miss1Threshold) return Miss1Color;
            else if (miss <= Miss2Threshold) return Miss2Color;
            else if (miss <= Miss3Threshold) return Miss3Color;
            else if (miss <= Miss4Threshold) return Miss4Color;
            else if (miss <= Miss5Threshold) return Miss5Color;
            else if (miss <= Miss6Threshold) return Miss6Color;
            else return Miss7Color;
        }
    }
}
