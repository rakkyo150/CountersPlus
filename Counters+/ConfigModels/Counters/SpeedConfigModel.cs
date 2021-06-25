using BeatSaberMarkupLanguage.Attributes;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace CountersPlus.ConfigModels
{
    internal class SpeedConfigModel : ConfigModel
    {
        [Ignore]
        public override string DisplayName => "Speed";

        public override bool Enabled { get; set; } = false;
        [UseConverter]
        public override CounterPositions Position { get; set; } = CounterPositions.BelowMultiplier;
        public override int Distance { get; set; } = 2;

        [UIValue(nameof(DecimalPrecision))]
        public virtual int DecimalPrecision { get; set; } = 2;
        [UseConverter]
        [UIValue(nameof(Mode))]
        public virtual SpeedMode Mode { get; set; } = SpeedMode.Average;

        [UIValue(nameof(Modes))]
        public List<object> Modes => ModeToNames.Keys.Cast<object>().ToList();

        [UIAction(nameof(ModeFormat))]
        public string ModeFormat(SpeedMode pos) => ModeToNames[pos];

        [UIValue(nameof(CustomSpeedColors))]
        public virtual bool CustomSpeedColors { get; set; } = true;

        [UIValue(nameof(Speed1Threshold))]
        public double Speed1Threshold { get; set; } = 250;
        [UIValue(nameof(Speed2Threshold))]
        public double Speed2Threshold { get; set; } = 225;
        [UIValue(nameof(Speed3Threshold))]
        public double Speed3Threshold { get; set; } = 200;
        [UIValue(nameof(Speed4Threshold))]
        public double Speed4Threshold { get; set; } = 175;
        [UIValue(nameof(Speed5Threshold))]
        public double Speed5Threshold { get; set; } = 150;
        [UIValue(nameof(Speed6Threshold))]
        public double Speed6Threshold { get; set; } = 100;

        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Speed1Color))]
        public virtual Color Speed1Color { get; set; } = Color.red;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Speed2Color))]
        public virtual Color Speed2Color { get; set; } = Color.red;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Speed3Color))]
        public virtual Color Speed3Color { get; set; } = new Color(1, 0.5f, 0);
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Speed4Color))]
        public virtual Color Speed4Color { get; set; } = Color.yellow;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Speed5Color))]
        public virtual Color Speed5Color { get; set; } = Color.green;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Speed6Color))]
        public virtual Color Speed6Color { get; set; } = Color.blue;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Speed7Color))]
        public virtual Color Speed7Color { get; set; } = Color.magenta;

        public Color GetSpeedColorFromSpeed(double speed)
        {
            if (speed >= Speed1Threshold) return Speed1Color;
            else if (speed >= Speed2Threshold) return Speed2Color;
            else if (speed >= Speed3Threshold) return Speed3Color;
            else if (speed >= Speed4Threshold) return Speed4Color;
            else if (speed >= Speed5Threshold) return Speed5Color;
            else if (speed >= Speed6Threshold) return Speed6Color;
            else return Speed7Color;
        }

        private static Dictionary<SpeedMode, string> ModeToNames = new Dictionary<SpeedMode, string>()
        {
            { SpeedMode.Average, "Average" },
            { SpeedMode.Top5Sec, "Top from 5 Seconds" },
            { SpeedMode.Both, "Both Metrics" },
            { SpeedMode.SplitAverage, "Split Average" },
            { SpeedMode.SplitBoth, "Split Both Metrics" },
        };
    }

    public enum SpeedMode { Average, Top5Sec, Both, SplitAverage, SplitBoth }
}
