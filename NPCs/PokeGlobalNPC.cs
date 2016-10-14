using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using PokeModBlue.Projectiles.Minions;
using PokeModBlue.Projectiles.Minions.PokemonProjectiles;
using PokeModBlue.Items.Weapons;

namespace PokeModBlue.NPCs.Pokemon
{
    public class PokeGlobalNPC : GlobalNPC
    {
		PokemonWeapon lastHit;
		
        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
		{
			//check if hit by a pokemon projectile and save it
			if (projectile.modProjectile != null)
			{
				PokemonProjectile pokemonProjectile;
				pokemonProjectile = projectile.modProjectile as PokemonProjectile;
				if (pokemonProjectile != null)
				{
					lastHit = pokemonProjectile.pokemon;
				}
			}
		}
		
		public override void OnHitNPC(NPC npc, NPC target, int damage, float knockback, bool crit)
		{
			//check if hit a pokemon and save it
			if (target.modNPC != null)
			{
				PokemonNPC pokemonNPC;
				pokemonNPC = target.modNPC as PokemonNPC;
				if (pokemonNPC != null)
				{
					if (npc.releaseOwner == 255)
					{
						lastHit = pokemonNPC.pokemon;
					}
				}
			}
		}
		
		public override void ModifyHitNPC(NPC npc, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
			// target is actually your Pokemon
			// npc is the npc that is hitting your Pokemon
			// this says let your Pokemon hit first, and if they kill the enemy take no damage
			// will need to update this to check each Pokemon's speed stat when both are Pokemon and letting the winner strike first
			if (target.modNPC != null)
			{
				PokemonNPC pokemonNPC;
				pokemonNPC = target.modNPC as PokemonNPC;
				if (pokemonNPC != null)
				{
                    npc.StrikeNPC(target.damage, 1.0f, target.direction);
                    if (npc.life <= 0)
					{
						damage = 0; // at most will reduce the damage to 1, since it will not ever deal 0 damage
					}
				}
			}
		}
		
		public override bool CheckDead(NPC npc)
		{
			// if lastHit isn't null then call check if this npc is a pokemon or otherwise, if pokemon give xp based on EXPV otherwise give xp based on the NPC's health in relation to their defense as an indicator for level
			// if it is a Pokemon, use stats, if not, give 1 xp
			if (lastHit != null)
			{
				if (npc.modNPC != null)
				{
					PokemonNPC pokemonNPC;
					pokemonNPC = npc.modNPC as PokemonNPC;
					if (pokemonNPC != null)
					{
						lastHit.AddExperience(pokemonNPC);
						return pokemonNPC.CheckDead();
					}
					lastHit.AddExperience(npc.lifeMax, npc.defense);
					return npc.modNPC.CheckDead();
				}
				lastHit.AddExperience(npc.lifeMax, npc.defense);
				return base.CheckDead(npc);
			}
			return base.CheckDead(npc);
		}
		
		public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if(type == NPCID.Merchant)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("PokeBall"));
                nextSlot++;
				if (NPC.downedBoss2)
				{
					shop.item[nextSlot].SetDefaults(mod.ItemType("GreatBall"));
					nextSlot++;
				}
				if (Main.hardMode)
				{
					shop.item[nextSlot].SetDefaults(mod.ItemType("UltraBall"));
					nextSlot++;
				}
            }
			
			if(type == NPCID.Clothier)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("RedCap"));
                nextSlot++;
            }
        }

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            PokeModBlue.originalSpawnPool = pool;
            
            List<int> keys = new List<int>(pool.Keys);
            foreach (int key in keys)
            {
                ModNPC modNPC = NPCLoader.GetNPC(key);
                if (modNPC == null) // if the ModNPC returned is empty it is a vanilla NPC
                {
                    if (PokeModBlue.pokeSpawns == 2)
                    {
                        //edit the value of pool at [key]
                        pool[key] = 0f;
                    }
                    else if (PokeModBlue.pokeSpawns == 3 || PokeModBlue.pokeSpawns == 1)
                    {
                        //edit the value of pool at [key]
                        pool[key] = PokeModBlue.originalSpawnPool[key];
                    }
                }
            }
        }
    }
}