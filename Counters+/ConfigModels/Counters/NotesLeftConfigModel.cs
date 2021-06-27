using BeatSaberMarkupLanguage.Attributes;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using UnityEngine;

namespace CountersPlus.ConfigModels
{
    internal class NotesLeftConfigModel : ConfigModel
    {
        [Ignore]
        public override string DisplayName => "Notes Left";

        public override bool Enabled { get; set; } = false;
        [UseConverter]
        public override CounterPositions Position { get; set; } = CounterPositions.AboveHighway;
        public override int Distance { get; set; } = -1;

        [UIValue(nameof(LabelAboveCount))]
        public virtual bool LabelAboveCount { get; set; } = false;
        [UIValue(nameof(CustomLeftColors))]
        public virtual bool CustomLeftColors { get; set; } = true;

        [UIValue(nameof(Left1Threshold))]
        public double Left1Threshold { get; set; } = 100;
        [UIValue(nameof(Left2Threshold))]
        public double Left2Threshold { get; set; } = 90;
        [UIValue(nameof(Left3Threshold))]
        public double Left3Threshold { get; set; } = 80;
        [UIValue(nameof(Left4Threshold))]
        public double Left4Threshold { get; set; } = 75;
        [UIValue(nameof(Left5Threshold))]
        public double Left5Threshold { get; set; } = 50;
        [UIValue(nameof(Left6Threshold))]
        public double Left6Threshold { get; set; } = 25;

        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Left1Color))]
        public virtual Color Left1Color { get; set; } = Color.red;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Left2Color))]
        public virtual Color Left2Color { get; set; } = Color.red;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Left3Color))]
        public virtual Color Left3Color { get; set; } = Color.red;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Left4Color))]
        public virtual Color Left4Color { get; set; } = Color.red;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Left5Color))]
        public virtual Color Left5Color { get; set; } = Color.yellow;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Left6Color))]
        public virtual Color Left6Color { get; set; } = Color.green;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Left7Color))]
        public virtual Color Left7Color { get; set; } = Color.cyan;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(LeftLastColor))]
        public virtual Color LeftLastColor { get; set; } = Color.white;

        public Color GetLeftColorFromLeft(double leftRatio)
        {
            if (leftRatio >= Left1Threshold) return Left1Color;
            else if (leftRatio >= Left2Threshold) return Left2Color;
            else if (leftRatio >= Left3Threshold) return Left3Color;
            else if (leftRatio >= Left4Threshold) return Left4Color;
            else if (leftRatio >= Left5Threshold) return Left5Color;
            else if (leftRatio >= Left6Threshold) return Left6Color;
            else if (leftRatio == 0) return LeftLastColor;
            else return Left7Color;
        }
    }
}

