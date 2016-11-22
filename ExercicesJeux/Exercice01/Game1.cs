using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Exercice01
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        int fullScreen = 0;
        int positionCoeur;
        int attenteSpawnEnemy = 3;
        int compteurEnemyTue = 0;
        string messageGameOver = "***** Game Over *****";
        Random rnd = new Random();

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState keys = new KeyboardState();
        KeyboardState previousKeys = new KeyboardState();
        Rectangle fenetre;

        Texture2D background;
        Texture2D inventaire;
        Texture2D coeurPlein;
        Texture2D coeurMoitie;
        Texture2D coeurVide;

        GameObjectAnime_Hero link;
        GameObjectAnimeEnemy[] tabEnemy = new GameObjectAnimeEnemy[10];
        GameObjectAnime_Hero.Etat etatHeroAvantAttaque = GameObjectAnime_Hero.Etat.AttenteGauche;

        SoundEffect sonCoupEpee;
        SoundEffectInstance coupEpee;
        SoundEffect sonMortLink;
        SoundEffectInstance mortLink;
        SoundEffect sonLinkTouche;
        SoundEffectInstance linkTouche;
        SoundEffect sonMortEnemy;
        SoundEffectInstance mortEnemy;

        SpriteFont font;

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

            background = Content.Load<Texture2D>("Images\\Background.jpg");
            inventaire = Content.Load<Texture2D>("Images\\Inventaire.png");
            coeurPlein = Content.Load<Texture2D>("Images\\CoeurPlein.png");
            coeurMoitie = Content.Load<Texture2D>("Images\\CoeurMoitie.png");
            coeurVide = Content.Load<Texture2D>("Images\\CoeurVide.png");

            Song song = Content.Load<Song>("Sounds\\Overworld");
            MediaPlayer.Volume = (float)0.75;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);

            font = Content.Load<SpriteFont>("Fonts\\Font");

            sonCoupEpee = Content.Load<SoundEffect>("Sounds\\LOZ_Sword_Slash");
            coupEpee = sonCoupEpee.CreateInstance();
            sonLinkTouche = Content.Load<SoundEffect>("Sounds\\LOZ_Link_Hurt");
            linkTouche = sonLinkTouche.CreateInstance();
            sonMortLink = Content.Load<SoundEffect>("Sounds\\LOZ_Link_Die");
            mortLink = sonMortLink.CreateInstance();
            sonMortEnemy = Content.Load<SoundEffect>("Sounds\\LOZ_Enemy_Die");
            mortEnemy = sonMortEnemy.CreateInstance();

            link = new GameObjectAnime_Hero();
            link.sprite = Content.Load<Texture2D>(link.imgHero);
            link.InitializeArme();

            for (int i = 0; i < tabEnemy.GetLength(0); i++)
            {
                tabEnemy[i] = new GameObjectAnimeEnemy((GameObjectAnimeEnemy.TypeEnemy)rnd.Next(0,2));
                tabEnemy[i].sprite = Content.Load<Texture2D>(tabEnemy[i].imgEnemy);
                tabEnemy[i].position = new Rectangle(rnd.Next(400,fenetre.Right - 75), rnd.Next(400,fenetre.Bottom-75), 75, 75);
                

                //tabEnemy[i].missile = new GameObjectAnime_Missile();
                tabEnemy[i].arme.vitesse = 3;
                tabEnemy[i].arme.sprite = Content.Load<Texture2D>(tabEnemy[i].imgMissile);
                tabEnemy[i].arme.direction = Vector2.Zero;
                tabEnemy[i].arme.etat = GameObjectAnime_Missile.Etat.TirGauche;
                tabEnemy[i].arme.estVivant = false;
                tabEnemy[i].arme.position = new Rectangle(fenetre.Right / 2, fenetre.Bottom / 2, 75, 75);
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
            Collision(gameTime);

            base.Update(gameTime);
        }

        protected void UpdateHeros(GameTime gameTime)
        {
            if (link.etat != GameObjectAnime_Hero.Etat.Mort)
            {
                keys = Keyboard.GetState();
                link.position.X += (int)(link.vitesse * link.direction.X);
                link.position.Y += (int)(link.vitesse * link.direction.Y);

                // Touche de direction droite
                if (keys.IsKeyDown(Keys.Right))
                {
                    link.direction.X = 2;
                    link.etat = GameObjectAnime_Hero.Etat.MarcheDroite;
                }
                if (keys.IsKeyUp(Keys.Right) && previousKeys.IsKeyDown(Keys.Right))
                {
                    link.direction.X = 0;
                    link.etat = GameObjectAnime_Hero.Etat.AttenteDroite;
                }

                // Touche de direction gauche
                if (keys.IsKeyDown(Keys.Left))
                {
                    link.direction.X = -2;
                    link.etat = GameObjectAnime_Hero.Etat.MarcheGauche;
                }
                if (keys.IsKeyUp(Keys.Left) && previousKeys.IsKeyDown(Keys.Left))
                {
                    link.direction.X = 0;
                    link.etat = GameObjectAnime_Hero.Etat.AttenteGauche;
                }

                // Touche de direction haut
                if (keys.IsKeyDown(Keys.Up))
                {
                    link.direction.Y = -2;
                    link.etat = GameObjectAnime_Hero.Etat.MarcheHaut;
                }
                if (keys.IsKeyUp(Keys.Up) && previousKeys.IsKeyDown(Keys.Up))
                {
                    link.direction.Y = 0;
                    link.etat = GameObjectAnime_Hero.Etat.AttenteHaut;
                }

                // Touche de direction bas
                if (keys.IsKeyDown(Keys.Down))
                {
                    link.direction.Y = 2;
                    link.etat = GameObjectAnime_Hero.Etat.MarcheBas;
                }
                if (keys.IsKeyUp(Keys.Down) && previousKeys.IsKeyDown(Keys.Down))
                {
                    link.direction.Y = 0;
                    link.etat = GameObjectAnime_Hero.Etat.AttenteBas;
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
                coupEpee.Play();
                if ((link.etat == GameObjectAnime_Hero.Etat.MarcheGauche) || (link.etat == GameObjectAnime_Hero.Etat.AttenteGauche))
                {
                    etatHeroAvantAttaque = link.etat;
                    link.etat = GameObjectAnime_Hero.Etat.AttaqueGauche;
                }
                else if ((link.etat == GameObjectAnime_Hero.Etat.MarcheDroite) || (link.etat == GameObjectAnime_Hero.Etat.AttenteDroite))
                {
                    etatHeroAvantAttaque = link.etat;
                    link.etat = GameObjectAnime_Hero.Etat.AttaqueDroite;
                }
                else if ((link.etat == GameObjectAnime_Hero.Etat.MarcheHaut) || (link.etat == GameObjectAnime_Hero.Etat.AttenteHaut))
                {
                    etatHeroAvantAttaque = link.etat;
                    link.etat = GameObjectAnime_Hero.Etat.AttaqueHaut;
                }
                else if ((link.etat == GameObjectAnime_Hero.Etat.MarcheBas) || (link.etat == GameObjectAnime_Hero.Etat.AttenteBas))
                {
                    etatHeroAvantAttaque = link.etat;
                    link.etat = GameObjectAnime_Hero.Etat.AttaqueBas;
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
                if (tabEnemy[i].estVivant == true)
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
                            if (tabEnemy[i].arme.estVivant != true)
                            {
                                tabEnemy[i].arme.estVivant = true;
                                tabEnemy[i].arme.position = new Rectangle(tabEnemy[i].position.X, tabEnemy[i].position.Y, 75, 24);
                                tabEnemy[i].arme.etat = GameObjectAnime_Missile.Etat.TirDroite;
                            }
                            break;

                        case GameObjectAnimeEnemy.Etat.MarcheGauche:
                            tabEnemy[i].direction.X = -tabEnemy[i].vitesse;
                            break;

                        case GameObjectAnimeEnemy.Etat.AttenteGauche:
                            tabEnemy[i].direction.X = 0;
                            if (tabEnemy[i].arme.estVivant != true)
                            {
                                tabEnemy[i].arme.estVivant = true;
                                tabEnemy[i].arme.position = new Rectangle(tabEnemy[i].position.X, tabEnemy[i].position.Y, 75, 24);
                                tabEnemy[i].arme.etat = GameObjectAnime_Missile.Etat.TirGauche;
                            }
                            break;

                        case GameObjectAnimeEnemy.Etat.MarcheHaut:
                            tabEnemy[i].direction.Y = -tabEnemy[i].vitesse;
                            break;

                        case GameObjectAnimeEnemy.Etat.AttenteHaut:
                            tabEnemy[i].direction.Y = 0;
                            if (tabEnemy[i].arme.estVivant != true)
                            {
                                tabEnemy[i].arme.estVivant = true;
                                tabEnemy[i].arme.position = new Rectangle(tabEnemy[i].position.X, tabEnemy[i].position.Y, 24, 75);
                                tabEnemy[i].arme.etat = GameObjectAnime_Missile.Etat.TirHaut;
                            }
                            break;

                        case GameObjectAnimeEnemy.Etat.MarcheBas:
                            tabEnemy[i].direction.Y = tabEnemy[i].vitesse;
                            break;

                        case GameObjectAnimeEnemy.Etat.AttenteBas:
                            tabEnemy[i].direction.Y = 0;
                            if (tabEnemy[i].arme.estVivant != true)
                            {
                                tabEnemy[i].arme.estVivant = true;
                                tabEnemy[i].arme.position = new Rectangle(tabEnemy[i].position.X, tabEnemy[i].position.Y, 24, 75);
                                tabEnemy[i].arme.etat = GameObjectAnime_Missile.Etat.TirBas;
                            }
                            break;
                    }


                }
                else
                {
                    // À toutes les x secondes, respawn un monstre s'il y en a moins de 4
                    //if (gameTime.TotalGameTime.Seconds >= debutAttenteSpawnEnemy + attenteSpawnEnemy)
                    //{
                    //    for (int i = 0; i < tabEnemy.GetLength(0); i++)
                    //    {
                    //        if (tabEnemy[i].estVivant == false)
                    //        {
                    //            tabEnemy[i].estVivant = true;
                    //            i = tabEnemy.GetLength(0) + 1;
                    //        }
                    //    }
                    //}
                    if (gameTime.TotalGameTime.Seconds >= tabEnemy[i].debutAttenteSpawnEnemy + attenteSpawnEnemy)
                    {
                        tabEnemy[i].estVivant = true;
                    }
                }
                tabEnemy[i].Update(gameTime);
            }
        }

        protected void UpdateMissile(GameTime gameTime)
        {
            for (int i = 0; i < tabEnemy.GetLength(0); i++)
            {
                if (tabEnemy[i].estVivant != true)
                {
                    tabEnemy[i].arme.estVivant = false;
                }

                if (tabEnemy[i].arme.estVivant)
                {
                    tabEnemy[i].arme.position.X += (int)(tabEnemy[i].arme.vitesse * tabEnemy[i].arme.direction.X);
                    tabEnemy[i].arme.position.Y += (int)(tabEnemy[i].arme.vitesse * tabEnemy[i].arme.direction.Y);

                    switch (tabEnemy[i].arme.etat)
                    {
                        case GameObjectAnime_Missile.Etat.TirDroite:
                            tabEnemy[i].arme.direction.X = tabEnemy[i].arme.vitesse;
                            tabEnemy[i].arme.direction.Y = 0;
                            break;

                        case GameObjectAnime_Missile.Etat.TirGauche:
                            tabEnemy[i].arme.direction.X = -tabEnemy[i].arme.vitesse;
                            tabEnemy[i].arme.direction.Y = 0;
                            break;
                        case GameObjectAnime_Missile.Etat.TirHaut:
                            tabEnemy[i].arme.direction.X = 0;
                            tabEnemy[i].arme.direction.Y = -tabEnemy[i].arme.vitesse;
                            break;
                        case GameObjectAnime_Missile.Etat.TirBas:
                            tabEnemy[i].arme.direction.X = 0;
                            tabEnemy[i].arme.direction.Y = tabEnemy[i].arme.vitesse;
                            break;
                        default:
                            break;
                    }

                    // Teste les collisions avec le héro

                    // Si le missile sort de l'écran, estVivant devient false
                    if ((tabEnemy[i].arme.position.X < fenetre.Left) || (tabEnemy[i].arme.position.X > fenetre.Right) || (tabEnemy[i].arme.position.Y < fenetre.Top) || (tabEnemy[i].arme.position.Y > fenetre.Bottom))
                    {
                        tabEnemy[i].arme.estVivant = false;
                    }

                    tabEnemy[i].arme.Update(gameTime);
                }
            }

        }

        protected void Collision(GameTime gameTime)
        {
            for (int i = 0; i < tabEnemy.GetLength(0); i++)
            {
                if (tabEnemy[i].estVivant && link.estVivant && link.estInvincible!=true && link.position.Intersects(tabEnemy[i].position))
                {
                    link.pointDeVie -= 1;
                    if (link.pointDeVie > 0)
                    {
                        linkTouche.Play();
                        link.estInvincible = true;
                        link.position = new Rectangle(link.position.X - 100, link.position.Y -= 100, link.position.Width, link.position.Height);
                    }
                    else
                    {
                        link.estVivant = false;
                        link.etat = GameObjectAnime_Hero.Etat.Mort;
                    }
                }

                if (tabEnemy[i].arme.estVivant && link.estVivant && link.estInvincible != true && link.position.Intersects(tabEnemy[i].arme.position))
                {
                    link.pointDeVie -= 2;
                    link.estInvincible = true;
                    if (link.pointDeVie > 0)
                    {
                        linkTouche.Play();
                        link.position.X -= 100;
                        link.position = new Rectangle(link.position.X - 100, link.position.Y -= 100, link.position.Width, link.position.Height);
                    }
                    else
                    {
                        link.estVivant = false;
                        link.etat = GameObjectAnime_Hero.Etat.Mort;
                        sonMortLink.Play();
                    }
                }

                if (link.arme.estVivant && tabEnemy[i].estVivant)
                {
                    if (link.arme.position.Intersects(tabEnemy[i].position))
                    {
                        mortEnemy.Play();
                        compteurEnemyTue++;
                        tabEnemy[i].estVivant = false;
                        tabEnemy[i].debutAttenteSpawnEnemy = gameTime.TotalGameTime.Seconds;
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
            if (link.estVivant !=true)
            {
                spriteBatch.DrawString(font, messageGameOver, new Vector2((fenetre.Width/2) - (font.MeasureString(messageGameOver).X / 2), fenetre.Height/2), Color.Black);
            }

            if (link.arme.estVivant)
            {
                spriteBatch.Draw(link.arme.sprite, link.arme.position, link.arme.spriteAffiche, Color.White);
            }

            for (int i = 0; i < tabEnemy.GetLength(0); i++)
            {
                if (tabEnemy[i].estVivant)
                {
                    spriteBatch.Draw(tabEnemy[i].sprite, tabEnemy[i].position, tabEnemy[i].spriteAffiche, Color.White);

                    if (tabEnemy[i].arme.estVivant)
                    {
                        spriteBatch.Draw(tabEnemy[i].arme.sprite, tabEnemy[i].arme.position, tabEnemy[i].arme.spriteAffiche, Color.White);
                    }
                }

            }

            spriteBatch.Draw(inventaire, new Rectangle(fenetre.Right - inventaire.Width, 50, inventaire.Width, inventaire.Height), Color.White);
            spriteBatch.DrawString(font, "Enemis tues " + compteurEnemyTue.ToString(), new Vector2(fenetre.Right - inventaire.Width + 175, 90), Color.Gainsboro);

            // Afficher le nombre de coeur vide correspondant a coeurMax
            positionCoeur = fenetre.Right - inventaire.Width + 30;
            for (int i = 0; i < link.vieMax / 2; i++)
            {
                spriteBatch.Draw(coeurVide, new Rectangle(positionCoeur, 100, coeurPlein.Width, coeurPlein.Height), Color.White);

                positionCoeur += (coeurPlein.Width + 5);
            }
            // Ensuite calculer le nombre de coeur plein et ensuite afficher le dernier coeur a moitité ou non
            positionCoeur = fenetre.Right - inventaire.Width + 30;
            int nbCoeurPlein = link.pointDeVie / 2;
            for (int i = 0; i < nbCoeurPlein; i++)
            {
                spriteBatch.Draw(coeurPlein, new Rectangle(positionCoeur, 100, coeurPlein.Width, coeurPlein.Height), Color.White);
                positionCoeur += (coeurPlein.Width + 5);
            }
            if(link.pointDeVie%2 != 0)
            {
                spriteBatch.Draw(coeurMoitie, new Rectangle(positionCoeur, 100, coeurPlein.Width, coeurPlein.Height), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
