using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using PokeModBlue.NPCs.Pokemon;

namespace PokeModBlue.Projectiles
{
	public abstract class PokeballProjectile : ModProjectile
	{
		public abstract float rate {get;} //override this with a higher rate for other balls, or a method that checks conditions and returns a value etc.
		
		public override void SetDefaults()
		{
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

        public override bool PreAI()
		{
            if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
			{
				int itemID = Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, 1, 1, mod.ItemType(projectile.name), 1, false, 0, false, false);
				projectile.Kill();
			}
			return true;
		}

		public override bool? CanHitNPC(NPC target)
		{
			//check if hit by a pokemon projectile or a pokemon and save it
			if (target.modNPC != null)
			{
				PokemonNPC pokemonNPC;
				pokemonNPC = target.modNPC as PokemonNPC;
				if (pokemonNPC != null)
				{
					if (target.releaseOwner==255)
					{
						return true;
					}
				}
			}
			return false;
		}
		
		// heals the target for the 1 damage it was forced to do
		// technically heals it before it takes damage, so it shouldn't even kill npcs with 1 hp
		//doesn't work in multiplayer, disabling for now
		/*
		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit)
		{
			target.life+=1;
		}
		*/
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			PokemonNPC pokemonNPC;
			pokemonNPC = target.modNPC as PokemonNPC;
			pokemonNPC.capture = 1;
			pokemonNPC.ballRate = rate;
		}
	}
}