using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP01_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP01_Library.Controllers;
using Moq;

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
            // variables
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