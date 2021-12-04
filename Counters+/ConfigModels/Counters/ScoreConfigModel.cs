using System.Collections.Generic;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using UnityEngine;

namespace CountersPlus.ConfigModels
{
    internal class ScoreConfigModel : ConfigModel
    {
        [Ignore]
        public override string DisplayName => "Score";

        public override bool Enabled { get; set; } = true;
        [UseConverter]
        public override CounterPositions Position { get; set; } = CounterPositions.BelowMultiplier;
        public override int Distance { get; set; } = 0;

        [UseConverter]
        [UIValue(nameof(Mode))]
        public virtual ScoreMode Mode { get; set; } = ScoreMode.Original;
        [UIValue(nameof(DecimalPrecision))]
        public virtual int DecimalPrecision { get; set; } = 2;
        [UIValue(nameof(DisplayRank))]
        public virtual bool DisplayRank { get; set; } = true;
        [UIValue(nameof(Bloom))]
        public virtual bool Bloom { get; set; } = true;
        [UIValue(nameof(CustomScoreColors))]
        public virtual bool CustomScoreColors { get; set; } = true;
        [UIValue(nameof(Score1Threshold))]
        public double Score1Threshold { get; set; } = 95;
        [UIValue(nameof(Score2Threshold))]
        public double Score2Threshold { get; set; } = 90;
        [UIValue(nameof(Score3Threshold))]
        public double Score3Threshold { get; set; } = 80;
        [UIValue(nameof(Score4Threshold))]
        public double Score4Threshold { get; set; } = 70;
        [UIValue(nameof(Score5Threshold))]
        public double Score5Threshold { get; set; } = 60;
        [UIValue(nameof(Score6Threshold))]
        public double Score6Threshold { get; set; } = 50;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Score1Color))]
        public virtual Color Score1Color { get; set; } = Color.magenta;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Score2Color))]
        public virtual Color Score2Color { get; set; } = Color.cyan;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Score3Color))]
        public virtual Color Score3Color { get; set; } = Color.green;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Score4Color))]
        public virtual Color Score4Color { get; set; } = Color.yellow;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Score5Color))]
        public virtual Color Score5Color { get; set; } = new Color(1, 0.5f, 0);
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Score6Color))]
        public virtual Color Score6Color { get; set; } = Color.red;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Score7Color))]
        public virtual Color Score7Color { get; set; } = Color.red;

        public Color GetScoreColorFromScore(double score)
        {
            if (score >= Score1Threshold) return Score1Color;
            else if (score >= Score2Threshold) return Score2Color;
            else if (score >= Score3Threshold) return Score3Color;
            else if (score >= Score4Threshold) return Score4Color;
            else if (score >= Score5Threshold) return Score5Color;
            else if (score >= Score6Threshold) return Score6Color;
            else return Score7Color;
        }

        [UIValue(nameof(Modes))]
        public List<object> Modes => ModeToNames.Keys.Cast<object>().ToList();

        [UIAction(nameof(ModeFormat))]
        public string ModeFormat(ScoreMode pos) => ModeToNames[pos];

        private static Dictionary<ScoreMode, string> ModeToNames = new Dictionary<ScoreMode, string>()
        {
            { ScoreMode.Original, "Original" },
            { ScoreMode.LeavePoints, "Dont Move Points" },
            { ScoreMode.ScoreOnly, "Remove Rank" },
            { ScoreMode.RankOnly, "Remove Percentage" },
        };
    }

    public enum ScoreMode { Original, ScoreOnly, LeavePoints, RankOnly }
}
