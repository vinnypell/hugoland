using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP01_Library.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace TP01_Library.Tests.Controllers
{
    [TestClass()]
    public class HeroControllerTests
    {
        HeroController controller = new HeroController();
        //Mock le dbContext
        Mock<HugoLandContext> context = new Mock<HugoLandContext>();

        [TestMethod()]
        public void AjouterHeroTest()
        {
            #region Arrange
            // variables
            context.
            #endregion

            #region Act
            // call de la méthode à testé
            #endregion

            #region Assert
            // Assert.IsTrue(valeur1, valeur2);
            #endregion

            Assert.Fail();
        }

        [TestMethod()]
        public void SupprimerHeroTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ModifierValeursHerosTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetObjetMondesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetHeroesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ModifierPositionHeroTest()
        {
Assert.Fail();
        }
    }
}