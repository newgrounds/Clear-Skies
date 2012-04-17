using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using D3DFont = Microsoft.DirectX.Direct3D.Font;
using WinFont = System.Drawing.Font; 
using DI = Microsoft.DirectX.DirectInput;
using ClearSkies.Exceptions;
using D3D = Microsoft.DirectX.Direct3D;
using ClearSkies.Prefabs;
using Microsoft.DirectX;
using ClearSkies.Prefabs.Turrets;
using ClearSkies.Prefabs.Enemies;
using ClearSkies.Managers;
using ClearSkies.Content;
using ClearSkies.Prefabs.Cameras;

namespace ClearSkies
{
    public partial class Game : Form
    {
        #region Fields

        private D3D.Device device;
        private DI.Device keyboard;
        private DI.Device mouse;

        Font systemFont = new Font("Arial", 12f, FontStyle.Regular);
        D3DFont theFont;

        Turret player;
        float health;

        private ClearSkies.Prefabs.Cameras.ThirdPersonCamera camera;
        private GameState gameState;
        //private bool enterPressed; // used to prevent errors when holding enter

        private List<Manager> managers;

        private GUI gui;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates a form for the Turret game
        /// </summary>
        public Game()
        {
            InitializeWindow();
            InitializeInputDevices();
            InitializeGraphics();
            InitializeGame();

            theFont = new D3DFont(device, systemFont);
        }

        /// <summary>
        /// Initialize the game windows size parameters.
        /// </summary>
        private void InitializeWindow()
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Rectangle scrn = Screen.GetBounds(this);
            this.Width = scrn.Width;
            this.Height = scrn.Height;
            
            //enterPressed = false;
        }

        /// <summary>
        /// Load all initial game logic.
        /// </summary>
        private void InitializeGame()
        {
            this.gameState = GameState.Start;
        }

        /// <summary>
        /// Initializes the display parameters and creates the game objects.
        /// </summary>
        private void InitializeGraphics()
        {
            #region Presentation Parameters

            D3D.PresentParameters pres = new D3D.PresentParameters();
            pres.Windowed = true;
            pres.SwapEffect = D3D.SwapEffect.Discard;
            pres.EnableAutoDepthStencil = true;
            pres.AutoDepthStencilFormat = D3D.DepthFormat.D16;

            device = new D3D.Device(0, D3D.DeviceType.Hardware, this, D3D.CreateFlags.SoftwareVertexProcessing, pres);
            device.RenderState.CullMode = D3D.Cull.None;

            #endregion

            #region Game Objects

            ContentLoader.initialize(device);

            player = TurretManager.spawnTurret(TurretType.Basic, Vector3.Empty, Vector3.Empty, new Vector3(1f, 1f, 1f), keyboard);

            managers = new List<Manager>();
            BulletManager bulletManager = new BulletManager();
            managers.Add(bulletManager);
            TurretManager turretManager = new TurretManager();
            managers.Add(turretManager);

            // the enemy manager also adds enemies
            List<Wave> waves = new List<Wave>();
            Wave wave = new Wave();
            wave.planesToSpawn = 5;
            wave.tanksToSpawn = 5;
            wave.planeSpeed = 1f;
            wave.planeTurnSpeed = 1f;
            wave.tankSpeed = 1f;
            wave.tankTurnSpeed = 1f;
            wave.tanksSpawned = 0;
            wave.planesSpawned = 0;
            wave.tanksDestroyed = 0;
            wave.planesDestroyed = 0;
            wave.spawnDelay = 5f;
            wave.enemiesPerSpawn = 10;
            wave.spawnDistance = 50f;
            waves.Add(wave);
            EnemyManager enemyManager = new EnemyManager(waves, 0);
            managers.Add(enemyManager);
            
            // TODO: make the gui redraw based on the window size
            gui = new GUI(this.player, new Rectangle(this.Width - (int)(this.Width * 0.0625),
                (int)(this.Height * 0.0052),
                (int)(this.Width * 0.0521),
                (int)(this.Height * 0.0104)),
                new Point(this.Width - (int)(this.Width * 0.183), 0), 
                device, this.Width, this.Height);
            
            this.camera = new ThirdPersonCamera(player, new Vector3(0f, 7f, -7f));
            player.Head.addChild(camera);

            ParticleEmitterManager particleManager = new ParticleEmitterManager(camera);
            managers.Add(particleManager);

            #endregion
        }


