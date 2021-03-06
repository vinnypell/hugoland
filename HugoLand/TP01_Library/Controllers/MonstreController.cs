﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP01_Library.Controllers;

namespace TP01_Library.Controllers
{
    /// <summary>
    /// Auteur :        Vincent Pelland
    /// Descripton:     Gère les actions sur les Monstres.
    /// Date :          2021-02-10
    /// </summary>
    public class MonstreController
    {
        Random _rnd = new Random();

        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Permet d'ajouter un monstre dans un monde spécifique.
        /// Date :          2021-02-10
        /// </summary>
        /// <param name="p_monde"></param>
        /// <param name="p_iPositionX"></param>
        /// <param name="p_iPositionY"></param>
        /// <param name="p_sNom"></param>
        public void AjouterMonstre(Monde p_monde, int p_iPositionX, int p_iPositionY, string p_sNom)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                int iNiveauLVL = _rnd.Next(0, Constantes.MAX_LEVEL);
                int iDmgMIN = Constantes.DMG_PER_LEVEL * iNiveauLVL - Constantes.DMG_MIN_GAP;
                int iDmgMAX = Constantes.DMG_PER_LEVEL * iNiveauLVL;
                int iStatPV = Constantes.HP_PER_LEVEL * iNiveauLVL;
                //randomize img id

                dbContext.Monstres.Add(new Monstre()
                {
                    x = p_iPositionX,
                    y = p_iPositionY,
                    Nom = p_sNom,
                    Niveau = iNiveauLVL,
                    StatPV = iStatPV,
                    StatDmgMax = iDmgMAX,
                    StatDmgMin = iDmgMIN,
                    Monde = p_monde,
                    MondeId = p_monde.Id
                    //need img, but what
                });
                dbContext.SaveChanges();
            }
        }


        /// <summary>
        /// Auteur :        Vincent Pelland
        /// Description:    Permet de supprimer/tuer un monstre selon son monde passé en paramètre.
        /// Date :          2021-02-10
        /// </summary>
        /// <param name="p_monde"></param>
        /// <param name="p_monstre"></param>
        public void SupprimerMonstre(int p_iMonstreId)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {

                Monstre monstre = dbContext.Monstres.FirstOrDefault(x => x.Id == p_iMonstreId);

                dbContext.Monstres.Remove(monstre);
                dbContext.SaveChanges();
            }
        }


        /// <summary>
        /// Auteur :    Vincent Pelland
        /// Description:    Permet de modifier les informations 
        ///                 du monstre selon son monde passé en paramètre.
        /// Date :  2021-02-10
        /// </summary>
        /// <param name="p_monde"></param>
        /// <param name="p_monstre"></param>
        /// <param name="p_sNom"></param>
        /// <param name="p_iNouveauNiveau"></param>
        public void ModifierInfoMonstre(int p_iMonstreid, int p_Pv,int p_iMondeId, string p_sNom, int p_iNiveau)
        {
            using (HugoLandContext dbContext = new HugoLandContext())
            {
                Monstre monstre = dbContext.Monstres.FirstOrDefault(x => x.Id == p_iMonstreid);

                if (p_iNiveau != monstre.Niveau)
                {
                    int iDmgMIN = Constantes.DMG_PER_LEVEL * p_iNiveau - Constantes.DMG_MIN_GAP;
                    int iDmgMAX = Constantes.DMG_PER_LEVEL * p_iNiveau;


                    monstre.Niveau = p_iNiveau;
                    monstre.StatDmgMax = iDmgMAX;
                    monstre.StatDmgMin = iDmgMIN;
                    
                }

                if (p_sNom != monstre.Nom)
                {
                    monstre.Nom = p_sNom;
                }

                if (p_iMondeId != monstre.MondeId)
                {
                    monstre.MondeId = p_iMondeId;
                }

                if (p_Pv != monstre.StatPV)
                {
                    monstre.StatPV = p_Pv;
                }

                dbContext.SaveChanges();
            }
        }
    }
}
