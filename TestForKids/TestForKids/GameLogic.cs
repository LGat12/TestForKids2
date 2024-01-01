using System;
using System.Windows;
using System.Windows.Threading;
using TestForKids;
using System.Windows.Input;

namespace TestForKids
{
    internal class GameLogic
    {
        private EnterGame enterGame;
        private TimerManager timerManager;
        private int num1;
        private int num2;
        private int numberOfTargilim;
        private int SumGrade;
        private int timeRemaining;
        private int WrongForMinus;
        private int WrongForPlus;
       // private int onFire;
        private Random rnd = new Random();
        public GameLogic(EnterGame enterGame, int NumberOfTargilim, int SumGrade, int timeRemaining, int WrongForMinus, int WrongForPlus)
        {
            this.enterGame = enterGame;
            this.numberOfTargilim = 1;
            this.SumGrade = 100;
            this.timeRemaining = 30;
            this.WrongForMinus = 0;
            this.WrongForPlus = 0;

        }

        public void InitializeTimer()
        {
            timerManager = new TimerManager(HandleTimerTick);
            timerManager.StartTimer();
            enterGame.Clock.Play();
        }
        public void ResetTimer()
        {
            timeRemaining = 30;
            enterGame.Timer.Text = $"Time Remaining: \n {timeRemaining} seconds";
        }


        public string GenerateRandomString(int length)
        {
            const string chars = "+-";
            char[] randomArray = new char[length];

            for (int i = 0; i < length; i++)
            {
                randomArray[i] = chars[rnd.Next(chars.Length)];
            }

            return new string(randomArray);
        }

        
        public void GenerateNextQuestion()
        {
            enterGame.Final_Grade.Visibility = Visibility.Collapsed;
            ResetTimer();

            enterGame.number_Q.Text = numberOfTargilim.ToString();
            Generate_Targilim_Class();
            enterGame.Generate_Targil.Visibility = Visibility.Collapsed;
            enterGame.Check.Visibility = Visibility.Visible;
            enterGame.answer.Text = "הכנס תשובה";

            if (this.numberOfTargilim == 5)
            {
                enterGame.Clock.Stop();
                enterGame.Timer.Visibility = Visibility.Collapsed;
                enterGame.Bordern1.Visibility = Visibility.Collapsed;
                enterGame.Bordern2.Visibility = Visibility.Collapsed;
                enterGame.Plus_Minus.Visibility = Visibility.Collapsed;
                enterGame.equal.Visibility = Visibility.Collapsed;
                enterGame.answer.Visibility = Visibility.Collapsed;
                enterGame.חשב.Visibility = Visibility.Collapsed;
                enterGame.Generate_Targil.Visibility = Visibility.Collapsed;
                enterGame.Check.Visibility = Visibility.Collapsed;
                enterGame.Final_Grade.Visibility = Visibility.Visible;
            }

            this.numberOfTargilim++;
        }

        public void Generate_Targilim_Class()
        {
            int random1 = rnd.Next(1, 20);
            int random2 = rnd.Next(1, 20);
            string randomOperator = GenerateRandomString(1);

            num1 = random1; 
            num2 = random2; 

            enterGame.num1txt.Text = num1.ToString();
            enterGame.num2txt.Text = num2.ToString();
            enterGame.Plus_Minus.Text = randomOperator;

            if (randomOperator == "-")
            {
                HandleSub();
            }
        }


        public bool HandleCheckClick()
        {
            
            try
            {
                int userAnswer = int.Parse(enterGame.answer.Text);
                int num1 = int.Parse(enterGame.num1txt.Text);
                int num2 = int.Parse(enterGame.num2txt.Text);
                
                int correctAnswer = enterGame.Plus_Minus.Text == "+" ? num1 + num2 : num1 - num2;

                if (userAnswer == correctAnswer)
                {
                    HandleCorrectAnswer();
                    return true;
                }
                else
                {
                    HandleIncorrectAnswer();
                    return false;
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
                return false;
            }
        }

        public void HandleTimerTick(object sender, EventArgs e)
        {
            timeRemaining--;

            enterGame.Timer.Text = $"Time Remaining: \n {timeRemaining} seconds";

            if (timeRemaining <= 0)
            {
                timerManager.StopTimer();
                MessageBox.Show("!נגמר הזמן");
                MessageBox.Show("- 10 points!");
                SumGrade -= 10;
                GenerateNextQuestion();
            }
        }

        public void ShowFinalGrade(string name)
        {
            FinalGrade finalGradeWindow = new FinalGrade(SumGrade, name, WrongForPlus, WrongForMinus);
            finalGradeWindow.Show();
            enterGame.Close();
        }




        private void OnTimerTick(object sender, EventArgs e)
        {
            HandleTimerTick(sender, e);
        }


        private void HandleCorrectAnswer()
        {
           

            try
            {
                // Access these variables through 'enterGame'
                int userAnswer1 = int.Parse(enterGame.answer.Text);
                int correctAnswer;

                if (enterGame.Plus_Minus.Text == "+")
                {
                    correctAnswer = int.Parse(enterGame.num1txt.Text) + int.Parse(enterGame.num2txt.Text);
                }
                else
                {
                    correctAnswer = int.Parse(enterGame.num1txt.Text) - int.Parse(enterGame.num2txt.Text);
                }

                if (userAnswer1 == correctAnswer)
                {
                    enterGame.Correct.Play();
                    enterGame.Correct.Stop();
                    enterGame.Correct.Position = TimeSpan.Zero;
                    enterGame.Correct.Play();
                    MessageBox.Show("Correct! Keep Going:)");
                    enterGame.Generate_Targil.Visibility = Visibility.Visible;
                    enterGame.Check.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }


        public void HandleIncorrectAnswer()
        {

            if (enterGame.Plus_Minus.Text == "-")
            {
                WrongForMinus++;
            }
            else if (enterGame.Plus_Minus.Text == "+")
            {
                WrongForPlus++;
            }

            enterGame.Wrong_Answer.Play();
            enterGame.Wrong_Answer.Stop();
            enterGame.Wrong_Answer.Position = TimeSpan.Zero;
            enterGame.Wrong_Answer.Play();

            MessageBox.Show("Incorrect, -10 points :(");
            SumGrade -= 10;
            enterGame.Generate_Targil.Visibility = Visibility.Collapsed;
        }

        public void HandleSub()
        {

            if (this.num1 < this.num2)
            {
                int temp = this.num1;
                this.num1 = this.num2;
                this.num2 = temp;

                enterGame.num1txt.Text = this.num1.ToString();
                enterGame.num2txt.Text = this.num2.ToString();
            }

        }
        private void HandleError(Exception ex)
        {
            enterGame.Error.Play();
            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            enterGame.Generate_Targil.Visibility = Visibility.Collapsed;
        }

    }
}
