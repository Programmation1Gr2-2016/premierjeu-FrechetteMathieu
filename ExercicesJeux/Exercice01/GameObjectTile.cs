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
        public const int LIGNE = 11;
        public const int COLONNE = 20;
        public const int LARGEUR_TUILE = 100;
        public const int HAUTEUR_TUILE = 100;

        public Texture2D texture;

        public Rectangle rectSource = new Rectangle(0, 0, LARGEUR_TUILE, HAUTEUR_TUILE);

        public Tuiles[,] map1 = {
                                {new Tuiles(Tuiles.TypeSol.Mont), new Tuiles(Tuiles.TypeSol.Mont), new Tuiles(Tuiles.TypeSol.Mont), new Tuiles(Tuiles.TypeSol.Mont), new Tuiles(Tuiles.TypeSol.Mont), new Tuiles(Tuiles.TypeSol.Mont), new Tuiles(Tuiles.TypeSol.Mont), new Tuiles(Tuiles.TypeSol.Mont), new Tuiles(Tuiles.TypeSol.Mont), new Tuiles(Tuiles.TypeSol.Mont), new Tuiles(Tuiles.TypeSol.Mont), new Tuiles(Tuiles.TypeSol.Mont), new Tuiles(Tuiles.TypeSol.EauCentre), new Tuiles(Tuiles.TypeSol.EauCentre), new Tuiles(Tuiles.TypeSol.EauCentre), new Tuiles(Tuiles.TypeSol.EauCentre), new Tuiles(Tuiles.TypeSol.EauCentre), new Tuiles(Tuiles.TypeSol.EauCentre), new Tuiles(Tuiles.TypeSol.EauCentre), new Tuiles(Tuiles.TypeSol.EauCentre)},
                                {new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.MontHautGauche),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.MontHautDroite),new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.EauCentre),new Tuiles(Tuiles.TypeSol.EauCentre),new Tuiles(Tuiles.TypeSol.EauCentre),new Tuiles(Tuiles.TypeSol.EauCentre),new Tuiles(Tuiles.TypeSol.EauCentre),new Tuiles(Tuiles.TypeSol.EauCentre),new Tuiles(Tuiles.TypeSol.EauCentre),new Tuiles(Tuiles.TypeSol.EauCentre)},
                                {new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.SableMouvant),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.MontHautDroite),new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.Chute),new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.Mont)},
                                {new Tuiles(Tuiles.TypeSol.MontHautGauche),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Statue),new Tuiles(Tuiles.TypeSol.SableMouvant),new Tuiles(Tuiles.TypeSol.Statue),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.MontHautDroite),new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.Chute),new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.Mont)},
                                {new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.SableMouvant),new Tuiles(Tuiles.TypeSol.SableMouvant),new Tuiles(Tuiles.TypeSol.SableMouvant),new Tuiles(Tuiles.TypeSol.SableMouvant),new Tuiles(Tuiles.TypeSol.SableMouvant),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.EauHautGauche),new Tuiles(Tuiles.TypeSol.EauHautCentre),new Tuiles(Tuiles.TypeSol.EauHautCentre),new Tuiles(Tuiles.TypeSol.EauCentre),new Tuiles(Tuiles.TypeSol.EauCentre),new Tuiles(Tuiles.TypeSol.EauBasDroite),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.MontHautDroite),new Tuiles(Tuiles.TypeSol.Mont)},
                                {new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.SableMouvant),new Tuiles(Tuiles.TypeSol.SableMouvant),new Tuiles(Tuiles.TypeSol.SableMouvant),new Tuiles(Tuiles.TypeSol.SableMouvant),new Tuiles(Tuiles.TypeSol.SableMouvant),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.EauCentre),new Tuiles(Tuiles.TypeSol.EauCoinHautDroit),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Mont)},
                                {new Tuiles(Tuiles.TypeSol.MontBasGauche),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Statue),new Tuiles(Tuiles.TypeSol.SableMouvant),new Tuiles(Tuiles.TypeSol.Statue),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Pont),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.BuissonBrun),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.BuissonBrun),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.BuissonBrun),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Mont)},
                                {new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.SableMouvant),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.EauCentre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.BuissonBrun),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Mont)},
                                {new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.EauCentre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.BuissonBrun),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.BuissonBrun),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.BuissonVert),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Mont)},
                                {new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.MontBasGauche),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.EauCentre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.Terre),new Tuiles(Tuiles.TypeSol.MontBasDroite),new Tuiles(Tuiles.TypeSol.Mont)},
                                {new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.MontBasCentre),new Tuiles(Tuiles.TypeSol.MontBasCentre),new Tuiles(Tuiles.TypeSol.MontBasCentre),new Tuiles(Tuiles.TypeSol.MontBasCentre),new Tuiles(Tuiles.TypeSol.MontBasCentre),new Tuiles(Tuiles.TypeSol.MontBasCentre),new Tuiles(Tuiles.TypeSol.MontBasCentre),new Tuiles(Tuiles.TypeSol.Chute),new Tuiles(Tuiles.TypeSol.MontBasCentre),new Tuiles(Tuiles.TypeSol.MontBasCentre),new Tuiles(Tuiles.TypeSol.MontBasCentre),new Tuiles(Tuiles.TypeSol.MontBasCentre),new Tuiles(Tuiles.TypeSol.MontBasCentre),new Tuiles(Tuiles.TypeSol.MontBasCentre),new Tuiles(Tuiles.TypeSol.MontBasCentre),new Tuiles(Tuiles.TypeSol.MontBasCentre),new Tuiles(Tuiles.TypeSol.MontBasCentre),new Tuiles(Tuiles.TypeSol.Mont),new Tuiles(Tuiles.TypeSol.Mont)}
                                };

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < LIGNE; i++)
            {
                rectSource.Y = (i * 100);
                for (int j = 0; j < COLONNE; j++)
                {
                    rectSource.X = (j * 100);
                    spriteBatch.Draw(texture, rectSource, map1[i, j].rectSol, Color.White);
                }
            }
        }
    }


}
