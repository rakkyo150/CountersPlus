using System.Collections.Generic;
using System.Linq;
using CountersPlus.ConfigModels;
using CountersPlus.Counters.Bloom_Font_Asset_Makers;
using TMPro;
using UnityEngine;
using Zenject;

namespace CountersPlus.Counters
{
    internal class SpeedCounter : Counter<SpeedConfigModel>, ITickable
    {
        [Inject] private SaberManager saberManager;

        private Saber right;
        private Saber left;
        private List<float> rSpeedList = new List<float>();
        private List<float> lSpeedList = new List<float>();
        private List<float> fastest = new List<float>();
        private TMP_Text averageCounter;
        private TMP_Text lAverageCounter;
        private TMP_Text fastestCounter;
        private TMP_Text label;
        private TMP_Text labelSecond;

        private float t;

        public override void CounterInit()
        {
            right = saberManager.rightSaber;
            left = saberManager.leftSaber;

            switch (Settings.Mode)
            {
                case SpeedMode.Average:
                    GenerateBasicText("Average Speed", out averageCounter);
                    break;
                case SpeedMode.SplitAverage:
                    label = CanvasUtility.CreateTextFromSettings(Settings, new Vector3(0, 0, 0));
                    label.fontSize = 3;
                    label.text = "Average Speed";
                    averageCounter = CanvasUtility.CreateTextFromSettings(Settings, new Vector3(0.4f, -0.4f, 0));
                    averageCounter.text = "0";
                    averageCounter.fontSize = 4;
                    lAverageCounter = CanvasUtility.CreateTextFromSettings(Settings, new Vector3(-0.4f, -0.4f, 0));
                    lAverageCounter.text = "0";
                    lAverageCounter.fontSize = 4;
                    break;
                case SpeedMode.Top5Sec:
                    GenerateBasicText("Recent Top Speed", out fastestCounter);
                    break;
                case SpeedMode.Both:
                    GenerateBasicText("Average Speed", out averageCounter);
                    labelSecond = CanvasUtility.CreateTextFromSettings(Settings, new Vector3(0, -1, 0));
                    labelSecond.fontSize = 3;
                    labelSecond.text = "Recent Top Speed";
                    fastestCounter = CanvasUtility.CreateTextFromSettings(Settings, new Vector3(0, -1.4f, 0));
                    fastestCounter.text = "0";
                    break;
                case SpeedMode.SplitBoth:
                    label = CanvasUtility.CreateTextFromSettings(Settings, new Vector3(0, 0, 0));
                    label.fontSize = 3;
                    label.text = "Average Speed";
                    averageCounter = CanvasUtility.CreateTextFromSettings(Settings, new Vector3(0.4f, -0.4f, 0));
                    averageCounter.text = "0";
                    averageCounter.fontSize = 4;
                    lAverageCounter = CanvasUtility.CreateTextFromSettings(Settings, new Vector3(-0.4f, -0.4f, 0));
                    lAverageCounter.text = "0";
                    lAverageCounter.fontSize = 4;
                    labelSecond = CanvasUtility.CreateTextFromSettings(Settings, new Vector3(0, -0.8f, 0));
                    labelSecond.fontSize = 3;
                    labelSecond.text = "Recent Top Speed";
                    fastestCounter = CanvasUtility.CreateTextFromSettings(Settings, new Vector3(0, -1.2f, 0));
                    fastestCounter.text = "0";
                    break;
            }

            if (!Settings.Bloom) return;
            
            if (averageCounter != null)
                averageCounter.font = BloomFontAssetMaker.instance.BloomFontAsset();
            
            if(lAverageCounter != null)
                lAverageCounter.font = BloomFontAssetMaker.instance.BloomFontAsset();

            if(fastestCounter != null)
                fastestCounter.font = BloomFontAssetMaker.instance.BloomFontAsset();
        }

        public void Tick()
        {
            int precision = Settings.DecimalPrecision;
            // YES I AM USING GOTO TO LIMIT CODE DUPLICATION NOW STOP FLAMING ME
            switch (Settings.Mode)
            {
                case SpeedMode.Top5Sec:
                    TickFastestSpeed();
                    break;

                case SpeedMode.Both:
                    TickFastestSpeed();
                    goto case SpeedMode.Average;
                case SpeedMode.Average:
                    rSpeedList.Add((right.bladeSpeed + left.bladeSpeed) / 2f);
                    averageCounter.text = rSpeedList.Average().ToString($"F{precision}");
                    averageCounter.color = Settings.CustomSpeedColors ? Settings.GetSpeedColorFromSpeed(rSpeedList.Average()) : Color.white;
                    break;

                case SpeedMode.SplitBoth:
                    TickFastestSpeed();
                    goto case SpeedMode.SplitAverage;
                case SpeedMode.SplitAverage:
                    rSpeedList.Add(right.bladeSpeed);
                    lSpeedList.Add(left.bladeSpeed);
                    lAverageCounter.text = $"{lSpeedList.Average().ToString($"F{precision}")} ";
                    averageCounter.text = $"{rSpeedList.Average().ToString($"F{precision}")}";
                    lAverageCounter.color = Settings.CustomSpeedColors ? Settings.GetSpeedColorFromSpeed(lSpeedList.Average()) : Color.white;
                    averageCounter.color = Settings.CustomSpeedColors ? Settings.GetSpeedColorFromSpeed(rSpeedList.Average()) : Color.white;
                    break;
            }
        }

        // Ticked function instead of IEnumerator because its legit just better
        private void TickFastestSpeed()
        {
            fastest.Add((right.bladeSpeed + left.bladeSpeed) / 2f);
            t += Time.deltaTime;
            if (t >= 5)
            {
                t = 0;
                var top = fastest.Max();
                fastest.Clear();
                fastestCounter.text = top.ToString($"F{Settings.DecimalPrecision}");

                fastestCounter.color = Settings.CustomSpeedColors ? Settings.GetSpeedColorFromSpeed(top) : Color.white;
            }
        }
    }
}
