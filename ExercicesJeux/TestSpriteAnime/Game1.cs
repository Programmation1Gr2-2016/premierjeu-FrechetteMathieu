using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestSpriteAnime
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
        GameObjectAnime rambo;
        GameObject background;

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
            this.graphics.PreferredBackBufferWidth = 1024;//graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = 735;//graphics.GraphicsDevice.DisplayMode.Height;
            this.Window.Position = new Point(0, 0);
            this.graphics.ApplyChanges();

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
            background = new GameObject();
            background.sprite = Content.Load<Texture2D>("Images\\BackgroundContrat2.png");
            background.position = new Rectangle(0, 0, 11405, 809);

            rambo = new GameObjectAnime();
            rambo.direction = Vector2.Zero;
            rambo.vitesse.X = 2;
            rambo.objetState = GameObjectAnime.etats.attenteDroite;
            rambo.position = new Rectangle(350, 290, 65, 65);   //Position initiale de Rambo
            rambo.sprite = Content.Load<Texture2D>("Images\\Contra3.png");
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
            keys = Keyboard.GetState();
            //rambo.position.X += (int)(rambo.vitesse.X * rambo.direction.X);


            if (keys.IsKeyDown(Keys.Right))
            {
                rambo.direction.X = 2;
                background.position.X -= 3;
                rambo.objetState = GameObjectAnime.etats.runDroite;
            }
            if (keys.IsKeyUp(Keys.Right) && previousKeys.IsKeyDown(Keys.Right))
            {
                rambo.direction.X = 0;
                rambo.objetState = GameObjectAnime.etats.attenteDroite;
            }
            if (keys.IsKeyDown(Keys.Left))
            {
                rambo.direction.X = -2;
                background.position.X += 3;
                rambo.objetState = GameObjectAnime.etats.runGauche;
            }
            if (keys.IsKeyUp(Keys.Left) && previousKeys.IsKeyDown(Keys.Left))
            {
                rambo.direction.X = 0;
                rambo.objetState = GameObjectAnime.etats.attenteGauche;
            }

            //On appelle la méthode Update de Rambo qui permet de gérer les états
            rambo.Update(gameTime);
            previousKeys = keys;

            base.Update(gameTime);
        }

        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(background.sprite,background.position, Color.White);
            spriteBatch.Draw(rambo.sprite, rambo.position, rambo.spriteAfficher, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
