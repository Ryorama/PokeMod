using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PokeModBlue.NPCs.Pokemon
{
	public class Ninetales : PokemonNPC
	{	
		//constants unique to derived class
		public override float id {get{return 38.0f;}}
		public override int shoot {get{return mod.ProjectileType("Ember");}}
		
		public override void SetDefaults()
		{
			base.SetDefaults();
			npc.width = 52;
			npc.height = 42;
			Main.npcFrameCount[npc.type] = 3;
		}
		
		public override float CanSpawn(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.spawnTileY < Main.rockLayer && Main.dayTime ? 1f * base.CanSpawn(spawnInfo): 0f;
		}
	}
}
