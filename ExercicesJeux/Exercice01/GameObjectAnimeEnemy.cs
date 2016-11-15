using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice01
{
    class GameObjectAnimeEnemy
    {
        public Rectangle position;
        public int vitesse;
        public Texture2D sprite;
        public bool estVivant;
        public Vector2 direction;
        public Rectangle spriteAffiche;

        public GameObjectAnime_Missile missile; // Test de lier le missile a l'enemy

        private int secondeDebut = 0;
        public int secondeAttente = 1;
        private Random rnd = new Random();

        public enum Etat { MarcheDroite, AttenteDroite, MarcheGauche, AttenteGauche, MarcheHaut, AttenteHaut, MarcheBas, AttenteBas, AttaqueDroite };
        public Etat etat;
        private int compteur = 0;

        // Marche
        int marcheState = 0; //État de départ
        int nbEtatsMarche = 2; // Combien il y a de rectanbles pour l'état "Marcher"
        public Rectangle[] tabMarcheDroite = { new Rectangle(461, 0, 75, 75), new Rectangle(574, 0, 75, 75) };
        public Rectangle[] tabMarcheGauche = { new Rectangle(11, 0, 75, 75), new Rectangle(123, 0, 75, 75) };
        public Rectangle[] tabMarcheHaut = { new Rectangle(687, 0, 75, 75), new Rectangle(799, 0, 75, 75) };
        public Rectangle[] tabMarcheBas = { new Rectangle(236, 0, 75, 75), new Rectangle(349, 0, 75, 75) };

        // Arrêt
        int waitState = 0;
        public Rectangle[] tabAttenteDroite = { new Rectangle(461, 0, 75, 75) };
        public Rectangle[] tabAttenteGauche = { new Rectangle(11, 0, 75, 75) };
        public Rectangle[] tabAttenteHaut = { new Rectangle(687, 0, 75, 75) };
        public Rectangle[] tabAttenteBas = { new Rectangle(236, 0, 75, 75) };

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

            // Compteur permettant de gérer le changement d'images
            compteur++;
            if (compteur == 10)
            {
                marcheState++;
                if (marcheState == nbEtatsMarche)
                {
                    marcheState = 0;
                }

                compteur = 0;
            }
            
            // Change la direction de l'enemie après un certain temps
            if(gameTime.TotalGameTime.Seconds >= secondeDebut + secondeAttente)
            {
                secondeDebut = gameTime.TotalGameTime.Seconds;
                etat = (Etat)rnd.Next(0, 8);
                if((etat == Etat.AttenteBas) || (etat == Etat.AttenteDroite) || (etat == Etat.AttenteHaut) || (etat == Etat.AttenteGauche))
                {
                    secondeAttente = 1;
                }
                else
                {
                    secondeAttente = rnd.Next(2, 4);
                }
            }
        }
    }
}
