using Terraria;
using Terraria.ModLoader;

namespace PokeModBlue.NPCs.Pokemon {

    public class Arbok : PokemonNPC {
        public override float id { get { return 24.0f; } }

        public override void SetDefaults() {
            base.SetDefaults();
            npc.width = 52;
            npc.height = 64;
            Main.npcFrameCount[npc.type] = 3;
            drawOffsetY = 5;
        }

        public override float CanSpawn(NPCSpawnInfo spawnInfo) {
            return spawnInfo.spawnTileY < Main.rockLayer && Main.dayTime ? 1f * base.CanSpawn(spawnInfo) : 0f;
        }
    }
}