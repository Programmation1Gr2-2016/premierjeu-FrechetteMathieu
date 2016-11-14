using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice01
{
    class GameObjectAnime
    {
        public Rectangle position;
        public int vitesse;
        public Texture2D sprite;
        public bool estVivant;
        public Vector2 direction;
        public Rectangle spriteAffiche;

        public enum Etat { MarcheDroite, AttenteDroite, MarcheGauche, AttenteGauche, MarcheHaut, AttenteHaut, MarcheBas, AttenteBas, AttaqueDroite };
        public Etat etat;
        private int compteur = 0;

        // Gestion des tableaux de sprites (chaque sprite est un rectangle dans le tableau)

        // Marche
        int marcheState = 0; //État de départ
        int nbEtatsMarche = 2; // Combien il y a de rectanbles pour l'état "Marcher"

        public Rectangle[] tabMarcheDroite =
        {
            new Rectangle(421, 140, 71, 75),
            new Rectangle(421, 0, 71, 70)
        };

        public Rectangle[] tabMarcheGauche =
        {
            new Rectangle(140, 0, 70, 74),
            new Rectangle(145, 140, 70, 70)
        };

        public Rectangle[] tabMarcheHaut =
        {
            new Rectangle(290, 0, 70, 74),
            new Rectangle(290, 140, 70, 75)
        };

        public Rectangle[] tabMarcheBas =
        {
            new Rectangle(0, 0, 70, 74),
            new Rectangle(0, 140, 70, 75)
        };


        // Arrêt
        int waitState = 0;
        public Rectangle[] tabAttenteDroite = {new Rectangle(421, 140, 71, 75)};
        public Rectangle[] tabAttenteGauche = {new Rectangle(140, 0, 70, 74)};
        public Rectangle[] tabAttenteHaut = {new Rectangle(290, 0, 70, 74)};
        public Rectangle[] tabAttenteBas = {new Rectangle(0, 0, 70, 74)};

        // Attaque
        int attackState = 0;
        public Rectangle[] tabAttaqueDroite = {new Rectangle(393, 421, 75, 72)};


        public virtual void Update(GameTime gameTime)
        {
            // Attente
            if (etat == Etat.AttenteDroite)
            {
                spriteAffiche = tabAttenteDroite[waitState];
            }
            if (etat == Etat.AttenteGauche)
            {
                spriteAffiche = tabAttenteGauche[waitState];
            }
            if (etat == Etat.AttenteHaut)
            {
                spriteAffiche = tabAttenteHaut[waitState];
            }
            if (etat == Etat.AttenteBas)
            {
                spriteAffiche = tabAttenteBas[waitState];
            }

            // Marche
            if (etat == Etat.MarcheDroite)
            {
                spriteAffiche = tabMarcheDroite[marcheState];
            }
            if (etat == Etat.MarcheGauche)
            {
                spriteAffiche = tabMarcheGauche[marcheState];
            }
            if (etat == Etat.MarcheHaut)
            {
                spriteAffiche = tabMarcheHaut[marcheState];
            }
            if (etat == Etat.MarcheBas)
            {
                spriteAffiche = tabMarcheBas[marcheState];
            }

            // Attaque
            if(etat == Etat.AttaqueDroite)
            {
                spriteAffiche = tabAttaqueDroite[attackState];
            }



            // Compteur permettant de gérer le changement d'images
            compteur++;
            if (compteur == 8)
            {
                marcheState++;
                if (marcheState == nbEtatsMarche)
                {
                    marcheState = 0;
                }

                compteur = 0;
            }
        }
    }
}
