using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace MyMusic.TemplateControl
{
    public sealed class LyricViewControl : Control
    {
        public event EventHandler ParseLyric;
        private List<int> positions;
        private List<TextBlock> textBlocks;
        private Stopwatch watch;
        private ScrollViewer scrollViewer;
        private StackPanel stackPanel;
        private bool Hover;
        private CancellationTokenSource scroll_cts;
        private CancellationTokenSource hover_cts;
        private int cur_pos;
        private int nxt_pos;
        private double cur_offset;
        private int watch_offset;
        private int delay_offset;
        private int cur_draw_pos;
        private object SW_LOCK = new object();

        public int HoverInterval
        {
            get { return (int)GetValue(HoverIntervalProperty); }
            set { SetValue(HoverIntervalProperty, value); }
        }

        public double BaseLine
        {
            get { return (double)GetValue(BaseLineProperty); }
            set { SetValue(BaseLineProperty, value); }
        }

        public bool ScrollBarVisibility
        {
            get { return (bool)GetValue(ScrollBarVisibilityProperty); }
            set { SetValue(ScrollBarVisibilityProperty, value); }
        }

        public static readonly DependencyProperty HoverIntervalProperty =
            DependencyProperty.Register(
                "HoverInterval",
                typeof(int),
                typeof(LyricViewControl),
                new PropertyMetadata(false));

        public static readonly DependencyProperty BaseLineProperty =
            DependencyProperty.Register(
                "BaseLine",
                typeof(double),
                typeof(LyricViewControl),
                new PropertyMetadata(false)
            );

        public static readonly DependencyProperty ScrollBarVisibilityProperty =
            DependencyProperty.Register(
                "ScrollBarVisibility",
                typeof(bool),
                typeof(LyricViewControl),
                new PropertyMetadata(false)
            );

        public LyricViewControl()
        {
            DefaultStyleKey = typeof(LyricViewControl);
            watch = new Stopwatch();
            positions = new List<int>();
            textBlocks = new List<TextBlock>();
        }

        protected override void OnApplyTemplate()
        {
            scrollViewer = GetTemplateChild("scrollViewer") as ScrollViewer;
            stackPanel = GetTemplateChild("stackPanel") as StackPanel;
            scrollViewer.ViewChanged += ScrollViewer_ViewChanged;
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility ? Windows.UI.Xaml.Controls.ScrollBarVisibility.Visible : Windows.UI.Xaml.Controls.ScrollBarVisibility.Hidden;
            stackPanel.Background = Background;

        }

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (!e.IsIntermediate && cur_offset != scrollViewer.VerticalOffset)
            {
                if (hover_cts != null)
                {
                    hover_cts.Cancel();
                    hover_cts.Dispose();
                }
                hover_cts = new CancellationTokenSource();
                int interval = HoverInterval;
                Task.Run(async () =>
                {
                    Hover = true;
                    try
                    {
                        await Task.Delay(interval * 1000, hover_cts.Token);
                        Hover = false;
                        scrollTo(cur_pos);
                    }
                    catch (TaskCanceledException c) { }
                }, hover_cts.Token);
            }
        }

        public void init()
        {
            cur_pos = nxt_pos = cur_draw_pos = 0;
            delay_offset = 0;
            watch_offset = 0;
            watch.Reset();
            stackPanel.Children.Clear();
            textBlocks.Clear();
            ParseLyric(this, null);
            textBlocks[0].Margin = new Thickness(0, BaseLine, 0, 0);
            textBlocks[textBlocks.Count - 1].Margin = new Thickness(0, 0, 0, Height - BaseLine);
        }

        public async void start()
        {
            watch.Start();
            scroll_cts = new CancellationTokenSource();
            await Task.Factory.StartNew(scroll, scroll_cts.Token);
        }

        public void startWith(int millisecond)
        {
            watch_offset = millisecond;
            start();
        }

        public async void moveTo(int millisecond)
        {
            lock (SW_LOCK)
            {
                watch.Restart();
                scroll_cts.Cancel();
                scroll_cts = new CancellationTokenSource();
                watch_offset = millisecond;
            }
            await Task.Factory.StartNew(scroll, scroll_cts.Token);
        }

        private async void scroll()
        {
            CancellationTokenSource cts = scroll_cts;
            CancellationToken token = cts.Token;
            int ms;
            while (nxt_pos < positions.Count)
            {
                if (token.IsCancellationRequested)
                    return;
                lock (SW_LOCK)
                {
                    ms = (int)watch.ElapsedMilliseconds;
                    delay_offset = positions[nxt_pos] - ms - watch_offset;
                    while (nxt_pos < positions.Count && ms + watch_offset >= positions[nxt_pos])
                    {
                        cur_pos = nxt_pos;
                        drawOn(cur_pos);
                        scrollTo(cur_pos);
                        ++nxt_pos;
                    }
                }
                if (nxt_pos < positions.Count && ms + watch_offset - positions[nxt_pos] < 0)
                {
                    try
                    {
                        await Task.Delay((positions[nxt_pos] - ms - watch_offset), scroll_cts.Token);
                    }
                    catch (TaskCanceledException e)
                    {

                    }
                }
            }
        }

        public void pause()
        {
            if (scroll_cts != null)
                scroll_cts.Cancel();
            watch.Stop();
        }

        public void reset()
        {
            scroll_cts.Cancel();
            scroll_cts.Dispose();
            watch.Reset();
        }

        public void stop()
        {

        }

        public void Add(string lyric, int position)
        {
            TextBlock textBlock = createTextBlock(lyric);
            stackPanel.Children.Add(textBlock);
            textBlocks.Add(textBlock);
            positions.Add(position);
        }

        private TextBlock createTextBlock(string text)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.TextAlignment = TextAlignment.Left;
            textBlock.TextWrapping = TextWrapping.WrapWholeWords;
            textBlock.Text = text;
            return textBlock;
        }

        private async void drawOn(int cur_index)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
            {
                textBlocks[cur_draw_pos].Foreground = new SolidColorBrush(Colors.Black);
                textBlocks[cur_index].Foreground = new SolidColorBrush(Colors.White);
                cur_draw_pos = cur_index;
            });
        }

        private async void scrollTo(int index)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
            {
                GeneralTransform transform = textBlocks[index].TransformToVisual(stackPanel);
                Point point = transform.TransformPoint(new Point(0, 0));
                cur_offset = (point.Y -= BaseLine);
                if (!Hover)
                    scrollViewer.ChangeView(null, point.Y, null);
            });
        }
    }
}
