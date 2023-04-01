using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ST
{
    public class SRTDisplayer : MonoBehaviour
    {
        public TextAsset Subtitle;
        public TextMeshProUGUI TextContent;
        public TextMeshProUGUI TextFade;

        [Range(0, 1)]
        public float FadeTime;

        private bool _isPaused;
        private bool _isPausedTimeSet;
        private float _pausedTime;

        void Update()
        {
            // 测试，暂停操作
            if (Input.GetKeyDown("space"))
            {
                _isPaused = !_isPaused;
            }
        }

        public IEnumerator Begin()
        {
            var currentlyDisplayingText = TextContent;
            var fadedOutText = TextFade;

            currentlyDisplayingText.text = string.Empty;
            fadedOutText.text = string.Empty;

            currentlyDisplayingText.gameObject.SetActive(true);
            fadedOutText.gameObject.SetActive(true);

            yield return FadeTextOut(currentlyDisplayingText);
            yield return FadeTextOut(fadedOutText);

            var parser = new SRTParser(Subtitle);

            var startTime = Time.time;
            SubtitleItem currentSubtitle = null;
            while (true)
            {
                while (_isPaused)
                {
                    if (!_isPausedTimeSet)
                    {
                        _pausedTime = Time.time;
                        _isPausedTimeSet = true;
                    }

                    yield return null;
                }

                if (_isPausedTimeSet)
                {
                    startTime += Time.time - _pausedTime;
                    _isPausedTimeSet = false;
                }

                var elapsed = Time.time - startTime;

                var subtitle = parser.GetForTime(elapsed);
                if (subtitle != null)
                {
                    if (!subtitle.Equals(currentSubtitle))
                    {
                        currentSubtitle = subtitle;

                        // Swap references around
                        var temp = currentlyDisplayingText;
                        currentlyDisplayingText = fadedOutText;
                        fadedOutText = temp;

                        // Switch subtitle text
                        currentlyDisplayingText.text = currentSubtitle.Text;

                        // And fade out the old one. Yield on this one to wait for the fade to finish before doing anything else.
                        StartCoroutine(FadeTextOut(fadedOutText));

                        // Yield a bit for the fade out to get part-way
                        yield return new WaitForSeconds(FadeTime / 3);

                        // Fade in the new current
                        yield return FadeTextIn(currentlyDisplayingText);
                    }
                    yield return null;
                }
                else
                {
                    // 字幕结束
                    StartCoroutine(FadeTextOut(currentlyDisplayingText));
                    yield return FadeTextOut(fadedOutText);
                    currentlyDisplayingText.gameObject.SetActive(false);
                    fadedOutText.gameObject.SetActive(false);
                    yield break;
                }
            }
        }

        void OnValidate()
        {
            FadeTime = (int)(FadeTime * 10) / 10f;
        }

        IEnumerator FadeTextOut(TextMeshProUGUI text)
        {
            var toColor = text.color;
            toColor.a = 0;
            yield return Fade(text, toColor, Ease.OutSine);
        }

        IEnumerator FadeTextIn(TextMeshProUGUI text)
        {
            var toColor = text.color;
            toColor.a = 1;
            yield return Fade(text, toColor, Ease.InSine);
        }

        IEnumerator Fade(TextMeshProUGUI text, Color toColor, Ease ease)
        {
            yield return DOTween.To(() => text.color, color => text.color = color, toColor, FadeTime).SetEase(ease).WaitForCompletion();
        }
    }
}