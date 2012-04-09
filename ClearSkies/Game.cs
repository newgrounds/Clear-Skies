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
using Microsoft.DirectX.Direct3D;
using ClearSkies.Prefabs;
using Microsoft.DirectX;
using ClearSkies.Prefabs.Turrets;

namespace ClearSkies
{
    public partial class Game : Form
    {
        #region Fields

        private Prefab playerTurret;

        private Device device;
        private DI.Device keyBoard;
        private DI.Device mouse;

        private ClearSkies.Prefabs.Cameras.ThirdPersonCamera camera;
        private GameState gameState;
        private bool enterPressed; // used to prevent errors when holding enter

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

            Show();

            DateTime lastUpdate = DateTime.Now;

            while (gameState != GameState.Quit)
            {
                TimeSpan deltaTime = DateTime.Now.Subtract(lastUpdate);

                update(deltaTime);
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

            PresentParameters pres = new PresentParameters();
            pres.Windowed = true;
            pres.SwapEffect = SwapEffect.Discard;
            pres.EnableAutoDepthStencil = true;
            pres.AutoDepthStencilFormat = DepthFormat.D16;

            device = new Device(0, DeviceType.Hardware, this, CreateFlags.SoftwareVertexProcessing, pres);
            device.RenderState.CullMode = Cull.None;

            #endregion

            #region Fonts

            //infoWinFont = new WinFont(FontFamily.GenericSerif, 12, FontStyle.Bold);
            //infoD3DFont = new D3DFont(device, infoWinFont);

            #endregion

            #region Game Objects

            Material turretMaterial = new Material();
            turretMaterial.Diffuse = Color.White;
            turretMaterial.Specular = Color.White;
            turretMaterial.SpecularSharpness = 15.0f;

            Mesh turretBarrelMesh = Mesh.Box(device, 0.1f, 1f, 0.1f);
            Model turretBarrel = new Model(turretBarrelMesh, new Material[] { turretMaterial }, new Texture[] { null }, device, true);

            Mesh turretBaseMesh = Mesh.Box(device, 1f, 0.5f, 1f);
            Model turretBase = new Model(turretBaseMesh, new Material[] { turretMaterial}, new Texture[] { null }, device, true);

            playerTurret = new Turret(turretBarrel, turretBase, Vector3.Empty, Vector3.Empty);

            this.camera = new Prefabs.Cameras.ThirdPersonCamera(device, playerTurret, new Vector3(0f, 2f, -5f));

            #endregion
        }

        /// <summary>
        /// Initializes the Keyboard and Mouse.
        /// </summary>
        /// <exception cref="KeyboardNotFoundException">DirectInput could not aquire Keyboard</exception>
        /// <exception cref="MouseNotFoundException">DirectInput could not aquire Mouse</exception>
        private void InitializeInputDevices()
        {
            keyBoard = new DI.Device(DI.SystemGuid.Keyboard);
            if (keyBoard == null) throw new KeyboardNotFoundException("No keyboard found.");

            mouse = new DI.Device(DI.SystemGuid.Mouse);
            if (mouse == null) throw new MouseNotFoundException("No mouse found.");

            keyBoard.SetCooperativeLevel(this, DI.CooperativeLevelFlags.Background | DI.CooperativeLevelFlags.NonExclusive);
            keyBoard.Acquire();

            mouse.SetCooperativeLevel(this, DI.CooperativeLevelFlags.NonExclusive | DI.CooperativeLevelFlags.Background);
            mouse.Acquire();
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
        /// <param name="deltaTime">The amount of miliseconds since last update.</param>
        private void update(TimeSpan deltaTime)
        {
            DI.KeyboardState keys = keyBoard.GetCurrentKeyboardState();

            switch (gameState)
            {
                case GameState.Lose:
                    break;
                case GameState.Pause:
                    break;
                case GameState.Play:
                    float timeInSeconds = deltaTime.Milliseconds / 1000f;
                    // update stuff
                    break;
                case GameState.Quit:
                    break;
                case GameState.Start:
                    break;
                case GameState.Win:
                    break;
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
            device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.BlueViolet, 1f, 0);
            device.BeginScene();

            SetupLights();

            camera.view();

            switch (gameState)
            {
                case GameState.Lose:
                case GameState.Pause:
                case GameState.Play:
                case GameState.Quit:
                case GameState.Start:
                case GameState.Win:
                    playerTurret.draw(device);
                    break;
            }

            device.EndScene();
            device.Present();
        }

        /// <summary>
        /// Sets up the lighting in the game world.
        /// </summary>
        protected void SetupLights()
        {
            device.RenderState.Lighting = true;

            device.Lights[0].Type = LightType.Directional;
            device.Lights[0].Diffuse = Color.White;
            device.Lights[0].Direction = new Vector3(0, -1, 0);
            device.Lights[0].Update();
            device.Lights[0].Enabled = true;

            device.RenderState.Ambient = Color.White;
            
        }

        /// <summary>
        /// Draws the given text to the center of the screen using the given font.
        /// </summary>
        /// <param name="font">The font to write in.</param>
        /// <param name="text">The text to write.</param>
        protected void drawText(D3DFont font, string text)
        {
            Rectangle textRectangle = new Rectangle(this.Width / 6, this.Height / 6, this.Width * 2 / 3, this.Height * 2 / 3);
            font.DrawText(null, text, textRectangle, DrawTextFormat.WordBreak | DrawTextFormat.Center, Color.Black);
        }

        #endregion
    }
}
