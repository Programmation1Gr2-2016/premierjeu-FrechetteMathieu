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
        int scoreJoueur = 0;
        int bonusTriforce = 0;
        double bonusTemps = 0;
        int nbEnemyVivant;
        double tempsJoueurEnSecondes = 0;
        TimeSpan tempsDebutPartie;
        TimeSpan tempsFinPartie;
        double scoreMoyen = 0;
        double tempsMoyen = 0;
        int nbPartieJoue = 0;
        int nbEchec = 0;
        bool mustReinitialize = true;
        bool isFinPartie = false;
        string messageGameOver = "Game Over";
        string messageMenuPrincipale = "ENTER - Commencer\nESC - Quitter";
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
        Texture2D titreJeu;

        GameObjectAnime_Hero link;
        GameObjectAnimeEnemy[] tabEnemy = new GameObjectAnimeEnemy[10];
        GameObjectAnime_Hero.Etat etatHeroAvantAttaque = GameObjectAnime_Hero.Etat.AttenteGauche;
        GameObjectTile map = new GameObjectTile();
        GameObjectAnime_PowerUp triForce = new GameObjectAnime_PowerUp();

        SoundEffect sonCoupEpee;
        SoundEffectInstance coupEpee;
        SoundEffect sonMortLink;
        SoundEffectInstance mortLink;
        SoundEffect sonLinkTouche;
        SoundEffectInstance linkTouche;
        SoundEffect sonMortEnemy;
        SoundEffectInstance mortEnemy;

        SpriteFont font;

        Song songIntro;
        Song songEnJeuExterieur;
        Song songGameOver;

        enum Ecran { EcranTitre, Jeu, EcranRejouer };
        Ecran ecran = Ecran.EcranTitre;

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

            // Images
            background = Content.Load<Texture2D>("Images\\Background.jpg");
            inventaire = Content.Load<Texture2D>("Images\\Inventaire.png");
            coeurPlein = Content.Load<Texture2D>("Images\\CoeurPlein.png");
            coeurMoitie = Content.Load<Texture2D>("Images\\CoeurMoitie.png");
            coeurVide = Content.Load<Texture2D>("Images\\CoeurVide.png");
            map.texture = Content.Load<Texture2D>("Images\\Overworld_Tiles");
            titreJeu = Content.Load<Texture2D>("Images\\Titre.png");

            // Musique du jeu
            songIntro = Content.Load<Song>("Sounds\\Intro");
            songEnJeuExterieur = Content.Load<Song>("Sounds\\Overworld");
            songGameOver = Content.Load<Song>("Sounds\\GameOver");
            MediaPlayer.Volume = (float)0.75;
            MediaPlayer.IsRepeating = true;

            // Police
            font = Content.Load<SpriteFont>("Fonts\\Font");

            // Effets sonores
            sonCoupEpee = Content.Load<SoundEffect>("Sounds\\LOZ_Sword_Slash");
            coupEpee = sonCoupEpee.CreateInstance();
            sonLinkTouche = Content.Load<SoundEffect>("Sounds\\LOZ_Link_Hurt");
            linkTouche = sonLinkTouche.CreateInstance();
            sonMortLink = Content.Load<SoundEffect>("Sounds\\LOZ_Link_Die");
            mortLink = sonMortLink.CreateInstance();
            sonMortEnemy = Content.Load<SoundEffect>("Sounds\\LOZ_Enemy_Die");
            mortEnemy = sonMortEnemy.CreateInstance();

            // Héro
            Initialize_Hero();

            // Enemies
            Initialize_Enemy();

        }

        protected void Initialize_Hero()
        {
            link = new GameObjectAnime_Hero();
            link.sprite = Content.Load<Texture2D>("Images\\LinkSheet");
            link.InitializeArme();
            link.vitesse = 2;
            link.imgHero = "Images\\LinkSheet.png";
            link.position = new Rectangle(0, 450, 75, 75);
            link.direction = Vector2.Zero;
            link.estVivant = true;
            link.estInvincible = false;
            link.estRalenti = false;
            link.pointDeVie = link.vieMax;
            link.etat = GameObjectAnime_Hero.Etat.AttenteDroite;
        }
        protected void Initialize_Enemy()
        {
            for (int i = 0; i < tabEnemy.GetLength(0); i++)
            {
                tabEnemy[i] = new GameObjectAnimeEnemy((GameObjectAnimeEnemy.TypeEnemy)rnd.Next(0, 2));
                tabEnemy[i].sprite = Content.Load<Texture2D>(tabEnemy[i].imgEnemy);


                //tabEnemy[i].missile = new GameObjectAnime_Missile();

                //tabEnemy[i].arme.sprite = Content.Load<Texture2D>("Images\\Enemy_Missile.png");
                //tabEnemy[i].arme.direction = Vector2.Zero;
                //tabEnemy[i].arme.etat = GameObjectAnime_Missile.Etat.TirGauche;
                //tabEnemy[i].arme.estVivant = false;
                //tabEnemy[i].arme.position = new Rectangle(fenetre.Right / 2, fenetre.Bottom / 2, 75, 75);


                tabEnemy[i].arme.vitesse = 3;
                tabEnemy[i].arme.sprite = Content.Load<Texture2D>(tabEnemy[i].imgMissile);
                tabEnemy[i].arme.direction = Vector2.Zero;
                tabEnemy[i].arme.etat = GameObjectAnime_Missile.Etat.TirGauche;
                tabEnemy[i].arme.estVivant = false;
                tabEnemy[i].arme.position = new Rectangle(fenetre.Right / 2, fenetre.Bottom / 2, 75, 75);
            }

            tabEnemy[0].position = new Rectangle(1800, 700, 75, 75);
            tabEnemy[1].position = new Rectangle(1750, 520, 75, 75);
            tabEnemy[2].position = new Rectangle(1400, 510, 75, 75);
            tabEnemy[3].position = new Rectangle(1350, 650, 75, 75);
            tabEnemy[4].position = new Rectangle(1200, 800, 75, 75);
            tabEnemy[5].position = new Rectangle(1000, 700, 75, 75);
            tabEnemy[6].position = new Rectangle(950, 520, 75, 75);
            tabEnemy[7].position = new Rectangle(700, 250, 75, 75);
            tabEnemy[8].position = new Rectangle(700, 800, 75, 75);
            tabEnemy[9].position = new Rectangle(450, 800, 75, 75);
        }
        protected void ReinitializeContent(GameTime gameTime)
        {
            switch (ecran)
            {
                case Ecran.EcranTitre:
                    // Variable de pointage
                    nbPartieJoue = 0;
                    nbEchec = 0;
                    scoreMoyen = 0;
                    tempsMoyen = 0;
                    // Musique
                    MediaPlayer.Play(songIntro);
                    break;

                case Ecran.Jeu:
                    // Variable de pointage
                    nbPartieJoue++;
                    scoreJoueur = 0;
                    tempsJoueurEnSecondes = 0;
                    tempsDebutPartie = gameTime.TotalGameTime;
                    bonusTriforce = 0;
                    bonusTemps = 0;
                    isFinPartie = false;
                    nbEnemyVivant = tabEnemy.GetLength(0);
                    // Musique 
                    MediaPlayer.Play(songEnJeuExterieur);
                    // Héro
                    Initialize_Hero();
                    // Enemies
                    Initialize_Enemy();
                    break;

                case Ecran.EcranRejouer:
                    // Variable de pointage
                    tempsJoueurEnSecondes = tempsFinPartie.TotalSeconds - tempsDebutPartie.TotalSeconds;
                    MediaPlayer.Play(songGameOver);
                    break;
                default:
                    break;
            }

            mustReinitialize = false;
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
            if (mustReinitialize)
            {
                ReinitializeContent(gameTime);
            }

            switch (ecran)
            {
                case Ecran.EcranTitre:
                    UpdateMenuTitre(gameTime);
                    break;

                case Ecran.Jeu:
                    UpdateHeros(gameTime);
                    UpdateEnemy(gameTime);
                    UpdateMissile(gameTime);
                    Collision(gameTime);
                    TestFinPartie(gameTime);
                    break;

                case Ecran.EcranRejouer:
                    UpdateGameOver(gameTime);
                    break;
            }

            base.Update(gameTime);
        }

        protected void UpdateMenuTitre(GameTime gameTime)
        {
            keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.Enter))
            {
                //MediaPlayer.Stop();
                mustReinitialize = true;
                ecran = Ecran.Jeu;
            }
        }
        protected void UpdateGameOver(GameTime gameTime)
        {
            keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.R))
            {
                mustReinitialize = true;
                ecran = Ecran.Jeu;
            }
            else if (keys.IsKeyDown(Keys.Q))
            {
                mustReinitialize = true;
                ecran = Ecran.EcranTitre;
            }
        }
        protected void UpdateHeros(GameTime gameTime)
        {
            if (link.etat != GameObjectAnime_Hero.Etat.Mort)
            {
                keys = Keyboard.GetState();

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

                if (link.estRalenti != true)
                {
                    link.position.X += (int)(link.vitesse * link.direction.X);
                    link.position.Y += (int)(link.vitesse * link.direction.Y);
                }
                else
                {
                    link.position.X += (int)(link.direction.X);
                    link.position.Y += (int)(link.direction.Y);
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
                    // Teste les bordures d'écran
                    if (tabEnemy[i].position.X < fenetre.Left)
                    {
                        tabEnemy[i].position.X = fenetre.Left;
                        tabEnemy[i].etat = GameObjectAnimeEnemy.Etat.MarcheDroite;
                    }
                    else if (tabEnemy[i].position.X + tabEnemy[i].spriteAffiche.Width > fenetre.Right)
                    {
                        tabEnemy[i].position.X = fenetre.Right - tabEnemy[i].spriteAffiche.Width;
                        tabEnemy[i].etat = GameObjectAnimeEnemy.Etat.MarcheGauche;
                    }
                    else if (tabEnemy[i].position.Y < fenetre.Top)
                    {
                        tabEnemy[i].position.Y = fenetre.Top;
                    }
                    else if (tabEnemy[i].position.Y + tabEnemy[i].spriteAffiche.Height > fenetre.Bottom)
                    {
                        tabEnemy[i].position.Y = fenetre.Bottom - tabEnemy[i].spriteAffiche.Height;
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

                    tabEnemy[i].position.X += (int)(tabEnemy[i].vitesse * tabEnemy[i].direction.X);
                    tabEnemy[i].position.Y += (int)(tabEnemy[i].vitesse * tabEnemy[i].direction.Y);
                }
                else
                {
                    if (gameTime.TotalGameTime.Seconds >= tabEnemy[i].debutAttenteSpawnEnemy + attenteSpawnEnemy)
                    {
                        //tabEnemy[i].estVivant = true;
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
            link.estRalenti = false;

            for (int i = 0; i < tabEnemy.GetLength(0); i++)
            {
                if (tabEnemy[i].estVivant && link.estVivant && link.estInvincible != true && link.position.Intersects(tabEnemy[i].position))
                {
                    link.pointDeVie -= 1;
                    if (link.pointDeVie > 0)
                    {
                        linkTouche.Play();
                        link.estInvincible = true;
                        //link.position = new Rectangle(link.position.X - 50, link.position.Y -= 50, link.position.Width, link.position.Height);
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
                        //link.position = new Rectangle(link.position.X - 50, link.position.Y -= 50, link.position.Width, link.position.Height);
                    }
                    else
                    {
                        link.estVivant = false;
                        link.etat = GameObjectAnime_Hero.Etat.Mort;
                    }
                }

                if (link.arme.estVivant && tabEnemy[i].estVivant)
                {
                    if (link.arme.position.Intersects(tabEnemy[i].position))
                    {
                        scoreJoueur += 100;
                        nbEnemyVivant--;
                        mortEnemy.Play();
                        compteurEnemyTue++;
                        tabEnemy[i].estVivant = false;
                        tabEnemy[i].etat = GameObjectAnimeEnemy.Etat.Mort;
                        tabEnemy[i].debutAttenteSpawnEnemy = gameTime.TotalGameTime.Seconds;
                    }
                }

            }

            for (int i = 0; i < GameObjectTile.LIGNE; i++)
            {
                map.rectSource.Y = (i * 100);
                for (int j = 0; j < GameObjectTile.COLONNE; j++)
                {
                    map.rectSource.X = (j * 100);

                    // Détection des collisions du héro avec le fond
                    if (link.estVivant && link.position.Intersects(map.rectSource))
                    {
                        if (map.map1[i, j].bloqueHero)
                        {
                            link.position.X -= (int)(link.vitesse * link.direction.X);
                            link.position.Y -= (int)(link.vitesse * link.direction.Y);
                        }
                        else if (map.map1[i, j].slowHero)
                        {
                            link.estRalenti = true;
                        }
                    }

                    // Détection des collisions du héro avec le fond
                    if (link.arme.estVivant && link.arme.position.Intersects(map.rectSource))
                    {
                        if (map.map1[i, j].detruisable)
                        {
                            map.map1[i, j] = new Tuiles(Tuiles.TypeSol.Terre);
                        }
                    }

                    // Détection des collisions de l'enemy avec le fond
                    for (int k = 0; k < tabEnemy.GetLength(0); k++)
                    {
                        if (tabEnemy[k].estVivant && tabEnemy[k].position.Intersects(map.rectSource))
                        {
                            if (map.map1[i, j].bloqueHero)
                            {
                                tabEnemy[k].position.X -= (int)(tabEnemy[k].vitesse * tabEnemy[k].direction.X);
                                tabEnemy[k].position.Y -= (int)(tabEnemy[k].vitesse * tabEnemy[k].direction.Y);
                            }
                        }

                        if (tabEnemy[k].arme.estVivant && tabEnemy[k].arme.position.Intersects(map.rectSource))
                        {
                            if (map.map1[i, j].bloqueMissile)
                            {
                                tabEnemy[k].arme.estVivant = false;
                            }
                        }
                    }
                }
            }
        }
        protected void TestFinPartie(GameTime gameTime)
        {
            if (120 - (gameTime.TotalGameTime.TotalSeconds - tempsDebutPartie.TotalSeconds) < 0)
            {
                link.etat = GameObjectAnime_Hero.Etat.Mort;
                link.Update(gameTime);
                nbEchec++;
                isFinPartie = true;
            }
            else if (link.etat == GameObjectAnime_Hero.Etat.Mort)
            {
                nbEchec++;
                isFinPartie = true;
            }
            else if (nbEnemyVivant <= 0)
            {
                link.etat = GameObjectAnime_Hero.Etat.Win;
                link.Update(gameTime);
                link.position.Height = link.tabWin[0].Height;
                isFinPartie = true;
            }

            if (isFinPartie)
            {
                bonusTriforce = 0;
                if(link.etat == GameObjectAnime_Hero.Etat.Win)
                {
                    bonusTemps = 120 - (tempsFinPartie.TotalSeconds - tempsDebutPartie.TotalSeconds);
                }
                scoreMoyen += scoreJoueur + bonusTriforce + bonusTemps;
                tempsFinPartie = gameTime.TotalGameTime;
                mustReinitialize = true;
                ecran = Ecran.EcranRejouer;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            switch (ecran)
            {
                case Ecran.EcranTitre:
                    spriteBatch.Draw(titreJeu, new Rectangle((fenetre.Right / 2) - (titreJeu.Width / 2), (fenetre.Bottom / 2) - (300), titreJeu.Width, titreJeu.Height), Color.White);
                    spriteBatch.DrawString(font, messageMenuPrincipale, new Vector2((fenetre.Width / 2) - (font.MeasureString(messageGameOver).X / 2), fenetre.Height / 2), Color.White);
                    break;

                case Ecran.Jeu:
                    // Affichage de la map
                    map.Draw(spriteBatch);
                    DrawHero(gameTime);
                    DrawEnemy(gameTime);
                    DrawInfoJeu(gameTime);
                    break;

                case Ecran.EcranRejouer:
                    DrawGameOver(gameTime);
                    break;
            }



            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected void DrawHero(GameTime gameTime)
        {
            spriteBatch.Draw(link.sprite, link.position, link.spriteAffiche, Color.White);
            if (link.estVivant != true)
            {

            }

            if (link.arme.estVivant)
            {
                spriteBatch.Draw(link.arme.sprite, link.arme.position, link.arme.spriteAffiche, Color.White);
            }
        }
        protected void DrawEnemy(GameTime gameTime)
        {
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
        }
        protected void DrawInfoJeu(GameTime gameTime)
        {
            TimeSpan chrono = gameTime.TotalGameTime;


            spriteBatch.Draw(inventaire, new Rectangle(fenetre.Right - inventaire.Width, 50, inventaire.Width, inventaire.Height), Color.White);
            spriteBatch.DrawString(font, "Chrono : " + string.Format("{0:0.00}", 120 - (gameTime.TotalGameTime.TotalSeconds - tempsDebutPartie.TotalSeconds)), new Vector2(fenetre.Right - inventaire.Width + 175, 50), Color.Gainsboro);
            spriteBatch.DrawString(font, "Score   : " + scoreJoueur.ToString(), new Vector2(fenetre.Right - inventaire.Width + 175, 90), Color.Gainsboro);

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
            if (link.pointDeVie % 2 != 0)
            {
                spriteBatch.Draw(coeurMoitie, new Rectangle(positionCoeur, 100, coeurPlein.Width, coeurPlein.Height), Color.White);
            }
        }
        protected void DrawGameOver(GameTime gameTime)
        {
            spriteBatch.DrawString(font, messageGameOver, new Vector2((fenetre.Width / 2) - (font.MeasureString(messageGameOver).X / 2), 100), Color.White);
            spriteBatch.Draw(link.sprite, new Rectangle((fenetre.Width / 2) - (link.position.Width / 2), 250, link.position.Width, link.position.Height), link.spriteAffiche, Color.White);

            spriteBatch.DrawString(font, "Score", new Vector2((fenetre.Width / 2) - 200, 350), Color.White);
            spriteBatch.DrawString(font, "_______________________", new Vector2((fenetre.Width / 2) - 200, 400), Color.White);
            spriteBatch.DrawString(font, string.Format("{0:0}", scoreJoueur + bonusTriforce + bonusTemps), new Vector2((fenetre.Width / 2) + 100, 350), Color.White);
            spriteBatch.DrawString(font, "Enemis tues :", new Vector2((fenetre.Width / 2) - 200, 450), Color.White);
            spriteBatch.DrawString(font, scoreJoueur.ToString(), new Vector2((fenetre.Width / 2) + 100, 450), Color.White);
            spriteBatch.DrawString(font, "Bonus triforce :", new Vector2((fenetre.Width / 2) - 200, 500), Color.White);
            spriteBatch.DrawString(font, bonusTriforce.ToString(), new Vector2((fenetre.Width / 2) + 100, 500), Color.White);
            spriteBatch.DrawString(font, "Bonus temps : ", new Vector2((fenetre.Width / 2) - 200, 550), Color.White);
            spriteBatch.DrawString(font, string.Format("{0:0}", bonusTemps), new Vector2((fenetre.Width / 2) + 100, 550), Color.White);


            spriteBatch.DrawString(font, "Statistique", new Vector2((fenetre.Width / 2) - 200, 650), Color.White);
            spriteBatch.DrawString(font, "_______________________", new Vector2((fenetre.Width / 2) - 200, 650), Color.White);
            spriteBatch.DrawString(font, "Parties joues :", new Vector2((fenetre.Width / 2) - 200, 700), Color.White);
            spriteBatch.DrawString(font, nbPartieJoue.ToString(), new Vector2((fenetre.Width / 2) + 100, 700), Color.White);
            spriteBatch.DrawString(font, "% reussite :", new Vector2((fenetre.Width / 2) - 200, 750), Color.White);
            spriteBatch.DrawString(font, Convert.ToString((nbPartieJoue - nbEchec) * 100 / nbPartieJoue), new Vector2((fenetre.Width / 2) + 100, 750), Color.White);
            spriteBatch.DrawString(font, "Score moyen :", new Vector2((fenetre.Width / 2) - 200, 800), Color.White);
            if(nbPartieJoue == 0)
            {
                spriteBatch.DrawString(font, "0", new Vector2((fenetre.Width / 2) + 100, 800), Color.White);
            }
            else
            {
                spriteBatch.DrawString(font, string.Format("{0:0}", scoreMoyen / nbPartieJoue), new Vector2((fenetre.Width / 2) + 100, 800), Color.White);
            }
            
        }
    }
}
