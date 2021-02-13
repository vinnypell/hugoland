using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP01_Library.Controllers
{
    /// <summary>
    /// Auteur :        Vincent Pelland
    /// Description:    Gère les actions liées au Héros.
    /// Date :          2021-02-13
    /// </summary>
    public class HeroController
    {
        Random _rnd = new Random();

        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Permet d'ajouter un héro avec des valeurs aléatoires basé sur le niveau du héro ou selon passé en paramètre.
        /// Date :          2021-02-13
        /// </summary>
        /// <param name="p_compteJoueur"></param>
        /// <param name="p_monde"></param>
        /// <param name="p_classe"></param>
        /// <param name="p_sNom"></param>
        /// <param name="p_bEstConnecte"></param>
        /// <param name="p_iExperience"></param>
        /// <param name="p_iPositionX"></param>
        /// <param name="p_iPositionY"></param>
        /// <param name="p_iNiveau"></param>
        public void AjouterHero(CompteJoueur p_compteJoueur, Monde p_monde, Classe p_classe, string p_sNom, bool p_bEstConnecte,
                                    int p_iExperience, int p_iPositionX, int p_iPositionY, int p_iNiveau = 1)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                int iStatPV = 0;
                if (p_iNiveau != 1)
                {
                    iStatPV = (int)((double)Constantes.HP_PER_LEVEL * Constantes.MULTIPLE_HERO_STAT) * p_iNiveau;
                }

                int iMultiplier = _rnd.Next(0, (int)((double)Constantes.MAX_STAT * Constantes.MULTIPLE_HERO_STAT));
                int iStatStr = (int)((double)Constantes.MAX_STAT * Constantes.MULTIPLE_HERO_STAT) * iMultiplier;
                iMultiplier = _rnd.Next(0, (int)((double)Constantes.MAX_STAT * Constantes.MULTIPLE_HERO_STAT));
                int iStatDex = (int)((double)Constantes.MAX_STAT * Constantes.MULTIPLE_HERO_STAT) * iMultiplier;
                iMultiplier = _rnd.Next(0, (int)((double)Constantes.MAX_STAT * Constantes.MULTIPLE_HERO_STAT));
                int iStatInt = (int)((double)Constantes.MAX_STAT * Constantes.MULTIPLE_HERO_STAT) * iMultiplier;

                if (p_compteJoueur != null && p_monde != null && p_classe != null)
                {
                    dbContext.Heros.Add(new Hero()
                    {
                        CompteJoueur = p_compteJoueur,
                        CompteJoueurId = p_compteJoueur.Id,
                        Niveau = p_iNiveau,
                        Experience = 0,
                        x = p_iPositionX,
                        y = p_iPositionY,
                        StatStr = iStatStr,
                        StatDex = iStatDex,
                        StatInt = iStatInt,
                        StatVitalite = iStatPV == 0 ? Constantes.HP_PER_LEVEL : iStatPV,
                        Monde = p_monde,
                        MondeId = p_monde.Id,
                        Classe = p_classe,
                        ClasseId = p_classe.Id,
                        NomHero = p_sNom,
                        EstConnecte = p_bEstConnecte
                    });
                }
            }
        }

        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Permet de supprimer un héro selon le compte joueur et le monde passé en paramètre.
        /// Date :          2021-02-13
        /// </summary>
        /// <param name="p_compteJoueur"></param>
        /// <param name="p_monde"></param>
        /// <param name="p_hero"></param>
        public void SupprimerHero(CompteJoueur p_compteJoueur, Monde p_monde, int p_iHeroId)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                if (p_compteJoueur != null && p_monde != null)
                {
                    Hero hero = dbContext.Heros.FirstOrDefault(x => x.Id == p_iHeroId
                                                                && x.CompteJoueurId == p_compteJoueur.Id
                                                                && x.MondeId == p_monde.Id);

                    dbContext.Heros.Remove(hero);
                    dbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Permet de modifier les valeurs d'un hero selon le compte joueur et le monde passé en paramètre.
        /// Date :          2021-02-13
        /// </summary>
        /// <param name="p_compteJoueur"></param>
        /// <param name="p_monde"></param>
        /// <param name="p_hero"></param>
        /// <param name="p_iExperience"></param>
        /// <param name="p_iNiveau"></param>
        /// <param name="p_sNom"></param>
        /// <param name="p_newCompteJoueur"></param>
        /// <param name="p_iStatPV"></param>
        /// <param name="p_newMonde"></param>
        public void ModifierValeursHeros(CompteJoueur p_compteJoueur, Monde p_monde, Hero p_hero, int p_iExperience = -1,
                                        int p_iNiveau = -1, string p_sNom = "", CompteJoueur p_newCompteJoueur = null,
                                        int p_iStatPV = -1, Monde p_newMonde = null)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                if (p_compteJoueur != null && p_monde != null && p_hero != null)
                {
                    Hero hero = dbContext.Heros.FirstOrDefault(x => x.Id == p_hero.Id
                                            && x.CompteJoueurId == p_compteJoueur.Id
                                            && x.MondeId == p_monde.Id);
                    if (p_iExperience != -1)
                    {
                        hero.Experience = p_iExperience;
                    }

                    if (p_iNiveau != -1)
                    {
                        int iMultiplier = _rnd.Next(0, (int)((double)Constantes.MAX_STAT * (p_iNiveau * Constantes.MULTIPLE_HERO_STAT)));
                        int iStatPV = (int)((double)Constantes.HP_PER_LEVEL * Constantes.MULTIPLE_HERO_STAT) * p_iNiveau;

                        iMultiplier = _rnd.Next(0, (int)((double)Constantes.MAX_STAT * Constantes.MULTIPLE_HERO_STAT));
                        int iStatStr = (int)((double)Constantes.MAX_STAT * Constantes.MULTIPLE_HERO_STAT) * iMultiplier;
                        iMultiplier = _rnd.Next(0, (int)((double)Constantes.MAX_STAT * Constantes.MULTIPLE_HERO_STAT));
                        int iStatDex = (int)((double)Constantes.MAX_STAT * Constantes.MULTIPLE_HERO_STAT) * iMultiplier;
                        iMultiplier = _rnd.Next(0, (int)((double)Constantes.MAX_STAT * Constantes.MULTIPLE_HERO_STAT));
                        int iStatInt = (int)((double)Constantes.MAX_STAT * Constantes.MULTIPLE_HERO_STAT) * iMultiplier;

                        hero.StatDex = iStatDex;
                        hero.StatStr = iStatStr;
                        hero.StatInt = iStatInt;
                        hero.StatVitalite = iStatPV;
                    }

                    if (p_iStatPV != -1)
                    {
                        hero.StatVitalite = p_iStatPV;
                    }

                    if (!string.IsNullOrEmpty(p_sNom))
                    {
                        hero.NomHero = p_sNom;
                    }

                    if (p_newCompteJoueur != null)
                    {
                        hero.CompteJoueur = p_newCompteJoueur;
                        hero.CompteJoueurId = p_newCompteJoueur.Id;
                    }

                    if (p_newMonde != null)
                    {
                        hero.Monde = p_newMonde;
                        hero.MondeId = p_newMonde.Id;
                    }

                    dbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Retourne la liste des objets dans le monde spécifique d'un héro dans un rayon de 200.
        /// Date :          2021-02-13
        /// </summary>
        /// <param name="p_hero"></param>
        /// <param name="p_monde"></param>
        /// <returns></returns>
        public List<ObjetMonde> GetObjetMondes(Hero p_hero, Monde p_monde)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                if (p_monde != null && p_hero != null)
                {
                    int iMaxPos = 200;
                    return dbContext.ObjetMondes.Where(x => x.x < (p_hero.x + iMaxPos) || x.x > (p_hero.x - iMaxPos)
                                                              && x.y < (p_hero.y + iMaxPos) || x.y < (p_hero.y - iMaxPos)).ToList();
                }

                return null;
            }
        }

        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Retourne la liste des héros d'un compte joueur
        /// Date :          2021-02-13
        /// </summary>
        /// <param name="p_compteJoueur"></param>
        /// <returns></returns>
        public List<Hero> GetHeroes(CompteJoueur p_compteJoueur)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                if (p_compteJoueur != null)
                {
                    return dbContext.CompteJoueurs.Where(x => x.Id == p_compteJoueur.Id).SelectMany(h => h.Heros).ToList();
                } 

                return null;
            }
        }

        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Permet de modifier la position du héro en x ou y passé en paramètre.
        /// Date :          2021-02-13
        /// </summary>
        /// <param name="p_hero"></param>
        /// <param name="p_iNewPositionX"></param>
        /// <param name="p_iNewPositionY"></param>
        public void ModifierPositionHero(Hero p_hero, int p_iNewPositionX, int p_iNewPositionY)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                if (p_hero != null)
                {
                    Hero hero = dbContext.Heros.FirstOrDefault(x => x.Id == p_hero.Id);

                    hero.x = p_iNewPositionX;
                    hero.y = p_iNewPositionY;

                    dbContext.SaveChanges();
                }
            }
        }
    }
}