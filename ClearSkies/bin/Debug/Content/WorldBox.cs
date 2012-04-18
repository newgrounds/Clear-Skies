using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;

namespace ClearSkies.Content
{
    class WorldBox
    {
        private Texture top;
        private Texture bottom;
        private Texture left;
        private Texture right;
        private Texture front;
        private Texture back;

        public WorldBox(Texture top, Texture bottom, Texture left, Texture right, Texture front, Texture back)
        {
            this.top = top;
            this.bottom = bottom;
            this.left = left;
            this.right = right;
            this.front = front;
            this.back = back;
        }

        public void draw(Device device)
        {
            drawTerrain(device, bottom,
                new CustomVertex.PositionNormalTextured(new Vector3(-128f, 0, -128f), new Vector3(0, 0, 1), 0, 32),
                new CustomVertex.PositionNormalTextured(new Vector3(-128f, 0, 128f), new Vector3(0, 0, 1), 0, 0),
                new CustomVertex.PositionNormalTextured(new Vector3(128f, 0, -128f), new Vector3(0, 0, 1), 32, 32),
                new CustomVertex.PositionNormalTextured(new Vector3(128f, 0, 128f), new Vector3(0, 0, 1), 32, 0));

            drawTerrain(device, top,
                new CustomVertex.PositionNormalTextured(new Vector3(-256f, 254.5f, -256f), new Vector3(0, 0, -1), 0, 1),
                new CustomVertex.PositionNormalTextured(new Vector3(-256f, 254.5f, 256f), new Vector3(0, 0, -1), 0, 0),
                new CustomVertex.PositionNormalTextured(new Vector3(256f, 254.5f, -256f), new Vector3(0, 0, -1), 1, 1),
                new CustomVertex.PositionNormalTextured(new Vector3(256f, 254.5f, 256f), new Vector3(0, 0, -1), 1, 0));

            drawTerrain(device, left,
                new CustomVertex.PositionNormalTextured(new Vector3(-254.5f, -256f, -256f), new Vector3(0, 0, 1), 0, 1),
                new CustomVertex.PositionNormalTextured(new Vector3(-254.5f, 256f, -256f), new Vector3(0, 0, 1), 0, 0),
                new CustomVertex.PositionNormalTextured(new Vector3(-254.5f, -256f, 256f), new Vector3(0, 0, 1), 1, 1),
                new CustomVertex.PositionNormalTextured(new Vector3(-254.5f, 256f, 256f), new Vector3(0, 0, 1), 1, 0));

            drawTerrain(device, right,
                new CustomVertex.PositionNormalTextured(new Vector3(254.5f, -256f, -256f), new Vector3(0, 0, -1), 0, 1),
                new CustomVertex.PositionNormalTextured(new Vector3(254.5f, 256f, -256f), new Vector3(0, 0, -1), 0, 0),
                new CustomVertex.PositionNormalTextured(new Vector3(254.5f, -256f, 256f), new Vector3(0, 0, -1), 1, 1),
                new CustomVertex.PositionNormalTextured(new Vector3(254.5f, 256f, 256f), new Vector3(0, 0, -1), 1, 0));

            drawTerrain(device, front,
                new CustomVertex.PositionNormalTextured(new Vector3(-256f, -256f, 254.5f), new Vector3(0, 0, 1), 0, 1),
                new CustomVertex.PositionNormalTextured(new Vector3(-256f, 256f, 254.5f), new Vector3(0, 0, 1), 0, 0),
                new CustomVertex.PositionNormalTextured(new Vector3(256f, -256f, 254.5f), new Vector3(0, 0, 1), 1, 1),
                new CustomVertex.PositionNormalTextured(new Vector3(256f, 256f, 254.5f), new Vector3(0, 0, 1), 1, 0));

            drawTerrain(device, back,
                new CustomVertex.PositionNormalTextured(new Vector3(-256f, -256f, -254.5f), new Vector3(0, 0, -1), 0, 1),
                new CustomVertex.PositionNormalTextured(new Vector3(-256f, 256f, -254.5f), new Vector3(0, 0, -1), 0, 0),
                new CustomVertex.PositionNormalTextured(new Vector3(256f, -256f, -254.5f), new Vector3(0, 0, -1), 1, 1),
                new CustomVertex.PositionNormalTextured(new Vector3(256f, 256f, -254.5f), new Vector3(0, 0, -1), 1, 0));
        }

        /// <summary>
        /// Creates and draws the terrain and skybox based on input vertices.
        /// </summary>
        /// <param name="vert0">The first vertex to be drawn.</param>
        /// <param name="vert1">The second vertex to be drawn.</param>
        /// <param name="vert2">The third vertex to be drawn.</param>
        /// <param name="vert3">The fourth vertex to be drawn.</param>
        /// <param name="texture">The texture to draw.</param>
        private void drawTerrain(Device device, Texture texture,
            CustomVertex.PositionNormalTextured vert0,
            CustomVertex.PositionNormalTextured vert1,
            CustomVertex.PositionNormalTextured vert2,
            CustomVertex.PositionNormalTextured vert3)
        {
            device.SetTexture(0, texture);

            VertexBuffer buffer = new VertexBuffer(typeof(CustomVertex.PositionNormalTextured),
                4, device, 0, CustomVertex.PositionNormalTextured.Format, Pool.Default);

            CustomVertex.PositionNormalTextured[] vertices = (CustomVertex.PositionNormalTextured[])buffer.Lock(0, 0);

            vertices[0] = vert0;
            vertices[1] = vert1;
            vertices[2] = vert2;
            vertices[3] = vert3;

            buffer.Unlock();

            device.VertexFormat = CustomVertex.PositionNormalTextured.Format;
            device.SetStreamSource(0, buffer, 0);
            device.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
        }
    }
}
