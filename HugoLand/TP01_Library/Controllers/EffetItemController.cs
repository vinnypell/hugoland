using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP01_Library.Controllers
{
    public class EffetItemController
    {
        /// <summary>
        /// Auteur : Vincent Pelland
        /// Description: 
        /// Date : 2021-02-13
        /// </summary>
        /// <param name="p_item"></param>
        /// <param name="p_iValeurEffet"></param>
        /// <param name="p_iTypeEffet"></param>
        public void AjouterEffetItem(Item p_item, int p_iValeurEffet, int p_iTypeEffet)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                if (p_item != null)
                {
                    dbContext.EffetItems.Add(new EffetItem()
                    {
                        Item = p_item,
                        ItemId = p_item.Id,
                        ValeurEffet = p_iValeurEffet,
                        TypeEffet = p_iTypeEffet
                    });
                    dbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Permet de supprimer un effetitem spécifique à un item.
        /// Date :          2021-02-13
        /// </summary>
        /// <param name="p_item"></param>
        public void SupprimerEffetItem(int p_iEffetItemId, Item p_item)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                if (p_item != null)
                {
                    EffetItem effetItem = dbContext.EffetItems.FirstOrDefault(x => x.ItemId == p_item.Id && x.Id == p_iEffetItemId);
                    dbContext.EffetItems.Remove(effetItem);
                    dbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Permet de modifier les informations d'un effetitem d'un item spécifique,
        ///                 ainsi que modifier l'item auquel il appartient.
        /// Date :          2021-02-13
        /// </summary>
        /// <param name="p_item"></param>
        /// <param name="p_newItem"></param>
        /// <param name="p_iValeurEffet"></param>
        /// <param name="p_iTypeEffet"></param>
        public void ModifierEffetItem(Item p_item, Item p_newItem, int p_iEffetItemId, int p_iValeurEffet = -1, int p_iTypeEffet = -1)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                if (p_item != null)
                {
                    EffetItem effetItem = dbContext.EffetItems.FirstOrDefault(x => x.ItemId == p_item.Id && x.Id == p_iEffetItemId);

                    if (p_iTypeEffet != -1)
                    {
                        effetItem.TypeEffet = p_iTypeEffet;
                    }

                    if (p_iValeurEffet != -1)
                    {
                        effetItem.ValeurEffet = p_iValeurEffet;
                    }

                    if (p_newItem != null)
                    {
                        effetItem.Item = p_newItem;
                        effetItem.ItemId = p_newItem.Id;
                    }

                    dbContext.SaveChanges();

                }
            }
        }
    }
}
