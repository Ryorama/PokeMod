using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PokeModRed.NPCs
{
	public class Weedle : PokemonNPC
	{	
		public override float id {get{return 13.0f;} }

        public override void SetDefaults()
		{
			base.SetDefaults();
			npc.width = 44;
			npc.height = 42;
			Main.npcFrameCount[npc.type] = 3;
		}

		public override float CanSpawn(NPCSpawnInfo spawnInfo)
		{
            return spawnInfo.spawnTileY < Main.rockLayer && Main.dayTime ? 1f * base.CanSpawn(spawnInfo) : 0f;
		}
	}
}
