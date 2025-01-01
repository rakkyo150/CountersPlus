﻿using CountersPlus.ConfigModels;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CountersPlus.ConfigModels;
using CountersPlus.Counters.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;
using CountersPlus.Utils.Bloom_Font_Asset_Makers;
using static ScoreModel;

namespace CountersPlus.Counters
{
    internal class CutCounter : Counter<CutConfigModel>
    {
        [Inject] ScoreController scoreController;

        private TMP_Text cutCounterLeft;
        private TMP_Text cutCounterRight;

        // [0] = pre-swing, [1] = post-swing, [2] = accuracy
        private int[] cutCountLeft = new int[] { 0, 0, 0 };
        private int[] cutCountRight = new int[] { 0, 0, 0 };
        private int[] totalScoresLeft = new int[] { 0, 0, 0 };
        private int[] totalScoresRight = new int[] { 0, 0, 0 };

        public override void CounterInit()
        {
            string defaultValue = FormatLabel(0, 0, Settings.AveragePrecision);

            var label = CanvasUtility.CreateTextFromSettings(Settings);
            label.text = "Average Cut";
            label.fontSize = 3;
            label.color = Color.white;

            Vector3 leftOffset = Vector3.up * -0.2f;
            TextAlignmentOptions leftAlign = TextAlignmentOptions.Top;
            
            if (Settings.SeparateSaberCounts)
            {
                cutCounterRight = CanvasUtility.CreateTextFromSettings(Settings, new Vector3(0.2f, -0.2f, 0));
                cutCounterRight.lineSpacing = -26;
                if (Settings.Bloom)
                {
                    cutCounterRight.font = BloomFontAssetMaker.BloomFontAsset();
                    // なぜかずれるので
                    cutCounterRight.lineSpacing = -40;
                }
                cutCounterRight.text = Settings.SeparateCutValues ? $"{defaultValue}\n{defaultValue}\n{defaultValue}" : $"{defaultValue}";
                cutCounterRight.alignment = TextAlignmentOptions.TopLeft;
                cutCounterRight.color = Color.white;

                leftOffset = new Vector3(-0.2f, -0.2f, 0);
                leftAlign = TextAlignmentOptions.TopRight;
            }

            cutCounterLeft = CanvasUtility.CreateTextFromSettings(Settings, leftOffset);
            cutCounterLeft.lineSpacing = -26;
            if (Settings.Bloom)
            {
                cutCounterLeft.font = BloomFontAssetMaker.BloomFontAsset();
                cutCounterLeft.lineSpacing = -40;
            }
            cutCounterLeft.text = Settings.SeparateCutValues ? $"{defaultValue}\n{defaultValue}\n{defaultValue}" : $"{defaultValue}";
            cutCounterLeft.alignment = leftAlign;
            cutCounterRight.color = Color.white;

            scoreController.scoringForNoteFinishedEvent += ScoreController_scoringForNoteFinishedEvent;
        }

        public override void CounterDestroy()
        {
            scoreController.scoringForNoteFinishedEvent -= ScoreController_scoringForNoteFinishedEvent;
        }

