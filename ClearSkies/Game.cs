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

        private static int score = 0;

        private D3D.Device device;
        private DI.Device keyboard;
        private DI.Device mouse;

        Font systemFont = new Font("Arial", 12f, FontStyle.Regular);
        D3DFont theFont;

        private ClearSkies.Prefabs.Cameras.ThirdPersonCamera camera;
        private GameState gameState;
        private bool enterPressed; // used to prevent errors when holding enter

        private List<Manager> managers;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates a form for the Chase game
        /// </summary>
        public Game()
        {
            InitializeWindow();
            InitializeInputDevices();
            InitializeGraphics();
            InitializeGame();

            theFont = new D3DFont(device, systemFont);

            Show();

            DateTime lastUpdate = DateTime.Now;

            while (gameState != GameState.Quit)
            {
                TimeSpan deltaTime = DateTime.Now.Subtract(lastUpdate);

                update(deltaTime.Milliseconds / 1000f);
                lastUpdate = DateTime.Now;

                draw();
            }

            DisposeGraphics();
            Application.Exit();
        }

        /// <summary>
        /// Initialize the game windows size parameters.
        /// </summary>
        private void InitializeWindow()
        {
            this.Width = 400;
            this.Height = 400;
            enterPressed = false;
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

            #region Fonts

            //infoWinFont = new WinFont(FontFamily.GenericSerif, 12, FontStyle.Bold);
            //infoD3DFont = new D3DFont(device, infoWinFont);

            #endregion

            #region Game Objects

            ContentLoader.initialize(device);

            managers = new List<Manager>();
            BulletManager bulletManager = new BulletManager();
            managers.Add(bulletManager);
            TurretManager turretManager = new TurretManager();
            managers.Add(turretManager);
            // the enemy manager also adds enemies
            EnemyManager enemyManager = new EnemyManager();
            managers.Add(enemyManager);

            Turret player = TurretManager.spawnTurret(TurretType.Test, Vector3.Empty, Vector3.Empty, keyboard);

            this.camera = new ThirdPersonCamera(player, new Vector3(0f, 1f, -3f));
            
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

        #region Getter and Setter Methods

        /// <summary>
        /// The current score of the game
        /// </summary>
        public static int Score
        {
            get { return score; }
            set { score = value; }
        }

        #endregion

        #region DisposalMethods

        /// <summary>
        /// Disposes of the graphics device.
        /// </summary>
        private void DisposeGraphics()
        {
            device.Dispose();
        }

        #endregion

        #region Update Methods

        /// <summary>
        /// Updates all the game objects and listens for user input from mouse and keyboard.
        /// </summary>
        /// <param name="deltaTime">Time in seconds since last update.</param>
        private void update(float deltaTime)
        {
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
        private void draw()
        {
            device.Clear(D3D.ClearFlags.Target | D3D.ClearFlags.ZBuffer, Color.BlueViolet, 1f, 0);
            device.BeginScene();

            SetupLights();
            camera.view(device);

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

            drawText(theFont, new Rectangle(10, 10, 100, 20), "Score: " + score.ToString());

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
            device.Lights[0].Diffuse = Color.White;
            device.Lights[0].Direction = new Vector3(0, -1, 0);
            device.Lights[0].Update();
            device.Lights[0].Enabled = true;

            device.RenderState.Ambient = Color.White;
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
