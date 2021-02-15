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
            // variables obj monde
            ObjetMonde objetMonde = new ObjetMonde();
            string sDescription = "Objet monde test";
            int iPosX = 50;
            int iPosY = 100;
            int iTypeObjet = 5;

            // variables monde
            Monde monde = new Monde();
            int mondeId;
            bool newMonde = false;

            // environnement de test
            using (HugoLandContext db = new HugoLandContext())
            {
                monde = db.Mondes.FirstOrDefault();
                mondeId = monde.Id;

                if (monde == null)
                {
                    monde = new Monde()
                    {
                        Description = "Monde test",
                        LimiteX = 200,
                        LimiteY = 200
                    };

                    db.Mondes.Add(monde);
                    db.SaveChanges();

                    mondeId = monde.Id;
                    newMonde = true;
                }
            }
            #endregion

            #region Act
            // call de la méthode à testé
            ctrl.AjouterObjetMonde(monde, sDescription, iPosX, iPosY, iTypeObjet);
            #endregion

            #region Assert
            // aller chercher l'obj monde créer
            using (HugoLandContext db = new HugoLandContext())
            {
                objetMonde = db.ObjetMondes.FirstOrDefault(x => x.MondeId == mondeId && x.Description == sDescription);
            }

            // comparer chaque valeur
            Assert.AreEqual(sDescription, objetMonde.Description);
            Assert.AreEqual(iPosX, objetMonde.x);
            Assert.AreEqual(iPosY, objetMonde.y);
            Assert.AreEqual(iTypeObjet, objetMonde.TypeObjet);
            Assert.AreEqual(mondeId, objetMonde.MondeId);
            #endregion

            #region Supprimer
            using (HugoLandContext db = new HugoLandContext())
            {
                objetMonde = db.ObjetMondes.FirstOrDefault(x => x.MondeId == mondeId && x.Description == sDescription &&
                                                            x.x == iPosX && x.y == iPosY && x.TypeObjet == iTypeObjet);
                db.ObjetMondes.Remove(objetMonde);
                if (newMonde)
                    db.Mondes.Remove(monde);
                db.SaveChanges();
            }
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