        private void ScoreController_scoringForNoteFinishedEvent(ScoringElement scoringElement)
        {
            if (scoringElement is GoodCutScoringElement goodCut)
            {
                var cutScoreBuffer = goodCut.cutScoreBuffer;

                var beforeCut = cutScoreBuffer.beforeCutScore;
                var afterCut = cutScoreBuffer.afterCutScore;
                var cutDistance = cutScoreBuffer.centerDistanceCutScore;
                var fixedScore = cutScoreBuffer.noteScoreDefinition.fixedCutScore;

                var totalScoresForHand = goodCut.noteData.colorType == ColorType.ColorA
                    ? totalScoresLeft
                    : totalScoresRight;

                var cutCountForHand = goodCut.noteData.colorType == ColorType.ColorA
                    ? cutCountLeft
                    : cutCountRight;

                switch (goodCut.noteData.scoringType)
                {
                    case NoteData.ScoringType.Normal:
                        totalScoresForHand[0] += beforeCut;
                        totalScoresForHand[1] += afterCut;
                        totalScoresForHand[2] += cutDistance;

                        cutCountForHand[0]++;
                        cutCountForHand[1]++;
                        cutCountForHand[2]++;
                        break;
                    case NoteData.ScoringType.ArcHead when Settings.IncludeArcs:
                    case NoteData.ScoringType.ChainHead when Settings.IncludeArcs:
                        totalScoresForHand[0] += beforeCut;
                        totalScoresForHand[2] += cutDistance;

                        cutCountForHand[0]++;
                        cutCountForHand[2]++;
                        break;
                    case NoteData.ScoringType.ArcTail when Settings.IncludeArcs:
                        totalScoresForHand[1] += afterCut;
                        totalScoresForHand[2] += cutDistance;

                        cutCountForHand[1]++;
                        cutCountForHand[2]++;
                        break;
                    case NoteData.ScoringType.ArcHeadArcTail when Settings.IncludeArcs:
                    case NoteData.ScoringType.ChainHeadArcTail when Settings.IncludeArcs:
                        totalScoresForHand[2] += cutDistance;

                        cutCountForHand[2]++;
                        break;
                    
                    // Chain links are not being tracked at all because they give a fixed 20 score for every hit.
                    /*case NoteData.ScoringType.ChainLink when Settings.IncludeChains:
                     case NoteData.ScoringType.ChainLinkArcHead when Settings.IncludeChains:
                        totalScoresForHand[2] += fixedScore;
                        cutCountForHand[2]++;
                        break;*/
                }

                UpdateLabels();
            }
        }

