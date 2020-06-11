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
        public MainWindow()
        {
            InitializeComponent();

            


            // 定期的に実行
            DispatcherTimer timer = new DispatcherTimer();
            // 0時間、0分、1秒ごとに動作
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            timer.Start();

            this.stopwatch.Stop();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int countMax = 60 * 30;
            int totalSecounds = ((int)this.stopwatch.Elapsed.TotalSeconds);

            TimeSpan countTime = new TimeSpan(0, 0, (countMax - totalSecounds));
            timerTextBlock.Text = countTime.ToString(@"mm\:ss");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
                btnStartAndStop.Content = "開始する";
            }
            else
            {
                stopwatch.Start();
                btnStartAndStop.Content = "停止する";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            stopwatch.Restart();
        }
    }
}
