using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP01_Library.Controllers
{
    /// <summary>
    /// Auteur :        Simon Lalancette
    /// Description:    Gère les actions liées au Héros.
    /// Date :          2021-02-13
    /// </summary>
    public class HeroController
    {
        Random _rnd = new Random();
        /// <summary>
        /// Auteur : Simon Lalancette
        /// Desc : Permet de créer un héro et de le sauvegarder dans la Bd
        /// </summary>
        /// <param name="joueur">le joueur qui crée le héro</param>
        /// <param name="monde">le monde dans lequel le héro se trouve</param>
        /// <param name="classe">la classe choisie</param>
        /// <param name="position_x">position de départ x</param>
        /// <param name="position_y">position de départ y</param>
        /// <param name="nomHero">Nom du héro</param>
        public void CreerHero(CompteJoueur joueur, Monde monde, Classe classe, int position_x, int position_y, string nomHero)
        {
            using (var context = new HugoLandContext())
            {
                //randomize base stats
                int Dex = Outil.RollD20();
                int intel = Outil.RollD20();
                int str = Outil.RollD20();
                int pv = 10 + Outil.RollD20();


                Hero newHero = new Hero()
                {
                    Monde = monde,
                    MondeId = monde.Id,
                    NomHero = nomHero,
                    Classe = classe,
                    ClasseId = classe.Id,
                    Experience = 0,
                    Niveau = 1,
                    x = position_x,
                    y = position_y,
                    StatDex = Dex + classe.StatBaseDex,
                    StatInt = intel + classe.StatBaseInt,
                    StatStr = str + classe.StatBaseStr,
                    StatVitalite = pv + classe.StatBaseVitalite,
                    EstConnecte = false,
                    CompteJoueur = joueur,
                    CompteJoueurId = joueur.Id
                };

                context.Heros.Add(newHero);
                context.SaveChanges();
            }
        }


        /// <summary>
        /// Auteur : Simon Lalancette
        /// Desc : Permet de supprimer en cascade un héro et tout objet lié à lui
        /// </summary>
        /// <param name="heroId"></param>
        public void DeleteHero(int heroId)
        {
            using (var context = new HugoLandContext())
            {
                Hero toDelete = context.Heros.FirstOrDefault(x => x.Id == heroId);

                if (toDelete != null)
                {
                    context.InventaireHeroes.RemoveRange(toDelete.InventaireHeroes);
                    toDelete.InventaireHeroes.Clear();

                    context.Items.RemoveRange(toDelete.Items);
                    toDelete.Items.Clear();

                    context.Heros.Remove(toDelete);

                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Auteur : Simon Lalancette
        /// Desc : Permet d'appliqué les modifications apportées à un héro grâce à un formulaire/menu
        /// </summary>
        /// <param name="modified"></param>
        public void EditHero(Hero modified)
        {
            using (var context = new HugoLandContext())
            {
                Hero original = context.Heros.FirstOrDefault(x => x.Id == modified.Id);

                if (original != null)
                {
                    original.Classe = modified.Classe;
                    original.CompteJoueur = modified.CompteJoueur;
                    original.Experience = modified.Experience;
                    original.InventaireHeroes = modified.InventaireHeroes;
                    original.Monde = modified.Monde;
                    original.Niveau = modified.Niveau;
                    original.NomHero = modified.NomHero;
                    original.StatDex = modified.StatDex;
                    original.StatInt = modified.StatInt;
                    original.StatStr = modified.StatStr;
                    original.StatVitalite = modified.StatVitalite;
                    original.x = modified.x;
                    original.y = modified.y;
                    original.Items = modified.Items;
                    original.EstConnecte = modified.EstConnecte;
                }
                context.SaveChanges();
            }

        }
        /// <summary>
        /// Auteur : Simon Lalancette
        /// Desc : retourne la liste des objestMonde dans un rayon de 200m autour du héro
        /// </summary>
        /// <param name="hero"></param>
        /// <returns></returns>
        public List<ObjetMonde> ObjetsVuParHero(Hero hero)
        {
            return hero.Monde.ObjetMondes.Where(o => o.x >= (hero.x - 200) && o.x <= (hero.x + 200) &&
                                                o.y >= (hero.y - 200) && o.y <= (hero.y + 200)).ToList();
        }

        /// <summary>
        /// Auteur : Simon Lalancette
        /// Desc : retourne la liste de tous les héro associé au compte joueur
        /// </summary>
        /// <param name="JoueurId"></param>
        /// <returns></returns>
        public List<Hero> HeroCompteJoueur(int JoueurId)
        {
            using (var context = new HugoLandContext())
            {
                return context.Heros.Where(x => x.CompteJoueurId == JoueurId).ToList();
            }
        }

        /// <summary>
        /// Auteur : Simon Lalancette
        /// Desc : Met à jour la position du héro selon les nouvelles coordonnées
        /// </summary>
        /// <param name="HeroId"></param>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        public void Move(int HeroId, int newX, int newY)
        {
            using (var context = new HugoLandContext())
            {
                Hero hero = context.Heros.Find(HeroId);
                hero.x = newX;
                hero.y = newY;

                context.SaveChanges();
            }
        }
    }
}