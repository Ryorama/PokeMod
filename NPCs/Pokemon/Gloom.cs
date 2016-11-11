using Terraria;
using Terraria.ModLoader;

namespace PokeModBlue.NPCs.Pokemon {

    public class Gloom : PokemonNPC {

        //constants unique to derived class
        public override float id { get { return 44.0f; } }

        public override int shoot { get { return mod.ProjectileType("VineWhip"); } }

        public override void SetDefaults() {
            base.SetDefaults();
            npc.width = 36;
            npc.height = 48;
            Main.npcFrameCount[npc.type] = 3;
        }

        public override float CanSpawn(NPCSpawnInfo spawnInfo) {
            return spawnInfo.spawnTileY < Main.rockLayer && Main.dayTime ? 1f * base.CanSpawn(spawnInfo) : 0f;
        }
    }
}