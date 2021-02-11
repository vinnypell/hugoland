using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP01_Library
{
    /// <summary>
    /// Auteur :        Vincent Pelland
    /// Description:    Gère les actions sur les ObjetMondes.
    /// Date :          2020-02-10
    /// </summary>
    public class ObjetMondeController
    {
        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Créer un nouvel objetmonde d'un monde spécifique passé en paramètre.
        /// Date:           2020-02-10
        /// </summary>
        /// <param name="p_monde"></param>
        /// <param name="p_sDescription"></param>
        /// <param name="p_iPositionX"></param>
        /// <param name="p_iPositionY"></param>
        /// <param name="p_iTypeObjet"></param>
        public void AjouterObjetMonde(Monde p_monde, string p_sDescription, int p_iPositionX, int p_iPositionY, int p_iTypeObjet)
        {
            using (HugoLandContext dbContext = new HugoLandContext()){
                dbContext.ObjetMondes.Add(new ObjetMonde()
                {
                    Description = p_sDescription,
                    x = p_iPositionX,
                    y = p_iPositionY,
                    TypeObjet = p_iTypeObjet,
                    Monde = p_monde,
                    MondeId = p_monde.Id
                });
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Permet de supprimer un objetmonde spécifique à un monde passé en paramètre.
        /// Date :          2020-02-10
        /// </summary>
        /// <param name="p_monde"></param>
        /// <param name="p_objetMonde"></param>
        public void SupprimerObjetMonde(Monde p_monde, ObjetMonde p_objetMonde)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                ObjetMonde objetMonde = dbContext.ObjetMondes.FirstOrDefault(x => x.MondeId == p_monde.Id 
                                                                    && x.Id == p_objetMonde.Id);
                dbContext.ObjetMondes.Remove(objetMonde);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Permet de modifier la description 
        ///                 d'un objetmonde spécifique à un monde, passé en paramètre.
        /// Date :          2020-02-10
        /// </summary>
        /// <param name="p_monde"></param>
        /// <param name="p_objetMonde"></param>
        /// <param name="p_sNouvelleDescription"></param>
        public void ModifierDescriptionObjetMonde(Monde p_monde, ObjetMonde p_objetMonde, string p_sNouvelleDescription)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                ObjetMonde objetMonde = dbContext.ObjetMondes.FirstOrDefault(x => x.MondeId == p_monde.Id &&
                                                                             x.Id == p_objetMonde.Id);
                objetMonde.Description = p_sNouvelleDescription;
                dbContext.SaveChanges();
            }
        }
    }
}
