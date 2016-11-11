using Terraria;
using Terraria.ModLoader;

namespace PokeModBlue.NPCs.Pokemon {

    public class Bulbasaur : PokemonNPC {

        //constants unique to derived class
        public override float id { get { return 1.0f; } }

        public override int shoot { get { return mod.ProjectileType("VineWhip"); } }

        public override void SetDefaults() {
            base.SetDefaults();
            npc.width = 46;
            npc.height = 44;
            Main.npcFrameCount[npc.type] = 3;
        }

        public override float CanSpawn(NPCSpawnInfo spawnInfo) {
            return 0f;
        }
    }
}