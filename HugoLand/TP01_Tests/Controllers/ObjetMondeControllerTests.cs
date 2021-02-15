using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP01_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP01_Library.Controllers;
using Moq;
using System.Data.Entity;

namespace TP01_Library.Tests.Controllers
{
    [TestClass()]
    public class ObjetMondeControllerTests
    {
        private ObjetMondeController ctrl = new ObjetMondeController();

        [TestMethod()]
        public void AjouterObjetMondeTest()
        {
            #region Arrange
            // environnement de test
            Mock<DbSet<ObjetMonde>> mockSetObjMonde = new Mock<DbSet<ObjetMonde>>();
            Mock<DbSet<Monde>> mockSetMonde = new Mock<DbSet<Monde>>();
            Mock<HugoLandContext> mockDb = new Mock<HugoLandContext>();
            mockDb.Setup(m => m.ObjetMondes).Returns(mockSetObjMonde.Object);
            mockDb.Setup(m => m.Mondes).Returns(mockSetMonde.Object);

            // variables monde
            Monde monde;
            int mondeId;

            // variables obj monde
            string sDescription = "Objet monde test";
            int iPosX = 50;
            int iPosY = 100;
            int iTypeObjet = 5;

            if (!mockDb.Object.Mondes.Any())
            {
                monde = mockDb.Object.Mondes.Add(new Monde()
                {
                    Description = "Monde test",
                    LimiteX = 200,
                    LimiteY = 200
                });

                mockDb.Object.SaveChanges();
                mondeId = monde.Id;
            }
            else
            {
                monde = mockDb.Object.Mondes.FirstOrDefault();
                mondeId = monde.Id;
            }
            #endregion

            #region Act
            // call de la méthode à testé
            ctrl.AjouterObjetMonde(monde, sDescription, iPosX, iPosY, iTypeObjet);
            #endregion

            #region Assert
            // aller chercher l'obj monde créer
            ObjetMonde objetMonde = mockDb.Object.ObjetMondes.FirstOrDefault(x => x.MondeId == mondeId && x.Description == sDescription);

            // comparer chaque valeur
            Assert.AreEqual(sDescription, objetMonde.Description);
            Assert.AreEqual(iPosX, objetMonde.x);
            Assert.AreEqual(iPosY, objetMonde.y);
            Assert.AreEqual(iTypeObjet, objetMonde.TypeObjet);
            Assert.AreEqual(mondeId, objetMonde.MondeId);
            #endregion
        }

        [TestMethod()]
        public void SupprimerObjetMondeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ModifierDescriptionObjetMondeTest()
        {
            Assert.Fail();
        }
    }
}