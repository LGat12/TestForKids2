using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForKids
{
    internal class Name
    {
        private string name;
        public Name() { }

        public Name(string name)
        {
            this.name = name;
        }
        public string GetName()
        {
            return name;
        }

        public bool check1()
        {

            if (this.name != null && this.name.Length > 0)
            {
                return true;
            }
            return false;

        }

        public bool check2()
        {
            if (!string.IsNullOrEmpty(name) & !string.IsNullOrWhiteSpace(name) & name != "שם" & !name.All(char.IsDigit))
            {
                return true;
            }
            return false;
        }
    }
}
