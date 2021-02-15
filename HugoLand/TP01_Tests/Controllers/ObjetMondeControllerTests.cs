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
            #region Monde - pas null

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
            bool newMonde = false;
            int count;

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
                if (newMonde)
                    db.Mondes.Remove(monde);
                db.SaveChanges();

                count = db.ObjetMondes.Count();
            }
            #endregion

            #endregion
        }

        [TestMethod()]
        public void SupprimerObjetMondeTest()
        {
            #region Monde - pas null

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
            bool newMonde = false;

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

                if (newMonde)
                {
                    Monde monde_ = db.Mondes.Find(mondeId);
                    db.Mondes.Remove(monde_);
                    db.SaveChanges();
                }
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
            bool newObjMonde = false;
            Monde monde;
            ObjetMonde objetMonde;

            using (HugoLandContext db = new HugoLandContext())
            {
                objetMonde = db.ObjetMondes.FirstOrDefault();

                if (objetMonde == null)
                {
                    monde = db.Mondes.FirstOrDefault();

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
                    mondeId = monde.Id;
                }

                newObjMonde = true;

                objetMondeId = objetMonde.Id;
                mondeId = objetMonde.MondeId;

                newMondeId = db.Mondes.FirstOrDefault(x => x.Id != mondeId).Id;
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

                if (newObjMonde)
                {
                    Monde monde_ = db.Mondes.Find(mondeId);
                    db.Mondes.Remove(monde_);
                    db.ObjetMondes.Remove(objetMonde_);
                    db.SaveChanges();
                }
            }
            #endregion
        }
    }
}