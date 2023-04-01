using System;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class SRTParser
    {
        List<SubtitleItem> _subtitles;
        public SRTParser(string textAssetResourcePath)
        {
            var text = Resources.Load<TextAsset>(textAssetResourcePath);
            Load(text);
        }

        public SRTParser(TextAsset textAsset)
        {
            this._subtitles = Load(textAsset);
        }

        static public List<SubtitleItem> Load(TextAsset textAsset)
        {
            if (textAsset == null)
            {
                Debug.LogError("Subtitle file is null");
                return null;
            }

            var lines = textAsset.text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            var currentState = eReadState.Index;

            var subs = new List<SubtitleItem>();

            int currentIndex = 0;
            double currentFrom = 0, currentTo = 0;
            var currentText = string.Empty;
            for (var l = 0; l < lines.Length; l++)
            {
                var line = lines[l];

                switch (currentState)
                {
                    case eReadState.Index:
                        {
                            int index;
                            if (Int32.TryParse(line, out index))
                            {
                                currentIndex = index;
                                currentState = eReadState.Time;
                            }
                        }
                        break;
                    case eReadState.Time:
                        {
                            // 当然这里解析也可以通过正则表达式解析
                            line = line.Replace(',', '.');
                            var parts = line.Split(new[] { "-->" }, StringSplitOptions.RemoveEmptyEntries);

                            // 解析时间线
                            if (parts.Length == 2)
                            {
                                TimeSpan fromTime;
                                if (TimeSpan.TryParse(parts[0], out fromTime))
                                {
                                    TimeSpan toTime;
                                    if (TimeSpan.TryParse(parts[1], out toTime))
                                    {
                                        currentFrom = fromTime.TotalSeconds;
                                        currentTo = toTime.TotalSeconds;
                                        currentState = eReadState.Text;
                                    }
                                }
                            }
                        }
                        break;
                    case eReadState.Text:
                        {
                            if (currentText != string.Empty)
                                currentText += "\r\n";

                            currentText += line;

                            // 当遇到空行的时候需要考虑是否已经结束
                            if (string.IsNullOrEmpty(line) || l == lines.Length - 1)
                            {
                                // 创建一个字幕
                                subs.Add(new SubtitleItem(currentIndex, currentFrom, currentTo, currentText));

                                // 需要释放
                                currentText = string.Empty;
                                currentState = eReadState.Index;
                            }
                        }
                        break;
                }
            }
            return subs;
        }

        public SubtitleItem GetForTime(float time)
        {
            if (_subtitles.Count > 0)
            {
                var subtitle = _subtitles[0];

                if (time >= subtitle.To)
                {
                    _subtitles.RemoveAt(0);

                    if (_subtitles.Count == 0)
                        return null;

                    subtitle = _subtitles[0];
                }

                if (subtitle.From > time)
                    return SubtitleItem.Blank;

                return subtitle;
            }
            return null;
        }

        enum eReadState
        {
            Index,
            Time,
            Text
        }
    }
}
