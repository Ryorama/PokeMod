using Terraria;
using Terraria.ModLoader;

namespace PokeModBlue.NPCs.Pokemon {

    public class Charmander : PokemonNPC {

        //constants unique to derived class
        public override float id { get { return 4.0f; } }

        public override int shoot { get { return mod.ProjectileType("Ember"); } }

        public override void SetDefaults() {
            base.SetDefaults();
            npc.width = 42;
            npc.height = 44;
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void CreateDust() {
            Lighting.AddLight((int)(npc.Center.X / 16f), (int)(npc.Center.Y / 16f), 0.6f, 0.6f, 0.6f);
        }

        public override float CanSpawn(NPCSpawnInfo spawnInfo) {
            return 0f;
        }
    }
}