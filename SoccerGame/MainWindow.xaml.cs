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
using System.Timers;
using System.Threading;
using System.Windows.Threading;

namespace SoccerGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool keeperMoving = false;
        int keeperVx = 0;
        int keeperVy = 0;
        int[] verticalJumpVelocity = new int[] { -12,-9, -6, -3, 0, 3, 6, 9, 12};
        int verticalVelocityIndex = 0;
        DispatcherTimer uiTimer;

        int moveInterval = 7;
        int goalBoundLeft = 126;
        int goalBoundRight = 344;
        int goalBoundBottom = 88;

        public MainWindow()
        {
            InitializeComponent();
            uiTimer = new DispatcherTimer(); //This timer is created on GUI thread.
            uiTimer.Tick += new EventHandler(animate);
            uiTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 25); // 25 ticks per second
            uiTimer.Start();

        }

        private void animate(object sender, EventArgs e)
        {
            Image keeper = Keeper;
            if (keeperMoving)
            {
                Canvas.SetTop(keeper, Canvas.GetTop(keeper) + keeperVy);
                double newLeftPosition = Canvas.GetLeft(keeper) + keeperVx;
                if (newLeftPosition > goalBoundLeft && newLeftPosition < goalBoundRight)
                {
                    Canvas.SetLeft(keeper, newLeftPosition);
                }
                verticalVelocityIndex++;
                if (verticalVelocityIndex >= verticalJumpVelocity.Count())
                {
                    verticalVelocityIndex = 0;
                    keeperMoving = false;
                    keeperVx = 0;
                    keeperVy = 0;
                }
                else
                {
                    keeperVy = verticalJumpVelocity[verticalVelocityIndex];
                }
            }
        }


        private void jump(Image keeper, int goalBoundLeft, int goalBoundBottom, int vx)
        {
            keeperMoving = true;
            keeperVy = verticalJumpVelocity[0];
            keeperVx = vx;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Image keeper = Keeper;
            
           
            if (!keeperMoving)
            {

                Key pressedKey = e.Key;


                if (pressedKey == Key.Up && Keyboard.IsKeyDown(Key.Right))
                {
                    jump(keeper, goalBoundLeft, goalBoundBottom, 1 * moveInterval);
                }
                else if (pressedKey == Key.Up && Keyboard.IsKeyDown(Key.Left))
                {
                    jump(keeper, goalBoundLeft, goalBoundBottom, -1*moveInterval);

                }
                else if (pressedKey == Key.Up)
                {
                    jump(keeper, goalBoundLeft, goalBoundBottom, 0);
                }
                else if (pressedKey == Key.Right)
                {
                    if (Canvas.GetLeft(keeper) + moveInterval < goalBoundRight)
                    {
                        Canvas.SetLeft(keeper, Canvas.GetLeft(keeper) + moveInterval);
                    }
                }
                else if (pressedKey == Key.Left)
                {
                    if (Canvas.GetLeft(keeper) - moveInterval > goalBoundLeft)
                    {
                        Canvas.SetLeft(keeper, Canvas.GetLeft(keeper) - moveInterval);
                    }
                }                
            }
                      
        }
    }
}
