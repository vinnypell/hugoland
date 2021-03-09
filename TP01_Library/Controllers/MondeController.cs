using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace TP01_Library.Controllers
{
    /// <summary>
    /// Auteur :        Vincent Pelland
    /// Description:    Gestion des mondes créés dans HugoLand.
    /// Date :          2021-02-10
    /// </summary>
    public class MondeController
    {
        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Ajoute un monde neuf, sans listes mais fourni les dimensions et sa description.
        /// Date :          2021-02-10
        /// </summary>
        /// <param name="p_sDescription"></param>
        /// <param name="p_iLimiteX"></param>
        /// <param name="p_iLimiteY"></param>
        public void AjouterMonde(string p_sDescription, int p_iLimiteX, int p_iLimiteY)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                dbContext.Mondes.Add(new Monde()
                {
                    Description = p_sDescription,
                    LimiteX = p_iLimiteX,
                    LimiteY = p_iLimiteY,
                    ObjetMondes = new List<ObjetMonde>(),
                    Monstres = new List<Monstre>(),
                    Items = new List<Item>()
                });
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Supprime le monde passé en paramètre.
        /// Date :          2021-02-10
        /// </summary>
        /// <param name="p_monde"></param>
        public void SupprimerMonde(int p_iMondeId)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                Monde monde = dbContext.Mondes.FirstOrDefault(x => x.Id == p_iMondeId);

                dbContext.Mondes.Remove(monde);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Permet de modifier de nouvelles dimensions d'un monde passé en paramètre.
        /// Date :          2021-02-10
        /// </summary>
        /// <param name="p_monde"></param>
        /// <param name="p_iNouvelleDimensionsX"></param>
        /// <param name="p_iNouvelleDimensionsY"></param>
        public void ModifierDimensionsMonde(int p_iMondeId, int p_iNouvelleDimensionsX, int p_iNouvelleDimensionsY)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                Monde mondeModif = dbContext.Mondes.FirstOrDefault(x => x.Id == p_iMondeId);

                mondeModif.LimiteX = p_iNouvelleDimensionsX;
                mondeModif.LimiteY = p_iNouvelleDimensionsY;

                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Permet de modifier la description d'un monde passé en paramètre.
        /// Date :          2021-02-10
        /// </summary>
        /// <param name="p_monde"></param>
        /// <param name="p_sNouvelleDescription"></param>
        public void ModifierDescriptionMonde(int p_iMondeId, string p_sNouvelleDescription)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                Monde mondeModif = dbContext.Mondes.FirstOrDefault(x => x.Id == p_iMondeId);

                mondeModif.Description = p_sNouvelleDescription;

                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Retourne la liste des Mondes.
        /// Date :          2021-02-10
        /// </summary>
        /// <returns></returns>
        public List<Monde> ListerMondes()
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                return dbContext.Mondes.ToList();
            }
        }

        /// <summary>
        /// Auteur : Simon Lalancette
        /// Description : Retourne un monde selon l'id
        /// Date : 2021-03-05
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Monde GetMonde(int id)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                return dbContext.Mondes.Include(x => x.Items)
                                          .Include(x => x.ObjetMondes)
                                          .Include(x => x.Monstres)
                                          .Include(x => x.Classes)
                                          .Include(x => x.Heros)
                                          .FirstOrDefault(x => x.Id == id);
            }
        }

        #region Méthodes de gestion

        /// <summary>
        /// Description: Retourne les items du monde passé en paramètre
        /// </summary>
        /// <param name="p_monde"></param>
        /// <returns></returns>
        public List<Item> ListerItems(Monde p_monde)
        {
            using (HugoLandContext ctx = new HugoLandContext())
            {
                return ctx.Items.Where(x => x.MondeId == p_monde.Id).ToList();
            }
        }

        /// <summary>
        /// Description: Retourne les objetmondes du monde passé en paramètre
        /// </summary>
        /// <param name="p_monde"></param>
        /// <returns></returns>
        public List<ObjetMonde> ListerObjetMondes(Monde p_monde)
        {
            using (HugoLandContext ctx = new HugoLandContext())
            {
                return ctx.ObjetMondes.Where(x => x.MondeId == p_monde.Id).ToList();
            }
        }

        /// <summary>
        /// Description: Retourne les monstres du monde passé en paramètre
        /// </summary>
        /// <param name="p_monde"></param>
        /// <returns></returns>
        public List<Monstre> ListerMonstres(Monde p_monde)
        {
            using (HugoLandContext ctx = new HugoLandContext())
            {
                return ctx.Monstres.Where(x => x.MondeId == p_monde.Id).ToList();
            }
        }

        /// <summary>
        /// Description: Retourne les héros du monde passé en paramètre
        /// </summary>
        /// <param name="p_monde"></param>
        /// <returns></returns>
        public List<Hero> ListerHeroes(Monde p_monde)
        {
            using (HugoLandContext ctx = new HugoLandContext())
            {
                return ctx.Heros.Where(x => x.MondeId == p_monde.Id).ToList();
            }
        }

        #endregion Méthodes de gestion
    }
}