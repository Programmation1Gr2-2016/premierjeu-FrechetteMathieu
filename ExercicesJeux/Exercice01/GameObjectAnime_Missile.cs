using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice01
{
    class GameObjectAnime_Missile
    {
        public Rectangle position;
        public int vitesse;
        public Texture2D sprite;
        public bool estVivant;
        public Vector2 direction;
        public Rectangle spriteAffiche;

        public enum Etat { TirDroite, TirGauche, TirHaut, TirBas };
        public Etat etat;
        private int compteur = 0;

        // Tir
        int tirState = 0; //État de départ
        int nbEtatsTir = 1; // Combien il y a de rectanbles pour l'état "Tir"
        public Rectangle[] tabTirDroite = { new Rectangle(237, 43, 75, 24)};
        public Rectangle[] tabTirGauche = { new Rectangle(12, 48, 75, 24) };
        public Rectangle[] tabTirHaut = { new Rectangle(373, 20, 24, 75)};
        public Rectangle[] tabTirBas = { new Rectangle(153, 20, 24, 75) };

        public virtual void Update(GameTime gameTime)
        {
            if (etat == Etat.TirDroite)
            {
                spriteAffiche = tabTirDroite[tirState];
            }
            if (etat == Etat.TirGauche)
            {
                spriteAffiche = tabTirGauche[tirState];
            }
            if (etat == Etat.TirHaut)
            {
                spriteAffiche = tabTirHaut[tirState];
            }
            if (etat == Etat.TirBas)
            {
                spriteAffiche = tabTirBas[tirState];
            }


            // Compteur permettant de gérer le changement d'images
            compteur++;
            if (compteur == 10)
            {
                tirState++;
                if (tirState == nbEtatsTir)
                {
                    tirState = 0;
                }

                compteur = 0;
            }
        }
    }
}
