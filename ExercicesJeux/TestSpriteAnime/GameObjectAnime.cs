using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSpriteAnime
{
    class GameObjectAnime
    {
        public Texture2D sprite;
        public Vector2 vitesse;
        public Vector2 direction;
        public Rectangle position;
        public Rectangle spriteAfficher; //Le rectangle affiché à l'écran

        public enum etats { attenteDroite, attenteGauche, runDroite, runGauche };
        public etats objetState;

        //Compteur qui changera le sprite affiché
        private int cpt = 0;

        //GESTION DES TABLEAUX DE SPRITES (chaque sprite est un rectangle dans le tableau)
        int runState = 0; //État de départ
        int nbEtatRun = 4; //Combien il y a de rectangles pour l’état “courrir”

        public Rectangle[] tabRunDroite = {
            new Rectangle(10, 162, 68, 80),
            new Rectangle(81, 162, 68, 80),
            new Rectangle(156, 164, 68, 80),
            new Rectangle(229, 164, 68, 80) };
        public Rectangle[] tabRunGauche = {
            new Rectangle(7, 863, 68, 80),
            new Rectangle(80, 863, 68, 80),
            new Rectangle(157, 863, 68, 80),
            new Rectangle(228, 862, 68, 80) };

        int waitState = 0;
        public Rectangle[] tabAttenteDroite =
        {
            new Rectangle(77, 6, 68, 80)
        };
        public Rectangle[] tabAttenteGauche =
        {
            new Rectangle(303, 862, 68, 80)
        };

        public virtual void Update(GameTime gameTime)
        {
            if (objetState == etats.attenteDroite)
            {
                spriteAfficher = tabAttenteDroite[waitState];
            }
            if (objetState == etats.attenteGauche)
            {
                spriteAfficher = tabAttenteGauche[waitState];
            }
            if (objetState == etats.runDroite)
            {
                spriteAfficher = tabRunDroite[runState];
            }
            if (objetState == etats.runGauche)
            {
                spriteAfficher = tabRunGauche[runState];
            }

            //Compteur permettant de gérer le changement d'images
            cpt++;
            if (cpt == 6) //Vitesse défilement
            {
                //Gestion de la course
                runState++;
                if (runState == nbEtatRun)
                {
                    runState = 0;
                }
                cpt = 0;
            }
        }
    }
}
