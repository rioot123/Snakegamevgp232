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
using System.Windows.Forms;

namespace Snakes
{

    public partial class MainWindow : Window
    {
        DispatcherTimer time;
        List<Snake> snakebody;
        List<Food> food;
        List<GameMap> map; 
        Random rd = new Random();
        double x = 100;
        double y = 100;
        int direction = 0;
        int left = 4;
        int right = 6;
        int up = 8;
        int down = 2;
        int score = 0;
        int count = 0;
        bool editx = false;
        public MainWindow()
        {
            InitializeComponent();
            time = new DispatcherTimer();
            map = new List<GameMap>();
            snakebody = new List<Snake>();
            food = new List<Food>();
            snakebody.Add(new Snake(x, y));
            food.Add(new Food(rd.Next(0, 37) * 10, rd.Next(0, 35) * 10));
            time.Interval = new TimeSpan(0, 0, 0, 0, 50);//edit to change difficulty higher number = easier
            time.Tick += time_Tick;
        }

        void resume(object sender, RoutedEventArgs e)//Resume Game
        {
            time.Start();
            editx = false;
        }

        void pause(object sender, RoutedEventArgs e) // Pause Game
        {
            time.Stop();
        }

        void edit(object sender, RoutedEventArgs e)//Edit Game
        {
            time.Start();
            editx = true;
        }

        void addsnakeincanvas()//Snek.
        {
            foreach (Snake snake in snakebody)
            {
                snake.setsnakeposition();
                mycanvas.Children.Add(snake.rec);
            }
        }


        void addfoodincanvas()//FOOOOOOOOOOOOOOOOOOOOOOOODDDDDDDDDDDDDDDDDDD
        {
            food[0].setfoodposition();
            mycanvas.Children.Insert(0, food[0].ell);
        }

        void addwallincanvas()
        {
            foreach(GameMap gamemap in map)
            {if (mycanvas != null)
                {
                    mycanvas.Children.Remove(gamemap.rec);
                        }
                gamemap.setwallposition();
                mycanvas.Children.Add(gamemap.rec);
            }
        }
        void getwall()
        {      
                Point p = Mouse.GetPosition(mycanvas);
            /*           p.X = p.X / 10;
                       p.Y = p.Y / 10;

                       p.X = p.X * 10;
                       p.Y = p.Y * 10;*/
            p.X = p.X - (p.X % 10);
            p.Y = p.Y - (p.Y % 10);
            map.Add(new GameMap(p.X, p.Y));
        }

        void editmode()
        {
             getwall();
        }

        void time_Tick(object sender, EventArgs e)//ゲームスタート！
        {
            if (editx == false)
            {
                if (direction != 0)
                {
                    for (int i = snakebody.Count - 1; i > 0; i--)
                    {
                        snakebody[i] = snakebody[i - 1];
                    }
                }

                //Movement
                if (direction == up)
                    y -= 10;
                if (direction == down)
                    y += 10;
                if (direction == left)
                    x -= 10;
                if (direction == right)
                    x += 10;
                if (snakebody[0].x == food[0].x && snakebody[0].y == food[0].y)//Foooooooooooooooooooooooooodddddddddd
                {
                    snakebody.Add(new Snake(food[0].x, food[0].y));
                    food[0] = new Food(rd.Next(0, 37) * 10, rd.Next(0, 35) * 10);
                    mycanvas.Children.RemoveAt(0);
                    addfoodincanvas();
                    score++;
                    txtbScore.Text = score.ToString();
                }
                if (map.Count != 0)
                {
                    if (map[0].x == food[0].x && map[0].y == food[0].y)//Foooooooooooooooooooooooooodddddddddd collision with wall
                    {
                        mycanvas.Children.RemoveAt(0);
                        food[0] = new Food(rd.Next(0, 37) * 10, rd.Next(0, 35) * 10);
                        addfoodincanvas();
                    }
                }

                snakebody[0] = new Snake(x, y);

                if (snakebody[0].x > 370 || snakebody[0].y > 350 || snakebody[0].x < 0 || snakebody[0].y < 0)//Border death
                {
                    time.Stop();
                    System.Windows.Forms.MessageBox.Show("You Lose!", "Loser!");
                    this.Close();

                }


                for (int i = 1; i < snakebody.Count; i++)//Touch yourself
                {
                    if (snakebody[0].x == snakebody[i].x && snakebody[0].y == snakebody[i].y)
                    {
                        time.Stop();
                        System.Windows.Forms.MessageBox.Show("You Lose!", "Loser!");
                        this.Close();
                    }
                }
                for (int j = 0; j < map.Count; j++) //touch wall
                {
                    if (snakebody[0].x == map[j].x && snakebody[0].y == map[j].y)
                    {
                        time.Stop();
                        System.Windows.Forms.MessageBox.Show("You Lose!", "Loser!");
                        this.Close();
                    }
                }


                for (int i = 0; i < mycanvas.Children.Count; i++)
                {
                    if (mycanvas.Children[i] is Rectangle)
                        count++;
                    if (mycanvas.Children[i] is Ellipse)
                        count++;
                }

                mycanvas.Children.RemoveRange(1, count);
                count = 0;
                addsnakeincanvas();

            }
            addwallincanvas();

        }


        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)//Input
        {
            if (e.Key == Key.Up && direction != down)
                direction = up;
            if (e.Key == Key.Down && direction != up)
                direction = down;
            if (e.Key == Key.Left && direction != right)
                direction = left;
            if (e.Key == Key.Right && direction != left)
                direction = right;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)//ゲームスタート！
        {
            addsnakeincanvas();
            addfoodincanvas();
            time.Start();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && editx == true)
            {
                editmode();
            }
        }
    }
}

