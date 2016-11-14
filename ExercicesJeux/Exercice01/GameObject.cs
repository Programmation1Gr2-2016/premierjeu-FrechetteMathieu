using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice01
{
    class GameObject
    {
        public Rectangle position;
        public int vitesse;
        public Texture2D sprite;
        public bool estVivant;
        public enum directionObjet { gauche, droite, haut, bas};
        public directionObjet direction;
        public Rectangle rectSprite;
    }
}
