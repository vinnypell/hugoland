using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP01_Library;
using System;
using TP01_Library.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;

namespace TP01_Library.Tests.Controllers
{
    [TestClass()]
    public class ItemControllerTests
    {
        private ItemController controller = new ItemController();
        private CompteJoueurController CJcontroller = new CompteJoueurController();
        private HeroController Hcontroller = new HeroController();
        HugoLandContext context = new HugoLandContext();

        /// <summary>
        /// Auteur: Mathias Lavoie-Rivard
        /// Description: Teste de la méthode AjouterItems
        /// Date: 2021/2/15
        /// </summary>
        [TestMethod()]
        public void AjouterItemsTest()
        {
            #region Arrange
            string sNom = "TestItem";
            string sDescription = "TestDescription";
            int iCoordx = 82;
            int iCoordy = 23;
            int iImage = 2;
            Monde mondetest = new Monde() { Description = sDescription };
            #endregion

            #region Act
            context.Mondes.Add(mondetest);
            context.SaveChanges();
            Monde testworld = context.Mondes.FirstOrDefault(x => x.Description == sDescription);
            controller.AjouterItems(sNom, sDescription, iCoordx, iCoordy, iImage, testworld.Id);
            #endregion

            #region Assert
            Item itemTest = context.Items.FirstOrDefault(x => x.Nom == sNom);
            Assert.IsNotNull(itemTest);
            Assert.AreEqual(sNom, itemTest.Nom);
            Assert.AreEqual(sDescription, itemTest.Description);
            Assert.AreEqual(iCoordx, itemTest.x);
            Assert.AreEqual(iCoordy, itemTest.y);
            Assert.AreEqual(iImage, itemTest.ImageId);
            Assert.AreEqual(testworld.Id, itemTest.MondeId);
            #endregion

            //Cleanup
            itemTest = context.Items.FirstOrDefault(x => x.Nom == sNom);
            testworld = context.Mondes.FirstOrDefault(x => x.Description == sDescription);
            context.Mondes.Remove(testworld);
            context.Items.Remove(itemTest);
            context.SaveChanges();
        }
        /// <summary>
        /// Auteur: Mathias Lavoie-Rivard
        /// Description: Teste de la méthode SupprimerItem
        /// Date: 2021/2/15
        /// </summary>
        [TestMethod()]
        public void SupprimerItemTest()
        {
            #region Arrange

            #region Arrange - création de héro
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

            Hero hero;
            int heroId;

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

                hero = new Hero()
                {
                    MondeId = mondeId,
                    NomHero = "Junior au poulet",
                    ClasseId = classeId,
                    Experience = 0,
                    Niveau = 1,
                    x = xPos,
                    y = yPos,
                    StatDex = classe.StatBaseDex,
                    StatInt = classe.StatBaseInt,
                    StatStr = classe.StatBaseStr,
                    StatVitalite = classe.StatBaseVitalite,
                    EstConnecte = false,
                    CompteJoueurId = compteJoueurId
                };

                db.Heros.Add(hero);
                db.SaveChanges();

                heroId = hero.Id;
            }
            #endregion

            #region Act
            // call de la méthode à testé

            //Item
            string sNomItem = "TestItem";
            string sDescription = "TestDescription";
            int iCoordx = 82;
            int iCoordy = 23;
            int iImage = 2;
            Item item;
            int itemId;

            using (HugoLandContext context = new HugoLandContext())
            {
                item = new Item()
                {
                    Nom = sNomItem,
                    Description = sDescription,
                    x = iCoordx,
                    y = iCoordy,
                    ImageId = iImage,
                    MondeId = mondeId
                };

                context.Items.Add(item);
                context.SaveChanges();
                itemId = item.Id;
            }
            #endregion

            #endregion

            #region Assert

            controller.SupprimerItem(itemId, hero);

            using (HugoLandContext context = new HugoLandContext())
            {
                Item deletedItem = context.Items.Find(itemId);
                Assert.IsNotNull(deletedItem.IdHero);
                Assert.IsNull(deletedItem.y);
                Assert.IsNull(deletedItem.x);
                #endregion

                //clean
                Hero hero_ = context.Heros.Find(heroId);
                context.Heros.Remove(hero_);
                CompteJoueur compteJoueur_ = context.CompteJoueurs.Find(compteJoueurId);
                context.CompteJoueurs.Remove(compteJoueur_);
                Monde monde_ = context.Mondes.Find(mondeId);
                context.Mondes.Remove(monde_);
                Classe classe_ = context.Classes.Find(classeId);
                context.Classes.Remove(classe_);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// Auteur: Mathias Lavoie-Rivard
        /// Description: Teste de la méthode ModifierQuantitéItem
        /// Date: 2021/2/15
        /// </summary>
        [TestMethod()]
        public void ModifierQuantiteItemTest()
        {
            #region Arrange

            #endregion
        }
    }
}