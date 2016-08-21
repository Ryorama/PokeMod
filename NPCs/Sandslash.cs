using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PokeModBlue.NPCs
{
	public class Sandslash : PokemonNPC
	{	
		public override float id {get{return 28.0f;}}
		
		public override void SetDefaults()
		{
			base.SetDefaults();
			npc.width = 50;
			npc.height = 50;
			Main.npcFrameCount[npc.type] = 3;
		}

		public override float CanSpawn(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.spawnTileY < Main.rockLayer && Main.dayTime ? 1f * base.CanSpawn(spawnInfo): 0f;
		}
	}
}
