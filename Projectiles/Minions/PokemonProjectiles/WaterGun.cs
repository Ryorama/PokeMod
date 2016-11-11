using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace PokeModBlue.Projectiles.Minions.PokemonProjectiles {

    public class WaterGun : PokemonProjectile {

        public override void SetDefaults() {
            base.SetDefaults();
            projectile.width = 20;
            projectile.height = 26;
            projectile.penetrate = 1;
            projectile.ignoreWater = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
        }

        public override void Behavior() {
            if (projectile.localAI[0] == 0f) {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 20);
                projectile.localAI[0] = 1f;
            }
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 66, 0f, 0f, 100, new Color(0, 0, 255), 1.5f);
            Main.dust[dust].velocity *= 0.1f;
            if (projectile.velocity == Vector2.Zero) {
                Main.dust[dust].velocity.Y -= 1f;
                Main.dust[dust].scale = 1.2f;
            } else {
                Main.dust[dust].velocity += projectile.velocity * 0.2f;
            }
            Main.dust[dust].position.X = projectile.Center.X + 4f + (float)Main.rand.Next(-2, 3);
            Main.dust[dust].position.Y = projectile.Center.Y + (float)Main.rand.Next(-2, 3);
            Main.dust[dust].noGravity = true;
            projectile.rotation = projectile.velocity.ToRotation() - 1.5f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
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