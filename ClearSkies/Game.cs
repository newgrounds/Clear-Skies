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

        WorldBox world;
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
            Rectangle scrn = Screen.GetBounds(this);
            this.Width = scrn.Width;
            this.Height = scrn.Height;
            //this.Width = 400;
            //this.Height = 400;
            
            //enterPressed = false;
        }

        /// <summary>
        /// Load all initial game logic.
        /// </summary>
        private void InitializeGame()
        {
            this.gameState = GameState.Start;

            player = TurretManager.spawnTurret(TurretType.Basic, Vector3.Empty, Vector3.Empty, new Vector3(1f, 1f, 1f), keyboard);
            this.camera = new ThirdPersonCamera(player, new Vector3(0f, 10f, -15f));
            player.Head.addChild(camera);

            List<Wave> waves = new List<Wave>();
            for (int i = 1; i <= 10; i++)
            {
                Wave wave = new Wave();
                wave.waveNumber = i;
                wave.enemiesPerSpawn = i;
                wave.planeSpeed = 5f + i * 2;
                wave.planeTurnSpeed = (float)(Math.PI / 8);
                wave.tankSpeed = 5f + i;
                wave.tankTurnSpeed = (float)(Math.PI / 8);
                wave.planesToSpawn = (int)(i / 2);
                wave.tanksToSpawn = (int)(i / 2) + (i % 2);
                wave.spawnDelay = 1f + i;
                wave.minimumSpawnDistance = 150f;
                wave.maximumSpawnDistance = 200f;
                waves.Add(wave);
            }

            managers = new List<Manager>();
            BulletManager bulletManager = new BulletManager();
            managers.Add(bulletManager);
            TurretManager turretManager = new TurretManager();
            managers.Add(turretManager);
            EnemyManager enemyManager = new EnemyManager(waves, 0);
            managers.Add(enemyManager);
            ParticleEmitterManager particleEmitterManager = new ParticleEmitterManager(camera);
            managers.Add(particleEmitterManager);

            world = new WorldBox(
                ContentLoader.WorldBoxTop,
                ContentLoader.WorldBoxBottom,
                ContentLoader.WorldBoxLeft,
                ContentLoader.WorldBoxRight,
                ContentLoader.WorldBoxFront,
                ContentLoader.WorldBoxBack);

            // TODO: make the gui redraw based on the window size
            gui = new GUI(player,
                new Rectangle(this.Width - (int)(this.Width * 0.0625),
                (int)(this.Height * 0.0052),
                (int)(this.Width * 0.0521),
                (int)(this.Height * 0.0104)),
                new Point(this.Width - (int)(this.Width * 0.183), 0),
                device, this.Width, this.Height);
        }

        /// <summary>
        /// Initializes the display parameters and creates the game objects.
        /// </summary>
        private void InitializeGraphics()
        {
            D3D.PresentParameters pres = new D3D.PresentParameters();
            pres.Windowed = true;
            pres.SwapEffect = D3D.SwapEffect.Discard;
            pres.EnableAutoDepthStencil = true;
            pres.AutoDepthStencilFormat = D3D.DepthFormat.D16;

            device = new D3D.Device(0, D3D.DeviceType.Hardware, this, D3D.CreateFlags.SoftwareVertexProcessing, pres);
            device.RenderState.CullMode = D3D.Cull.None;

            ContentLoader.initialize(device);
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
            world.draw(device);

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

            device.EndScene();
            device.Present();
        }

        /// <summary>
        /// Sets up the lighting in the game world.
        /// </summary>
        protected void SetupLights()
        {
            device.RenderState.Lighting = true;

            D3D.Material material = device.Material;
            material.Emissive = Color.White;
            device.Material = material;
            
            device.Lights[0].Type = D3D.LightType.Directional;
            device.Lights[0].Diffuse = System.Drawing.Color.White;
            device.Lights[0].Direction = new Vector3(0,-1,5);
            device.Lights[0].Enabled = true;
            
            device.Lights[1].Type = D3D.LightType.Directional;
            device.Lights[1].Diffuse = System.Drawing.Color.White;
            device.Lights[1].Direction = new Vector3(0, -1, -5);
            device.Lights[1].Enabled = true;
            
            // this doesn't show any noticeable changes:
            device.RenderState.Ambient = Color.Gray;
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

        #endregion
    }
}
