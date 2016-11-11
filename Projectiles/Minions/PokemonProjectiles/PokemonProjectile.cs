using PokeModBlue.Items.Weapons;
using Terraria;
using Terraria.ModLoader;

namespace PokeModBlue.Projectiles.Minions.PokemonProjectiles {

    public abstract class PokemonProjectile : ModProjectile {
        public PokemonWeapon pokemon;

        public override void SetDefaults() {
        }

        public void SelectFrame() {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 8) {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % Main.projFrames[projectile.type];
            }
        }

        public override void AI() {
            SelectFrame();
            Behavior();
        }

        public abstract void Behavior();
    }
}