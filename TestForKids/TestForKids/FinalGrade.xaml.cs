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
using System.Windows.Shapes;

namespace TestForKids
{
    /// <summary>
    /// Interaction logic for FinalGrade.xaml
    /// </summary>
    public partial class FinalGrade : Window
    {
        private Grade grade;
        public FinalGrade(int sumGrade, string name, int wrongForPlus, int wrongForMinus)
        {
            InitializeComponent();
            grade = new Grade(this);

            int finalGrade = sumGrade;
            Final_Grade.Text = $"{finalGrade}";

            GradeEvaluation(finalGrade, name, wrongForPlus, wrongForMinus);
        }



        public void GradeEvaluation(int finalGrade, string name, int wrongForPlus, int wrongForMinus)
        {
            string notes = grade.GetNotes(wrongForPlus, wrongForMinus);
            Notes.Text = notes;

            string gradeMessage = grade.EvaluateGrade(finalGrade , name, wrongForPlus, wrongForMinus);
            Last_Words.Text = gradeMessage;
        }

    }
    }

