using Microsoft.Xna.Framework;
using PokeModBlue.Items.Weapons;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PokeModBlue.Projectiles {

    public class PokeballSummon : ModProjectile {
        public PokemonWeapon pokemon;
        public Player player;

        public override void SetDefaults() {
            projectile.width = 24;
            projectile.height = 24;
            projectile.scale = 1.0f;
            projectile.penetrate = 1;
            projectile.friendly = true;
            aiType = ProjectileID.Boulder;
            projectile.aiStyle = 14;
            projectile.ignoreWater = true;
            projectile.timeLeft = 3600;
        }

        public override bool? CanHitNPC(NPC target) {
            return false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            if (player == null || pokemon == null) {
                return false;
            } else {
                pokemon.SummonPokemon(player, projectile.position);
            }
            projectile.Kill();
            pokemon.pokeball = null;
            return false;
        }
    }
}