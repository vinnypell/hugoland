using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP01_Library
{
    public class ItemController
    {
        /// <summary>
        /// Auteur: Mathias Lavoie-Rivard | 
        /// Summary: Permet de créer un nouvel item. | 
        /// Date: 2021-02-11
        /// </summary>
        /// <param name="p_nom"></param>
        /// <param name="p_description"></param>
        /// <param name="p_x"></param>
        /// <param name="p_y"></param>
        /// <param name="p_ImageID"></param>
        /// <param name="p_MondeID"></param>
        public void AjouterItems(string p_nom, string p_description, int p_x, int p_y, int p_ImageID, int p_MondeID)
        {
            using (HugoLandContext dbcontext = new HugoLandContext())
            {
                dbcontext.Items.Add(new Item()
                {
                    Nom = p_nom,
                    Description = p_description,
                    x = p_x,
                    y = p_y,
                    MondeId = p_MondeID,
                });

                dbcontext.SaveChanges();
            }
        }
        /// <summary>
        /// Auteur: Mathias Lavoie-Rivard | 
        /// Summary: Permet de supprimer un item de la map et de l'ajouter dans l'inventaire d'un hero | 
        /// Date: 2021-02-11
        /// </summary>
        /// <param name="p_Item"></param>
        /// <param name="p_CompteJoeur"></param>
        public void SupprimerItem(Item p_Item, Hero p_Hero)
        {
            using (HugoLandContext dbcontext = new HugoLandContext())
            {
                Item itemDelete = dbcontext.Items.FirstOrDefault(x => x.Id == p_Item.Id);

                itemDelete.x = null;
                itemDelete.y = null;
                itemDelete.IdHero = p_Hero.Id;
                dbcontext.SaveChanges();
            }
        }
        /// <summary>
        /// Auteur: Mathias Lavoie-Rivard | 
        /// Summary: Permet de modifier la quantité d'item qu'un hero a | 
        /// Date: 2021-02-11
        /// </summary>
        public void ModifierQuantiteItem()
        {

        }
    }
}
