using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP01_Library.Controllers
{
    public static class Outil
    {
        private static CompteJoueur _ActiveUser;

        public static int RollD20()
        {
            Random rnd = new Random();
            return rnd.Next(1, 21);
        }

        public static void SetActiveUser(CompteJoueur user)
        {
            _ActiveUser = user;
        }

        public static CompteJoueur GetActiveUser()
        {
            return _ActiveUser;
        }

    }
}
