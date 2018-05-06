using AvoidMaster.Bus;
using AvoidMaster.Components;
using AvoidMaster.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvoidMaster.States
{
    public class GameState : State
    {
        private List<Component> components;
        private Background background;
        private Car blueCar;
        private Car redCar;
        private GameObjects gameObjects;
        private ObstacleMangager obstacleMangager;
        private Score score;
        public GameState(MainGame game, GraphicsDeviceManager graphics, ContentManager content) : base(game, graphics, content)
        {
            //Background init
            Texture2D backgroundTexture;
            using (var stream = TitleContainer.OpenStream("Content/GameBackGround.png"))
            {
                backgroundTexture = Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
            }

            game.Window.Position = new Point(game.Window.ClientBounds.Width/2, 0); // xPos and yPos (pixel)
            graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            graphics.PreferredBackBufferWidth = backgroundTexture.Width;
            graphics.ApplyChanges();
            background = new Background(backgroundTexture,GameBoundaries);

            //Blue car init
            using (var stream = TitleContainer.OpenStream("Content/BlueCar.png"))
            {
                var blueCarTexture= Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var location = new Vector2(blueCarTexture.Width-5,
                    GameBoundaries.Height-20-blueCarTexture.Height);
                blueCar = new Car(Car.CarTypes.BlueCar,blueCarTexture, location, GameBoundaries);
            }

            //Red car init
            using (var stream = TitleContainer.OpenStream("Content/RedCar.png"))
            {
                var redCarTexture= Texture2D.FromStream(this.graphics.GraphicsDevice, stream);
                var location = new Vector2(GameBoundaries.Width/2+redCarTexture.Width-5,
                    GameBoundaries.Height - 20-redCarTexture.Height);
                redCar= new Car(Car.CarTypes.RedCar,redCarTexture, location, GameBoundaries);
            }
            //Init game objects
            score = new Score(content.Load<SpriteFont>("ScoreFont"), GameBoundaries);
            obstacleMangager = new ObstacleMangager(GameBoundaries, graphics.GraphicsDevice );
            gameObjects = new GameObjects(blueCar, redCar,obstacleMangager,score);
            //Score init
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            background.Draw(gameTime, spriteBatch);
            blueCar.Draw(spriteBatch);
            redCar.Draw(spriteBatch);
            obstacleMangager.Draw(spriteBatch);
            score.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            blueCar.Update(gameTime,gameObjects);
            redCar.Update(gameTime, gameObjects);
            obstacleMangager.Update(gameTime,gameObjects);
            gameObjects.Update(gameTime);
            score.Update(gameTime, gameObjects);
        }
    }
}
