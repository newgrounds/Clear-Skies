using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using D3DFont = Microsoft.DirectX.Direct3D.Font;
using ClearSkies.Prefabs.Turrets;

namespace ClearSkies
{
    /// <summary>
    /// This script handles the GUI
    /// </summary>
    class GUI
    {
        #region Fields

        private Turret player;

        private float score = 0;
        private const Texture SCORE_TEXTURE = null;
        private const String SCORE_STRING = "Score: ";
        private Rectangle scoreArea;
        private Point scoreTexturePoint;

        private float health = 100;
        private const Texture HEALTH_TEXTURE = null;
        private const String HEALTH_STRING = "Health: ";
        private Rectangle healthArea;
        private Point healthTexturePoint;

        private Device device;

        private System.Drawing.Font sysFont = 
            new System.Drawing.Font("Arial", 12f, FontStyle.Regular);
        private D3DFont font;

        private int width;
        private int height;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Handles GUI creation and drawing.
        /// </summary>
        /// <param name="p">The player, a Turret.</param>
        /// <param name="scoreA">Area for score on screen.</param>
        /// <param name="scoreP">Point for score on screen.</param>
        /// <param name="healthA">Area for health on screen.</param>
        /// <param name="healthP">Point for health on screen.</param>
        public GUI(Turret p, Rectangle scoreA, Point scoreP,
            Rectangle healthA, Point healthP,
            Device device, int w, int h)
        {
            this.player = p;
            this.scoreArea = scoreA;
            this.scoreTexturePoint = scoreP;
            this.healthArea = healthA;
            this.healthTexturePoint = healthP;
            this.device = device;
            this.width = w;
            this.height = h;

            font = new D3DFont(device, sysFont);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Draw the GUI to the device.
        /// </summary>
        public void draw()
        {
            this.score = player.Score;
            this.health = player.Health;

            if (SCORE_TEXTURE == null)
            {
                drawText(new Rectangle(10, 10, 100, 20),
                    SCORE_STRING + score.ToString());
            }
            else
            {
                drawTexture(SCORE_TEXTURE, scoreTexturePoint);
            }
            if (HEALTH_TEXTURE == null)
            {
                drawText(new Rectangle(this.width - 120, 10, 100, 20),
                    HEALTH_STRING + health.ToString());
            }
            else
            {
                drawTexture(HEALTH_TEXTURE, healthTexturePoint);
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
        public void drawTexture(Texture texture, Point point)
        {
            using (Sprite s = new Sprite(device))
            {
                s.Begin(SpriteFlags.AlphaBlend);
                s.Draw2D(texture, new Point(0, 0), 0f, point, Color.White);
                s.End();
            }
        }

        #endregion
    }
}
