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
            #region Monde

            #region Arrange
            // variables obj monde
            ObjetMonde objetMonde;
            string sDescription = "Objet monde test";
            int iPosX = 50;
            int iPosY = 100;
            int iTypeObjet = 5;

            // variables monde
            Monde monde;
            int mondeId;
            int oldCount, currCount;

            // environnement de test
            using (HugoLandContext db = new HugoLandContext())
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
                oldCount = db.ObjetMondes.Count();
            }
            #endregion

            #region Act
            // call de la méthode à testé
            ctrl.AjouterObjetMonde(mondeId, sDescription, iPosX, iPosY, iTypeObjet);
            #endregion

            #region Assert
            // aller chercher l'obj monde créer
            using (HugoLandContext db = new HugoLandContext())
            {
                objetMonde = db.ObjetMondes.FirstOrDefault(x => x.MondeId == mondeId && x.Description == sDescription);

                // comparer chaque valeur
                Assert.AreEqual(sDescription, objetMonde.Description);
                Assert.AreEqual(iPosX, objetMonde.x);
                Assert.AreEqual(iPosY, objetMonde.y);
                Assert.AreEqual(iTypeObjet, objetMonde.TypeObjet);
                Assert.AreEqual(mondeId, objetMonde.MondeId);
            }
            #endregion

            #region Supprimer
            using (HugoLandContext db = new HugoLandContext())
            {
                objetMonde = db.ObjetMondes.FirstOrDefault(x => x.MondeId == mondeId && x.Description == sDescription &&
                                                            x.x == iPosX && x.y == iPosY && x.TypeObjet == iTypeObjet);
                db.ObjetMondes.Remove(objetMonde);
                db.Mondes.Remove(monde);
                db.SaveChanges();

                currCount = db.ObjetMondes.Count();
                Assert.AreEqual(oldCount, currCount);
            }
            #endregion

            #endregion
        }

        [TestMethod()]
        public void SupprimerObjetMondeTest()
        {
            #region Monde

            #region Arrange
            // variables obj monde
            ObjetMonde objetMonde;
            int objetMondeId;
            string sDescription = "Objet monde test";
            int iPosX = 50;
            int iPosY = 100;
            int iTypeObjet = 5;
            int objMondeCount;

            // variables monde
            Monde monde;
            int mondeId;

            using (HugoLandContext db = new HugoLandContext())
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

                objetMonde = new ObjetMonde()
                {
                    Description = sDescription,
                    x = iPosX,
                    y = iPosY,
                    TypeObjet = iTypeObjet,
                    MondeId = mondeId
                };

                db.ObjetMondes.Add(objetMonde);
                db.SaveChanges();

                objetMondeId = objetMonde.Id;

                objMondeCount = db.ObjetMondes.Count();
            }
            #endregion

            #region Act & Assert
            // call de la méthode
            ctrl.SupprimerObjetMonde(objetMondeId);

            int newObjMondeCount;
            // vérifie le fonctionnement
            using (HugoLandContext db = new HugoLandContext())
            {
                newObjMondeCount = db.ObjetMondes.Count();

                Assert.AreNotEqual(objMondeCount, newObjMondeCount);

                Monde monde_ = db.Mondes.Find(mondeId);
                db.Mondes.Remove(monde_);
                db.SaveChanges();
            }
            #endregion

            #endregion
        }

        [TestMethod()]
        public void ModifierDescriptionObjetMondeTest()
        {
            #region Arrange
            // variables locales
            string sNewDescription = "Test modif description";
            int objetMondeId;
            int mondeId;
            int newMondeId;
            string sDescription = "Objet monde test";
            int iPosX = 50;
            int iPosY = 100;
            int iTypeObjet = 5;
            Monde monde, newMonde;
            ObjetMonde objetMonde;

            using (HugoLandContext db = new HugoLandContext())
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

                objetMonde = new ObjetMonde()
                {
                    Description = sDescription,
                    x = iPosX,
                    y = iPosY,
                    TypeObjet = iTypeObjet,
                    MondeId = mondeId
                };

                db.ObjetMondes.Add(objetMonde);
                db.SaveChanges();

                objetMondeId = objetMonde.Id;

                newMonde = new Monde()
                {
                    Description = "newMonde",
                    LimiteX = 50,
                    LimiteY = 50
                };

                db.Mondes.Add(newMonde);
                db.SaveChanges();
                newMondeId = newMonde.Id;
            }
            #endregion

            #region Act & Assert
            // call de méthode
            ctrl.ModifierDescriptionObjetMonde(objetMondeId, mondeId, newMondeId, sNewDescription);

            // vérification
            using (HugoLandContext db = new HugoLandContext())
            {
                ObjetMonde objetMonde_ = db.ObjetMondes.Find(objetMondeId);

                Assert.AreEqual(sNewDescription, objetMonde_.Description);
                Assert.AreEqual(newMondeId, objetMonde_.MondeId);
            }
            #endregion

            #region Act & Assert - scénario deux
            // call de méthode - sans changer la description
            ctrl.ModifierDescriptionObjetMonde(objetMondeId, newMondeId, mondeId);

            // vérification
            using (HugoLandContext db = new HugoLandContext())
            {
                ObjetMonde objetMonde_ = db.ObjetMondes.Find(objetMondeId);

                Assert.AreEqual(sNewDescription, objetMonde_.Description);
                Assert.AreEqual(mondeId, objetMonde_.MondeId);
            }
            #endregion

            #region Act & Assert - scénario trois
            // call de méthode - sans changer le monde, mais modifie la description pour rien, qui ne devrait pas être changée
            ctrl.ModifierDescriptionObjetMonde(objetMondeId, mondeId, p_sNouvelleDescription: "");

            // vérification
            using (HugoLandContext db = new HugoLandContext())
            {
                ObjetMonde objetMonde_ = db.ObjetMondes.Find(objetMondeId);

                Assert.AreEqual(sNewDescription, objetMonde_.Description);
                Assert.AreEqual(mondeId, objetMonde_.MondeId);

                Monde monde_ = db.Mondes.Find(mondeId);
                db.Mondes.Remove(monde_);
                db.ObjetMondes.Remove(objetMonde_);
                db.SaveChanges();
            }
            #endregion
        }
    }
}