using Microsoft.Xna.Framework;
using PokeModBlue.Items.Weapons;
using PokeModBlue.Projectiles.Minions.PokemonProjectiles;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PokeModBlue.NPCs.Pokemon {

    public class PokeGlobalNPC : GlobalNPC {
        private PokemonWeapon lastHit;
        private int combatTextNum;

        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit) {
            //check if hit by a pokemon projectile and save it
            if (projectile.modProjectile != null) {
                PokemonProjectile pokemonProjectile;
                pokemonProjectile = projectile.modProjectile as PokemonProjectile;
                if (pokemonProjectile != null) {
                    lastHit = pokemonProjectile.pokemon;
                }
            }
        }

        public override void OnHitNPC(NPC npc, NPC target, int damage, float knockback, bool crit) {
            //check if hit a pokemon and save it
            if (target.modNPC != null) {
                PokemonNPC pokemonNPC;
                pokemonNPC = target.modNPC as PokemonNPC;
                if (pokemonNPC != null) {
                    if (npc.releaseOwner == 255) {
                        lastHit = pokemonNPC.pokemon;
                    }
                }
            }
        }

        public override void ModifyHitNPC(NPC npc, NPC target, ref int damage, ref float knockback, ref bool crit) {
            // target is actually your Pokemon
            // npc is the npc that is hitting your Pokemon
            // this says let your Pokemon hit first, and if they kill the enemy take no damage
            // will need to update this to check each Pokemon's speed stat when both are Pokemon and letting the winner strike first
            if (target.modNPC != null) {
                PokemonNPC targetPokemon;
                targetPokemon = target.modNPC as PokemonNPC;
                if (targetPokemon != null) {
                    PokemonNPC npcPokemon;
                    npcPokemon = npc.modNPC as PokemonNPC;
                    if (npcPokemon != null) {
                        int direction;
                        if (npc.position.X > target.position.X) {
                            direction = 1;
                        } else {
                            direction = -1;
                        }

                        float effectiveness = PokemonNPC.getTypeEffectiveness(targetPokemon.getTypeI(), npcPokemon.getTypeI(), npcPokemon.getTypeII());
                        int npcdamage = (int)(target.damage * effectiveness);

                        if (effectiveness < 1 && effectiveness > 0) {
                            combatTextNum = CombatText.NewText(new Rectangle((int)npc.position.X + 150 * direction, (int)npc.position.Y, npc.width, npc.height), PokemonNPC.PokemonText, "It's not very effective...", false, false);
                            if (Main.netMode == 2 && combatTextNum != 100) {
                                CombatText combatText = Main.combatText[combatTextNum];
                                NetMessage.SendData(81, -1, -1, combatText.text, (int)combatText.color.PackedValue, combatText.position.X, combatText.position.Y, 0f, 0, 0, 0);
                            }
                        } else if (effectiveness > 1) {
                            combatTextNum = CombatText.NewText(new Rectangle((int)npc.position.X + 150 * direction, (int)npc.position.Y, npc.width, npc.height), PokemonNPC.PokemonText, "It's super effective!", false, false);
                            if (Main.netMode == 2 && combatTextNum != 100) {
                                CombatText combatText = Main.combatText[combatTextNum];
                                NetMessage.SendData(81, -1, -1, combatText.text, (int)combatText.color.PackedValue, combatText.position.X, combatText.position.Y, 0f, 0, 0, 0);
                            }
                        } else if (effectiveness == 0) {
                            combatTextNum = CombatText.NewText(new Rectangle((int)npc.position.X + 150 * direction, (int)npc.position.Y, npc.width, npc.height), PokemonNPC.PokemonText, "It doesn't affect " + npc.name + "...", false, false);
                            if (Main.netMode == 2 && combatTextNum != 100) {
                                CombatText combatText = Main.combatText[combatTextNum];
                                NetMessage.SendData(81, -1, -1, combatText.text, (int)combatText.color.PackedValue, combatText.position.X, combatText.position.Y, 0f, 0, 0, 0);
                            }
                        }
                        npc.StrikeNPC(npcdamage, 1.0f, target.direction);
                    } else {
                        npc.StrikeNPC(target.damage, 1.0f, target.direction);
                    }
                }
            }
        }

        public override bool CheckDead(NPC npc) {
            // if lastHit isn't null then call check if this npc is a pokemon or otherwise, if pokemon give xp based on EXPV otherwise give xp based on the NPC's health in relation to their defense as an indicator for level
            // if it is a Pokemon, use stats, if not, give 1 xp
            if (lastHit != null) {
                if (npc.modNPC != null) {
                    PokemonNPC pokemonNPC;
                    pokemonNPC = npc.modNPC as PokemonNPC;
                    if (pokemonNPC != null) {
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

        public override void SetupShop(int type, Chest shop, ref int nextSlot) {
            if (type == NPCID.Merchant) {
                shop.item[nextSlot].SetDefaults(mod.ItemType("PokeBall"));
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("Potion"));
                nextSlot++;
                if (NPC.downedBoss1) {
                    shop.item[nextSlot].SetDefaults(mod.ItemType("SuperPotion"));
                    nextSlot++;
                }
                if (NPC.downedBoss2) {
                    shop.item[nextSlot].SetDefaults(mod.ItemType("GreatBall"));
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(mod.ItemType("HyperPotion"));
                    nextSlot++;
                }
                if (Main.hardMode) {
                    shop.item[nextSlot].SetDefaults(mod.ItemType("UltraBall"));
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(mod.ItemType("MaxPotion"));
                    nextSlot++;
                }
            }

            if (type == NPCID.Clothier) {
                shop.item[nextSlot].SetDefaults(mod.ItemType("RedCap"));
                nextSlot++;
            }
        }

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo) {
            PokeModBlue.originalSpawnPool = pool;
            List<int> keys = new List<int>(pool.Keys);
            foreach (int key in keys) {
                // All NPCs Spawn
                if (PokeModBlue.pokeSpawns == 1) {
                    pool[key] = PokeModBlue.originalSpawnPool[key];
                    continue;
                }

                // Only Mod NPCs Spawn
                if (PokeModBlue.pokeSpawns == 2) {
                    ModNPC modNPC = NPCLoader.GetNPC(key);
                    if (modNPC != null) {
                        pool[key] = PokeModBlue.originalSpawnPool[key];
                    } else {
                        pool[key] = 0f;
                    }
                    continue;
                }

                // Only Normal NPCs Spawn
                if (PokeModBlue.pokeSpawns == 3) {
                    ModNPC modNPC = NPCLoader.GetNPC(key);
                    if (modNPC != null) {
                        pool[key] = 0f;
                    } else {
                        pool[key] = PokeModBlue.originalSpawnPool[key];
                    }
                    continue;
                }

                // Only Pokemon Spawn
                if (PokeModBlue.pokeSpawns == 4) {
                    PokemonNPC pokemonNPC = NPCLoader.GetNPC(key) as PokemonNPC;
                    if (pokemonNPC != null) {
                        pool[key] = PokeModBlue.originalSpawnPool[key];
                    } else {
                        pool[key] = 0f;
                    }
                    continue;
                }
            }
        }
    }
}