using System.Collections.Generic;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using UnityEngine;

namespace CountersPlus.ConfigModels
{
    internal class ProgressConfigModel : ConfigModel
    {
        [Ignore]
        public override string DisplayName => "Progress";

        public override bool Enabled { get; set; } = true;
        [UseConverter]
        public override CounterPositions Position { get; set; } = CounterPositions.BelowEnergy;
        public override int Distance { get; set; } = 0;

        [UseConverter]
        [UIValue(nameof(Mode))]
        public virtual ProgressMode Mode { get; set; } = ProgressMode.Original;
        [UIValue(nameof(ProgressTimeLeft))]
        public virtual bool ProgressTimeLeft { get; set; } = false;
        [UIValue(nameof(IncludeRing))]
        public virtual bool IncludeRing { get; set; } = false;

        [UIValue(nameof(Modes))]
        public List<object> Modes => ModeToNames.Keys.Cast<object>().ToList();

        [UIAction(nameof(ModeFormat))]
        public string ModeFormat(ProgressMode pos) => ModeToNames[pos];

        [UIValue(nameof(Bloom))]
        public virtual bool Bloom { get; set; } = true;

        [UIValue(nameof(CustomProgressColors))]
        public virtual bool CustomProgressColors { get; set; } = true;

        [UIValue(nameof(Progress1Threshold))]
        public double Progress1Threshold { get; set; } = 5;
        [UIValue(nameof(Progress2Threshold))]
        public double Progress2Threshold { get; set; } = 10;
        [UIValue(nameof(Progress3Threshold))]
        public double Progress3Threshold { get; set; } = 15;
        [UIValue(nameof(Progress4Threshold))]
        public double Progress4Threshold { get; set; } = 25;
        [UIValue(nameof(Progress5Threshold))]
        public double Progress5Threshold { get; set; } = 50;
        [UIValue(nameof(Progress6Threshold))]
        public double Progress6Threshold { get; set; } = 75;

        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Progress1Color))]
        public virtual Color Progress1Color { get; set; } = Color.red;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Progress2Color))]
        public virtual Color Progress2Color { get; set; } = Color.red;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Progress3Color))]
        public virtual Color Progress3Color { get; set; } = Color.red;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Progress4Color))]
        public virtual Color Progress4Color { get; set; } = Color.red;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Progress5Color))]
        public virtual Color Progress5Color { get; set; } = Color.yellow;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Progress6Color))]
        public virtual Color Progress6Color { get; set; } = Color.green;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Progress7Color))]
        public virtual Color Progress7Color { get; set; } = Color.cyan;

        public Color GetProgressColorFromProgress(double progressRatio)
        {
            if (progressRatio <= Progress1Threshold) return Progress1Color;
            else if (progressRatio <= Progress2Threshold) return Progress2Color;
            else if (progressRatio <= Progress3Threshold) return Progress3Color;
            else if (progressRatio <= Progress4Threshold) return Progress4Color;
            else if (progressRatio <= Progress5Threshold) return Progress5Color;
            else if (progressRatio <= Progress6Threshold) return Progress6Color;
            else return Progress7Color;
        }

        private static Dictionary<ProgressMode, string> ModeToNames = new Dictionary<ProgressMode, string>()
        {
            { ProgressMode.Original, "Original" },
            { ProgressMode.BaseGame, "Base Game" },
            { ProgressMode.TimeInBeats, "Time in Beats" },
            { ProgressMode.Percent, "Percentage" },
        };
    }

    public enum ProgressMode { Original, BaseGame, TimeInBeats, Percent }
}
