using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP01_Library.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP01_Library.Tests.Controllers
{
    [TestClass()]
    public class ClasseControllerTests
    {
        ClasseController controller = new ClasseController();

        [TestMethod()]
        public void AjouterClasseTest()
        {
            Monde monde = new Monde() { Description = "TestMonde" };
            controller.AjouterClasse(monde, "test1", "Testkjahwlkhawl", 12, 12, 15, 80);

            using (var context = new HugoLandContext())
            {
                Classe classe = context.Classes.FirstOrDefault(x => x.NomClasse == "test1");
                Assert.IsNotNull(classe);

                context.Classes.Remove(classe);
                context.SaveChanges();
            }

        }

        [TestMethod()]
        public void SupprimerClasseTest()
        {
            Classe classe = new Classe() { NomClasse = "testClasse16", Description = "testClasse" };
            Monde monde = new Monde() { Description = "TestMonde", Classes = { classe } };

            using (var context = new HugoLandContext())
            {
                context.Mondes.Add(monde);
                context.Classes.Add(classe);
                context.SaveChanges();
                monde = context.Mondes.FirstOrDefault(x => x.Description == "testMonde");
                classe = context.Classes.FirstOrDefault(x => x.NomClasse == "testClasse16");
            }

            controller.SupprimerClasse(classe.Id);

            using (var context = new HugoLandContext())
            {
                Assert.IsNull(context.Classes.FirstOrDefault(x => x.Id == classe.Id));
                monde = context.Mondes.FirstOrDefault(x => x.Description == "testMonde");
                context.Mondes.Remove(monde);
                context.SaveChanges();
            }

        }

        [TestMethod()]
        public void ModifierClasseTest()
        {
            Classe classe = new Classe() { NomClasse = "testClasse16", Description = "testClasse" };
            Monde monde = new Monde() { Description = "TestMonde", Classes = { classe } };

            using (var context = new HugoLandContext())
            {
                context.Mondes.Add(monde);
                context.Classes.Add(classe);
                monde = context.Mondes.First(x => x.Description == "testMonde");
                classe = context.Classes.First(x => x.NomClasse == "testClasse16");
            }
            Classe original = classe.Clone();
            classe.NomClasse = "Modifié";
            controller.ModifierClasse(classe);

            using (var context = new HugoLandContext())
            {
                classe = context.Classes.FirstOrDefault(x => x.Id == classe.Id);

                Assert.AreNotEqual(original.NomClasse, classe.NomClasse);

                context.Classes.Remove(classe);
                context.Mondes.Remove(monde);
            }

        }

        [TestMethod()]
        public void GetClassesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetClasseHeroTest()
        {
            Assert.Fail();
        }
    }
}