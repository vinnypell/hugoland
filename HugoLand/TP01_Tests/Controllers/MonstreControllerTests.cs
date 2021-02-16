using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP01_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP01_Library.Controllers;

namespace TP01_Library.Tests.Controllers
{
    [TestClass()]
    public class MonstreControllerTests
    {
        private MonstreController controller = new MonstreController();

        HugoLandContext context = new HugoLandContext();

        /// <summary>
        /// Auteur : Mathias Lavoie-Rivard
        /// Desc : Teste de la méthode AjouterMonstre
        /// Date: 2121-02-15
        /// </summary>
        [TestMethod()]
        public void AjouterMonstreTest()
        {
            #region Arrange
            Monde monde = new Monde() { Description = "TestMonde" };
            int xPos = 56;
            int yPos = 20;
            string sNom = "TestMonstre";
            #endregion

            #region Act
            controller.AjouterMonstre(monde, xPos, yPos, sNom);
            context.SaveChanges();
            #endregion


            #region Assert
            Monstre monstre = context.Monstres.FirstOrDefault(x => x.Nom == sNom);

            Assert.IsNotNull(monstre);
            Assert.AreEqual(xPos, monstre.x);
            Assert.AreEqual(yPos, monstre.y);
            #endregion

            //cleanup
            Monde mondetest = context.Mondes.FirstOrDefault(x => x.Description == "TestMonde");
            context.Mondes.Remove(mondetest);
            context.Monstres.Remove(monstre);
            context.SaveChanges();
        }
        /// <summary>
        /// Auteur : Mathias Lavoie-Rivard
        /// Desc : Teste de la méthode SupprimerMonstre
        /// Date: 2121-02-15
        /// </summary>
        [TestMethod()]
        public void SupprimerMonstreTest()
        {
            #region Arrange

            Monde monde = new Monde() { Description = "TestMyolo" };
            int xPos = 56;
            int yPos = 20;
            string sNom = "TestMonstre";
            #endregion

            //Vérifie avec un monde non null
            #region Act and Assert

            context.Mondes.Add(monde);
            context.SaveChanges();

            Monstre m = new Monstre()
            {
                Monde = monde,
                x = xPos,
                y = yPos,
                Nom = sNom
            };

            context.Monstres.Add(m);
            context.SaveChanges();

            Monstre monstre = context.Monstres.FirstOrDefault(x => x.Nom == sNom);
            Assert.IsNotNull(monstre);

            controller.SupprimerMonstre(monstre.Id);
            monstre = context.Monstres.FirstOrDefault(x => x.Id == monstre.Id);
            Assert.IsNull(monstre);

            #endregion

            //cleanup

            Monde Mondetest = context.Mondes.FirstOrDefault(x => x.Description == monde.Description);
            context.Mondes.Remove(Mondetest);
            context.SaveChanges();
        }
        /// <summary>
        /// Auteur : Mathias Lavoie-Rivard
        /// Desc : Teste de la méthode ModifierMonstre
        /// Date: 2021-02-15
        /// </summary>
        [TestMethod()]
        public void ModifierInfoMonstreTest()
        {

            #region Arrange
            Monde monde = new Monde() { Description = "TestMonde" };
            int niveau = 4;
            int xPos = 56;
            int yPos = 20;
            string sNom = "TestMonstre";


            //Information modifier
            Monde nouveauMonde = new Monde() { Description = "TestNouveauMonde" };
            string sNouveauNom = "NouveauNom";
            int iNouveauPV = 23;
            int iNouveauNiveau = 96;


            #endregion

            #region Act
            Monstre m = new Monstre()
            {
                Monde = monde,
                x = xPos,
                y = yPos,
                Nom = sNom,
                Niveau = niveau
            };
            context.Monstres.Add(m);
            context.SaveChanges();
            #endregion

            #region Assert
            //Vérifier si le monstre de test est présent et a les informations de bases
            Monstre monstre = context.Monstres.FirstOrDefault(x => x.Nom == sNom);
            Assert.IsNotNull(monstre);
            Assert.AreEqual(xPos, monstre.x);
            Assert.AreEqual(yPos, monstre.y);
            Assert.AreEqual(sNom,monstre.Nom);


            Monstre MonstreAModifier = context.Monstres.FirstOrDefault(x => x.Nom == sNom);

            controller.ModifierInfoMonstre(MonstreAModifier,iNouveauPV,nouveauMonde,sNouveauNom,iNouveauNiveau);

            Monstre MonstreModifier = context.Monstres.FirstOrDefault(x => x.Nom == sNouveauNom);
            Monde NewWorld = context.Mondes.FirstOrDefault(x => x.Description == "NouveauNom");
            Assert.IsNotNull(MonstreModifier);
            Assert.AreEqual(sNouveauNom, MonstreModifier.Nom);
            Assert.AreEqual(iNouveauPV, MonstreModifier.Niveau);
            Assert.AreEqual(monde.Id, MonstreModifier.MondeId);

           
            #endregion
        }
    }
}