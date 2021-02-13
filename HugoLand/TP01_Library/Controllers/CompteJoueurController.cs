using System;
using System.Collections.Generic;
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
        public string CreerJoueur(string p_NomJoueur, string p_Courriel, string p_Prenom,string p_Nom, int p_TypeUtilisateur, string p_Mdp)
        {
            System.Data.Entity.Core.Objects.ObjectParameter myOutputParamString = new System.Data.Entity.Core.Objects.ObjectParameter("myOutputParamString", typeof(string));
            using (HugoLandContext dbcontext = new HugoLandContext())
            {
                dbcontext.CreerCompteJoueur(p_NomJoueur, p_Courriel, p_Prenom, p_Nom, p_TypeUtilisateur,p_Mdp, myOutputParamString);
            }
            return (Convert.ToString(myOutputParamString.Value));
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
        public void ModifierJoueur(CompteJoueur p_CompteJoueur, string p_NomJoueur = null, string p_Courriel = null, string p_Prenom = null, string p_Nom = null, int? p_TypeUtilisateur = null)
        {
            //TODO AJOUTER LES INFORAMTION AU COMPTE ET VÉRIFIER SI L'INFORMATION EST CHANGÉ OU NON.
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                CompteJoueur joueurModif = dbContext.CompteJoueurs.FirstOrDefault(x => x.Id == p_CompteJoueur.Id);

                if (string.IsNullOrEmpty(p_NomJoueur))
                {
                joueurModif.NomJoueur = p_NomJoueur;
                }
                if (string.IsNullOrEmpty(p_Courriel))
                {
                    joueurModif.Courriel = p_Courriel;
                }
                if (string.IsNullOrEmpty(p_Prenom))
                {
                    joueurModif.Prenom = p_Prenom;
                }
                if (string.IsNullOrEmpty(p_Nom))
                {
                    joueurModif.Nom = p_Nom;
                }
                if (p_TypeUtilisateur == null)
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
            System.Data.Entity.Core.Objects.ObjectParameter myOutputParamString = new System.Data.Entity.Core.Objects.ObjectParameter("myOutputParamString", typeof(string));
            using (HugoLandContext dbcontext = new HugoLandContext())
            {
                dbcontext.Connexion(p_NomJoueur,p_Mdp,myOutputParamString);
            }
            return (Convert.ToString(myOutputParamString.Value));
        }
    }
}
