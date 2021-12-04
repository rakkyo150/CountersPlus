using BeatSaberMarkupLanguage.Attributes;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using UnityEngine;

namespace CountersPlus.ConfigModels
{
    internal class CutConfigModel : ConfigModel
    {
        [Ignore]
        public override string DisplayName => "Cut";

        public override bool Enabled { get; set; } = false;
        [UseConverter]
        public override CounterPositions Position { get; set; } = CounterPositions.AboveHighway;
        public override int Distance { get; set; } = 1;

     
        [UIValue(nameof(SeparateSaberCounts))]
        public virtual bool SeparateSaberCounts { get; set; } = false;
        [UIValue(nameof(SeparateCutValues))]
        public virtual bool SeparateCutValues { get; set; } = false;
        [UIValue(nameof(AveragePrecision))]
        public virtual int AveragePrecision { get; set; } = 1;
        [UIValue(nameof(Bloom))]
        public virtual bool Bloom { get; set; } = true;
        [UIValue(nameof(CustomCutColors))]
        public virtual bool CustomCutColors { get; set; } = true;
        [UIValue(nameof(Cut1Threshold))]
        public double Cut1Threshold { get; set; } = 112;
        [UIValue(nameof(Cut2Threshold))]
        public double Cut2Threshold { get; set; } = 110;
        [UIValue(nameof(Cut3Threshold))]
        public double Cut3Threshold { get; set; } = 108;
        [UIValue(nameof(Cut4Threshold))]
        public double Cut4Threshold { get; set; } = 106;
        [UIValue(nameof(Cut5Threshold))]
        public double Cut5Threshold { get; set; } = 100;
        [UIValue(nameof(Cut6Threshold))]
        public double Cut6Threshold { get; set; } = 90;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Cut1Color))]
        public virtual Color Cut1Color { get; set; } = Color.magenta;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Cut2Color))]
        public virtual Color Cut2Color { get; set; } = Color.cyan;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Cut3Color))]
        public virtual Color Cut3Color { get; set; } = Color.green;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Cut4Color))]
        public virtual Color Cut4Color { get; set; } = Color.yellow;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Cut5Color))]
        public virtual Color Cut5Color { get; set; } = new Color(1, 0.5f, 0);
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Cut6Color))]
        public virtual Color Cut6Color { get; set; } = Color.red;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Cut7Color))]
        public virtual Color Cut7Color { get; set; } = Color.red;

        public Color GetCutColorFromCut(double score)
        {
            if (score >= Cut1Threshold) return Cut1Color;
            else if (score >= Cut2Threshold) return Cut2Color;
            else if (score >= Cut3Threshold) return Cut3Color;
            else if (score >= Cut4Threshold) return Cut4Color;
            else if (score >= Cut5Threshold) return Cut5Color;
            else if (score >= Cut6Threshold) return Cut6Color;
            else if (score == 0) return Color.white;
            else return Cut7Color;
        }
    }
}
