using Microsoft.Xna.Framework;
using PokeModBlue.NPCs.Pokemon;
using System.Threading.Tasks;
using System.Timers;
using Terraria;
using Terraria.ID;

namespace PokeModBlue.NPCs.Trainers {

    public class Brock : Trainer {
        private int combatTextNum;
        private PokemonNPC summonPokemon;
        public static Color PokemonText = new Color(255, 255, 255, 255);
        private bool pokemonOneDefeated = false;
        private bool pokemonTwoDefeated = false;
        private int loop = 0;
        private Timer timer;
        private int chat = 0;
        private string button = "Next";

        public override bool Autoload(ref string name, ref string texture, ref string[] altTextures) {
            name = "Pewter Gym Leader";
            altTextures = new string[] { "PokeModBlue/NPCs/Trainers/Brock_Alt_1" };
            return mod.Properties.Autoload;
        }

        public override void SetDefaults() {
            //npc.CloneDefaults(NPCID.Guide);
            npc.name = "Pewter Gym Leader";
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
            Main.npcFrameCount[npc.type] = 26;
            NPCID.Sets.ExtraFramesCount[npc.type] = 9;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 90;
            NPCID.Sets.AttackAverageChance[npc.type] = 30;
            NPCID.Sets.HatOffsetY[npc.type] = 4;
            NPCID.Sets.ExtraTextureCount[npc.type] = 1;
            animationType = NPCID.Guide;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money) {
            return true;
        }

        public override string TownNPCName() {
            return "Brock";
        }

        public override string GetChat() {
            switch (chat) {
                case 0:
                    chat++;;
                    return "I'm BROCK! I'm PEWTER's GYM LEADER! I believe in rock hard defense and determination! That's why my POKEMON are all the rock-type!";
                case 1:
                    chat++;
                    return "Do you still want to challenge me?";
                default:
                    chat++;
                    return "Fine then! Show me your best!";
            }
        }

        public override void SetChatButtons(ref string button, ref string button2) {
            button = "Next";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop) {
            if (firstButton && chat >= 2) {
                if (pokemonOneDefeated == false) {
                    combatTextNum = CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), PokemonText, "Ready?", false, false);
                    if (Main.netMode == 2 && combatTextNum != 100) {
                        CombatText combatText = Main.combatText[combatTextNum];
                        NetMessage.SendData(81, -1, -1, combatText.text, (int)combatText.color.PackedValue, combatText.position.X, combatText.position.Y, 0f, 0, 0, 0);
                    }
                }
                loop = 0;
                timer = new Timer(1000);
                timer.Elapsed += (o, s) => Task.Factory.StartNew(() => TimerEventProcessor(o, s));
                timer.AutoReset = true;
                timer.Start();
                chat = 0;
            } else if (firstButton) {
                npc.GetChat();
            }

        }

        private void TimerEventProcessor(object sender, ElapsedEventArgs e) {
            if (loop == 0) {
                combatTextNum = CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), PokemonText, "Set?", false, false);
                if (Main.netMode == 2 && combatTextNum != 100) {
                    CombatText combatText = Main.combatText[combatTextNum];
                    NetMessage.SendData(81, -1, -1, combatText.text, (int)combatText.color.PackedValue, combatText.position.X, combatText.position.Y, 0f, 0, 0, 0);
                }
            } else if (loop == 1) {
                combatTextNum = CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), PokemonText, "Go!", false, false);
                if (Main.netMode == 2 && combatTextNum != 100) {
                    CombatText combatText = Main.combatText[combatTextNum];
                    NetMessage.SendData(81, -1, -1, combatText.text, (int)combatText.color.PackedValue, combatText.position.X, combatText.position.Y, 0f, 0, 0, 0);
                }
            } else if (loop == 2) {
                combatTextNum = CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), PokemonText, "Brock sends out Geodude!", false, false);
                if (Main.netMode == 2 && combatTextNum != 100) {
                    CombatText combatText = Main.combatText[combatTextNum];
                    NetMessage.SendData(81, -1, -1, combatText.text, (int)combatText.color.PackedValue, combatText.position.X, combatText.position.Y, 0f, 0, 0, 0);
                }
                int summonNPC = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("Geodude"));
                summonPokemon = Main.npc[summonNPC].modNPC as PokemonNPC;
                summonPokemon.level = 12;
                timer.Stop();
                timer.Dispose();
            }
            loop++;
        }

        public override bool PreAI() {
            if (summonPokemon != null) {
                if (summonPokemon.npc.life < 1 && pokemonOneDefeated == false) {
                    pokemonOneDefeated = true;
                    combatTextNum = CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), PokemonText, "Brock sends out Onix!", false, false);
                    if (Main.netMode == 2 && combatTextNum != 100) {
                        CombatText combatText = Main.combatText[combatTextNum];
                        NetMessage.SendData(81, -1, -1, combatText.text, (int)combatText.color.PackedValue, combatText.position.X, combatText.position.Y, 0f, 0, 0, 0);
                    }
                    int summonNPC = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("Onix"));
                    summonPokemon = Main.npc[summonNPC].modNPC as PokemonNPC;
                    summonPokemon.level = 14;
                } else if (summonPokemon.npc.life < 1 && pokemonTwoDefeated == false) {
                    pokemonTwoDefeated = true;
                    summonPokemon = null;
                    // reset
                    pokemonOneDefeated = false;
                    pokemonTwoDefeated = false;
                }
            }
            return base.PreAI();
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback) {
            damage = 20;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown) {
            cooldown = 30;
            randExtraCooldown = 30;
        }
    }
}