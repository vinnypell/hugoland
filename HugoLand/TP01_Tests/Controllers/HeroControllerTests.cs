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
        CompteJoueur joueur;
        Monde monde;
        Classe classe;
        [TestInitialize]
        public void innit()
        {
            joueur = new CompteJoueur() { Nom = "TestJoueur", Prenom = "testPrenom", NomJoueur = "TestJoueur", Courriel = "testCourriel", MotDePasseHash = new byte[5] };
            monde = new Monde() { Description = "TestMonde" };
            classe = new Classe() { NomClasse = "testClasse", Description = "testClasse" };
        }
        /// <summary>
        /// Auteur : Simon Lalancette
        /// Desc : Test de la méthode AjouterHéro
        /// Date : 2021-02-15
        /// </summary>
        [TestMethod()]
        public void AjouterHeroTest()
        {
            #region Arrange
            // variables

            int xPos = 14;
            int yPos = 16;
            string Nom = "TestHero";
            #endregion

            #region Act
            // call de la méthode à testé
            controller.CreerHero(joueur, monde, classe, xPos, yPos, Nom);
            #endregion

            #region Assert
            using (var context = new HugoLandContext())
            {
                Hero hero = context.Heros.FirstOrDefault(x => x.NomHero == Nom);

                Assert.IsNotNull(hero);
                Assert.AreEqual(joueur, hero.CompteJoueur);
                Assert.AreEqual(monde, hero.Monde);
                Assert.AreEqual(xPos, hero.x);
                #endregion

                //Cleanup
                context.Heros.Remove(hero);
                context.SaveChanges();
            }
        }

        [TestMethod()]
        public void SupprimerHeroTest()
        {
            Hero hero;
            #region Arrange
            // variables
            int xPos = 14;
            int yPos = 16;
            string Nom = "TestHeroToDelete";
            #endregion

            #region Act
            using (var context = new HugoLandContext())
            {
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
                context.SaveChanges();
                #endregion

                #region Assert
                hero = context.Heros.FirstOrDefault(x => x.NomHero == Nom);
                Assert.IsNotNull(hero);
            }

            controller.DeleteHero(hero.Id);
            using (var context = new HugoLandContext())
            {
                hero = context.Heros.FirstOrDefault(x => x.NomHero == Nom);
                Assert.IsNull(hero);
            }
            #endregion
        }

        [TestMethod()]
        public void ModifierValeursHerosTest()
        {
            Hero modified;
            Hero original;
            int heroId = 0;
            #region Arrange
            // variables
            int xPos = 14;
            int yPos = 16;
            string Nom = "TestHeroToDelete";
            #endregion

            #region Act
            using (var context = new HugoLandContext())
            {
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
                context.SaveChanges();
                heroId = context.Heros.First(x => x.NomHero == h.NomHero && x.Classe == h.Classe).Id;
                original = h.Clone();
                modified = h.Clone();
            }
            #endregion
            modified.NomHero = "nomModifié";
            modified.x = 4512;

            controller.EditHero(modified);

            using (var ctx = new HugoLandContext())
            {
                Hero fromDb = ctx.Heros.Find(heroId);
                Assert.AreNotEqual<Hero>(original, fromDb);
                Assert.AreEqual<Hero>(modified, fromDb);

                ctx.Heros.Remove(fromDb);
                ctx.SaveChanges();
            }
        }

        [TestMethod()]
        public void GetObjetMondesTest()
        {
            Hero hero;
            string sDescription = "Objet monde test";
      
            List<ObjetMonde> objs = new List<ObjetMonde>();
            for (int i = 0; i < 5; i++)
            {
                int iPosX = 50 + i;
                int iPosY = 100 + i;

                ObjetMonde obj = new ObjetMonde()
                {
                    Description = sDescription,
                    x = iPosX,
                    y = iPosY,
                    TypeObjet = i
                };
                objs.Add(obj);
            }

            monde.ObjetMondes = objs;
            Hero h = new Hero()
            {
                CompteJoueur = joueur,
                Monde = monde,
                Classe = classe,
                x = 51,
                y = 101,
                NomHero = "TestNom"
            };

            using (var context = new HugoLandContext())
            {
                context.ObjetMondes.AddRange(objs);
                context.Mondes.Add(monde);
                context.Heros.Add(h);
                context.SaveChanges();

                hero = context.Heros.First(x => x.NomHero == h.NomHero && x.Monde.Description == h.Monde.Description);
            }

            List<ObjetMonde> objsVu = controller.ObjetsVuParHero(hero);
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(objs[i], objsVu[i]);
            }

            using (var context = new HugoLandContext())
            {
                List<ObjetMonde> objetMondes = context.ObjetMondes.Where(x => x.Description == sDescription).ToList();
                Monde monde_ = context.Mondes.FirstOrDefault(x => x.Description == "TestMonde");
                Hero hero_ = context.Heros.First(x => x.NomHero == h.NomHero && x.Monde.Description == h.Monde.Description);
                context.ObjetMondes.RemoveRange(objetMondes);
                context.Mondes.Remove(monde_);
                context.Heros.Remove(hero_);
                context.SaveChanges();
            }
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