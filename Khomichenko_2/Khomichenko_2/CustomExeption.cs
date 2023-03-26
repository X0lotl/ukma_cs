using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khomichenko_2
{
    public class CustomExeption : Exception
    {
        public CustomExeption(string exeption) : base(exeption) { }
    }
}