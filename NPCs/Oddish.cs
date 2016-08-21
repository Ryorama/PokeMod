using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PokeModBlue.NPCs
{
	public class Oddish : PokemonNPC
	{	
		//constants unique to derived class
		public override float id {get{return 43.0f;}}
		public override int shoot {get{return mod.ProjectileType("VineWhip");}}
		
		public override void SetDefaults()
		{
			base.SetDefaults();
			npc.width = 38;
			npc.height = 42;
			Main.npcFrameCount[npc.type] = 3;
		}

		public override float CanSpawn(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.spawnTileY < Main.rockLayer && Main.dayTime ? 1f * base.CanSpawn(spawnInfo): 0f;
		}
	}
}
