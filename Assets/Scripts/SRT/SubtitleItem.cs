
namespace ST
{
    public class SubtitleItem
    {
        static SubtitleItem _blank;
        public static SubtitleItem Blank => _blank ?? (_blank = new SubtitleItem(0, 0, 0, string.Empty));
        public int Index { get; }  /*字幕序号*/
        public double Length { get; } /*字幕时长*/
        public double From { get; } /*字幕开始时间*/
        public double To { get; } /*字幕结束时间*/
        public string Text { get; } /*字幕内容*/

        public SubtitleItem(int index, double from, double to, string text)
        {
            Index = index;
            From = from;
            To = to;
            Length = to - from;
            Text = text;
        }
    }
}
