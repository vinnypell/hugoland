using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP01_Library.Controllers
{
    public class CompteJoueurController
    {


        /// <summary>
        /// Auteur: Mathias Lavoie-Rivard |
        /// Summary: Permet de créer un compte. |
        /// Date: 2021-02-11
        /// </summary>
        /// <param name="p_NomJoueur"></param>
        /// <param name="p_Courriel"></param>
        /// <param name="p_Prenom"></param>
        /// <param name="p_Nom"></param>
        /// <param name="p_TypeUtilisateur"></param>
        /// <param name="p_Mdp"></param>
        /// <returns>Message de succès ou d'erreur</returns>
        public string CreerJoueur(string p_NomJoueur, string p_Courriel, string p_Prenom, string p_Nom, int p_TypeUtilisateur, string p_Mdp)
        {
            using (HugoLandContext dbcontext = new HugoLandContext())
            {
                ObjectParameter message = new ObjectParameter("message", typeof(string));
                dbcontext.CreerCompteJoueur(p_NomJoueur, p_Courriel, p_Prenom, p_Nom, p_TypeUtilisateur, p_Mdp, message);
                return (Convert.ToString(message.Value));
            }
        }
        /// <summary>
        /// Auteur: Mathias Lavoie-Rivard | 
        /// Summary: Permet de supprimer un compte. | 
        /// Date: 2021-02-11
        /// </summary>
        /// <param name="p_CompteJoueur"></param>
        public void SupprimerJoueur(int p_iCompteJoueurId)
        {
            using (HugoLandContext dbcontext = new HugoLandContext())
            {
                CompteJoueur compteJoueur = dbcontext.CompteJoueurs.FirstOrDefault(x => x.Id == p_iCompteJoueurId);

                dbcontext.CompteJoueurs.Remove(compteJoueur);
                dbcontext.SaveChanges();
            }
        }
        /// <summary>
        /// Auteur: Mathias Lavoie-Rivard | 
        /// Summary: Permet de modifier un compte. | 
        /// Date: 2021-02-11
        /// </summary>
        /// <param name="p_CompteJoueur"></param>
        public void ModifierJoueur(int p_iCompteJoueurId, string p_NomJoueur, string p_Courriel, string p_Prenom, string p_Nom, int? p_TypeUtilisateur)
        {
            //TODO AJOUTER LES INFORAMTION AU COMPTE ET VÉRIFIER SI L'INFORMATION EST CHANGÉ OU NON.
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                CompteJoueur joueurModif = dbContext.CompteJoueurs.FirstOrDefault(x => x.Id == p_iCompteJoueurId);

                if (p_NomJoueur != joueurModif.NomJoueur)
                {
                    joueurModif.NomJoueur = p_NomJoueur;
                }
                if (p_Courriel != joueurModif.Courriel)
                {
                    joueurModif.Courriel = p_Courriel;
                }
                if (p_Prenom != joueurModif.Prenom)
                {
                    joueurModif.Prenom = p_Prenom;
                }
                if (p_Nom != joueurModif.Nom)
                {
                    joueurModif.Nom = p_Nom;
                }
                if (p_TypeUtilisateur != null && p_TypeUtilisateur != joueurModif.TypeUtilisateur)
                {
                    joueurModif.TypeUtilisateur = (int)p_TypeUtilisateur;
                }

                dbContext.SaveChanges();
            }
        }
        /// <summary>
        /// Auteur: Mathias Lavoie-Rivard | 
        /// Summary: Permet de Valider la connexion du joueur | 
        /// Date: 2021-02-11
        /// </summary>
        /// <param name="p_Mdp"></param>
        /// <param name="p_NomJoueur"></param>
        /// <returns>Message de succès ou d'erreur</returns>
        public string ValiderConnexion(string p_Mdp, string p_NomJoueur)
        {
            using (HugoLandContext dbcontext = new HugoLandContext())
            {
                ObjectParameter message = new ObjectParameter("message", typeof(string));
                dbcontext.Connexion(p_NomJoueur, p_Mdp, message);
                return (Convert.ToString(message.Value));
            }
        }

        /// <summary>
        /// Auteur: Mathias Lavoie-Rivard
        /// Date: 2021-03-05
        /// Description: Permet de trouver un joueur
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public CompteJoueur TrouverJoueur(string username)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                return dbContext.CompteJoueurs.FirstOrDefault(x => x.NomJoueur == username);
            }
        }
        /// <summary>
        /// Auteur: Mathias Lavoie-Rivard
        /// Date: 2021-03-07
        /// Description: Liste tous les joueurs présent dans la bd
        /// </summary>
        /// <returns></returns>
        public List<CompteJoueur> ListerCompte()
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                return dbContext.CompteJoueurs.ToList();
            }
        }
    }
}
