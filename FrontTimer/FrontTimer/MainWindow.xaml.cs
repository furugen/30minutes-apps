using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FrontTimer
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        private Stopwatch stopwatch = new Stopwatch();
        DispatcherTimer timer = new DispatcherTimer();
        private int countMax = 60 * 30;

        public MainWindow()
        {
            InitializeComponent();

            // 定期的に実行
            // 0時間、0分、1秒ごとに動作
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            timer.Start();

            this.stopwatch.Stop();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            RefreshTimer();
            // 時間が経過したらアラームを表示
            int totalSecounds = ((int)this.stopwatch.Elapsed.TotalSeconds);
            if(totalSecounds >= countMax)
            {
                this.stopwatch.Stop();
                this.timer.Stop();
                MessageBox.Show("終了!!!");
            }
        }

        private void RefreshTimer()
        {
            int totalSecounds = ((int)this.stopwatch.Elapsed.TotalSeconds);

            TimeSpan countTime = new TimeSpan(0, 0, (countMax - totalSecounds));

            if(countTime.TotalSeconds <= 0)
            {
                countTime = new TimeSpan(0, 0, 0);
            }

            timerTextBlock.Text = countTime.ToString(@"mm\:ss");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
                this.timer.Stop();

                // タイマーを非表示に、編集を表示にする。
                showTimer.Visibility = Visibility.Collapsed;
                editTimer.Visibility = Visibility.Visible;
                // 現在時刻をエディターに反映
                int totalSecounds = ((int)this.stopwatch.Elapsed.TotalSeconds);
                TimeSpan countTime = new TimeSpan(0, 0, (countMax - totalSecounds));
                timerTextBoxMM.Text = countTime.ToString(@"mm");
                timerTextBoxSS.Text = countTime.ToString(@"ss");

                btnStartAndStop.Content = "開始";
            }
            else
            {
                int minute = int.Parse(timerTextBoxMM.Text);
                int secound = int.Parse(timerTextBoxSS.Text);

                // ストップウォッチを再開
                countMax = (minute * 60) + secound;
                stopwatch.Restart();
                this.timer.Start();

                // タイマー表示をリセット
                RefreshTimer();

                btnStartAndStop.Content = "一時停止";

                // タイマーを表示に、編集を非表示にする。
                showTimer.Visibility = Visibility.Visible;
                editTimer.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            stopwatch.Restart();
        }
    }
}

