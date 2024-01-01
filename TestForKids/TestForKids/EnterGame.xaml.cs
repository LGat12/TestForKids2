using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using TestForKids;


namespace TestForKids
{
    /// <summary>
    /// Interaction logic for EnterGame.xaml
    /// </summary>
    public partial class EnterGame : Window
    {
        //     Random rnd = new Random();
        //int numberOfTargilim = 1;
        //int SumGrade = 100;
        //   private int timeRemaining = 30; // שעון זמן
        //     private DispatcherTimer timer; // מחלקה מובנית בשפה
        //int WrongForMinus = 0;
        // int WrongForPlus = 0;
        // int onfire = 0;

        private GameLogic gameLogic;
        private int onFire;
        public EnterGame(string name)
        {
            InitializeComponent();
            gameLogic = new GameLogic(this, 1, 100, 30, 0, 0);
            this.onFire = 0;                                                      

            gameLogic.InitializeTimer();
            gameLogic.GenerateNextQuestion();
            Name1.Text = name;

        }



        private void Timer_Tick(object sender, EventArgs e)
        {
            gameLogic.HandleTimerTick(sender, e);

            
        }

        

        

        

        private void Generate_Targil_Click(object sender, RoutedEventArgs e)
        {

            gameLogic.GenerateNextQuestion();


        }



        private void Check_Click(object sender, RoutedEventArgs e)
        {
            bool isCorrect = gameLogic.HandleCheckClick();

            if (isCorrect)
            {
                onFire++;
            }
            else
            {
                onFire = 0;
            }

            if (onFire >= 3)
            {
                Fire.Visibility = Visibility.Visible;
            }
            else
            {
                Fire.Visibility = Visibility.Collapsed;
            }
        }



        private void Final_Grade_Click(object sender, RoutedEventArgs e)
            {
                if (gameLogic != null)
                {
                    ShowFinalGrade(Name1.Text.ToString());
                }
                else
                {
                    MessageBox.Show("Error: gameLogic is not initialized.");
                }
            }

            private void ShowFinalGrade(string name)
            {
                gameLogic.ShowFinalGrade(name);
            }

            private void answer_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Enter && Check.Visibility == Visibility.Visible)
                {
                    gameLogic.HandleCheckClick();
                    e.Handled = true;
                }
            }

            private void answer_GotFocus(object sender, RoutedEventArgs e)
            {
                TextBox textBox = (TextBox)sender;
                textBox.Text = "";
            }



         
    
    } 
}

