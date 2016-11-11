using Terraria;

namespace PokeModBlue.NPCs.Pokemon {

    public class Snorlax : PokemonNPC {
        public override float id { get { return 143f; } }

        public override void SetDefaults() {
            base.SetDefaults();
            npc.width = 20;
            npc.height = 20;
            Main.npcFrameCount[npc.type] = 3;
        }
    }
}