using Terraria;
using Terraria.ModLoader;

namespace PokeModBlue.NPCs.Pokemon {

    public class Venomoth : PokemonNPC {
        public override float id { get { return 49.0f; } }
        public override byte aiMode { get { return flying; } }

        public override void SetDefaults() {
            base.SetDefaults();
            npc.width = 40;
            npc.height = 58;
            Main.npcFrameCount[npc.type] = 3;
        }

        public override float CanSpawn(NPCSpawnInfo spawnInfo) {
            return spawnInfo.spawnTileY < Main.rockLayer && Main.dayTime ? 1f * base.CanSpawn(spawnInfo) : 0f;
        }

        public override void FindFrame(int frameHeight) {
            npc.frameCounter += 1f;
            npc.frame.Y = frameHeight * ((int)(npc.frameCounter / 7) % Main.npcFrameCount[npc.type]);
        }
    }
}