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
            //Item
            string sNom = "TestItem";
            string sDescription = "TestDescription";
            int iCoordx = 82;
            int iCoordy = 23;
            int iImage = 2;

            //Hero


            string message = CJcontroller.CreerJoueur("TestJoueur", "testCourriel", "testPrenom", "TestJoueur", 1, "dawdawdawdaw");
            Monde monde = new Monde() { Description = "TestMonde" };
            Classe classe = new Classe() { NomClasse = "testClasse", Description = "testClasse" };

            #endregion

            #region Act

            context.Mondes.Add(monde);
            context.SaveChanges();

            monde = context.Mondes.FirstOrDefault(x => x.Description == "TestMonde");
            classe = new Classe() { NomClasse = "testClasse", Description = "testClasse",MondeId = monde.Id };
            context.Classes.Add(classe);
            context.SaveChanges();

            CompteJoueur joueur = context.CompteJoueurs.FirstOrDefault(x => x.Nom == "TestJoeur");
            monde = context.Mondes.FirstOrDefault(x => x.Description == "TestMonde");
            classe = context.Classes.FirstOrDefault(x => x.NomClasse == "testClasse");



            Item testItem = new Item()
            {
                Nom = sNom,
                Description = sDescription,
                x = iCoordx,
                y = iCoordy,
                ImageId = iImage,
                MondeId = monde.Id
            };

            context.Items.Add(testItem);
            context.SaveChanges();



            Hcontroller.CreerHero(joueur,monde,classe,2,3,"TestHero");

            #endregion

            #region Assert
            Item ItemTest = context.Items.FirstOrDefault(x => x.Nom == sNom);
            Hero HeroTest = context.Heros.FirstOrDefault(x => x.NomHero == "TestHero");
            controller.SupprimerItem(ItemTest.Id, HeroTest);

            Item deletedItem = context.Items.FirstOrDefault(x => x.Nom == sNom);
            Assert.IsNotNull(deletedItem.IdHero);
            Assert.IsNull(deletedItem.y);
            Assert.IsNull(deletedItem.x);
            #endregion

            //clean
            context.Heros.Remove(HeroTest);
            context.CompteJoueurs.Remove(joueur);
            context.Mondes.Remove(monde);
            context.Classes.Remove(classe);
            context.SaveChanges();
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