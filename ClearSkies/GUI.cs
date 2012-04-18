using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using D3DFont = Microsoft.DirectX.Direct3D.Font;
using ClearSkies.Prefabs.Turrets;
using ClearSkies.Managers;
using ClearSkies.Prefabs.Enemies;
using ClearSkies.Content;

namespace ClearSkies
{
    /// <summary>
    /// This script handles the GUI
    /// </summary>
    class GUI
    {
        #region Fields

        private const String WAVE_STRING = "Wave: ";

        Size healthSize;
        Point newPoint;

        private float newScale;

        private int offset = 15;

        private float health = 100;
        private Texture healthBarTexture = ContentLoader.HealthBarTexture;
        private Texture healthTexture = ContentLoader.HealthTexture;
        private const String HEALTH_STRING = "Health: ";
        public Rectangle healthArea;
        public Point healthTexturePoint;
        public Size healthBarSize;

        private List<Enemy> enemies;
        private Texture radarEnemyTexture = ContentLoader.RadarEnemy;
        private Point radarPoint;
        private Size radarSize;
        private float radarScaleX = 0.1f;
        private float radarScaleY = 0.2f;

        private Size enemySize = new Size(30, 30);

        private Device device;

        private Turret player;
        private Point playerP;

        private System.Drawing.Font sysFont = 
            new System.Drawing.Font("Arial", 12f, FontStyle.Regular);
        private D3DFont font;

        public int width;
        public int height;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Handles GUI creation and drawing.
        /// </summary>
        /// <param name="p">The player, a Turret.</param>
        /// <param name="healthA">Area for health on screen.</param>
        /// <param name="healthP">Point for health on screen.</param>
        /// <param name="device">The graphics device.</param>
        /// <param name="w">Width of the screen.</param>
        /// <param name="h">Height of the screen.</param>
        public GUI(Turret p, Rectangle healthA, Point healthP,
            Device device, int w, int h)
        {
            this.healthArea = healthA;
            this.healthTexturePoint = healthP;
            this.device = device;
            this.width = w;
            this.height = h;
            this.player = p;

            font = new D3DFont(device, sysFont);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Draw the GUI to the device.
        /// </summary>
        public void draw()
        {
            this.health = player.Health;
            this.enemies = EnemyManager.ManagedEnemies;
            this.radarPoint = new Point(0, (int)(this.height - (this.height * radarScaleY)));
            this.radarSize = new Size((int)(this.width * this.radarScaleX),
                (int)(this.height - (this.height * this.radarScaleY)));

            int waveTextX = (int)(this.width*0.0104);

            drawText(new Rectangle(waveTextX, 10, (int)(this.width * 0.06f), 20),
                WAVE_STRING + EnemyManager.CurrentWave.waveNumber.ToString());
            drawText(new Rectangle(waveTextX, 34, (int)(this.width * 0.06f), 20),
                "Tanks: " + EnemyManager.CurrentWave.tanksDestroyed
                + "/" + EnemyManager.CurrentWave.tanksSpawned);
            drawText(new Rectangle(waveTextX, 58, (int)(this.width * 0.06f), 20),
                "Planes: " + EnemyManager.CurrentWave.planesDestroyed
                + "/" + EnemyManager.CurrentWave.planesSpawned);

            this.newScale = player.Health * 0.01f;
            this.healthSize = new Size((int)((this.width * 0.288f) * newScale),
                (int)(this.height * 0.060));
            this.healthBarSize = new Size((int)(this.width * 0.530f),
                (int)(this.height * 0.24));
            this.newPoint = new Point((int)(this.width * 0.0344) + healthTexturePoint.X,
                (int)(this.height * 0.0736) + healthTexturePoint.Y);

            drawEverything();
        }

        /// <summary>
        /// Draws the given text to the given area of the screen using the given font.
        /// </summary>
        /// <param name="textRect">Where to display the text on the screen.</param>
        /// <param name="text">The text to write.</param>
        public void drawText(Rectangle textRect, string text)
        {
            font.DrawText(null, text, textRect, DrawTextFormat.WordBreak | DrawTextFormat.Center, Color.Black);
        }

        /// <summary>
        /// Draws GUI elements.
        /// </summary>
        public void drawEverything()
        {
            Point playerP = new Point((int)player.Location.X + offset + radarSize.Width,
                (int)player.Location.Z + offset + radarSize.Height);

            Sprite s = new Sprite(device);
            s.Begin(SpriteFlags.AlphaBlend);

            // Draw the Health
            s.Draw2D(healthTexture, new Rectangle(healthTexturePoint, healthSize),
                    healthSize, newPoint, Color.Red);
            s.Draw2D(healthBarTexture, Rectangle.Empty,
                healthBarSize, this.healthTexturePoint, Color.Black);

            // Draw the radar background
            s.Draw2D(ContentLoader.Radar, Rectangle.Empty, new Size(670, 614),
                new Point(radarPoint.X,
                    (int)(radarPoint.Y-(this.height * 2 * 0.0759f))), Color.White);

            // draw the player
            s.Draw2D(this.radarEnemyTexture, Rectangle.Empty,
                this.enemySize, playerP, Color.LightSkyBlue);

            // draw all the enemies
            foreach (Enemy e in enemies)
            {
                Point point = new Point(
                    (int)e.Location.X + radarSize.Width + offset,
                    (int)e.Location.Z + radarSize.Height + offset);
                    
                s.Draw2D(this.radarEnemyTexture, Rectangle.Empty,
                    this.enemySize, point, Color.Red);
            }
            s.End();
        }

        #endregion
    }
}
