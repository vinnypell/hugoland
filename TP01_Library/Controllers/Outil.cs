using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP01_Library.Controllers
{
    public static class Outil
    {
        private static Monde MondeToEdit;
        public static int RollD20()
        {
            Random rnd = new Random();
            return rnd.Next(1, 21);
        }

        public static void SetMondeToEdit(Monde m)
        {
            MondeToEdit = m;
        }

        public static Monde GetMondeToEdit()
        {
            return MondeToEdit;
        }
    }
}
