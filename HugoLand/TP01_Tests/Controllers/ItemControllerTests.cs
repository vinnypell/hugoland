using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP01_Library;
using System;
using TP01_Library.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP01_Library.Tests.Controllers
{
    [TestClass()]
    public class ItemControllerTests
    {
        private ItemController controller = new ItemController();

        HugoLandContext context = new HugoLandContext();
        CompteJoueur joueur;
        Monde monde;
        Classe classe;
        public void innit()
        {
            CompteJoueur joueur = new CompteJoueur() { Nom = "TestJoueur", Prenom = "testPrenom", NomJoueur = "TestJoueur", Courriel = "testCourriel", MotDePasseHash = new byte[5] };
            Monde monde = new Monde() { Description = "TestMonde" };
            Classe classe = new Classe() { NomClasse = "testClasse", Description = "testClasse" };
        }
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
            int iMondeId = 95;
            #endregion

            #region Act

            controller.AjouterItems(sNom, sDescription, iCoordx, iCoordy, iImage, iMondeId);
            #endregion

            #region Assert
            Item itemTest = context.Items.FirstOrDefault(x => x.Nom == sNom);
            Assert.IsNotNull(itemTest);
            Assert.AreEqual(sNom, itemTest.Nom);
            Assert.AreEqual(sDescription, itemTest.Description);
            Assert.AreEqual(iCoordx, itemTest.x);
            Assert.AreEqual(iCoordy, itemTest.y);
            Assert.AreEqual(iImage, itemTest.ImageId);
            Assert.AreEqual(iMondeId, itemTest.MondeId);
            #endregion

            //Cleanup
            Item ItemTest = context.Items.FirstOrDefault(x => x.Nom == sNom);
            context.Items.Remove(ItemTest);
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
            //Item
            string sNom = "TestItem";
            string sDescription = "TestDescription";
            int iCoordx = 82;
            int iCoordy = 23;
            int iImage = 2;
            int iMondeId = 95;

            //Hero

            int xPos = 14;
            int yPos = 16;
            string Nom = "TestHeroToDelete";



            #endregion

            #region Act


            Hero h = new Hero()
            {
                CompteJoueur = joueur,
                Monde = monde,
                Classe = classe,
                x = xPos,
                y = yPos,
                NomHero = Nom
            };

            Item testItem = new Item()
            {
                Nom = sNom,
                Description = sDescription,
                x = iCoordx,
                y = iCoordy,
                ImageId = iImage,
                MondeId = iMondeId
            };

            context.Heros.Add(h);
            context.Items.Add(testItem);
            context.SaveChanges();

            #endregion

            #region Assert
            Item ItemTest = context.Items.FirstOrDefault(x => x.Nom == sNom);
            Hero HeroTest = context.Heros.FirstOrDefault(x => x.NomHero == Nom);
            controller.SupprimerItem(ItemTest.Id, HeroTest);

            Item deletedItem = context.Items.FirstOrDefault(x => x.Nom == sNom);
            Assert.IsNotNull(deletedItem.IdHero);
            Assert.IsNull(deletedItem.MondeId);
            Assert.IsNull(deletedItem.y);
            Assert.IsNull(deletedItem.x);
            #endregion

            context.Heros.Remove(HeroTest);

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