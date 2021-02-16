using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP01_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP01_Library.Controllers;
using System.Data.Entity.Core.Objects;

namespace TP01_Library.Tests.Controllers
{
    [TestClass()]
    public class CompteJoueurControllerTests
    {
        private CompteJoueurController ctrl = new CompteJoueurController();

        [TestMethod()]
        public void CreerJoueurTest()
        {
            #region Arrange
            string answer = "";
            string expectedAnswer = "SUCCES";
            string sNomComplet = "Sun Wukong";
            string sCourriel = "sun_wukong@example.com";
            string sPrenom = "Sun";
            string sNom = "Wukong";
            string sMdp = "sunWukong99";
            int TypeUtilisateur = 1;
            #endregion

            #region Act & Assert
            // call la méthode
            answer = ctrl.CreerJoueur(sNomComplet, sCourriel, sPrenom, sNom, TypeUtilisateur, sMdp);

            Assert.AreEqual(expectedAnswer, answer);

            using (HugoLandContext db = new HugoLandContext())
            {
                CompteJoueur compteJoueur = db.CompteJoueurs.FirstOrDefault(x => x.NomJoueur == sNomComplet &&
                                                                            x.Courriel == sCourriel);
                Assert.IsNotNull(compteJoueur);
                Assert.AreEqual(sNomComplet, compteJoueur.NomJoueur);
                Assert.AreEqual(sCourriel, compteJoueur.Courriel);
                Assert.AreEqual(sPrenom, compteJoueur.Prenom);
                Assert.AreEqual(sNom, compteJoueur.Nom);
                Assert.AreNotEqual(sMdp, compteJoueur.MotDePasseHash);

                db.CompteJoueurs.Remove(compteJoueur);
                db.SaveChanges();
            }
            #endregion
        }

        [TestMethod()]
        public void SupprimerJoueurTest()
        {
            #region Arrange
            string sNomComplet = "Sun Wukong";
            string sCourriel = "sun_wukong@example.com";
            string sPrenom = "Sun";
            string sNom = "Wukong";
            string sMdp = "sunWukong99";
            int TypeUtilisateur = 1;
            int iCompteJoueur;
            int oldCount, newCount, currCount;

            using (HugoLandContext db = new HugoLandContext())
            {
                oldCount = db.CompteJoueurs.Count();

                ObjectParameter message = new ObjectParameter("message", typeof(string));
                db.CreerCompteJoueur(sNomComplet, sCourriel, sPrenom, sNom, TypeUtilisateur, sMdp, message);

                iCompteJoueur = db.CompteJoueurs.FirstOrDefault(x => x.NomJoueur == sNomComplet &&
                                                                            x.Courriel == sCourriel).Id;
                newCount = db.CompteJoueurs.Count();
            }
            #endregion

            #region Act & Assert
            // call la méthode
            ctrl.SupprimerJoueur(iCompteJoueur);

            using (HugoLandContext db = new HugoLandContext())
            {
                currCount = db.CompteJoueurs.Count();

                Assert.AreEqual(oldCount, currCount);
                Assert.AreNotEqual(oldCount, newCount);
            }
            #endregion
        }

        [TestMethod()]
        public void ModifierJoueurTest()
        {
            #region Arrange
            // old values
            string sNomComplet = "Sun Wukong";
            string sCourriel = "sun_wukong@example.com";
            string sPrenom = "Sun";
            string sNom = "Wukong";
            string sMdp = "sunWukong99";
            int TypeUtilisateur = 1;
            int iCompteJoueur;

            // new values
            string sNewNomComplet = "Monkey king";
            string sNewCourriel = "monkey_king@example.com";
            string sNewPrenom = "Monkey S";
            string sNewNom = "W King";
            int NewTypeUtilisateur = 5;

            using (HugoLandContext db = new HugoLandContext())
            {
                ObjectParameter message = new ObjectParameter("message", typeof(string));
                db.CreerCompteJoueur(sNomComplet, sCourriel, sPrenom, sNom, TypeUtilisateur, sMdp, message);

                iCompteJoueur = db.CompteJoueurs.FirstOrDefault(x => x.NomJoueur == sNomComplet &&
                                                                            x.Courriel == sCourriel).Id;
            }
            #endregion

            #region Act & Assert
            // call la méthode
            ctrl.ModifierJoueur(iCompteJoueur, sNewNomComplet, sNewCourriel, sNewPrenom, sNewNom, NewTypeUtilisateur);

            using (HugoLandContext db = new HugoLandContext())
            {
                CompteJoueur compteJoueur = db.CompteJoueurs.FirstOrDefault(x => x.NomJoueur == sNewNomComplet &&
                                                            x.Courriel == sNewCourriel && x.Id == iCompteJoueur);
                Assert.IsNotNull(compteJoueur);
                Assert.AreNotEqual(sNomComplet, compteJoueur.NomJoueur);
                Assert.AreNotEqual(sCourriel, compteJoueur.Courriel);
                Assert.AreNotEqual(sPrenom, compteJoueur.Prenom);
                Assert.AreNotEqual(sNom, compteJoueur.Nom);

                Assert.AreEqual(sNewNomComplet, compteJoueur.NomJoueur);
                Assert.AreEqual(sNewCourriel, compteJoueur.Courriel);
                Assert.AreEqual(sNewPrenom, compteJoueur.Prenom);
                Assert.AreEqual(sNewNom, compteJoueur.Nom);

                db.CompteJoueurs.Remove(compteJoueur);
                db.SaveChanges();
            }
            #endregion
        }

        [TestMethod()]
        public void ValiderConnexionTest()
        {
            #region Arrange
            string answer = "";
            string expectedAnswer = "SUCCESS";
            string sNomComplet = "Sun Wukong";
            string sCourriel = "sun_wukong@example.com";
            string sPrenom = "Sun";
            string sNom = "Wukong";
            string sMdp = "sunWukong99";
            int TypeUtilisateur = 1;
            int iCompteJoueur;

            using (HugoLandContext db = new HugoLandContext())
            {
                ObjectParameter message = new ObjectParameter("message", typeof(string));
                db.CreerCompteJoueur(sNomComplet, sCourriel, sPrenom, sNom, TypeUtilisateur, sMdp, message);

                iCompteJoueur = db.CompteJoueurs.FirstOrDefault(x => x.NomJoueur == sNomComplet &&
                                                                            x.Courriel == sCourriel).Id;
            }
            #endregion

            #region Act & Assert
            // call la méthode
            answer = ctrl.ValiderConnexion(sMdp, sNomComplet);

            Assert.AreEqual(expectedAnswer, answer);

            using (HugoLandContext db = new HugoLandContext())
            {
                CompteJoueur compteJoueur = db.CompteJoueurs.FirstOrDefault(x => x.NomJoueur == sNomComplet &&
                                                                            x.Courriel == sCourriel && x.Id == iCompteJoueur);
                db.CompteJoueurs.Remove(compteJoueur);
                db.SaveChanges();
            }
            #endregion
        }
    }
}