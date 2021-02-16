using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP01_Library.Controllers
{
    /// <summary>
    /// Auteur :        Vincent Pelland
    /// Description:    Gère les actions sur les ObjetMondes.
    /// Date :          2021-02-10
    /// </summary>
    public class ObjetMondeController
    {
        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Créer un nouvel objetmonde d'un monde spécifique passé en paramètre.
        /// Date:           2021-02-10
        /// </summary>
        /// <param name="p_monde"></param>
        /// <param name="p_sDescription"></param>
        /// <param name="p_iPositionX"></param>
        /// <param name="p_iPositionY"></param>
        /// <param name="p_iTypeObjet"></param>
        public void AjouterObjetMonde(int p_iMondeId, string p_sDescription, int p_iPositionX, int p_iPositionY, int p_iTypeObjet)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                dbContext.ObjetMondes.Add(new ObjetMonde()
                {
                    Description = p_sDescription,
                    x = p_iPositionX,
                    y = p_iPositionY,
                    TypeObjet = p_iTypeObjet,
                    MondeId = p_iMondeId
                });
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Permet de supprimer un objetmonde spécifique à un monde passé en paramètre.
        /// Date :          2021-02-10
        /// </summary>
        /// <param name="p_monde"></param>
        /// <param name="p_objetMonde"></param>
        public void SupprimerObjetMonde(int p_iObjetMondeId)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                ObjetMonde objetMonde = dbContext.ObjetMondes.FirstOrDefault(x => x.Id == p_iObjetMondeId);

                dbContext.ObjetMondes.Remove(objetMonde);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Permet de modifier la description 
        ///                 d'un objetmonde spécifique à un monde, passé en paramètre.
        /// Date :          2021-02-10
        /// </summary>
        /// <param name="p_objetMonde"></param>
        /// <param name="p_monde"></param>
        /// <param name="p_newMonde"></param>
        /// <param name="p_sNouvelleDescription"></param>
        public void ModifierDescriptionObjetMonde(int p_iObjetMondeId, int p_iMondeId, int p_iNewMondeId = -1, string p_sNouvelleDescription = "")
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {

                ObjetMonde objetMonde = dbContext.ObjetMondes.FirstOrDefault(x => x.MondeId == p_iMondeId &&
                                                                             x.Id == p_iObjetMondeId);

                objetMonde.MondeId = p_iMondeId;

                if (p_iNewMondeId > 0)
                {
                    objetMonde.MondeId = p_iNewMondeId;
                }

                if (!string.IsNullOrEmpty(p_sNouvelleDescription))
                {
                    objetMonde.Description = p_sNouvelleDescription;
                }

                dbContext.SaveChanges();
            }
        }
    }
}
