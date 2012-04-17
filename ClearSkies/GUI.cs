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
using ClearSkies;

namespace ClearSkies
{
    /// <summary>
    /// This script handles the GUI
    /// </summary>
    class GUI
    {
        #region Fields

        private int waveNumber;
        private const String WAVE_STRING = "Wave: ";

        private float health = 100;
        private Texture healthBarTexture = ContentLoader.HealthBarTexture;
        private Texture healthTexture = ContentLoader.HealthTexture;
        private const String HEALTH_STRING = "Health: ";
        public Rectangle healthArea;
        public Point healthTexturePoint;
        public Size healthBarSize;

        private List<Enemy> enemies;
        private Texture radarTexture;
        private Model radarEnemy = ContentLoader.RadarEnemy;

        private Device device;

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
        public GUI(Rectangle healthA, Point healthP,
            Device device, int w, int h)
        {
            this.healthArea = healthA;
            this.healthTexturePoint = healthP;
            this.device = device;
            this.width = w;
            this.height = h;
            this.enemies = EnemyManager.ManagedEnemies;
            this.waveNumber = EnemyManager.WaveNumber;

            this.healthBarSize = new Size((int)(this.width * 0.265f),
                (int)(this.height * 0.12));

            font = new D3DFont(device, sysFont);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Draw the GUI to the device.
        /// </summary>
        public void draw()
        {
            this.health = Turret.Health;
            this.enemies = EnemyManager.ManagedEnemies;
            this.waveNumber = EnemyManager.WaveNumber;

            drawText(new Rectangle(20, 10, 100, 20),
                    WAVE_STRING + waveNumber.ToString());

            if (healthBarTexture == null)
            {
                drawText(new Rectangle(this.width - 120, 10, 100, 20),
                    HEALTH_STRING + health.ToString());
            }
            else
            {
                float newScale = Turret.Health * 0.01f;
                drawTexture(healthBarTexture, healthTexturePoint, newScale);
            }
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
        /// Draws the given texture to the given point on the screen.
        /// </summary>
        /// <param name="texture">The texture to draw.</param>
        /// <param name="point">Where to display the texture on the screen.</param>
        /// <param name="scale">The amount to scale the x value of the Sprite.</param>
        public void drawTexture(Texture texture, Point point, float scale)
        {
            Size healthSize = new Size((int)((this.width * 2 * 0.144f) * scale),
                (int)(this.height * 2 * 0.030));
            this.healthBarSize = new Size((int)(this.width * 2 * 0.265f),
                (int)(this.height * 2 * 0.12));

            using (Sprite s = new Sprite(device))
            {
                s.Begin(SpriteFlags.AlphaBlend);
                s.Draw2D(healthTexture, new Rectangle(point, healthSize),
                    healthSize, new Point((int)(this.width * 2 * 0.0172) + point.X, 
                        (int)(this.height * 2 * 0.0368) + point.Y), Color.Red);
                s.Draw2D(texture, Rectangle.Empty, //new Point(0,0), 0, point, Color.Black);
                    healthBarSize, point, Color.Black);
                s.End();
            }
        }

        #endregion
    }
}
