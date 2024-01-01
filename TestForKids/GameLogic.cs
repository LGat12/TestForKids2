using System;
using System.Windows;

namespace GameLogic
{
    internal class GameLogic
    {
        private EnterGame enterGame;
        private TimerManager timerManager;
        private int numberOfTargilim = 1;
        private int SumGrade = 100;
        private int timeRemaining = 30;
        private int WrongForMinus = 0;
        private int WrongForPlus = 0;
        private int onFire = 0;
        private char PlusMinus;

        public GameLogic(string name, EnterGame enterGame)
        {
            this.enterGame = enterGame;
            timerManager = new TimerManager(OnTimerTick);
            Generate_Targilim();
        }

        public string GenerateRandomString(int length)
        {
            const string chars = "+-";
            Random random = new Random();

            char[] randomArray = new char[length];
            for (int i = 0; i < length; i++)
            {
                randomArray[i] = chars[random.Next(chars.Length)];
            }

            return randomOperator = new string(randomArray);
        }

        public void GenerateNextQuestion()
        {

            ResetTimer();

            number_Q.Text = numberOfTargilim.ToString();
            Generate_Targilim();
            Generate_Targil.Visibility = Visibility.Collapsed;
            Check.Visibility = Visibility.Visible;
            answer.Text = "הכנס תשובה";

            if (this.numberOfTargilim == 5)
            {
                Clock.Stop();
                Timer.Visibility = Visibility.Collapsed;
                Bordern1.Visibility = Visibility.Collapsed;
                Bordern2.Visibility = Visibility.Collapsed;
                Plus_Minus.Visibility = Visibility.Collapsed;
                equal.Visibility = Visibility.Collapsed;
                answer.Visibility = Visibility.Collapsed;
                חשב.Visibility = Visibility.Collapsed;
                Generate_Targil.Visibility = Visibility.Collapsed;
                Check.Visibility = Visibility.Collapsed;
                Final_Grade.Visibility = Visibility.Visible;
            }

            this.numberOfTargilim++;
        }

        private void Generate_Targilim()
        {
            int random1 = rnd.Next(1, 20);
            num1txt.Text = random1.ToString();

            int random2 = rnd.Next(1, 20);
            num2txt.Text = random2.ToString();

            string randomOperator = GenerateRandomString(1);
            Plus_Minus.Text = randomOperator;

            int num1 = random1;
            int num2 = random2;

            if (randomOperator == "-")
            {
                if (num1 < num2)
                {
                    string temp1 = num1txt.Text;
                    num1txt.Text = num2txt.Text;
                    num2txt.Text = temp1;
                }
            }
        }

        public void HandleCheckClick()
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
                }
                else
                {
                    HandleIncorrectAnswer();
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public void HandleTimerTick()
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

        public void ShowFinalGrade()
        {
            FinalGrade finalGradeWindow = new FinalGrade(SumGrade, Name1.Text, WrongForPlus, WrongForMinus);
            finalGradeWindow.Show();
            this.Close();
        }

        public void HandleAnswerKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && enterGame.Check.Visibility == Visibility.Visible)
            {
                HandleCheckClick();
                e.Handled = true;
            }
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            HandleTimerTick();
        }

        private void HandleCorrectAnswer()
        {
            if (onfire > 3)
            {
                Fire.Visibility = Visibility.Visible;

            }

            try
            {
                int userAnswer1 = int.Parse(answer.Text);
                int num1 = int.Parse(num1txt.Text);
                int num2 = int.Parse(num2txt.Text);
                int correctAnswer;
                if (Plus_Minus.Text == "+")
                {
                    correctAnswer = num1 + num2;
                }
                else
                {
                    correctAnswer = num1 - num2;

                }


                if (userAnswer1 == correctAnswer)
                {
                    Correct.Play();
                    Correct.Stop();
                    Correct.Position = TimeSpan.Zero;
                    Correct.Play();
                    onFire++;
            MessageBox.Show("Correct! Keep Going:)");
            enterGame.Generate_Targil.Visibility = Visibility.Visible;
            enterGame.Check.Visibility = Visibility.Collapsed;
        }

        private void HandleIncorrectAnswer()
        {
            onFire = 0;

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

        private void HandleError(Exception ex)
        {
            enterGame.Error.Play();
            MessageBox.Show($"An error occurred: {ex.Message}");
            enterGame.Generate_Targil.Visibility = Visibility.Collapsed;
        }
    }
}