        /// <summary>
        /// Initializes the Keyboard and Mouse.
        /// </summary>
        /// <exception cref="KeyboardNotFoundException">DirectInput could not aquire Keyboard</exception>
        /// <exception cref="MouseNotFoundException">DirectInput could not aquire Mouse</exception>
        private void InitializeInputDevices()
        {
            keyboard = new DI.Device(DI.SystemGuid.Keyboard);
            if (keyboard == null) throw new KeyboardNotFoundException("No keyboard found.");

            mouse = new DI.Device(DI.SystemGuid.Mouse);
            if (mouse == null) throw new MouseNotFoundException("No mouse found.");

            keyboard.SetCooperativeLevel(this, DI.CooperativeLevelFlags.Background | DI.CooperativeLevelFlags.NonExclusive);
            keyboard.Acquire();

            mouse.SetCooperativeLevel(this, DI.CooperativeLevelFlags.NonExclusive | DI.CooperativeLevelFlags.Background);
            mouse.Acquire();
        }

        #endregion

        #region DisposalMethods

        /// <summary>
        /// Disposes of the graphics device.
        /// </summary>
        public void DisposeGraphics()
        {
            device.Dispose();
        }

        #endregion

        #region Update Methods

        /// <summary>
        /// Updates all the game objects and listens for user input from mouse and keyboard.
        /// </summary>
        /// <param name="deltaTime">Time in seconds since last update.</param>
        public void update(float deltaTime)
        {
            health = player.Health;
            gui.width = this.Width;
            gui.height = this.Height;
            gui.healthArea = new Rectangle(
                this.Width - (int)(this.Width * 0.0625),
                (int)(this.Height * 0.0052), 
                (int)(this.Width * 0.0521),
                (int)(this.Height * 0.0104));
            gui.healthTexturePoint = new Point(this.Width - (int)(this.Width * 2 * 0.183),
                -(int)(this.Height * 0.05f));

            DI.KeyboardState keys = keyboard.GetCurrentKeyboardState();

            switch (gameState)
            {
                case GameState.Lose:
                    break;
                case GameState.Pause:
                    break;
                case GameState.Play:
                    
                    // update stuff
                    break;
                case GameState.Quit:
                    break;
                case GameState.Start:
                    break;
                case GameState.Win:
                    break;
            }            

            foreach (Manager m in managers)
            {
                m.update(deltaTime);
            }

            if (keys[DI.Key.Escape])
            {
                gameState = GameState.Quit;
            }
        }

        #endregion

        #region Rendering Methods

