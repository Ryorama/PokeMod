using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PokeModRed.NPCs
{
	public class Diglett : PokemonNPC
	{	
		public override float id {get{return 50.0f;}}
		
		public override void SetDefaults()
		{
			base.SetDefaults();
			npc.width = 44;
			npc.height = 34;
			Main.npcFrameCount[npc.type] = 6;
            drawOffsetY = 10;
        }

        public override void FindFrame(int frameHeight)
        {

            if (Math.Abs(npc.velocity.X) > 1.0f)
            {
                npc.frameCounter += 1f;
                npc.frame.Y = frameHeight * ((int)(npc.frameCounter / 7) % Main.npcFrameCount[npc.type]);
                if ((int)(npc.frameCounter / 7) % Main.npcFrameCount[npc.type] == 4)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = 0;
                }
            }
            else {
                npc.frameCounter += 1f;
                npc.frame.Y = frameHeight * ((((int)(npc.frameCounter / 14) % Main.npcFrameCount[npc.type]) + 4));
                if (((((int)(npc.frameCounter / 14) % Main.npcFrameCount[npc.type]) + 4)) == 6)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y = frameHeight * ((int)(npc.frameCounter / 14) % Main.npcFrameCount[npc.type] + 4);
                }
            }
        }

        public override float CanSpawn(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.spawnTileY < Main.rockLayer && !Main.dayTime ? 1f * base.CanSpawn(spawnInfo): 0f;
		}
	}
}
