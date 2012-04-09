using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;
using System.IO;

namespace ClearSkies
{
    /// <summary>
    /// A model class, used to make redering models easier
    /// </summary>
    class Model
    {
        #region Fields

        private List<Model> models;
        private Mesh mesh;
        private Material[] materials;
        private Texture[] textures;
        private Device device;
        private bool enableSpecular;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Loads a Model from the given path and takes a reference to the 
        /// device it should be draw on.
        /// </summary>
        /// <param name="path">Path of model.</param>
        /// <param name="device">Device to draw to.</param>
        public Model(string path, Device device)
        {
            ExtendedMaterial[] exMaterials;

            this.models = new List<Model>();
            this.device = device;
            this.mesh = Mesh.FromFile(path, MeshFlags.SystemMemory, device, out exMaterials);
            this.textures = new Texture[exMaterials.Length];
            this.materials = new Material[exMaterials.Length];

            for (int i = 0; i < exMaterials.Length; i++)
            {
                if (!String.IsNullOrEmpty(exMaterials[i].TextureFilename))
                {
                    string texturePath = Path.Combine(Path.GetDirectoryName(path), exMaterials[i].TextureFilename);
                    this.textures[i] = TextureLoader.FromFile(device, texturePath);
                }
                this.materials[i] = exMaterials[i].Material3D;
                this.materials[i].Ambient = materials[i].Diffuse;
            }
        }

        /// <summary>
        /// Creates a model out of a single Mesh. TEMP
        /// </summary>
        /// <param name="mesh">Mesh for the given model.</param>
        /// <param name="materials">Materials to apply to mesh</param>
        /// <param name="textures">Textures to apply to mesh</param>
        /// <param name="device">The device to draw to.</param>
        /// <param name="enableSpecular">True if use specular materials</param>
        public Model(Mesh mesh, Material[] materials, Texture[] textures, Device device, bool enableSpecular)
        {
            this.mesh = mesh;
            this.materials = materials;
            this.textures = textures;
            this.device = device;
            this.enableSpecular = enableSpecular;
            this.models = new List<Model>();
        }


        #endregion

        #region Public Methods

        /// <summary>
        /// Draw the Model to the device.
        /// </summary>
        public void draw()
        {
            device.RenderState.SpecularEnable = enableSpecular;

            for (int i = 0; i < materials.Length; i++)
            {
                if (textures[i] != null)
                {
                    device.SetTexture(0, textures[i]);
                }

                device.Material = materials[i];
                mesh.DrawSubset(i);
            }
        }

        #endregion
    }
}
