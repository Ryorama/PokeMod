using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PokeModRed.NPCs
{
	public class Metapod : PokemonNPC
	{	
		public override float id {get{return 11.0f;}}
		
		public override void SetDefaults()
		{
			base.SetDefaults();
			npc.width = 54;
			npc.height = 48;
			Main.npcFrameCount[npc.type] = 3;
		}

		public override float CanSpawn(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.spawnTileY < Main.rockLayer && !Main.dayTime ? 1f * base.CanSpawn(spawnInfo): 0f;
		}				
	}
}