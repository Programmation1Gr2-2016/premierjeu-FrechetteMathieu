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
        int fullScreen = 1;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState keys = new KeyboardState();
        KeyboardState previousKeys = new KeyboardState();
        Rectangle fenetre;

        Texture2D background;
        GameObjectAnime link;
        //GameObjectAnimeEnemy enemy;
        //GameObjectAnime_Missile missile;
        GameObjectAnimeEnemy[] tabEnemy = new GameObjectAnimeEnemy[4];
        Random rnd = new Random();
        GameObjectAnime.Etat etatHeroAvantAttaque = GameObjectAnime.Etat.AttenteGauche;

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

            if (fullScreen == 1)
            {
                this.graphics.ToggleFullScreen();
            }
            else
            {
                this.graphics.ApplyChanges();
            }

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
            link.InitializeArme();
            link.estVivant = true;

            for (int i = 0; i < tabEnemy.GetLength(0); i++)
            {
                tabEnemy[i] = new GameObjectAnimeEnemy();
                tabEnemy[i].position = new Rectangle(fenetre.Right - 75, fenetre.Bottom - (i * 150), 75, 75); // A FAIRE: Générer un position aléatoire pour les enemy qui n'entre pas en conflit avec la position du héro
                tabEnemy[i].vitesse = 2;
                tabEnemy[i].secondeAttente = rnd.Next(0, 5);
                tabEnemy[i].sprite = Content.Load<Texture2D>("Enemy_RedMoblin.png");
                tabEnemy[i].direction = Vector2.Zero;
                tabEnemy[i].etat = GameObjectAnimeEnemy.Etat.MarcheGauche;
                tabEnemy[i].estVivant = true;

                tabEnemy[i].missile = new GameObjectAnime_Missile();
                tabEnemy[i].missile.vitesse = 3;
                tabEnemy[i].missile.sprite = Content.Load<Texture2D>("Enemy_Missile.png");
                tabEnemy[i].missile.direction = Vector2.Zero;
                tabEnemy[i].missile.etat = GameObjectAnime_Missile.Etat.TirGauche;
                tabEnemy[i].missile.estVivant = false;
                tabEnemy[i].missile.position = new Rectangle(fenetre.Right / 2, fenetre.Bottom / 2, 75, 75);
            }

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
            if (link.etat != GameObjectAnime.Etat.Mort)
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
                if (keys.IsKeyDown(Keys.A))
                {
                    link.arme.estVivant = true;
                    AttaqueHero(gameTime);
                }
                if (keys.IsKeyUp(Keys.A) && previousKeys.IsKeyDown(Keys.A))
                {
                    link.arme.estVivant = false;
                    AttaqueHero(gameTime);
                }



                // Teste les bordures d'écran
                // A FAIRE: Trouver un façon d'avoir les dimensions de la sprite pour faire le teste des bordures
                if (link.position.X < fenetre.Left)
                {
                    link.position.X = fenetre.Left;
                }
                else if (link.position.X + 75 > fenetre.Right)
                {
                    link.position.X = fenetre.Right - 75;
                }
                else if (link.position.Y < fenetre.Top)
                {
                    link.position.Y = fenetre.Top;
                }
                else if (link.position.Y + 74 > fenetre.Bottom) // Le nombre est calculé selon la hauteur du sprite de marche
                {
                    link.position.Y = fenetre.Bottom - 74;
                }
                previousKeys = keys;
            }

            link.Update(gameTime);     

        }

        protected void AttaqueHero(GameTime gameTime)
        {
            if (link.arme.estVivant)
            {
                if ((link.etat == GameObjectAnime.Etat.MarcheGauche) || (link.etat == GameObjectAnime.Etat.AttenteGauche))
                {
                    etatHeroAvantAttaque = link.etat;
                    link.etat = GameObjectAnime.Etat.AttaqueGauche;
                }
                else if ((link.etat == GameObjectAnime.Etat.MarcheDroite) || (link.etat == GameObjectAnime.Etat.AttenteDroite))
                {
                    etatHeroAvantAttaque = link.etat;
                    link.etat = GameObjectAnime.Etat.AttaqueDroite;
                }
                else if ((link.etat == GameObjectAnime.Etat.MarcheHaut) || (link.etat == GameObjectAnime.Etat.AttenteHaut))
                {
                    etatHeroAvantAttaque = link.etat;
                    link.etat = GameObjectAnime.Etat.AttaqueHaut;
                }
                else if ((link.etat == GameObjectAnime.Etat.MarcheBas) || (link.etat == GameObjectAnime.Etat.AttenteBas))
                {
                    etatHeroAvantAttaque = link.etat;
                    link.etat = GameObjectAnime.Etat.AttaqueBas;
                }
            }
            else
            {
                link.etat = etatHeroAvantAttaque;
            }
        }

        protected void UpdateEnemy(GameTime gameTime)
        {
            for (int i = 0; i < tabEnemy.GetLength(0); i++)
            {
                tabEnemy[i].position.X += (int)(tabEnemy[i].vitesse * tabEnemy[i].direction.X);
                tabEnemy[i].position.Y += (int)(tabEnemy[i].vitesse * tabEnemy[i].direction.Y);

                // Teste les bordures d'écran
                // A FAIRE: Trouver un façon d'avoir les dimensions de la sprite pour faire le teste des bordures
                if (tabEnemy[i].position.X < fenetre.Left)
                {
                    tabEnemy[i].position.X = fenetre.Left;
                    tabEnemy[i].etat = GameObjectAnimeEnemy.Etat.MarcheDroite;
                }
                else if (tabEnemy[i].position.X + 75 > fenetre.Right)
                {
                    tabEnemy[i].position.X = fenetre.Right - 75;
                    tabEnemy[i].etat = GameObjectAnimeEnemy.Etat.MarcheGauche;
                }
                else if (tabEnemy[i].position.Y < fenetre.Top)
                {
                    tabEnemy[i].position.Y = fenetre.Top;
                }
                else if (tabEnemy[i].position.Y + 75 > fenetre.Bottom) // Le nombre est calculé selon la hauteur du sprite de marche
                {
                    tabEnemy[i].position.Y = fenetre.Bottom - 75;
                }

                tabEnemy[i].direction.X = 0;
                tabEnemy[i].direction.Y = 0;

                switch (tabEnemy[i].etat)
                {
                    case GameObjectAnimeEnemy.Etat.MarcheDroite:
                        tabEnemy[i].direction.X = tabEnemy[i].vitesse;
                        break;

                    case GameObjectAnimeEnemy.Etat.AttenteDroite:
                        tabEnemy[i].direction.X = 0;
                        if (tabEnemy[i].missile.estVivant != true)
                        {
                            tabEnemy[i].missile.estVivant = true;
                            tabEnemy[i].missile.position = new Rectangle(tabEnemy[i].position.X, tabEnemy[i].position.Y, 75, 24);
                            tabEnemy[i].missile.etat = GameObjectAnime_Missile.Etat.TirDroite;
                        }
                        break;

                    case GameObjectAnimeEnemy.Etat.MarcheGauche:
                        tabEnemy[i].direction.X = -tabEnemy[i].vitesse;
                        break;

                    case GameObjectAnimeEnemy.Etat.AttenteGauche:
                        tabEnemy[i].direction.X = 0;
                        if (tabEnemy[i].missile.estVivant != true)
                        {
                            tabEnemy[i].missile.estVivant = true;
                            tabEnemy[i].missile.position = new Rectangle(tabEnemy[i].position.X, tabEnemy[i].position.Y, 75, 24);
                            tabEnemy[i].missile.etat = GameObjectAnime_Missile.Etat.TirGauche;
                        }
                        break;

                    case GameObjectAnimeEnemy.Etat.MarcheHaut:
                        tabEnemy[i].direction.Y = -tabEnemy[i].vitesse;
                        break;

                    case GameObjectAnimeEnemy.Etat.AttenteHaut:
                        tabEnemy[i].direction.Y = 0;
                        if (tabEnemy[i].missile.estVivant != true)
                        {
                            tabEnemy[i].missile.estVivant = true;
                            tabEnemy[i].missile.position = new Rectangle(tabEnemy[i].position.X, tabEnemy[i].position.Y, 24, 75);
                            tabEnemy[i].missile.etat = GameObjectAnime_Missile.Etat.TirHaut;
                        }
                        break;

                    case GameObjectAnimeEnemy.Etat.MarcheBas:
                        tabEnemy[i].direction.Y = tabEnemy[i].vitesse;
                        break;

                    case GameObjectAnimeEnemy.Etat.AttenteBas:
                        tabEnemy[i].direction.Y = 0;
                        if (tabEnemy[i].missile.estVivant != true)
                        {
                            tabEnemy[i].missile.estVivant = true;
                            tabEnemy[i].missile.position = new Rectangle(tabEnemy[i].position.X, tabEnemy[i].position.Y, 24, 75);
                            tabEnemy[i].missile.etat = GameObjectAnime_Missile.Etat.TirBas;
                        }
                        break;
                }

                tabEnemy[i].Update(gameTime);
            }
        }

        protected void UpdateMissile(GameTime gameTime)
        {
            for (int i = 0; i < tabEnemy.GetLength(0); i++)
            {
                if (tabEnemy[i].missile.estVivant)
                {
                    tabEnemy[i].missile.position.X += (int)(tabEnemy[i].missile.vitesse * tabEnemy[i].missile.direction.X);
                    tabEnemy[i].missile.position.Y += (int)(tabEnemy[i].missile.vitesse * tabEnemy[i].missile.direction.Y);

                    switch (tabEnemy[i].missile.etat)
                    {
                        case GameObjectAnime_Missile.Etat.TirDroite:
                            tabEnemy[i].missile.direction.X = tabEnemy[i].missile.vitesse;
                            tabEnemy[i].missile.direction.Y = 0;
                            break;

                        case GameObjectAnime_Missile.Etat.TirGauche:
                            tabEnemy[i].missile.direction.X = -tabEnemy[i].missile.vitesse;
                            tabEnemy[i].missile.direction.Y = 0;
                            break;
                        case GameObjectAnime_Missile.Etat.TirHaut:
                            tabEnemy[i].missile.direction.X = 0;
                            tabEnemy[i].missile.direction.Y = -tabEnemy[i].missile.vitesse;
                            break;
                        case GameObjectAnime_Missile.Etat.TirBas:
                            tabEnemy[i].missile.direction.X = 0;
                            tabEnemy[i].missile.direction.Y = tabEnemy[i].missile.vitesse;
                            break;
                        default:
                            break;
                    }

                    // Teste les collisions avec le héro

                    // Si le missile sort de l'écran, estVivant devient false
                    if ((tabEnemy[i].missile.position.X < fenetre.Left) || (tabEnemy[i].missile.position.X > fenetre.Right) || (tabEnemy[i].missile.position.Y < fenetre.Top) || (tabEnemy[i].missile.position.Y > fenetre.Bottom))
                    {
                        tabEnemy[i].missile.estVivant = false;
                    }

                    tabEnemy[i].missile.Update(gameTime);
                }
            }

        }

        protected void Collision()
        {
            for (int i = 0; i < tabEnemy.GetLength(0); i++)
            {
                if ((tabEnemy[i].estVivant) && (link.position.Intersects(tabEnemy[i].position)))
                {
                    link.estVivant = false;
                    link.etat = GameObjectAnime.Etat.Mort;
                }

                if ((tabEnemy[i].missile.estVivant) && (link.position.Intersects(tabEnemy[i].missile.position)))
                {
                    tabEnemy[i].missile.estVivant = false;
                    link.estVivant = false;
                    link.etat = GameObjectAnime.Etat.Mort;
                }

                if (link.arme.estVivant)
                {
                    if (link.arme.position.Intersects(tabEnemy[i].position))
                    {
                        tabEnemy[i].estVivant = false;
                    }
                }
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

            spriteBatch.Draw(background, fenetre, Color.White);


            spriteBatch.Draw(link.sprite, link.position, link.spriteAffiche, Color.White);


            if (link.arme.estVivant)
            {
                spriteBatch.Draw(link.arme.sprite, link.arme.position, link.arme.spriteAffiche, Color.White);
            }

            for (int i = 0; i < tabEnemy.GetLength(0); i++)
            {
                if (tabEnemy[i].estVivant)
                {
                    spriteBatch.Draw(tabEnemy[i].sprite, tabEnemy[i].position, tabEnemy[i].spriteAffiche, Color.White);

                    if (tabEnemy[i].missile.estVivant)
                    {
                        spriteBatch.Draw(tabEnemy[i].missile.sprite, tabEnemy[i].missile.position, tabEnemy[i].missile.spriteAffiche, Color.White);
                    }
                }

            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
