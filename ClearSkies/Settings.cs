using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;

namespace ClearSkies
{
    class Settings
    {
        public const string TEST_PARTICLE_TEXTURE_PATH = @"Content\Textures\Particles\test_particle.png";
        public const string EXPLOSION_PARTICLE_TEXTURE_PATH = @"Content\Textures\Particles\explosion.png";
        public const string CLOUD_PARTICLE_TEXTURE_PATH = @"Content\Textures\Particles\cloud_particle.png";
        public const string BASIC_TURRET_BARREL_MODEL_PATH = @"Content\Models\Turrets\BasicTurret\basic_turret_barrel.x";
        public const string BASIC_TURRET_BASE_MODEL_PATH = @"Content\Models\Turrets\BasicTurret\basic_turret_base.x";
        public const string BASIC_TURRET_HEAD_MODEL_PATH = @"Content\Models\Turrets\BasicTurret\basic_turret_head.x";
        public const string BASIC_PLANE_MODEL_PATH = @"Content\Models\Planes\BasicPlane\basicPlane.x";
        public const string BASIC_TANK_BODY_MODEL_PATH = @"Content\Models\Tanks\BasicTank\basic_tank_body.x";
        public const string BASIC_TANK_HEAD_MODEL_PATH = @"Content\Models\Tanks\BasicTank\basic_tank_body.x";
        public const string BASIC_TANK_BARREL_MODEL_PATH = @"Content\Models\Tanks\BasicTank\basic_tank_body.x";

        public static Vector3 BASIC_TURRET_HEAD_OFFSET
        {
            get { return new Vector3(0f, 3.5f, 0f); }
        }
        public static Vector3 BASIC_TURRET_BARREL_DEFAULT_ROTATION
        {
            get { return new Vector3(0f, (float)Math.PI / 2f, 0f); }
        }
        public const float BASIC_TURRET_HEAD_ROTATION_SPEED = 1f;
        public const float BASIC_TURRET_BARREL_ROTATION_SPEED = 1;
        public const float BASIC_TURRET_COLLIDER_SIZE = 1f;
        public const float BASIC_TURRET_BARREL_MAX_PITCH = (float)(Math.PI / 2.0f);
        public const float BASIC_TURRET_BARREL_MIN_PITCH = 0f;

        public static Vector3 TEST_TURRET_HEAD_OFFSET
        {
            get { return new Vector3(0f, 1.25f, 0f); }
        }
        public static Vector3 TEST_TURRET_BARREL_DEFAULT_ROTATION
        {
            get { return new Vector3(0f, (float)(Math.PI / 2), 0f); }
        }
        public const float TEST_TURRET_HEAD_ROTATION_SPEED = 1f;
        public const float TEST_TURRET_BARREL_ROTATION_SPEED = 1f;
        public static Vector3 TEST_TURRET_BARREL_ONE_OFFSET
        {
            get { return new Vector3(0.25f, 0.5f, 0.5f); }
        }
        public static Vector3 TEST_TURRET_BARREL_TWO_OFFSET
        {
            get { return new Vector3(-0.25f, 0.5f, 0.5f); }
        }
        public const float TEST_TURRET_COLLIDER_SIZE = 1f;
        public const float TEST_TURRET_BARREL_MAX_PITCH = (float)(Math.PI / 2.0f);
        public const float TEST_TURRET_BARREL_MIN_PITCH = 0;

