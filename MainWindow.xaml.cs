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
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Canvas.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            Canvas.Arrange(new Rect(0.0,0.0, Canvas.Width, Canvas.DesiredSize.Height));

            Random rand = new Random();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Rectangle r = new Rectangle();
                    r.Width = Canvas.Width / width - 2;
                    r.Height = Canvas.Height / height - 2;
                    r.Fill = (rand.Next(0, 2) == 1) ? Brushes.Cyan : Brushes.Red;
                    Canvas.Children.Add(r);
                    Canvas.SetLeft(r, j * Canvas.Width / width);
                    Canvas.SetTop(r, i * Canvas.Height / height);
                    r.MouseDown += R_MouseDown;

                    felder[i, j] = r;
                }

            }

            timer.Interval = TimeSpan.FromSeconds(0.01);
            timer.Tick += Timer_tick;
        }



        const int mysize = 100;
        const int width = mysize;
        const int height = mysize;
        Rectangle[,] felder = new Rectangle[width, height];
        DispatcherTimer timer = new DispatcherTimer();
        private void Start_Click(object sender, RoutedEventArgs e)
        {
        }

        private void R_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ((Rectangle)sender).Fill = (((Rectangle)sender).Fill == Brushes.Cyan) ? Brushes.Red : Brushes.Cyan;

        }
        private void Timer_tick(object sender, EventArgs e)
        {
            int[,] mass = new int[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    
                    int i_up = i - 1;
                    if (i_up < 0)
                        i_up = height - 1;
                    int i_down = i + 1;
                    if (i_down >= height)
                        i_down = 0;

                    int j_right = j - 1;
                    if (j_right < 0)
                        j_right = width - 1;
                    int j_left = j + 1;
                    if (j_left >= width)
                        j_left = 0;

                    int neighbors = 0;

                    if (felder[i_up, j_right].Fill == Brushes.Red)
                        neighbors++;
                    if (felder[i_up, j].Fill == Brushes.Red)
                        neighbors++;
                    if (felder[i_up, j_left].Fill == Brushes.Red)
                        neighbors++;
                    if (felder[i, j_right].Fill == Brushes.Red)
                        neighbors++;
                    if (felder[i, j_left].Fill == Brushes.Red)
                        neighbors++;
                    if (felder[i_down, j_right].Fill == Brushes.Red)
                        neighbors++;
                    if (felder[i_down, j].Fill == Brushes.Red)
                        neighbors++;
                    if (felder[i_down, j_left].Fill == Brushes.Red)
                        neighbors++;

                    mass[i, j] = neighbors;
                }
            }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (mass[i, j] < 2 || mass[i, j] > 3)
                        felder[i, j].Fill = Brushes.Cyan;
                    else if (mass[i, j] == 3)
                        felder[i, j].Fill = Brushes.Red;
                }
            }
        }
        bool race = false;
        private void Next_step_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                //race = false;
                StartStop.Content = "Start animation";
            }
            else
            {
                timer.Start();
               // race = true;
                StartStop.Content = "Stop animation";
            }
        }
    }
}
