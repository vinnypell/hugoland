using System;

namespace TP01_Library.Controllers
{
    public static class Outil
    {
        private static CompteJoueur _ActiveUser;
        private static Monde MondeToEdit;

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