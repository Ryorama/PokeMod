using Terraria;

namespace PokeModBlue.NPCs.Pokemon {

    public class Geodude : PokemonNPC {
        public override float id { get { return 74f; } }

        public override void SetDefaults() {
            base.SetDefaults();
            npc.width = 28;
            npc.height = 28;
            Main.npcFrameCount[npc.type] = 3;
        }
    }
}