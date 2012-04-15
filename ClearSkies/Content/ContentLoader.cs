using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;
using ClearSkies.Exceptions;
using System.Drawing;

namespace ClearSkies.Content
{
    /// <summary>
    /// Manages the loading process for all declared game content.
    /// </summary>
    class ContentLoader
    {
        #region Fields

        private static bool initialized;

        private static Texture defaultTexture;
        private static Texture testParticleTexture;
		private static Texture explosionParticleTexture;

        private static Material defaultMaterial;

        private static Model basicBulletModel;
        private static Model tracerBulletModel;

        private static Model testTurretBarrelModel;
        private static Model testTurretBaseModel;
        private static Model testTurretHeadModel;

        private static Model basicTurretBarrelModel;
        private static Model basicTurretBaseModel; 
        private static Model basicTurretHeadModel;

        private static Model basicPlaneModel;

        private static Model tankModel;

        #endregion

        #region Initalizer Methods

        /// <summary>
        /// loads all the textures, materials, models, and other assets for use within game.
        /// </summary>
        /// <param name="device"></param>
        public static void initialize(Device device)
        {
            defaultTexture = null;
            testParticleTexture = TextureLoader.FromFile(device, @"Content\Textures\Particles\test_particle.png");
            explosionParticleTexture = TextureLoader.FromFile(device, @"Content\Textures\Particles\explosion.png");

            defaultMaterial = new Material();
            defaultMaterial.Diffuse = Color.White;
            defaultMaterial.Specular = Color.White;
            defaultMaterial.SpecularSharpness = 15.0f;

            basicBulletModel = new Model(Mesh.Sphere(device, 0.1f, 3, 3), 
                new Material[] { defaultMaterial }, 
                new Texture[] { defaultTexture }, 
                device, 
                true);

            Material tracerMaterial = new Material();
            tracerMaterial.Diffuse = Color.Yellow;
            tracerMaterial.Specular = Color.Yellow;
            tracerMaterial.SpecularSharpness = 15.0f;

            tracerBulletModel = new Model(Mesh.Sphere(device, 0.3f, 3, 3),
                new Material[] { tracerMaterial },
                new Texture[] { defaultTexture },
                device,
                true);

            testTurretBarrelModel = new Model(Mesh.Box(device, 0.1f, 1f, 0.1f), 
                new Material[] { defaultMaterial }, 
                new Texture[] { defaultTexture }, 
                device, 
                true);
            testTurretBaseModel = new Model(Mesh.Cylinder(device, 1f, 1f, 2f, 10, 1),
                new Material[] { defaultMaterial },
                new Texture[] { defaultTexture },
                device,
                true);
            testTurretHeadModel = new Model(Mesh.Box(device, 1f, 0.5f, 1f), 
                new Material[] { defaultMaterial }, 
                new Texture[] { defaultTexture }, 
                device, 
                true);

            basicTurretBarrelModel = new Model(@"Content\Models\BasicTurret\basic_turret_barrel.x", device);
            basicTurretBaseModel = new Model(@"Content\Models\BasicTurret\basic_turret_base.x", device);
            basicTurretHeadModel = new Model(@"Content\Models\BasicTurret\basic_turret_head.x", device);

            basicPlaneModel = new Model(@"Content\Models\BasicPlane\basicPlane.x", device);

            tankModel = new Model(Mesh.Box(device, 0.5f, 0.5f, 0.5f),
                new Material[] { defaultMaterial },
                new Texture[] { defaultTexture },
                device,
                true);

            initialized = true;
        }

        /// <summary>
        /// Checks to see if the content loader was initialized. This should be called 
        /// before every getter method.
        /// </summary>
        private static void checkIfInitialized()
        {
            if (!initialized)
            {
                throw new ContentLoaderUninitializedException("ContentLoader was not initialized properly before use. "
                    + "Please call initialize method before using ContentLoader");
            }
        }

        #endregion

        #region Getter Methods

        public static Texture DefaultTexture
        {
            get
            {
                checkIfInitialized();
                return defaultTexture;
            }
        }
        public static Texture TestParticleTexture
        {
            get
            {
                checkIfInitialized();
                return testParticleTexture;
            }
        }
		public static Texture ExplosionParticleTexture
        {
            get 
            {
                checkIfInitialized();
                return explosionParticleTexture; 
            }
        }

        public static Material DefaultMaterial
        {
            get 
            {
                checkIfInitialized();
                return defaultMaterial; 
            }
        }

        public static Model BasicBulletModel
        {
            get
            {
                checkIfInitialized();
                return basicBulletModel;
            }
        }
        public static Model TracerBulletModel
        {
            get
            {
                checkIfInitialized();
                return tracerBulletModel;
            }
        }

        public static Model TestTurretBarrelModel
        {
            get
            {
                checkIfInitialized();
                return testTurretBarrelModel;
            }
        }
        public static Model TestTurretBaseModel
        {
            get
            {
                checkIfInitialized();
                return testTurretBaseModel;
            }
        }
        public static Model TestTurretHeadModel
        {
            get
            {
                checkIfInitialized();
                return testTurretHeadModel;
            }
        }

        public static Model BasicTurretBarrelModel
        {
            get
            {
                checkIfInitialized();
                return basicTurretBarrelModel;
            }
        }
        public static Model BasicTurretBaseModel
        {
            get
            {
                checkIfInitialized();
                return basicTurretBaseModel;
            }
        }
        public static Model BasicTurretHeadModel
        {
            get
            {
                checkIfInitialized();
                return basicTurretHeadModel;
            }
        }

        public static Model BasicPlaneModel
        {
            get 
            { 
                checkIfInitialized();
                return basicPlaneModel;  
            }
        }
		
        public static Model TankModel
        {
            get
            {
                checkIfInitialized();
                return tankModel;
            }
        }

        #endregion
    }
}