using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Engine2D;

namespace TestGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        uint[][] colors2;
        Texture2D bricks;
        Texture2D bricksnormal;
        Light light = new Light();
        uint[][] brickarray;
        Texture2D tempt;
        Sprite s;
        Vector3[][] vnorm;
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1980;
            graphics.PreferredBackBufferHeight = 1000;
            light.Position = new Vector3(0, 0, 100.0f);
            // TODO: Add your initialization logic here
            texture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);            
            texture.SetData<Color>(new[] { Color.Lime });
            texture2 = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            texture2.SetData<Color>(new[] { Color.Red });
            texture3 = new Texture2D(GraphicsDevice, 500, 300, false, SurfaceFormat.Color);
            texture4 = new Texture2D(GraphicsDevice, 500, 300, false, SurfaceFormat.Color);
            bricks = Content.Load<Texture2D>("Content\\brick_texture");
            bricksnormal = Content.Load<Texture2D>("Content\\brick_normals");
            Effect sh = Content.Load<Effect>("Content\\Lighting");
            
            s = new Sprite(bricks, bricksnormal, 1280, 853, sh, GraphicsDevice);
            //brickarray = new uint[1280][];
            uint[][] colors = new uint[300][];
            colors2 = new uint[300][];
            for (int i = 0; i < 1280; i++)
            {
              //  brickarray[i] = new uint[853];
            }
            for (int i = 0; i < 300; i++)
            {
                colors[i] = new uint[500];
                colors2[i] = new uint[500];
                for (int x = 0; x < 500; x++)
                {
                    Color c = new Color();
                    //a transparent texture drawn on the blue background does not get blended, if u want blended then draw a blue texture over the whole screen first
                    c.A = 0xFF;
                    c.R = 0x00;
                    c.B = 0x00;
                    c.G = 0xFF;
                    colors[i][x] = Math2.ColorToUInt(c);
                    colors2[i][x] = 0x00000000;
                }
            }
            Light l = new Light();
            colors2 = Lighting.DrawCircle(colors2, 200, 100, 100, 500, 300, Color.White);
            //colors[y][x]
            texture3.SetData<uint>(TextureArray.Uint2DArraytoSingle(colors, 500, 300));
            texture4.SetData<uint>(TextureArray.Uint2DArraytoSingle(colors2, 500, 300));
            r.ScreenHeight = graphics.PreferredBackBufferHeight;
            r.ScreenWidth = graphics.PreferredBackBufferWidth;
            floor.RigidBody = new RigidBody();
            floor.RigidBody.Box = new CollisionBox() {Center = new Vector3(0, 200, 0), Depth = 50.0f, Height = 200.0f, Width = 600.0f };
            floor.RigidBody.Velocity = new Vector3(0, 0, 0);
            go.RigidBody = new RigidBody();
            go.RigidBody.Box = new CollisionBox() { Center = new Vector3(0.0f, 0.0f, 0f), Depth = 20.0f, Height = 100.0f, Width = 50.0f };
            go.RigidBody.Velocity = new Vector3(1.0f, 0.0f, 0.0f);
            go.RigidBody.Acceleration = new Vector3(0, -0.981f, 0);
            go2.RigidBody = new RigidBody();
            go2.RigidBody.Box = new CollisionBox() { Center = new Vector3(100.0f, 0.0f, 0f), Depth = 20.0f, Height = 100.0f, Width = 50.0f };
            go2.RigidBody.Velocity = new Vector3(0.0f, 0.0f, 0.0f);
            //GraphicsDevice.BlendState = BlendState.AlphaBlend;
            vnorm = light.CalculateNormals(TextureArray.FromTexture(bricksnormal, 1280, 853));
            norm = light.IlluminateTextureUsingNormalMap(TextureArray.FromTexture(bricks, 1280, 853), vnorm, 10.0f);
            graphics.ApplyChanges();
            base.Initialize();
        }
        Texture2D texture;
        Texture2D texture2;
        Texture2D texture3;
        Texture2D texture4;
        FrameCounter fc = new FrameCounter();


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        float x2 = 0;
        protected override void Update(GameTime gameTime)
        {
            bool coll;
            bool coll2;
            go.RigidBody.Box.ReadyForCollisionChecks();
            go2.RigidBody.Box.ReadyForCollisionChecks();
            floor.RigidBody.Box.ReadyForCollisionChecks();
            CollisionSide cs = go.RigidBody.Box.CheckCollisionWithOther(go2.RigidBody.Box, out coll);
            CollisionSide cs2 = go.RigidBody.Box.CheckCollisionWithOther(floor.RigidBody.Box, out coll2);
            if (coll || coll2)
            {
                go.RigidBody.Velocity = Vector3.Zero;
            }

            KeyboardState ks = Keyboard.GetState();
            float x = 0.0f;
            float y = 0.0f;
            float z = 0.0f;
            if (ks.IsKeyDown(Keys.Up) && !ks.IsKeyDown(Keys.LeftShift))
            {
                y = -10.0f;
            }
            if (ks.IsKeyDown(Keys.Up) && ks.IsKeyDown(Keys.LeftShift))
            {
                z = 10.0f;
            }
            if (ks.IsKeyDown(Keys.Down) && ks.IsKeyDown(Keys.LeftShift))
            {
                z = -10.0f;
            }
            if (ks.IsKeyDown(Keys.Down) && !ks.IsKeyDown(Keys.LeftShift))
            {
                y = 10.0f;
            }
            if (ks.IsKeyDown(Keys.Right))
            {
                x = 10.0f;
            }
            if (ks.IsKeyDown(Keys.Left))
            {
                x = -10.0f;
            }
            if (cs == CollisionSide.Right || cs == CollisionSide.Left)
                x = 0.0f;
            if (cs == CollisionSide.Top || cs == CollisionSide.Bottom)
                y = 0.0f;
            if (cs == CollisionSide.FarBack || cs == CollisionSide.Close)
                z = 0.0f;
            if(cs2 == CollisionSide.Bottom)
            {
                y = 0.0f;
            }
            go.RigidBody.Velocity = new Vector3(x, y + 0.981f * x2 * y, z);
            Physics.Update(go);
            //Math2.ResetTexture(colors2, 500, 300);
            //colors2 = Lighting.DrawCircle(colors2, x2, 100, 300, 500, 300, Color.White);
            // TODO: Add your update logic here
            x2 += 0.1f;
            light.Position += new Vector3(x, y, z);
            //go.RigidBody.Box.Center = new Vector3(light.Position.X, light.Position.Y, 0.0f);
            base.Update(gameTime);
        }
        Renderer r = new Renderer();
        GameObject go2 = new GameObject();        
        GameObject go = new GameObject();
        GameObject floor = new GameObject();
        TextureArray norm;
        int width2 = 1280;
        int height2 = 853;

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            //spriteBatch.Begin();
            //spriteBatch.Draw(texture, new Rectangle(0, 0, 1980, 1000), Color.White);
            //spriteBatch.End();
            //texture4.SetData<uint>(TextureArray.Uint2DArraytoSingle(colors2, 500, 300));
            //spriteBatch.Draw(bricks, new Rectangle(0, 0, 1980, 1000), Color.White);
            //norm = light.IlluminateTextureUsingNormalMap(TextureArray.FromTexture(bricks, 1280, 853), vnorm, 10.0f);
            //spriteBatch.Draw(norm.ToTexture(GraphicsDevice), new Rectangle(0, 0, 1280, 853), Color.White);
            s.Draw(spriteBatch, light);
            //spriteBatch.Draw(texture3, new Vector2(0, 100), Color.White);
            //spriteBatch.Draw(texture4, new Vector2(light.Position.X, light.Position.Y), Color.White);
            //r.RenderCollisionBox(spriteBatch, go.RigidBody.Box, texture, true, true);
            //spriteBatch.Draw(texture, new Rectangle((int)light.Position.X, (int)light.Position.Y, 10, 10), Color.White);
            spriteBatch.Begin();
            r.RenderCollisionBox(spriteBatch, go2.RigidBody.Box, texture2, true, true);
            r.RenderCollisionBox(spriteBatch, floor.RigidBody.Box, texture2, true, true);
            spriteBatch.End();
            // TODO: Add your drawing code here
            base.Draw(gameTime);
            //fc.NewFrame(gameTime.ElapsedGameTime.TotalMilliseconds);
        }
    }
}
