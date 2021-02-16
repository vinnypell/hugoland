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
            // variable monstre
            Monde monde, mondeModif;
            Monstre monstre, monstreAModif;
            string sNom = "TestMonstre";
            int niveau = 4;
            int xPos = 56;
            int yPos = 20;
            int mondeId, monstreId, mondeModifId;
            bool newMonde = false;
            bool newMonstre = false;
            bool newMondeModif = false;

            //Information modifier
            string sNouveauNom = "NouveauNom";
            int iNouveauPV = 23;
            int iNouveauNiveau = 96;

            using (HugoLandContext db = new HugoLandContext())
            {
                monstre = db.Monstres.FirstOrDefault();
                if (monstre == null)
                {
                    monde = db.Mondes.FirstOrDefault();
                    if (monde == null)
                    {
                        monde = new Monde()
                        {
                            Description = "",
                            LimiteX = 200,
                            LimiteY = 200
                        };

                        db.Mondes.Add(monde);
                        db.SaveChanges();

                        newMonde = true;
                    }

                    mondeId = monde.Id;

                    monstre = new Monstre()
                    {
                        MondeId = mondeId,
                        x = xPos,
                        y = yPos,
                        Nom = sNom,
                        Niveau = niveau
                    };
                    db.Monstres.Add(monstre);
                    db.SaveChanges();
                }
                monstreId = monstre.Id;
                mondeId = monstre.MondeId;
            }
            #endregion

            #region Act
            controller.ModifierInfoMonstre(monstreId, monstre.StatPV, mondeId, sNom, niveau);
            #endregion

            #region Assert
            //Vérifier si le monstre de test est présent et a les informations de bases
            using (HugoLandContext db = new HugoLandContext())
            {
                Monstre monstre_ = db.Monstres.Find(monstreId);

                Assert.IsNotNull(monstre);
                Assert.AreEqual(xPos, monstre.x);
                Assert.AreEqual(yPos, monstre.y);
                Assert.AreEqual(sNom, monstre.Nom);

                monstreAModif = db.Monstres.Find(monstreId);
                mondeModif = db.Mondes.FirstOrDefault(x => x.Id != mondeId);

                if (mondeModif == null)
                {
                    monde = new Monde()
                    {
                        Description = "mondeModif",
                        LimiteX = 50,
                        LimiteY = 50
                    };

                    db.Mondes.Add(monde);
                    db.SaveChanges();
                    newMondeModif = true;
                }
                mondeModifId = mondeModif.Id;
            }

            controller.ModifierInfoMonstre(monstreId, iNouveauPV, mondeModifId, sNouveauNom, iNouveauNiveau);

            using (HugoLandContext db = new HugoLandContext())
            {
                Monstre monstreFinal = context.Monstres.Find(monstreId);
                Monde nouveauMonde = context.Mondes.Find(mondeModifId);

                Assert.IsNotNull(monstreFinal);
                Assert.IsNotNull(nouveauMonde);
                Assert.AreEqual(sNouveauNom, monstreFinal.Nom);
                Assert.AreEqual(iNouveauPV, monstreFinal.StatPV);
                Assert.AreEqual(mondeModifId, monstreFinal.MondeId);
                Assert.AreEqual(iNouveauNiveau, monstreFinal.Niveau);

                if (newMonstre)
                {
                    db.Monstres.Remove(monstreFinal);
                    if (newMonde)
                    {
                        Monde monde_ = db.Mondes.Find(mondeId);
                        db.Mondes.Remove(monde_);
                        if (newMondeModif)
                        {
                            db.Mondes.Remove(nouveauMonde);
                        }
                    }
                    db.SaveChanges();
                }
            }

            #endregion
        }
    }
}