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
            Monde monde;
            Monstre monstre;
            int xPos = 56;
            int yPos = 20;
            string sNom = "TestMonstre";
            #endregion

            #region Act & Assert
            using (HugoLandContext context = new HugoLandContext())
            {
                monde = new Monde() { Description = "TestMonde" };
                context.SaveChanges();
            }

            controller.AjouterMonstre(monde, xPos, yPos, sNom);
            using (HugoLandContext context = new HugoLandContext())
            {
                monstre = context.Monstres.FirstOrDefault(x => x.Nom == sNom);

                Assert.IsNotNull(monstre);
                Assert.AreEqual(xPos, monstre.x);
                Assert.AreEqual(yPos, monstre.y);

                //cleanup
                Monde mondetest = context.Mondes.FirstOrDefault(x => x.Description == "TestMonde");
                context.Mondes.Remove(mondetest);
                context.Monstres.Remove(monstre);
                context.SaveChanges();
            }
            #endregion
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

            Monde monde;
            Monstre monstre;
            int xPos = 56;
            int yPos = 20;
            string sNom = "TestMonstre";

            using (HugoLandContext context = new HugoLandContext())
            {
                monde = new Monde() { Description = "TestMyolo" };
                context.Mondes.Add(monde);

                monstre = new Monstre()
                {
                    MondeId = monde.Id,
                    x = xPos,
                    y = yPos,
                    Nom = sNom
                };

                context.Monstres.Add(monstre);
                context.SaveChanges();
            }
            #endregion

            //Vérifie avec un monde non null
            #region Act and Assert

            controller.SupprimerMonstre(monstre.Id);

            using (HugoLandContext context = new HugoLandContext())
            {
                monstre = context.Monstres.Find(monstre.Id);
                Assert.IsNull(monstre);

                //cleanup
                Monde Mondetest = context.Mondes.Find(monde.Id);
                context.Mondes.Remove(Mondetest);
                context.SaveChanges();
            }
            #endregion

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

            //Information modifier
            string sNouveauNom = "NouveauNom";
            int iNouveauPV = 23;
            int iNouveauNiveau = 96;

            using (HugoLandContext db = new HugoLandContext())
            {
                monstre = db.Monstres.FirstOrDefault();

                monde = new Monde()
                {
                    Description = "",
                    LimiteX = 200,
                    LimiteY = 200
                };

                db.Mondes.Add(monde);
                db.SaveChanges();

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

                monstreId = monstre.Id;
            }
            #endregion

            #region Act & Assert
            controller.ModifierInfoMonstre(monstreId, monstre.StatPV, mondeId, sNom, niveau);

            //Vérifier si le monstre de test est présent et a les informations de bases
            using (HugoLandContext db = new HugoLandContext())
            {
                Monstre monstre_ = db.Monstres.Find(monstreId);

                Assert.IsNotNull(monstre);
                Assert.AreEqual(xPos, monstre.x);
                Assert.AreEqual(yPos, monstre.y);
                Assert.AreEqual(sNom, monstre.Nom);

                monstreAModif = db.Monstres.Find(monstreId);

                mondeModif = new Monde()
                {
                    Description = "mondeModif",
                    LimiteX = 50,
                    LimiteY = 50
                };

                db.Mondes.Add(mondeModif);
                db.SaveChanges();
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

                db.Monstres.Remove(monstreFinal);
                Monde monde_ = db.Mondes.Find(mondeId);
                db.Mondes.Remove(monde_);
                db.Mondes.Remove(nouveauMonde);
                db.SaveChanges();
            }
        }

        #endregion
    }
}
