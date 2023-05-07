using System;
using System.Collections.Generic;
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

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    using System.Windows.Threading;
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed--;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8) {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
            if (tenthsOfSecondsElapsed < 0) {
                timeTextBlock.Text = timeTextBlock.Text + "You lose!";
                timer.Stop();           
                Task.WaitAll(new Task[] { Task.Delay(2000) });
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void SetUpGame()
        {

            List<string> animalEmoji = new List<string>()
            {
            "🐘","🐘",
            "🐒","🐒",
            "🐷","🐷",
            "🐗","🐗",
            "🐭","🐭",
            "🦓","🦓",
            "🐸","🐸",
            "🐔","🐔"
            };

            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>()) {
                if (textBlock.Name != "timeTextBlock")
                {
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
                //textBlock.Visibility = Visibility.Hidden;

            }
            //string question = "?";

            //foreach (TextBlock textBlock2 in mainGrid2.Children.OfType<TextBlock>()) {
            //    textBlock2.Text = question;
            //    textBlock2.Visibility = Visibility.Visible;
            //}
            timer.Start();
            tenthsOfSecondsElapsed = 30;
            matchesFound = 0;
        }


        TextBlock lastTextBlockClicked;
        bool findingMatch = false;
        

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //TextBlock textBlock = mainGrid.Children.OfType<TextBlock>().FirstOrDefault();
            //TextBlock textBlock2 = mainGrid2.Children.OfType<TextBlock>().FirstOrDefault();
            TextBlock textBlock = sender as TextBlock;

            if (findingMatch == false) //& textBlock2 =="?"
            {
                //textBlock2.Visibility = Visibility.Hidden;
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
                
            }
            else if (textBlock.Text == lastTextBlockClicked.Text) //& textBlock2 =="?"
            {
                //textBlock2.Visibility = Visibility.Hidden;
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }
        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e) {
            if (matchesFound == 8) {
                SetUpGame();
            }
        }
    }
}
 