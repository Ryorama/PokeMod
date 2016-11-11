using PokeModBlue.Items.Weapons;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PokeModBlue.NPCs {

    public class PokeNurse : ModNPC {

        public override bool Autoload(ref string name, ref string texture, ref string[] altTextures) {
            name = "PokeNurse";
            altTextures = new string[] { "PokeModBlue/NPCs/PokeNurse_Alt_1" };
            return mod.Properties.Autoload;
        }

        public override void SetDefaults() {
            npc.CloneDefaults(NPCID.Nurse);
            npc.name = "PokeNurse";
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7;
            npc.damage = 10;
            npc.defense = 15;
            npc.lifeMax = 250;
            npc.soundHit = 1;
            npc.soundKilled = 1;
            npc.knockBackResist = 0.5f;
            Main.npcFrameCount[npc.type] = 25;
            NPCID.Sets.ExtraFramesCount[npc.type] = 9;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 90;
            NPCID.Sets.AttackAverageChance[npc.type] = 30;
            NPCID.Sets.HatOffsetY[npc.type] = 4;
            NPCID.Sets.ExtraTextureCount[npc.type] = 1;
            animationType = NPCID.Nurse;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money) {
            return true;
        }

        public override string TownNPCName() {
            return "Joy";
        }

        public override string GetChat() {
            switch (Main.rand.Next(3)) {
                case 0:
                    return "Good morning! Welcome to the Pokémon Center.";

                case 1:
                    return "We restore your tired Pokémon to full health.";

                default:
                    return "Would you like to rest your Pokémon?";
            }
        }

        public override void SetChatButtons(ref string button, ref string button2) {
            button = Lang.inter[54];
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop) {
            if (firstButton) {
                Main.NewText("OK, I'll take your Pokémon for a few seconds.");
                Main.NewText("...");
                Main.NewText("Thank you for waiting.");
                Main.NewText("We've restored your Pokémon to full health.");
                Main.NewText("We hope to see you again!");

                for (int i = 0; i < Main.player[Main.myPlayer].inventory.Length; i++) {
                    PokemonWeapon pokemonWeapon = Main.player[Main.myPlayer].inventory[i].modItem as PokemonWeapon;
                    if (pokemonWeapon != null) {
                        pokemonWeapon.currentHP = pokemonWeapon.maxHP;
                        pokemonWeapon.SetToolTip();
                        if (pokemonWeapon.npc != null) {
                            pokemonWeapon.npc.life = pokemonWeapon.maxHP;
                            pokemonWeapon.npc.HealEffect(pokemonWeapon.maxHP);
                        }
                    }
                }
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback) {
            damage = 20;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown) {
            cooldown = 30;
            randExtraCooldown = 30;
        }

        /*
		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
			projType = mod.ProjectileType("SparklingBall");
			attackDelay = 1;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
			multiplier = 12f;
			randomOffset = 2f;
		}
        */
    }
}