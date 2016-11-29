using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice01
{
    class GameObjectAnime_Hero
    {
        public Rectangle position;
        public int vitesse;
        public Texture2D sprite;
        public bool estVivant;
        public bool estInvincible;
        public Vector2 direction;
        public Rectangle spriteAffiche;
        public int pointDeVie;
        public int vieMax = 6;
        public string imgHero;
        public enum Etat { MarcheDroite, AttenteDroite, MarcheGauche, AttenteGauche, MarcheHaut, AttenteHaut, MarcheBas, AttenteBas, AttaqueDroite, AttaqueGauche, AttaqueHaut, AttaqueBas, Mort };
        public Etat etat;
        private int compteur = 0;
        public int compteurInvicibilite = 0;

        public GameObjectAnime_Missile arme = new GameObjectAnime_Missile();
        private int debutCoupArme = 0;
        //private int dureeVieCoup = 1;

        // Gestion des tableaux de sprites (chaque sprite est un rectangle dans le tableau)

        // Marche
        int marcheState = 0; //État de départ
        int nbEtatsMarche = 2; // Combien il y a de rectanbles pour l'état "Marcher"
        public Rectangle[] tabMarcheDroite = { new Rectangle(421, 140, 71, 75), new Rectangle(421, 0, 71, 75) };
        public Rectangle[] tabMarcheGauche = { new Rectangle(140, 0, 70, 75), new Rectangle(145, 140, 70, 75) };
        public Rectangle[] tabMarcheHaut = { new Rectangle(290, 0, 70, 74), new Rectangle(290, 140, 70, 75) };
        public Rectangle[] tabMarcheBas = { new Rectangle(0, 0, 70, 74), new Rectangle(0, 140, 70, 75) };

        // Arrêt
        int waitState = 0;
        public Rectangle[] tabAttenteDroite = { new Rectangle(421, 140, 71, 75) };
        public Rectangle[] tabAttenteGauche = { new Rectangle(140, 0, 70, 74) };
        public Rectangle[] tabAttenteHaut = { new Rectangle(290, 0, 70, 74) };
        public Rectangle[] tabAttenteBas = { new Rectangle(0, 0, 70, 74) };

        // Attaque
        int attackState = 0;
        public Rectangle[] tabAttaqueDroite = { new Rectangle(393, 417, 75, 75) };
        public Rectangle[] tabAttaqueGauche = { new Rectangle(164, 417, 75, 75) };
        public Rectangle[] tabAttaqueHaut = { new Rectangle(281, 445, 75, 75) };
        public Rectangle[] tabAttaqueBas = { new Rectangle(0, 393, 75, 75) };

        // Mort
        int mortState = 0;
        public Rectangle[] tabMort = { new Rectangle(1265, 702, 75, 75) };

        public GameObjectAnime_Hero()
        {
            position = new Rectangle(100, 100, 75, 75);
            vitesse = 2;
            pointDeVie = 6;
            imgHero = "Images\\LinkSheet.png";
            direction = Vector2.Zero;
            etat = GameObjectAnime_Hero.Etat.AttenteDroite;
            estVivant = true;
            estInvincible = false;
        }

        public virtual void InitializeArme()
        {
            arme.estVivant = false;
            arme.degatArme = 1;
            arme.tabTirGauche[0] =  new Rectangle(112, 417, 52, 75);
            arme.tabTirDroite[0] = new Rectangle(468, 417, 52, 75);
            arme.tabTirHaut[0] = new Rectangle(281, 393, 75, 52);
            arme.tabTirBas[0] = new Rectangle(0, 468, 75, 52);
            arme.etat = GameObjectAnime_Missile.Etat.TirGauche;
            arme.sprite = this.sprite;
        }

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

            // Mort
            if(etat == Etat.Mort)
            {
                spriteAffiche = tabMort[mortState];
            }

            // Attaque
            if((etat== Etat.AttaqueDroite)|| (etat == Etat.AttaqueGauche)|| (etat == Etat.AttaqueHaut)|| (etat == Etat.AttaqueBas))
            {
                //arme.estVivant = true;
                debutCoupArme = gameTime.TotalGameTime.Seconds;
                arme.position = this.position;
            }

            if (etat == Etat.AttaqueDroite)
            {
                spriteAffiche = tabAttaqueDroite[attackState];
                arme.etat = GameObjectAnime_Missile.Etat.TirDroite;
                arme.position.X = this.position.X + this.position.Width;
                arme.position.Width = arme.tabTirDroite[0].Width;
                arme.Update(gameTime);
            }
            if (etat == Etat.AttaqueGauche)
            {
                spriteAffiche = tabAttaqueGauche[attackState];
                arme.etat = GameObjectAnime_Missile.Etat.TirGauche;
                arme.position.X = this.position.X - arme.tabTirGauche[0].Width;
                arme.position.Width = arme.tabTirGauche[0].Width;
                arme.Update(gameTime);
            }
            if (etat == Etat.AttaqueHaut)
            {
                spriteAffiche = tabAttaqueHaut[attackState];
                arme.etat = GameObjectAnime_Missile.Etat.TirHaut;
                arme.position.Y = this.position.Y - arme.tabTirHaut[0].Height;
                arme.position.Height = arme.tabTirHaut[0].Height;
                arme.Update(gameTime);
            }
            if (etat == Etat.AttaqueBas)
            {
                spriteAffiche = tabAttaqueBas[attackState];
                arme.etat = GameObjectAnime_Missile.Etat.TirBas;
                arme.position.Y = this.position.Y + this.position.Height;
                arme.position.Height = arme.tabTirHaut[0].Height;
                arme.Update(gameTime);
            }


            //if(gameTime.TotalGameTime.Seconds>debutCoupArme + dureeVieCoup)
            //{
            //    arme.estVivant = false;
            //}

            // Compteur permettant de gérer le changement d'images
            compteur++;
            if (compteur == 10)
            {
                marcheState++;
                if (marcheState == nbEtatsMarche)
                {
                    marcheState = 0;
                }

                if (estInvincible)
                {
                    //estInvincible = false;
                }
                compteur = 0;
            }

            // Compteur permettant de gérer la durée d'invicibilité du héro
            if (estInvincible)
            {
                compteurInvicibilite++;
                if (compteurInvicibilite == 45)
                {
                    estInvincible = false;
                    compteurInvicibilite = 0;
                }
            }
        }
    }
}
