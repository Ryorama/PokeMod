using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PokeModBlue.Projectiles.Minions.PokemonProjectiles
{
	public class Ember : PokemonProjectile
	{
		public override void SetDefaults()
		{
            base.SetDefaults();
            projectile.width = 25;
			projectile.height = 24;
			Main.projFrames[projectile.type] = 5;
			projectile.scale = 1.0f;
			projectile.penetrate = 1;
			projectile.ignoreWater = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
		}

		public override void Behavior()
		{
			Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), 0.9f, 0.9f, 0.9f);
			if (projectile.localAI[0] == 0f)
			{
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 20);
				projectile.localAI[0] = 1f;
			}
			/*
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 66, 0f, 0f, 100, new Color(255, 0, 0), 1.5f);
			Main.dust[dust].velocity *= 0.1f;
			if (projectile.velocity == Vector2.Zero)
			{
				Main.dust[dust].velocity.Y -= 1f;
				Main.dust[dust].scale = 1.2f;
			}
			else
			{
				Main.dust[dust].velocity += projectile.velocity * 0.2f;
			}
			Main.dust[dust].position.X = projectile.Center.X + 4f + (float)Main.rand.Next(-2, 3);
			Main.dust[dust].position.Y = projectile.Center.Y + (float)Main.rand.Next(-2, 3);
			Main.dust[dust].noGravity = true;
			*/
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.penetrate = -1;
			projectile.maxPenetrate = -1;
			projectile.tileCollide = false;
			projectile.position += projectile.velocity;
			projectile.velocity = Vector2.Zero;
			projectile.timeLeft = 180;
			return false;
		}
	}
}
