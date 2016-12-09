using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice01
{
    class GameObjectAnime_PowerUp
    {
        public Rectangle position;
        public Texture2D sprite;
        public bool estVivant;

        public enum TypePowerUP { TriForce};
        public TypePowerUP typePowerUP;
        private int compteur = 0;
        public Rectangle spriteAffiche;

        // Triforce
        int triforceState = 0; //État de départ
        public Rectangle[] tabTriForce = { new Rectangle(237, 43, 75, 24) };

        public virtual void Update(GameTime gameTime)
        {
            if (typePowerUP == TypePowerUP.TriForce)
            {
                spriteAffiche = tabTriForce[triforceState];
            }


            // Compteur permettant de gérer le changement d'images
            compteur++;
            if (compteur == 10)
            {
                triforceState++;
                if (triforceState == 1)
                {
                    triforceState = 0;
                }

                compteur = 0;
            }
        }
    }
}
