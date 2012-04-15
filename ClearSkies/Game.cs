﻿using System;
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
        int score;

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

            player = TurretManager.spawnTurret(TurretType.Test, Vector3.Empty, Vector3.Empty, new Vector3(1f, 1f, 1f), keyboard);

            managers = new List<Manager>();
            BulletManager bulletManager = new BulletManager(player);
            managers.Add(bulletManager);
            TurretManager turretManager = new TurretManager();
            managers.Add(turretManager);
            // the enemy manager also adds enemies
            EnemyManager enemyManager = new EnemyManager(player);
            managers.Add(enemyManager);

            gui = new GUI(player, new Rectangle(10, 10, 100, 20), new Point(0, 0),
                new Rectangle(this.Width - 120, 10, 100, 20), new Point(0, 0), 
                device, this.Width, this.Height);

            this.camera = new ThirdPersonCamera(player, new Vector3(0f, 3f, -3f));
            player.Head.addChild(camera);

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
            health = player.Health;
            score = player.Score;

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
            /*
            // BEGIN SHADER SNIPPET

            // Load the effect from file.
            D3D.Effect effect = D3D.Effect.FromFile(device, "../../scene_toonEdges.fx/HLSL/scene_toonEdges.fx", null,
                                            null, D3D.ShaderFlags.Debug, null);
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
            /*
            device.Lights[1].Type = D3D.LightType.Directional;
            device.Lights[1].Diffuse = System.Drawing.Color.White;
            device.Lights[1].Direction = new Vector3(0, -3, -5);
            device.Lights[1].Enabled = true;
            */
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

        #endregion
    }
}
