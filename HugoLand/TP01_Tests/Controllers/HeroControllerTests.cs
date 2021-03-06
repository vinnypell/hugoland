﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP01_Library.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

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
            string Nom = "TestHero1";

            Monde monde;
            int mondeId;

            CompteJoueur compteJoueur;
            int compteJoueurId;
            string sNomComplet = "TestJoueur1";
            string sCourriel = "testCourriel@1";
            string sPrenom = "testPrenom1";
            string sNom = "testNom1";
            string sMdp = "salutmapoule1";
            int TypeUtilisateur = 3;

            Classe classe;
            int classeId;

            using (var db = new HugoLandContext())
            {
                monde = new Monde()
                {
                    Description = "",
                    LimiteX = 200,
                    LimiteY = 200
                };

                db.Mondes.Add(monde);
                db.SaveChanges();

                mondeId = monde.Id;

                ObjectParameter message = new ObjectParameter("message", typeof(string));
                db.CreerCompteJoueur(sNomComplet, sCourriel, sPrenom, sNom, TypeUtilisateur, sMdp, message);
                db.SaveChanges();

                classe = new Classe()
                {
                    NomClasse = "Test1",
                    Description = "Test1",
                    StatBaseDex = 1,
                    StatBaseInt = 1,
                    StatBaseStr = 1,
                    StatBaseVitalite = 5,
                    MondeId = mondeId
                };

                db.Classes.Add(classe);
                db.SaveChanges();

                classeId = classe.Id;

                compteJoueur = db.CompteJoueurs.FirstOrDefault(x => x.NomJoueur == sNomComplet && x.Courriel == sCourriel);
                compteJoueurId = compteJoueur.Id;

            }
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
                Assert.AreEqual(joueur.NomJoueur, hero.CompteJoueur.NomJoueur);
                Assert.AreEqual(monde.Description, hero.Monde.Description);
                Assert.AreEqual(xPos, hero.x);

                //Cleanup
                Classe classe_ = context.Classes.OrderByDescending(x => x.Id).First();
                context.Classes.Remove(classe_);

                Monde monde_ = context.Mondes.OrderByDescending(x => x.Id).First();
                context.Mondes.Remove(monde_);
 
                CompteJoueur cj = context.CompteJoueurs.OrderByDescending(x => x.Id).First();
                context.CompteJoueurs.Remove(cj);

                context.Heros.Remove(hero);
                context.SaveChanges();
            }
            #endregion

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
                    NomHero = Nom + "5"
                };

                context.Heros.Add(h);
                context.SaveChanges();
                #endregion

                #region Assert
                hero = context.Heros.FirstOrDefault(x => x.NomHero == Nom + "5");
                Assert.IsNotNull(hero);

            }

            controller.DeleteHero(hero.Id);
            using (var context = new HugoLandContext())
            {
                hero = context.Heros.FirstOrDefault(x => x.NomHero == Nom + "5");
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

                original = h.Clone();
                modified = h.Clone();
            }
            #endregion
            modified.NomHero = "nomModifié";
            modified.x = 4512;

            controller.EditHero(modified);

            using (var ctx = new HugoLandContext())
            {
                Hero fromDb = ctx.Heros.Find(modified.Id);
                Assert.AreNotEqual(original.NomHero, fromDb.NomHero);
                Assert.AreEqual(modified.NomHero, fromDb.NomHero);

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
            List<Hero> heros = new List<Hero>();
            using (var context = new HugoLandContext())
            {
                Hero h1 = new Hero()
                {
                    CompteJoueur = joueur,
                    Monde = monde,
                    Classe = classe,
                    x = 15,
                    y = 15,
                    NomHero = "testHero1"
                };
                Hero h2 = new Hero()
                {
                    CompteJoueur = joueur,
                    Monde = monde,
                    Classe = classe,
                    x = 15,
                    y = 15,
                    NomHero = "testHero2"
                };
                heros.Add(h1);
                heros.Add(h2);

                context.Mondes.Add(monde);
                joueur.Heros = heros;
                joueur.Heros.Add(h2);
                context.Heros.AddRange(heros);
                context.CompteJoueurs.Add(joueur);
                context.SaveChanges();

                joueur = context.CompteJoueurs.FirstOrDefault(x => x.NomJoueur == joueur.NomJoueur && x.Nom == joueur.Nom);
            }
            List<Hero> HeroCj = controller.HeroCompteJoueur(joueur.Id);

            for (int i = 0; i < HeroCj.Count; i++)
            {
                Assert.AreEqual(heros[i].NomHero, HeroCj[i].NomHero);
            }

            //Cleanup
            using (var context = new HugoLandContext())
            {
                Monde monde_ = context.Mondes.FirstOrDefault(x => x.Description == "TestMonde");
                Hero hero_ = context.Heros.First(x => x.NomHero == heros[0].NomHero && x.Monde.Description == heros[0].Monde.Description);
                Hero hero_2 = context.Heros.First(x => x.NomHero == heros[1].NomHero && x.Monde.Description == heros[1].Monde.Description);
                context.Mondes.Remove(monde_);
                context.Heros.Remove(hero_);
                context.Heros.Remove(hero_2);
                context.CompteJoueurs.Remove(joueur);
                context.SaveChanges();
            }
        }

        [TestMethod()]
        public void ModifierPositionHeroTest()
        {
            int newX = 25;
            int newY = 25;

            Hero h = new Hero()
            {
                CompteJoueur = joueur,
                Monde = monde,
                Classe = classe,
                x = 0,
                y = 0,
                NomHero = "testMouvement"
            };

            using (var context = new HugoLandContext())
            {
                context.Heros.Add(h);
                context.SaveChanges();
                h = context.Heros.First(x => x.NomHero == h.NomHero);
            }

            controller.Move(h.Id, newX, newY);

            using (var context = new HugoLandContext())
            {
                h = context.Heros.First(x => x.NomHero == h.NomHero);

                Assert.AreEqual(newX, h.x);
                Assert.AreEqual(newY, h.y);

                context.Heros.Remove(h);
                context.SaveChanges();
            }
        }
    }
}