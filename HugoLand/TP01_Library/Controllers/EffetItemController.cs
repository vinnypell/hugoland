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
        public void AjouterEffetItem(int p_iItemId, int p_iValeurEffet, int p_iTypeEffet)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                dbContext.EffetItems.Add(new EffetItem()
                {
                    ItemId = p_iItemId,
                    ValeurEffet = p_iValeurEffet,
                    TypeEffet = p_iTypeEffet
                });
                dbContext.SaveChanges();

            }
        }

        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Permet de supprimer un effetitem spécifique à un item.
        /// Date :          2021-02-13
        /// </summary>
        /// <param name="p_item"></param>
        public void SupprimerEffetItem(int p_iEffetItemId, int p_iItemId)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                EffetItem effetItem = dbContext.EffetItems.FirstOrDefault(x => x.ItemId == p_iItemId && x.Id == p_iEffetItemId);
                dbContext.EffetItems.Remove(effetItem);
                dbContext.SaveChanges();
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
        public void ModifierEffetItem(int p_iItemId, int p_iEffetItemId, int p_iValeurEffet, int p_iTypeEffet)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {

                EffetItem effetItem = dbContext.EffetItems.FirstOrDefault(x => x.Id == p_iEffetItemId);

                if (p_iTypeEffet != effetItem.TypeEffet)
                {
                    effetItem.TypeEffet = p_iTypeEffet;
                }

                if (p_iValeurEffet != effetItem.ValeurEffet)
                {
                    effetItem.ValeurEffet = p_iValeurEffet;
                }

                if (p_iItemId != effetItem.ItemId)
                {
                    effetItem.ItemId = p_iItemId;
                }

                dbContext.SaveChanges();
            }
        }
    }
}
