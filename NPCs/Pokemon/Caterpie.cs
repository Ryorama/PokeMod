using Terraria;
using Terraria.ModLoader;

namespace PokeModBlue.NPCs.Pokemon {

    public class Caterpie : PokemonNPC {

        //constants unique to derived class
        public override float id { get { return 10.0f; } }

        public override void SetDefaults() {
            base.SetDefaults();
            npc.width = 50;
            npc.height = 34;
            Main.npcFrameCount[npc.type] = 3;
        }

        public override float CanSpawn(NPCSpawnInfo spawnInfo) {
            return spawnInfo.spawnTileY < Main.rockLayer && Main.dayTime ? 1f * base.CanSpawn(spawnInfo) : 0f;
        }
    }
}