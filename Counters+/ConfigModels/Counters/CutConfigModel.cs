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
        [UIValue(nameof(CustomCutColors))]
        public virtual bool CustomCutColors { get; set; } = true;
        [UIValue(nameof(SSThreshold))]
        public double SSThreshold { get; set; } = 112;
        [UIValue(nameof(SThreshold))]
        public double SThreshold { get; set; } = 110;
        [UIValue(nameof(AThreshold))]
        public double AThreshold { get; set; } = 108;
        [UIValue(nameof(BThreshold))]
        public double BThreshold { get; set; } = 106;
        [UIValue(nameof(CThreshold))]
        public double CThreshold { get; set; } = 100;
        [UIValue(nameof(DThreshold))]
        public double DThreshold { get; set; } = 90;
        [UIValue(nameof(EThreshold))]
        public double EThreshold { get; set; } = 80;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(SSColor))]
        public virtual Color SSColor { get; set; } = Color.magenta;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(SColor))]
        public virtual Color SColor { get; set; } = Color.blue;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(AColor))]
        public virtual Color AColor { get; set; } = Color.green;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(BColor))]
        public virtual Color BColor { get; set; } = Color.yellow;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(CColor))]
        public virtual Color CColor { get; set; } = new Color(1, 0.5f, 0);
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(DColor))]
        public virtual Color DColor { get; set; } = Color.red;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(EColor))]
        public virtual Color EColor { get; set; } = Color.red;

        public Color GetCutColorFromCut(double score)
        {
            if (score >= SSThreshold) return SSColor;
            else if (score >= SThreshold) return SColor;
            else if (score >= AThreshold) return AColor;
            else if (score >= BThreshold) return BColor;
            else if (score >= CThreshold) return CColor;
            else if (score >= DThreshold) return DColor;
            else if (score == 0) return Color.white;
            else return EColor;
        }
    }
}
