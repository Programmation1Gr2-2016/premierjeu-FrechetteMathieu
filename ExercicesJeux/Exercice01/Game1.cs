using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        GameObjectAnime link;
        Rectangle fenetre;
        GameObject heros;
        GameObject ennemis;

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

            link = new GameObjectAnime();
            link.position = new Rectangle(300,100,75,75);
            link.vitesse = 2;
            link.sprite = Content.Load<Texture2D>("LinkSheet.png");
            link.direction = Vector2.Zero;
            link.etat = GameObjectAnime.Etat.AttenteDroite;

            //heros = new GameObject();
            //heros.estVivant = true;
            //heros.vitesse = 5;
            //heros.sprite = Content.Load<Texture2D>("LinkSheet.png");
            //heros.direction = GameObject.directionObjet.droite;
            //heros.position = new Rectangle(100,100,75,75);

            ennemis = new GameObject();
            ennemis.estVivant = true;
            ennemis.vitesse = 3;
            ennemis.sprite = Content.Load<Texture2D>("Ennemis01.png");
            ennemis.position = new Rectangle(fenetre.Right - ennemis.sprite.Width, fenetre.Bottom/2, ennemis.sprite.Width, ennemis.sprite.Height);
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
            //switch (Keyboard.GetState().GetPressedKeys)
            //{

            //}

            

            //if (Keyboard.GetState().IsKeyDown(Keys.D))
            //{
            //    heros.direction = GameObject.directionObjet.droite;
            //    heros.position.X += heros.vitesse;
            //}
            //else if (Keyboard.GetState().IsKeyDown(Keys.A))
            //{
            //    heros.direction = GameObject.directionObjet.gauche;
            //    heros.position.X -= heros.vitesse;
            //}
            //else if (Keyboard.GetState().IsKeyDown(Keys.W))
            //{
            //    heros.direction = GameObject.directionObjet.haut;
            //    heros.position.Y -= heros.vitesse;
            //}
            //else if (Keyboard.GetState().IsKeyDown(Keys.S))
            //{
            //    heros.direction = GameObject.directionObjet.bas;
            //    heros.position.Y += heros.vitesse;
            //}

            UpdateHeros(gameTime);

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

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            //spriteBatch.Draw(heros.sprite, heros.position, Color.White);
            //spriteBatch.Draw(heros.sprite, heros.position, heros.rectSprite, Color.White);

            spriteBatch.Draw(link.sprite, link.position, link.spriteAffiche, Color.White);

            spriteBatch.Draw(ennemis.sprite, ennemis.position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
