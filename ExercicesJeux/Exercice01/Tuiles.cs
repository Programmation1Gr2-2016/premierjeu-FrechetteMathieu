using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice01
{
    class Tuiles
    {
        public enum TypeSol
        {
            Terre,
            SableMouvant,
            BuissonVert,
            BuissonBrun,
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
            Pont,
            Statue
        }

        public Rectangle rectSol;
        public bool bloqueHero = false;
        public bool bloqueMissile = false;
        public bool slowHero = false;
        public bool detruisable = false;

        public Tuiles(TypeSol typeSol)
        {
            switch (typeSol)
            {
                case TypeSol.Terre:
                    rectSol = new Rectangle(218, 6, 100, 100);
                    break;

                case TypeSol.SableMouvant:
                    rectSol = new Rectangle(430, 537, 100, 100);
                    slowHero = true;
                    break;

                case TypeSol.BuissonVert:
                    rectSol = new Rectangle(749, 112, 100, 100);
                    bloqueHero = true;
                    detruisable = true;
                    break;

                case TypeSol.BuissonBrun:
                    rectSol = new Rectangle(112, 112, 100, 100);
                    bloqueHero = true;
                    detruisable = true;
                    break;

                case TypeSol.Statue:
                    rectSol = new Rectangle(218, 112, 100, 100);
                    bloqueHero = true;
                    bloqueMissile = true;
                    break;

                case TypeSol.Mont:
                    rectSol = new Rectangle(112, 324, 100, 100);
                    bloqueHero = true;
                    bloqueMissile = true;
                    break;

                case TypeSol.MontBasGauche:
                    rectSol = new Rectangle(218, 218, 100, 100);
                    bloqueHero = true;
                    bloqueMissile = true;
                    break;

                case TypeSol.MontBasCentre:
                    rectSol = new Rectangle(112, 218, 100, 100);
                    bloqueHero = true;
                    bloqueMissile = true;
                    break;

                case TypeSol.MontBasDroite:
                    rectSol = new Rectangle(6, 218, 100, 100);
                    bloqueHero = true;
                    bloqueMissile = true;
                    break;

                case TypeSol.MontHautGauche:
                    rectSol = new Rectangle(218, 324, 100, 100);
                    bloqueHero = true;
                    bloqueMissile = true;
                    break;

                case TypeSol.MontHautDroite:
                    rectSol = new Rectangle(6, 324, 100, 100);
                    bloqueHero = true;
                    bloqueMissile = true;
                    break;

                case TypeSol.EauGauche:
                    rectSol = new Rectangle(6, 537, 100, 100);
                    bloqueHero = true;
                    break;

                case TypeSol.EauCentre:
                    rectSol = new Rectangle(112, 537, 100, 100);
                    bloqueHero = true;
                    break;

                case TypeSol.EauDroite:
                    rectSol = new Rectangle(218, 537, 100, 100);
                    bloqueHero = true;
                    break;

                case TypeSol.EauBasGauche:
                    rectSol = new Rectangle(6, 643, 100, 100);
                    bloqueHero = true;
                    break;

                case TypeSol.EauBasCentre:
                    rectSol = new Rectangle(112, 643, 100, 100);
                    bloqueHero = true;
                    break;

                case TypeSol.EauBasDroite:
                    rectSol = new Rectangle(218, 643, 100, 100);
                    bloqueHero = true;
                    break;

                case TypeSol.EauHautGauche:
                    rectSol = new Rectangle(6, 430, 100, 100);
                    bloqueHero = true;
                    break;

                case TypeSol.EauHautCentre:
                    rectSol = new Rectangle(112, 430, 100, 100);
                    bloqueHero = true;
                    break;

                case TypeSol.EauHautDroite:
                    rectSol = new Rectangle(218, 430, 100, 100);
                    bloqueHero = true;
                    break; 

                case TypeSol.EauCoinHautDroit:
                    rectSol = new Rectangle(6, 749, 100, 100);
                    break;

                case TypeSol.RiviereVerticale:
                    rectSol = new Rectangle(1386, 537, 100, 100);
                    bloqueHero = true;
                    break;

                case TypeSol.RiviereHorizontale:
                    rectSol = new Rectangle(1386, 430, 100, 100);
                    bloqueHero = true;
                    break;

                case TypeSol.Chute:
                    rectSol = new Rectangle(430, 749, 100, 100);
                    bloqueHero = true;
                    bloqueMissile = true;
                    break;

                case TypeSol.Pont:
                    rectSol = new Rectangle(537, 749, 100, 100);
                    break;

                default:
                    break;
            }
        }
    }
}
