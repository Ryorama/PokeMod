using Terraria;

namespace PokeModBlue.NPCs.Pokemon {

    public class Magikarp : PokemonNPC {
        public override float id { get { return 129f; } }
        public override byte aiMode { get { return swimming; } }

        public override void SetDefaults() {
            base.SetDefaults();
            npc.width = 20;
            npc.height = 20;
            Main.npcFrameCount[npc.type] = 6;
        }

        public override void FindFrame(int frameHeight) {
            if (npc.wet) {
                npc.frameCounter += 1f;
                npc.frame.Y = frameHeight * ((int)(npc.frameCounter / 7) % Main.npcFrameCount[npc.type]);
                if ((int)(npc.frameCounter / 7) % Main.npcFrameCount[npc.type] == 3) {
                    npc.frameCounter = 0;
                    npc.frame.Y = 0;
                }
            } else {
                npc.frameCounter += 1f;
                npc.frame.Y = frameHeight * ((((int)(npc.frameCounter / 14) % Main.npcFrameCount[npc.type]) + 3));
                if (((((int)(npc.frameCounter / 14) % Main.npcFrameCount[npc.type]) + 3)) == 6) {
                    npc.frameCounter = 0;
                    npc.frame.Y = frameHeight * ((int)(npc.frameCounter / 14) % Main.npcFrameCount[npc.type] + 3);
                }
            }
        }
    }
}