        private void UpdateLabels()
        {
            int shownDecimals = Settings.AveragePrecision;

            if (Settings.SeparateCutValues && Settings.SeparateSaberCounts) // Before/After/Distance, for both sabers
            {
                string[] leftAverages = new string[3]
                {
                    FormatLabel(totalScoresLeft[0], cutCountLeft[0], shownDecimals),
                    FormatLabel(totalScoresLeft[1], cutCountLeft[1], shownDecimals),
                    FormatLabel(totalScoresLeft[2], cutCountLeft[2], shownDecimals)
                };
                string[] rightAverages = new string[3]
                {
                    FormatLabel(totalScoresRight[0], cutCountRight[0], shownDecimals),
                    FormatLabel(totalScoresRight[1], cutCountRight[1], shownDecimals),
                    FormatLabel(totalScoresRight[2], cutCountRight[2], shownDecimals)
                };
                cutCounterLeft.text = $"{leftAverages[0]}\n{leftAverages[1]}\n{leftAverages[2]}";
                cutCounterRight.text = $"{rightAverages[0]}\n{rightAverages[1]}\n{rightAverages[2]}";
                cutCounterLeft.color = Settings.CustomCutColors ? Settings.GetCutColorFromCut(
                    SafeDivideScore(totalScoresLeft[0], cutCountLeft[0])+
                    SafeDivideScore(totalScoresLeft[1],cutCountLeft[1])+
                    SafeDivideScore(totalScoresLeft[2],cutCountLeft[2])) : Color.white;
                cutCounterRight.color = Settings.CustomCutColors ? Settings.GetCutColorFromCut(
                    SafeDivideScore(totalScoresRight[0], cutCountRight[0]) +
                    SafeDivideScore(totalScoresRight[1], cutCountRight[1]) +
                    SafeDivideScore(totalScoresRight[2], cutCountRight[2])) : Color.white;
            }
            else if (Settings.SeparateCutValues) // Before/After/Distance, for combined sabers
            {
                string[] cutValueAverages = new string[3]
                {
                    FormatLabel(totalScoresLeft[0] + totalScoresRight[0], cutCountLeft[0] + cutCountRight[0], shownDecimals),
                    FormatLabel(totalScoresLeft[1] + totalScoresRight[1], cutCountLeft[1] + cutCountRight[1], shownDecimals),
                    FormatLabel(totalScoresLeft[2] + totalScoresRight[2], cutCountLeft[2] + cutCountRight[2], shownDecimals)
                };
                cutCounterLeft.text = $"{cutValueAverages[0]}\n{cutValueAverages[1]}\n{cutValueAverages[2]}";
                cutCounterLeft.color = Settings.CustomCutColors ? Settings.GetCutColorFromCut(
                    SafeDivideScore(totalScoresLeft[0], cutCountLeft[0]) +
                    SafeDivideScore(totalScoresLeft[1], cutCountLeft[1]) +
                    SafeDivideScore(totalScoresLeft[2], cutCountLeft[2]) +
                    SafeDivideScore(totalScoresRight[0], cutCountRight[0]) +
                    SafeDivideScore(totalScoresRight[1], cutCountRight[1]) +
                    SafeDivideScore(totalScoresRight[2], cutCountRight[2])) : Color.white;
            }
            else if (Settings.SeparateSaberCounts) // Combined cut, for separate sabers
            {
                var totalScoreLeft = SafeDivideScore(totalScoresLeft[0], cutCountLeft[0]) 
                    + SafeDivideScore(totalScoresLeft[1], cutCountLeft[1])
                    + SafeDivideScore(totalScoresLeft[2], cutCountLeft[2]);

                var totalScoreRight = SafeDivideScore(totalScoresRight[0], cutCountRight[0])
                    + SafeDivideScore(totalScoresRight[1], cutCountRight[1])
                    + SafeDivideScore(totalScoresRight[2], cutCountRight[2]);

                cutCounterLeft.text = totalScoreLeft.ToString($"F{shownDecimals}", CultureInfo.InvariantCulture);
                cutCounterRight.text = totalScoreRight.ToString($"F{shownDecimals}", CultureInfo.InvariantCulture);
                cutCounterLeft.color = Settings.CustomCutColors ? Settings.GetCutColorFromCut(
                    SafeDivideScore(totalScoresLeft[0], cutCountLeft[0]) +
                    SafeDivideScore(totalScoresLeft[1], cutCountLeft[1]) +
                    SafeDivideScore(totalScoresLeft[2], cutCountLeft[2])) : Color.white;
                cutCounterRight.color = Settings.CustomCutColors ? Settings.GetCutColorFromCut(
                    SafeDivideScore(totalScoresRight[0], cutCountRight[0]) +
                    SafeDivideScore(totalScoresRight[1], cutCountRight[1]) +
                    SafeDivideScore(totalScoresRight[2], cutCountRight[2])) : Color.white;
            }
            else // Combined cut, for combined sabers
            {
                var aggregateScores = totalScoresLeft.Sum() + totalScoresRight.Sum();
                var aggregateCuts = cutCountLeft.Sum() + cutCountRight.Sum();

                cutCounterLeft.text = FormatLabel(aggregateScores, aggregateCuts, shownDecimals);
                cutCounterLeft.color = Settings.CustomCutColors ? Settings.GetCutColorFromCut(
                    SafeDivideScore(totalScoresLeft[0], cutCountLeft[0]) +
                    SafeDivideScore(totalScoresLeft[1], cutCountLeft[1]) +
                    SafeDivideScore(totalScoresLeft[2], cutCountLeft[2]) +
                    SafeDivideScore(totalScoresRight[0], cutCountRight[0]) +
                    SafeDivideScore(totalScoresRight[1], cutCountRight[1]) +
                    SafeDivideScore(totalScoresRight[2], cutCountRight[2])) : Color.white;
            }
        }

        private double SafeDivideScore(int total, int count)
        {
            double result = (double)total / count;
            return double.IsNaN(result) ? 0 : result;
        }

        private string FormatLabel(int totalScore, int totalCuts, int decimals)
            => SafeDivideScore(totalScore, totalCuts).ToString($"F{decimals}", CultureInfo.InvariantCulture);
    }
}
