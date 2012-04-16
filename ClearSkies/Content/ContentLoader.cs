using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;
using ClearSkies.Exceptions;
using System.Drawing;
using Microsoft.DirectX;
using ClearSkies.Properties;

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
        private static Texture cloudParticleTexture;

        private static Material defaultMaterial;

        private static Model basicBulletModel;
        private static Model bombBulletModel;

        private static Model testTurretBarrelModel;
        private static Model testTurretBaseModel;
        private static Model testTurretHeadModel;

        private static Model basicTurretBarrelModel;
        private static Model basicTurretBaseModel; 
        private static Model basicTurretHeadModel;

        private static Model tankModel;

        private static Model basicPlaneModel;

        #endregion

        #region Initalizer Methods

        /// <summary>
        /// loads all the textures, materials, models, and other assets for use within game.
        /// </summary>
        /// <param name="device"></param>
        public static void initialize(Device device)
        {
            defaultTexture = null;
            testParticleTexture = TextureLoader.FromFile(device, Settings.TEST_PARTICLE_TEXTURE_PATH);
            explosionParticleTexture = TextureLoader.FromFile(device, Settings.EXPLOSION_PARTICLE_TEXTURE_PATH);
            cloudParticleTexture = TextureLoader.FromFile(device, Settings.CLOUD_PARTICLE_TEXTURE_PATH);

            defaultMaterial = new Material();
            defaultMaterial.Diffuse = Color.White;
            defaultMaterial.Specular = Color.White;
            defaultMaterial.SpecularSharpness = 15.0f;

            basicBulletModel = new Model(Mesh.Sphere(device, 1f, 3, 3), 
                new Material[] { defaultMaterial }, 
                new Texture[] { defaultTexture }, 
                Vector3.Empty,
                device, 
                true);

            Material tracerMaterial = new Material();
            tracerMaterial.Diffuse = Color.Yellow;
            tracerMaterial.Specular = Color.Yellow;
            tracerMaterial.SpecularSharpness = 15.0f;

            bombBulletModel = new Model(Mesh.Sphere(device, 1.5f, 2, 2),
                new Material[] { tracerMaterial },
                new Texture[] { defaultTexture },
                Vector3.Empty,
                device,
                true);

            testTurretBarrelModel = new Model(Mesh.Box(device, 0.1f, 1f, 0.1f), 
                new Material[] { defaultMaterial }, 
                new Texture[] { defaultTexture }, 
                Vector3.Empty,
                device, 
                true);
            testTurretBaseModel = new Model(Mesh.Cylinder(device, 1f, 1f, 2f, 10, 1),
                new Material[] { defaultMaterial },
                new Texture[] { defaultTexture },
                new Vector3(0f, (float)(Math.PI / 2), 0f),
                device,
                true);
            testTurretHeadModel = new Model(Mesh.Box(device, 1f, 0.5f, 1f), 
                new Material[] { defaultMaterial }, 
                new Texture[] { defaultTexture }, 
                Vector3.Empty,
                device, 
                true);

            basicTurretBarrelModel = new Model(Settings.BASIC_TURRET_BARREL_MODEL_PATH, Vector3.Empty, device);
            basicTurretBaseModel = new Model(Settings.BASIC_TURRET_BASE_MODEL_PATH, Vector3.Empty, device);
            basicTurretHeadModel = new Model(Settings.BASIC_TURRET_HEAD_MODEL_PATH, Vector3.Empty, device);

            basicPlaneModel = new Model(Settings.BASIC_PLANE_MODEL_PATH, new Vector3((float)Math.PI, 0f, 0f), device);

			tankModel = new Model(Mesh.Box(device, 0.5f, 0.5f, 0.5f),
                new Material[] { defaultMaterial },
                new Texture[] { defaultTexture },
                Vector3.Empty,
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
                throw new UninitializedException(Resources.Content_Loader_Uninitialized_Exception);
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
        public static Texture CloudParticleTexture
        {
            get
            {
                checkIfInitialized();
                return cloudParticleTexture;
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
        public static Model BombBulletModel
        {
            get
            {
                checkIfInitialized();
                return bombBulletModel;
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