        /// <summary>
        /// Draws everything to the device
        /// </summary>
        public void draw()
        {
            device.Clear(D3D.ClearFlags.Target | D3D.ClearFlags.ZBuffer, Color.BlueViolet, 1f, 0);
            device.BeginScene();

            SetupLights();

            device.Transform.World = Matrix.Translation(0, 0, 0);


            // TERRAIN
            drawTerrain(new D3D.CustomVertex.PositionNormalTextured(new Vector3(-128f, 0, -128f), new Vector3(0, 0, 1), 0, 32),
                new D3D.CustomVertex.PositionNormalTextured(new Vector3(-128f, 0, 128f), new Vector3(0, 0, 1), 0, 0),
                new D3D.CustomVertex.PositionNormalTextured(new Vector3(128f, 0, -128f), new Vector3(0, 0, 1), 32, 32),
                new D3D.CustomVertex.PositionNormalTextured(new Vector3(128f, 0, 128f), new Vector3(0, 0, 1), 32, 0),
                ContentLoader.Terrain);
            

            // TOP OF SKYBOX
            drawTerrain(new D3D.CustomVertex.PositionNormalTextured(new Vector3(-256f, 254.5f, -256f), new Vector3(0, 0, -1), 0, 1),
                new D3D.CustomVertex.PositionNormalTextured(new Vector3(-256f, 254.5f, 256f), new Vector3(0, 0, -1), 0, 0),
                new D3D.CustomVertex.PositionNormalTextured(new Vector3(256f, 254.5f, -256f), new Vector3(0, 0, -1), 1, 1),
                new D3D.CustomVertex.PositionNormalTextured(new Vector3(256f, 254.5f, 256f), new Vector3(0, 0, -1), 1, 0),
                ContentLoader.SkyTop);

            
            // LEFT OF SKYBOX
            drawTerrain(new D3D.CustomVertex.PositionNormalTextured(new Vector3(-254.5f, -256f, -256f), new Vector3(0, 0, 1), 0, 1),
                new D3D.CustomVertex.PositionNormalTextured(new Vector3(-254.5f, 256f, -256f), new Vector3(0, 0, 1), 0, 0),
                new D3D.CustomVertex.PositionNormalTextured(new Vector3(-254.5f, -256f, 256f), new Vector3(0, 0, 1), 1, 1),
                new D3D.CustomVertex.PositionNormalTextured(new Vector3(-254.5f, 256f, 256f), new Vector3(0, 0, 1), 1, 0),
                ContentLoader.SkyLeft);

            
            // RIGHT OF SKYBOX
            drawTerrain(new D3D.CustomVertex.PositionNormalTextured(new Vector3(254.5f, -256f, -256f), new Vector3(0, 0, -1), 0, 1),
                new D3D.CustomVertex.PositionNormalTextured(new Vector3(254.5f, 256f, -256f), new Vector3(0, 0, -1), 0, 0),
                new D3D.CustomVertex.PositionNormalTextured(new Vector3(254.5f, -256f, 256f), new Vector3(0, 0, -1), 1, 1),
                new D3D.CustomVertex.PositionNormalTextured(new Vector3(254.5f, 256f, 256f), new Vector3(0, 0, -1), 1, 0),
                ContentLoader.SkyRight);

            
            // FRONT OF SKYBOX
            drawTerrain(new D3D.CustomVertex.PositionNormalTextured(new Vector3(-256f, -256f, 254.5f), new Vector3(0, 0, 1), 0, 1),
                new D3D.CustomVertex.PositionNormalTextured(new Vector3(-256f, 256f, 254.5f), new Vector3(0, 0, 1), 0, 0),
                new D3D.CustomVertex.PositionNormalTextured(new Vector3(256f, -256f, 254.5f), new Vector3(0, 0, 1), 1, 1),
                new D3D.CustomVertex.PositionNormalTextured(new Vector3(256f, 256f, 254.5f), new Vector3(0, 0, 1), 1, 0),
                ContentLoader.SkyFront);


            // BACK OF SKYBOX
            drawTerrain(new D3D.CustomVertex.PositionNormalTextured(new Vector3(-256f, -256f, -254.5f), new Vector3(0, 0, -1), 0, 1),
                new D3D.CustomVertex.PositionNormalTextured(new Vector3(-256f, 256f, -254.5f), new Vector3(0, 0, -1), 0, 0),
                new D3D.CustomVertex.PositionNormalTextured(new Vector3(256f, -256f, -254.5f), new Vector3(0, 0, -1), 1, 1),
                new D3D.CustomVertex.PositionNormalTextured(new Vector3(256f, 256f, -254.5f), new Vector3(0, 0, -1), 1, 0),
                ContentLoader.SkyBack);
            

            /*
            // BEGIN SHADER SNIPPET

            // Load the effect from file.
            D3D.Effect effect = D3D.Effect.FromFile(device, "../../scene_toonEdges.fx/HLSL/scene_toonEdges.fx", 
                                            null, null, D3D.ShaderFlags.Debug, null);
            // Set the technique.
            effect.Technique = "Main";

            // Note: Effect.Begin returns the number of
            // passes required to render the effect.
            int passes = effect.Begin(0);

            // END SHADER SNIPPET*/

            switch (gameState)
            {
                case GameState.Lose:
                case GameState.Pause:
                case GameState.Play:
                case GameState.Quit:
                case GameState.Start:
                case GameState.Win:
                    foreach (Manager m in managers)
                    {
                        m.draw(device);
                    }

                    break;
            }

            camera.view(device);

            gui.draw();

            /*
            // BEGIN SHADER CODE

            // Loop through all of the effect's passes.
            for (int i = 0; i < passes; i++)
            {
                // Set a shader constant
                //effect.SetValue("WorldMatrix", true);

                // Set state for the current effect pass.
                effect.BeginPass(i);

                // Render some primitives.
                //device.DrawPrimitives(D3D.PrimitiveType.TriangleList, 0, 1);
                foreach (Manager m in managers)
                {
                    m.draw(device);
                }

                // End the effect pass
                effect.EndPass();
            }

            // Must call Effect.End to signal the end of the technique.
            effect.End();

            // END SHADER CODE*/

            device.EndScene();
            device.Present();
        }

