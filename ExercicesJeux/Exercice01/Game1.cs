using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Exercice01
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState keys = new KeyboardState();
        KeyboardState previousKeys = new KeyboardState();
        Rectangle fenetre;

        Texture2D background;
        GameObjectAnime link;
        GameObjectAnimeEnemy enemy;
        GameObjectAnime_Missile missile;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            //this.graphics.ApplyChanges();
            this.graphics.ToggleFullScreen(); // Fullscreen

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            fenetre = graphics.GraphicsDevice.Viewport.Bounds;
            fenetre.Width = graphics.GraphicsDevice.DisplayMode.Width;
            fenetre.Height = graphics.GraphicsDevice.DisplayMode.Height;

            background = Content.Load<Texture2D>("Background.jpg");

            link = new GameObjectAnime();
            link.position = new Rectangle(300, 100, 75, 75);
            link.vitesse = 2;
            link.sprite = Content.Load<Texture2D>("LinkSheet.png");
            link.direction = Vector2.Zero;
            link.etat = GameObjectAnime.Etat.AttenteDroite;

            enemy = new GameObjectAnimeEnemy();
            enemy.position = new Rectangle(fenetre.Right - 75, fenetre.Bottom / 2, 75, 75);
            enemy.vitesse = 2;
            enemy.sprite = Content.Load<Texture2D>("Enemy_RedMoblin.png");
            enemy.direction = Vector2.Zero;
            enemy.etat = GameObjectAnimeEnemy.Etat.MarcheGauche;
            enemy.estVivant = true;

            missile = new GameObjectAnime_Missile();
            missile.vitesse = 3;
            missile.sprite = Content.Load<Texture2D>("Enemy_Missile.png");
            missile.direction = Vector2.Zero;
            missile.etat = GameObjectAnime_Missile.Etat.TirGauche;
            missile.estVivant = false;
            missile.position = new Rectangle(fenetre.Right / 2, fenetre.Bottom / 2, 75, 75);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            UpdateHeros(gameTime);
            UpdateEnemy(gameTime);
            UpdateMissile(gameTime);
            Collision();

            base.Update(gameTime);
        }

        protected void UpdateHeros(GameTime gameTime)
        {

            keys = Keyboard.GetState();
            link.position.X += (int)(link.vitesse * link.direction.X);
            link.position.Y += (int)(link.vitesse * link.direction.Y);

            // Touche de direction droite
            if (keys.IsKeyDown(Keys.Right))
            {
                link.direction.X = 2;
                link.etat = GameObjectAnime.Etat.MarcheDroite;
            }
            if (keys.IsKeyUp(Keys.Right) && previousKeys.IsKeyDown(Keys.Right))
            {
                link.direction.X = 0;
                link.etat = GameObjectAnime.Etat.AttenteDroite;
            }

            // Touche de direction gauche
            if (keys.IsKeyDown(Keys.Left))
            {
                link.direction.X = -2;
                link.etat = GameObjectAnime.Etat.MarcheGauche;
            }
            if (keys.IsKeyUp(Keys.Left) && previousKeys.IsKeyDown(Keys.Left))
            {
                link.direction.X = 0;
                link.etat = GameObjectAnime.Etat.AttenteGauche;
            }

            // Touche de direction haut
            if (keys.IsKeyDown(Keys.Up))
            {
                link.direction.Y = -2;
                link.etat = GameObjectAnime.Etat.MarcheHaut;
            }
            if (keys.IsKeyUp(Keys.Up) && previousKeys.IsKeyDown(Keys.Up))
            {
                link.direction.Y = 0;
                link.etat = GameObjectAnime.Etat.AttenteHaut;
            }

            // Touche de direction bas
            if (keys.IsKeyDown(Keys.Down))
            {
                link.direction.Y = 2;
                link.etat = GameObjectAnime.Etat.MarcheBas;
            }
            if (keys.IsKeyUp(Keys.Down) && previousKeys.IsKeyDown(Keys.Down))
            {
                link.direction.Y = 0;
                link.etat = GameObjectAnime.Etat.AttenteBas;
            }

            // Touche attaque primaire
            //if (keys.IsKeyDown(Keys.A))
            //{
            //    link.etat = GameObjectAnime.Etat.AttaqueDroite;
            //}



            // Teste les bordures d'écran
            // A FAIRE: Trouver un façon d'avoir les dimensions de la sprite pour faire le teste des bordures
            if (link.position.X < fenetre.Left)
            {
                link.position.X = fenetre.Left;
            }
            else if (link.position.X + 75 > fenetre.Right)
            {
                link.position.X = fenetre.Right-75;
            }
            else if(link.position.Y < fenetre.Top)
            {
                link.position.Y = fenetre.Top;
            }
            else if(link.position.Y + 74> fenetre.Bottom) // Le nombre est calculé selon la hauteur du sprite de marche
            {
                link.position.Y = fenetre.Bottom - 74;
            }

            link.Update(gameTime);
            previousKeys = keys;
        }

        protected void UpdateEnemy(GameTime gameTime)
        {
            enemy.position.X += (int)(enemy.vitesse * enemy.direction.X);
            enemy.position.Y += (int)(enemy.vitesse * enemy.direction.Y);

            // Teste les bordures d'écran
            // A FAIRE: Trouver un façon d'avoir les dimensions de la sprite pour faire le teste des bordures
            if (enemy.position.X < fenetre.Left)
            {
                enemy.position.X = fenetre.Left;
                enemy.etat = GameObjectAnimeEnemy.Etat.MarcheDroite;
            }
            else if (enemy.position.X + 75 > fenetre.Right)
            {
                enemy.position.X = fenetre.Right - 75;
                enemy.etat = GameObjectAnimeEnemy.Etat.MarcheGauche;
            }
            else if (enemy.position.Y < fenetre.Top)
            {
                enemy.position.Y = fenetre.Top;
            }
            else if (enemy.position.Y + 75 > fenetre.Bottom) // Le nombre est calculé selon la hauteur du sprite de marche
            {
                enemy.position.Y = fenetre.Bottom - 75;
            }

            enemy.direction.X = 0;
            enemy.direction.Y = 0;

            switch (enemy.etat)
            {
                case GameObjectAnimeEnemy.Etat.MarcheDroite:
                    enemy.direction.X = enemy.vitesse;
                    break;

                case GameObjectAnimeEnemy.Etat.AttenteDroite:
                    enemy.direction.X = 0;
                    if(missile.estVivant!= true)
                    {
                        missile.estVivant = true;
                        missile.position = new Rectangle(enemy.position.X, enemy.position.Y, 75, 24);
                        missile.etat = GameObjectAnime_Missile.Etat.TirDroite;
                    }
                    break;

                case GameObjectAnimeEnemy.Etat.MarcheGauche:
                    enemy.direction.X = -enemy.vitesse;
                    break;

                case GameObjectAnimeEnemy.Etat.AttenteGauche:
                    enemy.direction.X = 0;
                    if (missile.estVivant != true)
                    {
                        missile.estVivant = true;
                        missile.position = new Rectangle(enemy.position.X, enemy.position.Y, 75, 24);
                        missile.etat = GameObjectAnime_Missile.Etat.TirGauche;
                    }
                    break;

                case GameObjectAnimeEnemy.Etat.MarcheHaut:
                    enemy.direction.Y = -enemy.vitesse;
                    break;

                case GameObjectAnimeEnemy.Etat.AttenteHaut:
                    enemy.direction.Y = 0;
                    if (missile.estVivant != true)
                    {
                        missile.estVivant = true;
                        missile.position = new Rectangle(enemy.position.X, enemy.position.Y, 24, 75);
                        missile.etat = GameObjectAnime_Missile.Etat.TirHaut;
                    }
                    break;

                case GameObjectAnimeEnemy.Etat.MarcheBas:
                    enemy.direction.Y = enemy.vitesse;
                    break;

                case GameObjectAnimeEnemy.Etat.AttenteBas:
                    enemy.direction.Y = 0;
                    if (missile.estVivant != true)
                    {
                        missile.estVivant = true;
                        missile.position = new Rectangle(enemy.position.X, enemy.position.Y, 24, 75);
                        missile.etat = GameObjectAnime_Missile.Etat.TirBas;
                    }
                    break;
            }

            enemy.Update(gameTime);
        }

        protected void UpdateMissile(GameTime gameTime)
        {
            if (missile.estVivant)
            {
                missile.position.X += (int)(missile.vitesse * missile.direction.X);
                missile.position.Y += (int)(missile.vitesse * missile.direction.Y);

                switch (missile.etat)
                {
                    case GameObjectAnime_Missile.Etat.TirDroite:
                        missile.direction.X = missile.vitesse;
                        missile.direction.Y = 0;
                        break;

                    case GameObjectAnime_Missile.Etat.TirGauche:
                        missile.direction.X = -missile.vitesse;
                        missile.direction.Y = 0;
                        break;
                    case GameObjectAnime_Missile.Etat.TirHaut:
                        missile.direction.X = 0;
                        missile.direction.Y = -missile.vitesse;
                        break;
                    case GameObjectAnime_Missile.Etat.TirBas:
                        missile.direction.X = 0;
                        missile.direction.Y = missile.vitesse;
                        break;
                    default:
                        break;
                }

                // Teste les collisions avec le héro

                // Si le missile sort de l'écran, estVivant devient false
                if ((missile.position.X < fenetre.Left) || (missile.position.X > fenetre.Right) || (missile.position.Y < fenetre.Top) || (missile.position.Y > fenetre.Bottom))
                {
                    missile.estVivant = false;
                }

                missile.Update(gameTime);
            }
        }

        protected void Collision()
        {
            if (link.position.Intersects(enemy.position))
            {
                link.position.X -= 30;
                link.position.Y -= 30;

            }

            if (link.position.Intersects(missile.position))
            {
                link.position.X -= 30;
                link.position.Y -= 30;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGreen);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            //spriteBatch.Draw(heros.sprite, heros.position, Color.White);
            //spriteBatch.Draw(heros.sprite, heros.position, heros.rectSprite, Color.White);

            spriteBatch.Draw(background, fenetre, Color.White);

            spriteBatch.Draw(link.sprite, link.position, link.spriteAffiche, Color.White);

            spriteBatch.Draw(enemy.sprite, enemy.position, enemy.spriteAffiche, Color.White);

            if (missile.estVivant)
            {
                spriteBatch.Draw(missile.sprite, missile.position, missile.spriteAffiche, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
