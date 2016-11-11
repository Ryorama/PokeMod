using System;
using Terraria;
using Terraria.ModLoader;

namespace PokeModBlue.NPCs.Pokemon {

    public class Dugtrio : PokemonNPC {
        public override float id { get { return 51.0f; } }

        public override void SetDefaults() {
            base.SetDefaults();
            npc.width = 64;
            npc.height = 54;
            Main.npcFrameCount[npc.type] = 6;
            drawOffsetY = 10;
        }

        public override void FindFrame(int frameHeight) {
            if (Math.Abs(npc.velocity.X) > 1.0f) {
                npc.frameCounter += 1f;
                npc.frame.Y = frameHeight * ((int)(npc.frameCounter / 7) % Main.npcFrameCount[npc.type]);
                if ((int)(npc.frameCounter / 7) % Main.npcFrameCount[npc.type] == 4) {
                    npc.frameCounter = 0;
                    npc.frame.Y = 0;
                }
            } else {
                npc.frameCounter += 1f;
                npc.frame.Y = frameHeight * ((((int)(npc.frameCounter / 14) % Main.npcFrameCount[npc.type]) + 4));
                if (((((int)(npc.frameCounter / 14) % Main.npcFrameCount[npc.type]) + 4)) == 6) {
                    npc.frameCounter = 0;
                    npc.frame.Y = frameHeight * ((int)(npc.frameCounter / 14) % Main.npcFrameCount[npc.type] + 4);
                }
            }
        }

        public override float CanSpawn(NPCSpawnInfo spawnInfo) {
            return spawnInfo.spawnTileY < Main.rockLayer && !Main.dayTime ? 1f * base.CanSpawn(spawnInfo) : 0f;
        }
    }
}