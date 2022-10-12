using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace compsci_NEA
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _noise;
        private OpenSimplexNoise _accessSimplex;
        private entity _character;

        private Color[] _noiseColors;

        Texture2D _stone;
        Texture2D[] _blocks = new Texture2D[1];
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _character = new entity(new Rectangle(50, 100, 50, 75), Content.Load<Texture2D>("character"), 800);

            _blocks[0] = _stone;

            _accessSimplex = new OpenSimplexNoise();

            int width = 80;
            int height = 50;
            float scale = 3f;
            float noiseValue;
            _noiseColors = new Color[width*height];
            _noise = new Texture2D(GraphicsDevice, width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    //get noise value at scale
                    noiseValue = (float)_accessSimplex.Evaluate(x / scale, y / scale);

                    if (noiseValue < 0.12 )
                    {
                        _noiseColors[(y * width) + x] = Color.DarkGreen;
                    }
                    else
                    {
                        _noiseColors[(y * width) + x] = Color.White;
                    }

                    //y*textures width, to get distance down texture,+x get distance along texture
                    //_noiseColors[(y * width) + x] = new Color(noiseValue,noiseValue,noiseValue);
                }
            }

            _noise.SetData(_noiseColors);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _stone = Content.Load<Texture2D>("stone");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(samplerState: SamplerState.PointWrap);

            _spriteBatch.Draw(_noise, new Rectangle(0,0, 800, 500), Color.White);
            _stone.Draw()
            _character.Draw(_spriteBatch, Color.White);

            //https://stackoverflow.com/questions/13456928/xna-arrays-and-drawing-textures 

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
