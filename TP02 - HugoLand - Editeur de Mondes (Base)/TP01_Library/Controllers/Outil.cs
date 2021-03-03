using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP01_Library.Controllers
{
    static class Outil
    {

        public static int RollD20()
        {
            Random rnd = new Random();
            return rnd.Next(1, 21);
        }
    }
}
