using BeatSaberMarkupLanguage.Attributes;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using UnityEngine;

namespace CountersPlus.ConfigModels
{
    internal class NoteConfigModel : ConfigModel
    {
        [Ignore]
        public override string DisplayName => "Notes";

        public override bool Enabled { get; set; } = false;
        [UseConverter]
        public override CounterPositions Position { get; set; } = CounterPositions.BelowCombo;
        public override int Distance { get; set; } = 1;

        [UIValue(nameof(ShowPercentage))]
        public virtual bool ShowPercentage { get; set; } = false;
        [UIValue(nameof(DecimalPrecision))]
        public virtual int DecimalPrecision { get; set; } = 2;

        [UIValue(nameof(CustomNoteColors))]
        public virtual bool CustomNoteColors { get; set; } = true;

        [UIValue(nameof(Note1Threshold))]
        public double Note1Threshold { get; set; } = 100;
        [UIValue(nameof(Note2Threshold))]
        public double Note2Threshold { get; set; } = 99.5;
        [UIValue(nameof(Note3Threshold))]
        public double Note3Threshold { get; set; } = 99;
        [UIValue(nameof(Note4Threshold))]
        public double Note4Threshold { get; set; } = 97;
        [UIValue(nameof(Note5Threshold))]
        public double Note5Threshold { get; set; } = 95;
        [UIValue(nameof(Note6Threshold))]
        public double Note6Threshold { get; set; } = 90;

        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Note1Color))]
        public virtual Color Note1Color { get; set; } = Color.magenta;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Note2Color))]
        public virtual Color Note2Color { get; set; } = Color.blue;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Note3Color))]
        public virtual Color Note3Color { get; set; } = Color.green;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Note4Color))]
        public virtual Color Note4Color { get; set; } = Color.yellow;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Note5Color))]
        public virtual Color Note5Color { get; set; } = new Color(1, 0.5f, 0);
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Note6Color))]
        public virtual Color Note6Color { get; set; } = Color.red;
        [UseConverter(typeof(HexColorConverter))]
        [UIValue(nameof(Note7Color))]
        public virtual Color Note7Color { get; set; } = Color.red;

        public Color GetNoteColorFromNote(double noteRatio)
        {
            if (noteRatio >= Note1Threshold) return Note1Color;
            else if (noteRatio >= Note2Threshold) return Note2Color;
            else if (noteRatio >= Note3Threshold) return Note3Color;
            else if (noteRatio >= Note4Threshold) return Note4Color;
            else if (noteRatio >= Note5Threshold) return Note5Color;
            else if (noteRatio >= Note6Threshold) return Note6Color;
            else if (noteRatio == 0) return Color.white;
            else return Note7Color;
        }
    }
}
