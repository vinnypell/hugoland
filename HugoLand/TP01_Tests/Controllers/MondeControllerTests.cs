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
    public class MondeControllerTests
    {
        private MondeController ctrl = new MondeController();

        [TestMethod()]
        public void AjouterMondeTest()
        {
            #region Arrange
            // variables locales
            string sDescription = "Monde test";
            int iLimiteX = 1000;
            int iLimiteY = 1500;
            #endregion

            #region Act & Assert
            // call de la méthode
            ctrl.AjouterMonde(sDescription, iLimiteX, iLimiteY);

            using (HugoLandContext db = new HugoLandContext())
            {
                Monde monde = db.Mondes.FirstOrDefault(x => x.Description == sDescription && x.LimiteX == iLimiteX &&
                                                        x.LimiteY == iLimiteY);

                Assert.IsNotNull(monde);
                Assert.AreEqual(sDescription, monde.Description);
                Assert.AreEqual(iLimiteX, monde.LimiteX);
                Assert.AreEqual(iLimiteY, monde.LimiteY);

                db.Mondes.Remove(monde);
                db.SaveChanges();
            }
            #endregion
        }

        [TestMethod()]
        public void SupprimerMondeTest()
        {
            #region Arrange
            // variables locales
            string sDescription = "Monde test";
            int iLimiteX = 1000;
            int iLimiteY = 1500;
            int oldCount, addCount, mondeId;
            Monde monde;


            using (HugoLandContext db = new HugoLandContext())
            {
                oldCount = db.Mondes.Count();

                monde = new Monde()
                {
                    Description = sDescription,
                    LimiteX = iLimiteX,
                    LimiteY = iLimiteY
                };

                db.Mondes.Add(monde);
                db.SaveChanges();

                mondeId = monde.Id;
                addCount = db.Mondes.Count();
            }
            #endregion

            #region Act & Assert
            // call de la méthode
            ctrl.SupprimerMonde(mondeId);

            using (HugoLandContext db = new HugoLandContext())
            {
                int newCount = db.Mondes.Count();

                Assert.AreNotEqual(addCount, newCount);
                Assert.AreEqual(oldCount, newCount);
            }
            #endregion
        }

        [TestMethod()]
        public void ModifierDimensionsMondeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ModifierDescriptionMondeTest()
        {
            #region Arrange
            // variables
            string sNewDescription = "Nouvelle description";
            string sOldDescription;
            Monde monde;
            bool newMonde = false;
            int mondeId;
            int iLimiteX = 1000;
            int iLimiteY = 1500;

            using (HugoLandContext db = new HugoLandContext())
            {
                monde = db.Mondes.FirstOrDefault();

                if (monde == null)
                {
                    monde = new Monde()
                    {
                        Description = "",
                        LimiteX = iLimiteX,
                        LimiteY = iLimiteY
                    };

                    db.Mondes.Add(monde);
                    db.SaveChanges();

                    newMonde = true;
                }

                sOldDescription = monde.Description;
                mondeId = monde.Id;
            }
            #endregion

            #region Act & Assert
            // call de la méthode
            ctrl.ModifierDescriptionMonde(mondeId, sNewDescription);

            // vérification
            using (HugoLandContext db = new HugoLandContext())
            {
                Monde monde_ = db.Mondes.Find(mondeId);

                Assert.AreEqual(sNewDescription, monde_.Description);
                Assert.AreEqual(mondeId, monde_.Id);

                if (newMonde)
                {
                    db.Mondes.Remove(monde_);
                    db.SaveChanges();
                }
            }
            #endregion
        }

        [TestMethod()]
        public void ListerMondesTest()
        {
            #region Arrange, Act & Assert
            // variables
            List<Monde> mondes = new List<Monde>();
            int expectedMondes;

            using (HugoLandContext db = new HugoLandContext())
            {
                expectedMondes = db.Mondes.Count();
            }

            // call de la méthode
            mondes = ctrl.ListerMondes();

            Assert.AreEqual(expectedMondes, mondes.Count());
            #endregion
        }
    }
}