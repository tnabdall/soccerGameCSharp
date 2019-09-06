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
using System.Windows.Media.Animation;


namespace SoccerGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int FPS = 60; // frames per second, how many times animate function runs within a second

        bool isKeeper;
        bool isPlayer;

        bool keeperJumping = false; // lets us know if goalie is jumping for key press logic
        int keeperVx = 0;
        int keeperVy = 0;
        // Animate function sets vertical velocity with this array and index.
        // Allows for gravity-like jump
        int[] verticalJumpVelocity = new int[] { -12, -9, -6, -3, 0, 3, 6, 9, 12 };
        int verticalVelocityIndex = 0;

        double initialSoccerBallWidth = 62;
        double soccerBallFinalWidth = 31;//soccerBall.Width = 31;

        // Game Timer
        DispatcherTimer uiTimer;


        int keeperMoveSpeed = 7; // Canvas units. Movement per frame.

        const int GOAL_BOUND_LEFT = 124; // Left goal post
        const int GOAL_BOUND_RIGHT = 344; // Right goal post

        const int GOAL_BOUND_TOP = 55; // Top goal post
        const int GOAL_BOUND_BOTTOM = 154; // Bottom goal post


        double horizontalArrowAngle = 90; // Arrow pointing up
        double horizontalArrowVRad = 0; // Angular velocity in degrees

        const double HORIZONTAL_ANGLE_BOUND_LEFT = 45; // Left bound of degree rotation for horiz. arrow
        const double HORIZONTAL_ANGLE_BOUND_RIGHT = 135; // Right bound of degree rotation for horiz. arrow



        double shotVerticalAngle = 0; // Vertical direction angle. Controls position on screen.
        bool angleRising = true; // Controls whether arrow is rising or falling

        int powerBar = 0; // Power of shot.
        int powerBarVelocity = 0; // How fast power bar increases per frame.

        public MainWindow()
        {
            InitializeComponent();

            // Start game timer and runs animate function on defined FPS
            uiTimer = new DispatcherTimer(); //This timer is created on GUI thread.
            uiTimer.Tick += new EventHandler(animate);
            uiTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / FPS); // 60 ticks per second
            uiTimer.Start();

        }

        // Function to animate window
        private void animate(object sender, EventArgs e)
        {
            animateKeeper();
            animatePowerBar();
            // Stop animation for arrows if user has pressed space
            if (powerBarVelocity == 0)
            {
                animateVerticalDirectionArrow();
                animateHorizontalDirectionArrow();
            }
        }

        private void animateKeeper()
        {
            Image keeper = Keeper;
            // Test to see if new horizontal position is within goal bounds
            double newHorizontalPosition = Canvas.GetLeft(keeper) + keeperVx;
            if (newHorizontalPosition > GOAL_BOUND_LEFT && newHorizontalPosition < GOAL_BOUND_RIGHT)
            {
                Canvas.SetLeft(keeper, newHorizontalPosition);
            }

            // Handles vertical jump path
            if (keeperJumping)
            {
                Canvas.SetTop(keeper, Canvas.GetTop(keeper) + keeperVy);

                // Gravity-like jump. Moves to next velocity in array.
                verticalVelocityIndex++;
                if (verticalVelocityIndex >= verticalJumpVelocity.Count())
                {
                    // Ends jump
                    verticalVelocityIndex = 0;
                    keeperJumping = false;
                    keeperVx = 0;
                    keeperVy = 0;
                }
                else
                {
                    // Go to next value in jump array
                    keeperVy = verticalJumpVelocity[verticalVelocityIndex];
                }
            }
        }

        // Rotates horizontal shot arrow as long as its within bounds.
        private void animateHorizontalDirectionArrow()
        {
            if (horizontalArrowAngle + horizontalArrowVRad > HORIZONTAL_ANGLE_BOUND_LEFT && horizontalArrowAngle + horizontalArrowVRad < HORIZONTAL_ANGLE_BOUND_RIGHT)
            {
                horizontalArrowAngle += horizontalArrowVRad;
                HorizontalDirectionArrow.RenderTransform = new RotateTransform(horizontalArrowAngle);
                PowerLabel.Content = shotVerticalAngle.ToString();
            }
        }

        // Increases power bar. Restarts at 0 if it gets to full width.
        private void animatePowerBar()
        {
            if (powerBar + powerBarVelocity <= PowerBarLimit.Width)
            {
                powerBar += powerBarVelocity;
            }
            else
            {
                powerBar = 0;
            }
            PowerBar.Width = powerBar;
        }

        // Moves vertical direction arrow up and down.
        private void animateVerticalDirectionArrow()
        {
            Image arrow = VerticalDirectionArrow;
            if (angleRising && shotVerticalAngle == 90)
            {
                angleRising = false;
            }
            else if (angleRising)
            {
                shotVerticalAngle += 90 / FPS;
            }
            else if (shotVerticalAngle == 0)
            {
                angleRising = true;
            }
            else
            {
                shotVerticalAngle -= 90 / FPS;
            }

            VerticalDirectionArrow.RenderTransform = new RotateTransform(shotVerticalAngle);
        }

        // Sets velocities for keeper jump path.
        private void jump(int vx)
        {
            keeperJumping = true; // Flag for animate function to run jump logic
            keeperVy = verticalJumpVelocity[0];
            keeperVx = vx;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Image keeper = Keeper;

            Key pressedKey = e.Key;

            // Process goalie movement if keeper isnt jumping
            if (!keeperJumping)
            {
                if (pressedKey == Key.Up && Keyboard.IsKeyDown(Key.Right))
                {
                    jump(1 * keeperMoveSpeed);
                }
                else if (pressedKey == Key.Up && Keyboard.IsKeyDown(Key.Left))
                {
                    jump(-1 * keeperMoveSpeed);

                }
                else if (pressedKey == Key.Up)
                {
                    jump(0);
                }
                else if (pressedKey == Key.Right)
                {
                    keeperVx = keeperMoveSpeed;

                }
                else if (pressedKey == Key.Left)
                {
                    keeperVx = -keeperMoveSpeed;
                }
            }

            // Controls for shooter
            if (pressedKey == Key.Right)
            {
                horizontalArrowVRad = 2;
            }
            else if (pressedKey == Key.Left)
            {
                horizontalArrowVRad = -2;
            }
            else if (pressedKey == Key.Space)
            {
                powerBarVelocity = 10;
            }

        }

        // Resets appropriate velocities when keys are released
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            horizontalArrowVRad = 0;
            powerBarVelocity = 0;
            powerBar = 0;
            if (!keeperJumping)
            {
                keeperVx = 0;
            }
            if (e.Key == Key.Space)
            {
                shoot();
            }
        }

        private void shoot()
        {
            Image soccerBall = SoccerBall;
            double soccerBallInitialLeftPosition = Canvas.GetLeft(soccerBall);
            double soccerBallInitialTopPosition = Canvas.GetTop(soccerBall);
            double soccerBallInitialWidth = soccerBall.Width;
            double soccerBallFinalLeftPosition = 3.887 * horizontalArrowAngle - 101.452;
            double soccerBallFinalTopPosition = -1.666 * shotVerticalAngle + 130;
            

            // Canvas.SetLeft(soccerBall, soccerBallFinalLeftPosition);
            //Canvas.SetTop(soccerBall, soccerBallFinalTopPosition);


            //double mousePositionX = e.GetPosition(canvas).X;
            //double mousePositionY = e.GetPosition(canvas).Y;

            
            //Defines the X-axis animation
            DoubleAnimation animationX = new DoubleAnimation();
            animationX.To = soccerBallFinalLeftPosition;
            //animationX.To = mousePositionX;
            Storyboard.SetTarget(animationX, soccerBall);
            Storyboard.SetTargetProperty(animationX, new PropertyPath("(Canvas.Left)"));

            //Defines the Y-axis animation
            DoubleAnimation animationY = new DoubleAnimation();
            animationY.To = soccerBallFinalTopPosition;
            //animationY.To = mousePositionY;
            Storyboard.SetTarget(animationY, soccerBall);
            Storyboard.SetTargetProperty(animationY, new PropertyPath("(Canvas.Top)"));

            DoubleAnimation ballSizeAnimation = new DoubleAnimation();
            ballSizeAnimation.From = 1;
            ballSizeAnimation.To = 0.5;
            
           

            ballScale.BeginAnimation(ScaleTransform.ScaleXProperty, ballSizeAnimation);
            ballScale.BeginAnimation(ScaleTransform.ScaleYProperty, ballSizeAnimation);


            Storyboard storyboard = new Storyboard();

            storyboard.Children.Add(animationX);
            storyboard.Children.Add(animationY);
            storyboard.Begin();

            
            
            

            //animateSoccerBall (soccerBall, soccerBallInitialLeftPosition, soccerBallInitialTopPosition, soccerBallInitialWidth);

            /*
            MessageBoxResult result = MessageBox.Show("Thanks For Playing","Notice",MessageBoxButton.OK);
            if (result == MessageBoxResult.OK)
            {
                soccerBall.Width = soccerBallInitialWidth;
                Canvas.SetLeft(soccerBall, soccerBallInitialLeftPosition);
                Canvas.SetTop(soccerBall, soccerBallInitialTopPosition);
            }
            */

            /*
            var seq = Enumerable.Range(0, 10);
            foreach (int num in seq)
            {
                Canvas.SetLeft(soccerBall, soccerBallInitialLeftPosition - 15);
                Canvas.SetTop(soccerBall, soccerBallInitialTopPosition - 15);
                soccerBall.Width = soccerBallInitialWidth - 3;
                //MessageBox.Show(num.ToString());
            }
            */
        }

        // Moves soccerball toward net and shrinks its size.
        private void animateSoccerBall(Image soccerBall, double soccerBallInitialLeftPosition, double soccerBallInitialTopPosition, double soccerBallInitialWidth)
        {
            double soccerBallFinalLeftPosition = 3.887 * horizontalArrowAngle - 101.452;
            double soccerBallFinalTopPosition = -1.666 * shotVerticalAngle + 130;

            //Canvas.SetLeft(soccerBall, soccerBallFinalLeftPosition);
            //Canvas.SetTop(soccerBall, soccerBallFinalTopPosition);
            if (powerBar + powerBarVelocity <= PowerBarLimit.Width)
            {
                powerBar += powerBarVelocity;
            }
            else
            {
                powerBar = 0;
            }
            PowerBar.Width = powerBar;

            /*
            var seq = Enumerable.Range(0, 10);
            foreach (int num in seq)
            {
                //Canvas.SetLeft(soccerBall, soccerBallInitialLeftPosition - 15);
                Canvas.SetTop(soccerBall, soccerBallInitialTopPosition - 15);
                soccerBall.Width = soccerBallInitialWidth - 3;
                //MessageBox.Show(num.ToString());
            }
            */
        }

    }
    
}
