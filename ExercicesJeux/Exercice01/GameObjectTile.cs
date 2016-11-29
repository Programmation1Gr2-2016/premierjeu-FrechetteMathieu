using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice01
{
    class GameObjectTile
    {
        public int LIGNE = 11;
        public int COLONNE = 20;
        public Texture2D texture;
        public Vector2 position;

        public enum Sol
        {
            Terre,
            BuissonVert,
            Mont,
            MontBasGauche,
            MontBasCentre,
            MontBasDroite,
            MontHautGauche,
            MontHautDroite,
            EauGauche,
            EauCentre,
            EauDroite,
            EauBasGauche,
            EauBasCentre,
            EauBasDroite,
            EauHautGauche,
            EauHautCentre,
            EauHautDroite,
            EauCoinHautDroit,
            RiviereVerticale,
            RiviereHorizontale,
            Chute,
            Pont
        }

        public Rectangle[] tabRectSol = { new Rectangle(218, 6, 100, 100) ,  // Terre
                                          new Rectangle(749, 112, 100, 100), // BuissonVert
                                          new Rectangle(112, 324, 100, 100),  // Mont
                                          new Rectangle(218, 218, 100, 100),  // MontBasGauche
                                          new Rectangle(112, 218, 100, 100),  // MontBasCentre
                                          new Rectangle(6, 218, 100, 100),  // MontBasDroite
                                          new Rectangle(218, 324, 100, 100), // MontHautGauche
                                          new Rectangle(6, 324, 100, 100), // MontHautDroite
                                          new Rectangle(6, 537, 100, 100), // EauGauche
                                          new Rectangle(112, 537, 100, 100), // EauCentre
                                          new Rectangle(218, 537, 100, 100), // EauDroite
                                          new Rectangle(6, 643, 100, 100), // EauBasGauche
                                          new Rectangle(112, 643, 100, 100), // EauBasCentre
                                          new Rectangle(218, 643, 100, 100), // EauBasDroite
                                          new Rectangle(6, 430, 100, 100), // EauHautGauche
                                          new Rectangle(112, 430, 100, 100), // EauHautCentre
                                          new Rectangle(218, 430, 100, 100), // EauHautDroite
                                          new Rectangle(6, 749, 100, 100), // EauCoinHautDroit
                                          new Rectangle(1386, 537, 100, 100), // Rivière verticale 
                                          new Rectangle(1386, 430, 100, 100), // Rivière horizontale
                                          new Rectangle(430, 749, 100, 100), // Chute
                                          new Rectangle(537, 749, 100, 100) // Pont
                                        };

        public Rectangle rectSource = new Rectangle(0, 0, 100, 100);

        //public Rectangle rectTerre = new Rectangle(35, 1, 16, 16);
        //public Rectangle rectBuissonVert = new Rectangle(120, 18, 16, 16);
        //public Rectangle rectSable = new Rectangle(404, 504, 48, 48);

        //La carte test pour les collisions
        //public Sol[,] map = {
        //                    {Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre},
        //                    {Sol.Terre,Sol.Terre,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.Terre},
        //                    {Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre},
        //                    {Sol.Terre,Sol.Terre,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.Terre},
        //                    {Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre},
        //                    {Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre},
        //                    {Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre},
        //                    {Sol.Terre,Sol.Terre,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.Terre},
        //                    {Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre},
        //                    {Sol.Terre,Sol.Terre,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.BuissonVert,Sol.Terre,Sol.Terre},
        //                    {Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre}
        //                    };

        //La carte qui est affichée
        public Sol[,] map = {
                            {Sol.Mont,Sol.Mont,Sol.Mont,Sol.Mont,Sol.Mont,Sol.Mont,Sol.Mont,Sol.Mont,Sol.Mont,Sol.Mont,Sol.Mont,Sol.Mont,Sol.EauCentre,Sol.EauCentre,Sol.EauCentre,Sol.EauCentre,Sol.EauCentre,Sol.EauCentre,Sol.EauCentre,Sol.EauCentre},
                            {Sol.Mont,Sol.MontHautGauche,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.MontHautDroite,Sol.Mont,Sol.Mont,Sol.Mont,Sol.Mont,Sol.EauCentre,Sol.EauCentre,Sol.EauCentre,Sol.EauCentre,Sol.EauCentre,Sol.EauCentre,Sol.EauCentre,Sol.EauCentre},
                            {Sol.Mont,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.MontHautDroite,Sol.Mont,Sol.Mont,Sol.Mont,Sol.Chute,Sol.Mont,Sol.Mont,Sol.Mont,Sol.Mont,Sol.Mont,Sol.Mont},
                            {Sol.MontHautGauche,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.MontHautDroite,Sol.Mont,Sol.Mont,Sol.Chute,Sol.Mont,Sol.Mont,Sol.Mont,Sol.Mont,Sol.Mont,Sol.Mont},
                            {Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.EauHautGauche,Sol.EauHautCentre,Sol.EauHautCentre,Sol.EauCentre,Sol.EauCentre,Sol.EauBasDroite,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.MontHautDroite,Sol.Mont},
                            {Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.EauCentre,Sol.EauCoinHautDroit,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Mont},
                            {Sol.MontBasGauche,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Pont,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Mont},
                            {Sol.Mont,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.EauCentre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Mont},
                            {Sol.Mont,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.EauCentre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Mont},
                            {Sol.Mont,Sol.MontBasGauche,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.EauCentre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.Terre,Sol.MontBasDroite,Sol.Mont},
                            {Sol.Mont,Sol.MontBasCentre,Sol.MontBasCentre,Sol.MontBasCentre,Sol.MontBasCentre,Sol.MontBasCentre,Sol.MontBasCentre,Sol.MontBasCentre,Sol.Chute,Sol.MontBasCentre,Sol.MontBasCentre,Sol.MontBasCentre,Sol.MontBasCentre,Sol.MontBasCentre,Sol.MontBasCentre,Sol.MontBasCentre,Sol.MontBasCentre,Sol.MontBasCentre,Sol.Mont,Sol.Mont}
                            };
    }
}
