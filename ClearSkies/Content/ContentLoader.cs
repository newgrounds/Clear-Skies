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

        private static Texture healthBarTexture;
        private static Texture healthTexture;

        private static Material defaultMaterial;

        private static Model basicBulletModel;
        private static Model bombBulletModel;

        private static Model testTurretBarrelModel;
        private static Model testTurretBaseModel;
        private static Model testTurretHeadModel;

        private static Model basicTurretBarrelModel;
        private static Model basicTurretBaseModel; 
        private static Model basicTurretHeadModel;

        private static Model basicTankBodyModel;
        private static Model basicTankHeadModel;
        private static Model basicTankBarrelModel;

        private static Model basicPlaneModel;

        private static Texture radarEnemy;
        private static Texture radar;
        private static Texture guiBack;

        private static Texture worldBoxTop;
        private static Texture worldBoxBottom;
        private static Texture worldBoxLeft;
        private static Texture worldBoxRight;
        private static Texture worldBoxFront;
        private static Texture worldBoxBack;

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
            explosionParticleTexture = TextureLoader.FromFile(device, Settings.EXPLOSION_PARTICLE_TEXTURE_PATH,
                0, 0, 1, Usage.None, Format.Unknown, Pool.Managed, Filter.None, Filter.None,
                Color.White.ToArgb());
            cloudParticleTexture = TextureLoader.FromFile(device, Settings.CLOUD_PARTICLE_TEXTURE_PATH,
                0, 0, 1, Usage.None, Format.Unknown, Pool.Managed, Filter.None, Filter.None,
                Color.White.ToArgb());

            // load health bar texture with alpha removed
            healthBarTexture = TextureLoader.FromFile(device, @"Content\Textures\healthBar.png",
                0, 0, 1, Usage.None, Format.Unknown, Pool.Managed, Filter.None, Filter.None,
                Color.White.ToArgb());
            // to fill the bar
            healthTexture = TextureLoader.FromFile(device, @"Content\Textures\health.png");

            defaultMaterial = new Material();
            defaultMaterial.Diffuse = Color.White;
            defaultMaterial.Specular = Color.White;
            defaultMaterial.SpecularSharpness = 15.0f;

            basicBulletModel = new Model(Mesh.Sphere(device, Settings.BASIC_BULLET_SIZE, 3, 3), 
                new Material[] { defaultMaterial }, 
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

            basicTankBodyModel = new Model(Settings.BASIC_TANK_BODY_MODEL_PATH, Vector3.Empty, device);
            basicTankHeadModel = new Model(Settings.BASIC_TANK_HEAD_MODEL_PATH, Vector3.Empty, device);
            basicTankBarrelModel = new Model(Settings.BASIC_TANK_BARREL_MODEL_PATH, new Vector3(0, (float)(Math.PI / 2), 0), device);

            bombBulletModel = new Model(Settings.BOMB_BULLET_MODEL_PATH, Vector3.Empty, device);

            radarEnemy = TextureLoader.FromFile(device, @"Content\Textures\enemy.png",
                0, 0, 1, Usage.None, Format.Unknown, Pool.Managed, Filter.None, Filter.None,
                Color.White.ToArgb());

            radar = TextureLoader.FromFile(device, @"Content\Textures\gui_radar.png",
                0, 0, 1, Usage.None, Format.Unknown, Pool.Managed, Filter.None, Filter.None,
                Color.White.ToArgb());
            guiBack = TextureLoader.FromFile(device, @"Content\Textures\gui_box.png",
                0, 0, 1, Usage.None, Format.Unknown, Pool.Managed, Filter.None, Filter.None,
                Color.White.ToArgb());

            worldBoxTop = TextureLoader.FromFile(device, Settings.WORLD_BOX_TOP_TEXTURE_PATH);
            worldBoxBottom = TextureLoader.FromFile(device, Settings.WORLD_BOX_BOTTOM_TEXTURE_PATH);
            worldBoxLeft = TextureLoader.FromFile(device, Settings.WORLD_BOX_LEFT_TEXTURE_PATH);
            worldBoxRight = TextureLoader.FromFile(device, Settings.WORLD_BOX_RIGHT_TEXTURE_PATH);
            worldBoxFront = TextureLoader.FromFile(device, Settings.WORLD_BOX_FRONT_TEXTURE_PATH);
            worldBoxBack = TextureLoader.FromFile(device, Settings.WORLD_BOX_BACK_TEXTURE_PATH);

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
        public static Texture HealthBarTexture
        {
            get
            {
                checkIfInitialized();
                return healthBarTexture;
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
        public static Texture HealthTexture
        {
            get
            {
                checkIfInitialized();
                return healthTexture;
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
		
        public static Model BasicTankBodyModel
        {
            get
            {
                checkIfInitialized();
                return basicTankBodyModel;
            }
        }
        public static Model BasicTankHeadModel
        {
            get
            {
                checkIfInitialized();
                return basicTankHeadModel;
            }
        }
        public static Model BasicTankBarrelModel
        {
            get
            {
                checkIfInitialized();
                return basicTankBarrelModel;
            }
        }

        public static Texture RadarEnemy
        {
            get
            {
                checkIfInitialized();
                return radarEnemy;
            }
        }

        public static Texture Radar
        {
            get
            {
                checkIfInitialized();
                return radar;
            }
        }
        public static Texture GUIBack
        {
            get
            {
                checkIfInitialized();
                return guiBack;
            }
        }
        public static Texture Terrain
        {
            get
            {
                checkIfInitialized();
                return worldBoxTop;
            }
        }
        public static Texture WorldBoxTop
        {
            get
            {
                checkIfInitialized();
                return worldBoxTop;
            }
        }
        public static Texture WorldBoxBottom
        {
            get
            {
                checkIfInitialized();
                return worldBoxBottom;
            }
        }
        public static Texture WorldBoxLeft
        {
            get
            {
                checkIfInitialized();
                return worldBoxLeft;
            }
        }
        public static Texture WorldBoxRight
        {
            get
            {
                checkIfInitialized();
                return worldBoxRight;
            }
        }
        public static Texture WorldBoxFront
        {
            get
            {
                checkIfInitialized();
                return worldBoxFront;
            }
        }
        public static Texture WorldBoxBack
        {
            get
            {
                checkIfInitialized();
                return worldBoxBack;
            }
        }

        #endregion
    }
}