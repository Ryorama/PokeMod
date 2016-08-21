using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PokeModBlue.NPCs
{
	public class Pidgeot : PokemonNPC
	{		
		public override float id {get{return 18.0f;}}
        public override int shoot { get { return mod.ProjectileType("Gust"); } }
        public override byte aiMode { get { return flying; } }

        public override void SetDefaults()
		{
			base.SetDefaults();
			npc.width = 60;
			npc.height = 54;
			Main.npcFrameCount[npc.type] = 3;
		}
		
		public override void FindFrame(int frameHeight)
		{
			npc.frameCounter+=1f;
			npc.frame.Y = frameHeight * ((int)(npc.frameCounter/7) % Main.npcFrameCount[npc.type]);
		}

		public override float CanSpawn(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.spawnTileY < Main.rockLayer && Main.dayTime ? 1f * base.CanSpawn(spawnInfo): 0f;
		}		
	}
}