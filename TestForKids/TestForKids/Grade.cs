using System;
using System.Windows;
using System.Windows.Media.Effects;

namespace TestForKids
{
    public class Grade
    {
        private FinalGrade finalGrade;

        public Grade(FinalGrade finalGrade)
        {
            this.finalGrade = finalGrade;
        }
        public string EvaluateGrade(int grade, string name, int wrongForPlus, int wrongForMinus)
        {
            string notes = GetNotes(wrongForPlus, wrongForMinus);

            try
            {
                if (grade == 100)
                {
                    finalGrade.Perfect.Play();
                    return $"Perfect score! Good job {name}!";
                }
                else if (grade >= 80 && grade < 100)
                {
                    finalGrade.Excelent.Play();
                    return $"Nice test {name}!";
                }
                else if (grade > 55 && grade < 80)
                {
                    finalGrade.Nice.Play();
                    return $"Nice Try Keep working {name}!";
                }
                else
                {
                    finalGrade.Fail.Play();
                    return ":(";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return string.Empty;
            }
        }

        public string GetNotes(int wrongForPlus, int wrongForMinus)
        {
            try
            {
                if (wrongForMinus >= 1 && wrongForPlus >= 1)
                {
                    return $"*You were wrong {wrongForPlus} time/s on your addition answers \n " +
                        $"and {wrongForMinus} time/s on your subtraction";
                }
                else if (wrongForPlus >= 1)
                {
                    return $"*Note to yourself, {wrongForPlus} of your addition answers were wrong";
                }
                else if (wrongForMinus >= 1)
                {
                    return $"*Note to yourself, {wrongForMinus} of your subtraction answers were wrong";
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return string.Empty;
            }
        }
    }
}
