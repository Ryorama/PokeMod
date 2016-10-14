using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PokeModBlue.NPCs.Pokemon
{
	public class Sandshrew : PokemonNPC
	{	
		public override float id {get{return 27.0f;}}
		
		public override void SetDefaults()
		{
			base.SetDefaults();
			npc.width = 44;
			npc.height = 46;
			Main.npcFrameCount[npc.type] = 3;
		}

		public override float CanSpawn(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.spawnTileY < Main.rockLayer && Main.dayTime ? 1f * base.CanSpawn(spawnInfo): 0f;
		}
	}
}
