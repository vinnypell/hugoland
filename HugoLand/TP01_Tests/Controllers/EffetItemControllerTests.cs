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
    public class EffetItemControllerTests
    {
        private EffetItemController ctrl = new EffetItemController();

        [TestMethod()]
        public void AjouterEffetItemTest()
        {
            #region Arrange
            // variables locale
            Item item;
            int itemId;
            Monde monde;
            int mondeId;
            EffetItem effetItem;
            int iValeurEffet = 5;
            int iTypeEffet = 10;
            bool newMonde = false;
            bool newItem = false;

            using (HugoLandContext db = new HugoLandContext())
            {
                item = db.Items.FirstOrDefault();

                if (item == null)
                {
                    monde = db.Mondes.FirstOrDefault();
                    if (monde == null)
                    {
                        monde = new Monde()
                        {
                            Description = "",
                            LimiteX = 0,
                            LimiteY = 0
                        };
                        db.Mondes.Add(monde);
                        db.SaveChanges();
                        newMonde = true;
                    }


                    item = new Item()
                    {
                        Nom = "Bâton magique",
                        Description = "Bâton de Sun-Wukong",
                        x = 0,
                        y = 0,
                        MondeId = monde.Id
                    };

                    db.Items.Add(item);
                    db.SaveChanges();
                    newItem = true;
                }
                mondeId = item.MondeId;
                itemId = item.Id;
            }
            #endregion

            #region Act & Assert
            // call de la méthode
            ctrl.AjouterEffetItem(itemId, iValeurEffet, iTypeEffet);

            // vérification
            using (HugoLandContext db = new HugoLandContext())
            {
                effetItem = db.EffetItems.FirstOrDefault(x => x.ItemId == itemId
                                        && x.ValeurEffet == iValeurEffet && x.TypeEffet == iTypeEffet);

                Assert.AreEqual(iTypeEffet, effetItem.TypeEffet);
                Assert.AreEqual(iValeurEffet, effetItem.ValeurEffet);
                Assert.AreEqual(itemId, effetItem.ItemId);

                if (newItem)
                {
                    Item item_ = db.Items.Find(itemId);
                    db.Items.Remove(item_);
                    if (newMonde)
                    {
                        Monde monde_ = db.Mondes.Find(mondeId);
                        db.Mondes.Remove(monde_);
                    }
                    db.SaveChanges();
                }
            }
            #endregion
        }

        [TestMethod()]
        public void SupprimerEffetItemTest()
        {
            #region Arrange
            // variables locale
            Item item;
            int itemId;
            Monde monde;
            int mondeId;
            EffetItem effetItem;
            int effetItemId;
            int iValeurEffet = 5;
            int iTypeEffet = 10;
            int m_count;
            bool newMonde = false;
            bool newItem = false;

            using (HugoLandContext db = new HugoLandContext())
            {
                item = db.Items.FirstOrDefault();

                if (item == null)
                {
                    monde = db.Mondes.FirstOrDefault();
                    if (monde == null)
                    {
                        monde = new Monde()
                        {
                            Description = "",
                            LimiteX = 0,
                            LimiteY = 0
                        };
                        db.Mondes.Add(monde);
                        db.SaveChanges();
                        newMonde = true;
                    }


                    item = new Item()
                    {
                        Nom = "Bâton magique",
                        Description = "Bâton de Sun-Wukong",
                        x = 0,
                        y = 0,
                        MondeId = monde.Id
                    };

                    db.Items.Add(item);
                    db.SaveChanges();
                    newItem = true;
                }
                mondeId = item.MondeId;
                itemId = item.Id;

                effetItem = new EffetItem()
                {
                    ItemId = itemId,
                    ValeurEffet = iValeurEffet,
                    TypeEffet = iTypeEffet
                };

                db.EffetItems.Add(effetItem);
                db.SaveChanges();
                effetItemId = effetItem.Id;
                m_count = db.EffetItems.Count();
            }
            #endregion

            #region Act & Assert
            // call de la méthode
            ctrl.SupprimerEffetItem(effetItemId, itemId);

            // vérification
            using (HugoLandContext db = new HugoLandContext())
            {
                int newCount = db.EffetItems.Count();

                Assert.AreNotEqual(m_count, newCount);

                if (newItem)
                {
                    Item item_ = db.Items.Find(itemId);
                    db.Items.Remove(item_);
                    if (newMonde)
                    {
                        Monde monde_ = db.Mondes.Find(mondeId);
                        db.Mondes.Remove(monde_);
                    }
                    db.SaveChanges();
                }
            }
            #endregion
        }

        [TestMethod()]
        public void ModifierEffetItemTest()
        {
            #region Arrange
            // variables locale
            Item item;
            int itemId;
            Monde monde;
            int mondeId;
            EffetItem effetItem;
            int effetItemId;
            int iValeurEffet = 5;
            int iTypeEffet = 10;
            int m_count;
            bool newMonde = false;
            bool newItem = false;
            bool newEffetItem = false;

            using (HugoLandContext db = new HugoLandContext())
            {
                effetItem = db.EffetItems.FirstOrDefault();
                if (effetItem == null)
                {
                    item = db.Items.FirstOrDefault();

                    if (item == null)
                    {
                        monde = db.Mondes.FirstOrDefault();
                        if (monde == null)
                        {
                            monde = new Monde()
                            {
                                Description = "",
                                LimiteX = 0,
                                LimiteY = 0
                            };
                            db.Mondes.Add(monde);
                            db.SaveChanges();
                            newMonde = true;
                        }

                        item = new Item()
                        {
                            Nom = "Bâton magique",
                            Description = "Bâton de Sun-Wukong",
                            x = 0,
                            y = 0,
                            MondeId = monde.Id
                        };

                        db.Items.Add(item);
                        db.SaveChanges();
                        newItem = true;
                    }
                    mondeId = item.MondeId;
                    itemId = item.Id;

                    effetItem = new EffetItem()
                    {
                        ItemId = itemId,
                        ValeurEffet = iValeurEffet,
                        TypeEffet = iTypeEffet
                    };

                    db.EffetItems.Add(effetItem);
                    db.SaveChanges();
                    newEffetItem = true;
                }

                effetItemId = effetItem.Id;
                itemId = effetItem.ItemId;
                mondeId = effetItem.Item.MondeId;
            }
            #endregion

            #region Act & Assert
            // call de la méthode
            ctrl.ModifierEffetItem(itemId, effetItemId, iValeurEffet, iTypeEffet);

            // vérification
            using (HugoLandContext db = new HugoLandContext())
            {
                int newCount = db.EffetItems.Count();

                if (newEffetItem)
                {
                    EffetItem effetItem_ = db.EffetItems.Find(effetItemId);
                    db.EffetItems.Remove(effetItem_);
                    if (newItem)
                    {
                        Item item_ = db.Items.Find(itemId);
                        db.Items.Remove(item_);
                        if (newMonde)
                        {
                            Monde monde_ = db.Mondes.Find(mondeId);
                            db.Mondes.Remove(monde_);
                        }
                    }
                    db.SaveChanges();
                }
            }
            #endregion
        }
    }
}