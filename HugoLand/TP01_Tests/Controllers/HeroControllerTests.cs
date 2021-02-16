using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP01_Library.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Data.Entity;

namespace TP01_Library.Tests.Controllers
{
    [TestClass()]
    public class HeroControllerTests
    {
        HeroController controller = new HeroController();
        //Mock le dbContext
        HugoLandContext context = new HugoLandContext();

        /// <summary>
        /// Auteur : Simon Lalancette
        /// Desc : Test de la méthode AjouterHéro
        /// Date : 2021-02-15
        /// </summary>
        [TestMethod()]
        public void AjouterHeroTest()
        {
            //test
            #region Arrange
            // variables
            CompteJoueur joueur = new CompteJoueur() { Nom = "TestJoueur"};
            Monde monde = new Monde() { Description = "TestMonde"};
            Classe classe = new Classe();
            int xPos = 14;
            int yPos = 16;
            string Nom = "TestHero";
            #endregion

            #region Act
            // call de la méthode à testé
            controller.CreerHero(joueur, monde, classe, xPos, yPos, Nom);
            #endregion

            #region Assert
            Hero hero = context.Heros.FirstOrDefault(x => x.NomHero == Nom);

            Assert.IsNotNull(hero);
            Assert.AreEqual(joueur, hero.CompteJoueur);
            Assert.AreEqual(monde, hero.Monde);
            Assert.AreEqual(xPos, hero.x);
            #endregion

            //Cleanup
            context.Heros.Remove(hero);

        }

        [TestMethod()]
        public void SupprimerHeroTest()
        {
            #region Arrange
            // variables
            CompteJoueur joueur = new CompteJoueur() { Nom = "TestJoueur" };
            Monde monde = new Monde() { Description = "TestMonde" };
            Classe classe = new Classe();
            int xPos = 14;
            int yPos = 16;
            string Nom = "TestHeroToDelete";
            #endregion

            #region Act
            // call de la méthode à testé
            Hero h = new Hero()
            {
                CompteJoueur = joueur,
                Monde = monde,
                Classe = classe,
                x = xPos,
                y = yPos,
                NomHero = Nom
            };
            context.Heros.Add(h);
            #endregion

            #region Assert
            Hero hero = context.Heros.FirstOrDefault(x => x.NomHero == Nom);
            Assert.IsNotNull(hero);

            controller.DeleteHero(hero.Id);
            hero = context.Heros.FirstOrDefault(x => x.NomHero == Nom);
            Assert.IsNull(hero);
            #endregion
        }

        [TestMethod()]
        public void ModifierValeursHerosTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetObjetMondesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetHeroesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ModifierPositionHeroTest()
        {
            Assert.Fail();
        }
    }
}