using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PokeModBlue.NPCs.Pokemon
{
	public class Venusaur : PokemonNPC
	{
		public override float id {get{return 3.0f;}}
		public override int shoot {get{return mod.ProjectileType("VineWhip");}}
		
		public override void SetDefaults()
		{
			base.SetDefaults();
			npc.width = 56;
			npc.height = 50;
			Main.npcFrameCount[npc.type] = 3;
		}
		
		public override float CanSpawn(NPCSpawnInfo spawnInfo)
		{
			return 0f;
		}
	}
}