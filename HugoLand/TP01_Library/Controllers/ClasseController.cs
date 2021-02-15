using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP01_Library.Controllers
{
    /// <summary>
    /// Auteur :        Vincent Pelland
    /// Description:    Gère les action liées aux classes d'un monde.
    /// Date :          2021-02-13
    /// </summary>
    public class ClasseController
    {
        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Permet d'ajouter une classe selon son monde passé en paramètre.
        /// Date :          2021-02-13
        /// </summary>
        /// <param name="p_monde"></param>
        /// <param name="p_sNomClasse"></param>
        /// <param name="p_sDescription"></param>
        /// <param name="p_iStatBaseStr"></param>
        /// <param name="p_iStatBaseDex"></param>
        /// <param name="p_iStatBaseInt"></param>
        /// <param name="p_iStatBaseVit"></param>
        public void AjouterClasse(Monde p_monde, string p_sNomClasse, string p_sDescription, int p_iStatBaseStr,
                                int p_iStatBaseDex, int p_iStatBaseInt, int p_iStatBaseVit)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                if (p_monde != null)
                {
                    dbContext.Classes.Add(new Classe()
                    {
                        NomClasse = p_sNomClasse ?? "Noob",
                        Description = p_sDescription ?? "Noob",
                        StatBaseStr = p_iStatBaseStr,
                        StatBaseDex = p_iStatBaseDex,
                        StatBaseInt = p_iStatBaseInt,
                        StatBaseVitalite = p_iStatBaseVit,
                        Monde = p_monde,
                        MondeId = p_monde.Id
                    });
                    dbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Permet de supprimer une classe selon un monde spécifique passé en paramètre.
        /// Date :          2021-02-13
        /// </summary>
        /// <param name="p_monde"></param>
        /// <param name="p_iClasseId"></param>
        public void SupprimerClasse(Monde p_monde, int p_iClasseId)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                if (p_monde != null)
                {
                    Classe classe = dbContext.Classes.FirstOrDefault(x => x.MondeId == p_monde.Id && x.Id == p_iClasseId);

                    dbContext.Classes.Remove(classe);
                    dbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Auteur : Simon Lalancette
        /// Desc : Modifie une classe selon une classe passée en paramètre
        /// </summary>
        /// <param name="modified"></param>
        public void ModifierClasse(Classe modified)
        {
            using (var context = new HugoLandContext())
            {
                Classe original = context.Classes.FirstOrDefault(x => x.Id == modified.Id);

                if(original != null)
                {
                    original.Heros = modified.Heros;
                    original.Monde = modified.Monde;
                    original.NomClasse = modified.NomClasse;
                    original.StatBaseDex = modified.StatBaseDex;
                    original.StatBaseInt = modified.StatBaseInt;
                    original.StatBaseStr = modified.StatBaseStr;
                    original.StatBaseVitalite = modified.StatBaseVitalite;
                    original.Description = modified.Description;
                }

                context.SaveChanges();

            }
        }

        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Permet de modifier les valeurs d'une classe selon le monde spécifier en paramètre.
        /// Date :          2021-02-13
        /// </summary>
        /// <param name="p_monde"></param>
        /// <param name="p_iClasseId"></param>
        /// <param name="p_sNomClasse"></param>
        /// <param name="p_sDescription"></param>
        /// <param name="p_iStatBaseStr"></param>
        /// <param name="p_iStatBaseDex"></param>
        /// <param name="p_iStatBaseInt"></param>
        /// <param name="p_iStatBaseVit"></param>
        public void ModifierClasse(Monde p_monde, int p_iClasseId, string p_sNomClasse, string p_sDescription, int p_iStatBaseStr = -1,
                                int p_iStatBaseDex = -1, int p_iStatBaseInt = -1, int p_iStatBaseVit = -1)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                if (p_monde != null)
                {
                    Classe classe = dbContext.Classes.FirstOrDefault(x => x.MondeId == p_monde.Id && x.Id == p_iClasseId);

                    classe.Description = p_sDescription ?? classe.Description;
                    classe.NomClasse = p_sNomClasse ?? classe.NomClasse;

                    if (p_iStatBaseDex != -1)
                    {
                        classe.StatBaseDex = p_iStatBaseDex;
                    }
                    if (p_iStatBaseInt != -1)
                    {
                        classe.StatBaseInt = p_iStatBaseInt;
                    }
                    if (p_iStatBaseStr != -1)
                    {
                        classe.StatBaseStr = p_iStatBaseStr;
                    }
                    if (p_iStatBaseVit != -1)
                    {
                        classe.StatBaseVitalite = p_iStatBaseVit;
                    }

                    dbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Retourne toutes les classes d'un monde spécifique passé en paramètre.
        /// Date :          2021-02-13
        /// </summary>
        /// <param name="p_monde"></param>
        /// <returns></returns>
        public List<Classe> GetClasses(Monde p_monde)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                if (p_monde != null)
                {
                    return dbContext.Classes.Where(x => x.MondeId == p_monde.Id).ToList();
                }

                return null;
            }
        }

        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Retourne la classe possédant un héro spécifique passé en paramètre.
        /// Date :          2021-02-13
        /// </summary>
        /// <param name="p_hero"></param>
        /// <returns></returns>
        public Classe GetClasseHero(Hero p_hero)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                if (p_hero != null)
                {
                    return dbContext.Classes.FirstOrDefault(x => x.Heros.Contains(p_hero));
                }

                return null;
            }
        }
    }
}