        public static Vector3 DEFAULT_TANK_SCALE
        {
            get { return new Vector3(1.0f, 1.0f, 1.0f); }
        }
        public const float TANK_COLLIDER_SIZE = 1.0f;
        public const float TANK_COLLIDE_DAMAGE = 4f;
        public const float BASIC_TANK_BARREL_MAX_PITCH = (float)(Math.PI / 2.0f);
        public const float BASIC_TANK_BARREL_MIN_PITCH = 0f;
        public const float BASIC_TANK_HEAD_ROTATION_SPEED = 1;
        public const float BASIC_TANK_BARREL_ROTATION_SPEED = 1;
        public static Vector3 BASIC_TANK_BARREL_DEFAULT_ROTATION
        {
            get { return new Vector3(0f, (float)Math.PI / 2f, 0f); }
        }
        public static Vector3 BASIC_TANK_BARREL_OFFSET
        {
            get { return new Vector3(0f, 0.5f, 0f); }
        }
        public static Vector3 BASIC_TANK_HEAD_OFFSET
        {
            get { return new Vector3(0f, 3.8f, 0f); }
        }

        public static Vector3 DEFAULT_PLANE_HEIGHT
        {
            get { return new Vector3(0f, 10f, 0f); }
        }
        public static Vector3 DEFAULT_PLANE_SCALE
        {
            get { return new Vector3(1.0f, 1.0f, 1.0f); }
        }
        public const float PLANE_COLLIDER_SIZE = 1.0f;
        public const float PLANE_RELOAD_TIME = 5f;

        public const int EXPLOSION_PARTICLES = 80;
        public static Vector3 EXPLOSION_PARTICLE_ACCELERATION
        {
            get { return new Vector3(2f, 2f, 2f); }
        }
        public const float EXPLOSION_PARTICLE_SIZE = 2f;
        public const float EXPLOSION_PARTICLE_GROWTH_RATE = -0.2f;
        public const float EXPLOSION_PARTICLE_LIFESPAN = 1f;

        public static Vector3 SMOKE_PARTICLE_ACCELERATION
        {
            get { return new Vector3(0.5f, 0.5f, 0.5f); }
        }
        public const float SMOKE_PARTICLE_SIZE = 1f;
        public const float SMOKE_PARTICLE_GROWTH_RATE = 1f;
        public const float SMOKE_PARTICLE_LIFESPAN = 5f;
        public const float SMOKE_PARTICLE_PARTICLES_PER_SECOND = 2f;

        public const float BASIC_BULLET_SPEED = 1f;
        public const float BASIC_BULLET_DAMAGE = 1f;
        public const float BASIC_BULLET_LIFESPAN = 5f;

        public const float BOMB_BULLET_SPEED = 2f;
        public const float BOMB_BULLET_DAMAGE = 4f;
        public const float BOMB_BULLET_LIFESPAN = 20f;

        public static Vector3 CAMERA_FOCUS_POINT_OFFSET
        {
            get { return new Vector3(0f, 2f, 0f); }
        }
        public static Vector3 CAMERA_UP_VECTOR
        {
            get { return new Vector3(0f, 1f, 0f); }
        }
        public const float CAMERA_FIELD_OF_VIEW = (float)Math.PI / 2;
        public const float CAMERA_ASPECT_RATIO = 1f;
        public const float CAMERA_NEAR_PLANE = 1f;
        public const float CAMERA_FAR_PLANE = 400f;

        public const float BOMB_DROP_DISTANCE_FROM_TARGET = 1f;
        public const float PLANE_FLY_OVER_OVERRUN_TIME = 5f;


        public const float TEST_TURRET_BARREL_SHOOT_DELAY = 0.2f;
        public const float TEST_TURRET_BARREL_PUSH_SPEED = 1f;
        public const float TEST_TURRET_BARREL_PULL_SPEED = 2f;


        public const float BASIC_TURRET_BARREL_PULL_SPEED = 2f;
        public const float BASIC_TURRET_BARREL_PUSH_SPEED = 1f;
        public const float BASIC_TURRET_BARREL_SHOOT_DELAY = 0.2f;

        public const float BASIC_TANK_BARREL_SHOOT_DISTANCE = 100f;
        public const float BASIC_TANK_BARREL_SHOOT_DELAY = 2f;
        public const float BASIC_TANK_BARREL_PUSH_SPEED = 2f;
        public const float BASIC_TANK_BARREL_PULL_SPEED = 0.2f;
    }
}