        /// <summary>
        /// Sets up the lighting in the game world.
        /// </summary>
        protected void SetupLights()
        {
            device.RenderState.Lighting = true;
            
            device.Lights[0].Type = D3D.LightType.Directional;
            device.Lights[0].Diffuse = System.Drawing.Color.White;
            device.Lights[0].Direction = new Vector3(0,-1,5);
            device.Lights[0].Enabled = true;
            
            device.Lights[1].Type = D3D.LightType.Directional;
            device.Lights[1].Diffuse = System.Drawing.Color.White;
            device.Lights[1].Direction = new Vector3(0, -1, -5);
            device.Lights[1].Enabled = true;
            
            // this doesn't show any noticeable changes:
            //device.RenderState.Ambient = Color.Gray;
        }

        /// <summary>
        /// Draws the given text to the given area of the screen using the given font.
        /// </summary>
        /// <param name="font">The font to write in.</param>
        /// <param name="textRect">Where to display the text on the screen.</param>
        /// <param name="text">The text to write.</param>
        protected void drawText(D3DFont font, Rectangle textRect, string text)
        {
            font.DrawText(null, text, textRect, D3D.DrawTextFormat.WordBreak | D3D.DrawTextFormat.Center, Color.Black);
        }

        /// <summary>
        /// Creates and draws the terrain and skybox based on input vertices.
        /// </summary>
        /// <param name="vert0">The first vertex to be drawn.</param>
        /// <param name="vert1">The second vertex to be drawn.</param>
        /// <param name="vert2">The third vertex to be drawn.</param>
        /// <param name="vert3">The fourth vertex to be drawn.</param>
        /// <param name="texture">The texture to draw.</param>
        protected void drawTerrain(D3D.CustomVertex.PositionNormalTextured vert0,
            D3D.CustomVertex.PositionNormalTextured vert1, 
            D3D.CustomVertex.PositionNormalTextured vert2,
            D3D.CustomVertex.PositionNormalTextured vert3, 
            D3D.Texture texture)
        {
            device.SetTexture(0, texture);

            D3D.VertexBuffer buffer = new D3D.VertexBuffer(typeof(D3D.CustomVertex.PositionNormalTextured),
                4, device, 0, D3D.CustomVertex.PositionNormalTextured.Format, D3D.Pool.Default);

            D3D.CustomVertex.PositionNormalTextured[] vertices = (D3D.CustomVertex.PositionNormalTextured[])buffer.Lock(0, 0);

            vertices[0] = vert0;
            vertices[1] = vert1;
            vertices[2] = vert2;
            vertices[3] = vert3;

            buffer.Unlock();

            device.VertexFormat = D3D.CustomVertex.PositionNormalTextured.Format;
            device.SetStreamSource(0, buffer, 0);
            device.DrawPrimitives(D3D.PrimitiveType.TriangleStrip, 0, 2);
        }

        #endregion
    }
}
