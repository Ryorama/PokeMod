using Microsoft.Xna.Framework;
using PokeModBlue.NPCs.Pokemon;
using PokeModBlue.Projectiles;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace PokeModBlue.Items.Weapons {

    /// <summary>
    /// This class is the base class for all Players summonable Pokemon.
    /// </summary>
    public abstract class PokemonWeapon : ModItem {
        public NPC npc;
        public PokeballSummon pokeball = null;
        public bool summoned = false;
        public int combatTextNum;
        public static Color PokemonText = new Color(255, 255, 255, 255);

        public int experience;
        public byte level;
        public byte nature;
        public byte HPIV, HPEV, AtkIV, AtkEV, DefIV, DefEV, SpAIV, SpAEV, SpDIV, SpDEV, SpeIV, SpeEV;
        public string originalTrainer = "";
        public int currentHP;

        public const byte Hardy = 1;
        public const byte Lonely = 2;
        public const byte Brave = 3;
        public const byte Adamant = 4;
        public const byte Naughty = 5;
        public const byte Bold = 6;
        public const byte Docile = 7;
        public const byte Relaxed = 8;
        public const byte Impish = 9;
        public const byte Lax = 10;
        public const byte Timid = 11;
        public const byte Hasty = 12;
        public const byte Serious = 13;
        public const byte Jolly = 14;
        public const byte Naive = 15;
        public const byte Modest = 16;
        public const byte Mild = 17;
        public const byte Quiet = 18;
        public const byte Bashful = 19;
        public const byte Rash = 20;
        public const byte Calm = 21;
        public const byte Gentle = 22;
        public const byte Sassy = 23;
        public const byte Careful = 24;
        public const byte Quirky = 25;

        public const int erratic = 600000;
        public const int fast = 800000;
        public const int medium_fast = 1000000;
        public const int medium_slow = 1059860;
        public const int slow = 1250000;
        public const int fluctuating = 1640000;

        public virtual float id { get; protected set; }

        public int baseHP {
            get {
                PokedexEntry val;
                if (Pokedex.pokedex.TryGetValue(id, out val)) {
                    return val.HP;
                } else {
                    return 0;
                }
            }
        }

        public int baseAtk {
            get {
                PokedexEntry val;
                if (Pokedex.pokedex.TryGetValue(id, out val)) {
                    return val.Atk;
                } else {
                    return 0;
                }
            }
        }

        public int baseDef {
            get {
                PokedexEntry val;
                if (Pokedex.pokedex.TryGetValue(id, out val)) {
                    return val.Def;
                } else {
                    return 0;
                }
            }
        }

        public int baseSpA {
            get {
                PokedexEntry val;
                if (Pokedex.pokedex.TryGetValue(id, out val)) {
                    return val.SpA;
                } else {
                    return 0;
                }
            }
        }

        public int baseSpD {
            get {
                PokedexEntry val;
                if (Pokedex.pokedex.TryGetValue(id, out val)) {
                    return val.SpD;
                } else {
                    return 0;
                }
            }
        }

        public int baseSpe {
            get {
                PokedexEntry val;
                if (Pokedex.pokedex.TryGetValue(id, out val)) {
                    return val.Spe;
                } else {
                    return 0;
                }
            }
        }

        public int EXP {
            get {
                PokedexEntry val;
                if (Pokedex.pokedex.TryGetValue(id, out val)) {
                    return val.EXP;
                } else {
                    return 0;
                }
            }
        }

        public int maxHP {
            get {
                return ((((2 * baseHP) + HPIV + (HPEV / 4)) * level) / 100) + level + 10;
            }
        }

        public int Atk {
            get {
                return (int)((float)(((((2 * baseAtk) + AtkIV + (AtkEV / 4)) * level) / 100) + level + 5) * NatureMultipler("Atk"));
            }
        }

        public int Def {
            get {
                return (int)((float)(((((2 * baseDef) + DefIV + (DefEV / 4)) * level) / 100) + level + 5) * NatureMultipler("Def"));
            }
        }

        public int SpA {
            get {
                return (int)((float)(((((2 * baseSpA) + SpAIV + (SpAEV / 4)) * level) / 100) + level + 5) * NatureMultipler("SpA"));
            }
        }

        public int SpD {
            get {
                return (int)((float)(((((2 * baseSpD) + SpDIV + (SpDEV / 4)) * level) / 100) + level + 5) * NatureMultipler("SpD"));
            }
        }

        public int Spe {
            get {
                return (int)((float)(((((2 * baseSpe) + SpeIV + (SpeEV / 4)) * level) / 100) + level + 5) * NatureMultipler("Spe"));
            }
        }

        public int EvolveLevel {
            get {
                PokedexEntry val;
                if (Pokedex.pokedex.TryGetValue(id, out val)) {
                    if (val.EvolveLevel != null) {
                        return (int)val.EvolveLevel;
                    } else {
                        return 0;
                    }
                } else {
                    return 0;
                }
            }
        }

        public float EvolveID {
            get {
                PokedexEntry val;
                if (Pokedex.pokedex.TryGetValue(id, out val)) {
                    if (val.EvolveID != null) {
                        return (float)val.EvolveID;
                    } else {
                        return 0f;
                    }
                } else {
                    return 0f;
                }
            }
        }

        public string Name {
            get {
                PokedexEntry val;
                if (Pokedex.pokedex.TryGetValue(id, out val)) {
                    return val.Pokemon;
                } else {
                    return "";
                }
            }
        }

        public override void SetDefaults() {
            item.name = Name;
            Random rnd = new Random();
            nature = (byte)rnd.Next(1, 25);
            HPIV = (byte)rnd.Next(0, 31);
            AtkIV = (byte)rnd.Next(0, 31);
            DefIV = (byte)rnd.Next(0, 31);
            SpAIV = (byte)rnd.Next(0, 31);
            SpDIV = (byte)rnd.Next(0, 31);
            SpeIV = (byte)rnd.Next(0, 31);
            HPEV = 0;
            AtkEV = 0;
            DefEV = 0;
            SpAEV = 0;
            SpDEV = 0;
            SpeEV = 0;
            level = 5;
            experience = 0;
            originalTrainer = Main.player[Main.myPlayer].name;
            item.width = 40;
            item.height = 40;
            item.damage = 0;
            item.useStyle = 1;
            item.useTime = 20;
            item.useAnimation = 20;
            item.noMelee = true;
            item.rare = 9;
            //item.buffType = mod.BuffType(Name + "Buff");
            Main.npcCatchable[mod.NPCType(item.name)] = true;
            item.makeNPC = (short)mod.NPCType(item.name);
            item.noUseGraphic = true;
            currentHP = maxHP;
            SetToolTip();
        }

        public override void OnCraft(Recipe recipe) {
            base.OnCraft(recipe);
            this.SetDefaults();
        }

        // disallow using pokemon weapons if they are picked up and clicked on world, for some reason this deletes their stats
        // removed for debugging why pokemon aren't spawning as of 1.3.3.2
        public override bool CanUseItem(Player player) {
            if (summoned) {
                return false;
            }
            if (player.selectedItem == 58) {
                combatTextNum = CombatText.NewText(new Rectangle((int)Main.player[item.owner].position.X, (int)Main.player[item.owner].position.Y, Main.player[item.owner].width, Main.player[item.owner].height), PokemonText, "Use Pokemon from the hotbar to summon.", false, false);
                if (Main.netMode == 2 && combatTextNum != 100) {
                    CombatText combatText = Main.combatText[combatTextNum];
                    NetMessage.SendData(81, -1, -1, combatText.text, (int)combatText.color.PackedValue, combatText.position.X, combatText.position.Y, 0f, 0, 0, 0);
                }
                return false;
            } else {
                if (currentHP > 0) {
                    return true;
                } else {
                    combatTextNum = CombatText.NewText(new Rectangle((int)Main.player[item.owner].position.X, (int)Main.player[item.owner].position.Y, Main.player[item.owner].width, Main.player[item.owner].height), PokemonText, item.name + " has no energy left to battle!", false, false);
                    if (Main.netMode == 2 && combatTextNum != 100) {
                        CombatText combatText = Main.combatText[combatTextNum];
                        NetMessage.SendData(81, -1, -1, combatText.text, (int)combatText.color.PackedValue, combatText.position.X, combatText.position.Y, 0f, 0, 0, 0);
                    }
                    return false;
                }
            }
        }

        public override bool UseItem(Player player) {
            if (pokeball == null) {
                int proj = Projectile.NewProjectile(player.position, new Vector2(3.0f * (float)player.direction + player.velocity.X, -2.0f), mod.ProjectileType("PokeballSummon"), 0, 0.0f);
                pokeball = Main.projectile[proj].modProjectile as PokeballSummon;
                if (pokeball != null) {
                    pokeball.pokemon = this;
                    pokeball.player = player;
                }
            }
            return true;
        }

        public void SummonPokemon(Player player, Vector2 position) {
            if (Main.netMode == 0) {
                int npc = NPC.NewNPC((int)position.X, (int)position.Y, (int)item.makeNPC);
                PokemonNPC pokemon = Main.npc[npc].modNPC as PokemonNPC;
                if (pokemon != null) {
                    pokemon.pokemon = Main.player[item.owner].inventory[Main.player[item.owner].selectedItem].modItem as PokemonWeapon;
                }
                Main.npc[npc].releaseOwner = (byte)player.whoAmI;
                Main.npc[npc].life = currentHP;
            } else if (Main.netMode == 1) {
                /*
                // commented out for now as it causes pokemon to reset their stats until I sync the items as well
                ModPacket packet = mod.GetPacket();
                packet.Write((byte)PokeModMessageType.SummonPokemon);
                packet.Write((int)position.X);
                packet.Write((int)position.Y);
                packet.Write((int)item.makeNPC);
                packet.Write((byte)player.whoAmI);
                packet.Write((int)currentHP);
                packet.Write((int)Main.player[item.owner].selectedItem);
                packet.Send();
                */
            }
            if (ModLoader.GetMod("PokeModBlueSounds") != null) {
                Main.PlaySound(SoundLoader.customSoundType, -1, -1, ModLoader.GetMod("PokeModBlueSounds").GetSoundSlot(SoundType.Custom, "Sounds/Custom/id" + ((int)id).ToString()));
            }

            summoned = true;
            /*
            PokePlayer modPlayer = (PokePlayer)player.GetModPlayer(mod, "PokePlayer");
            // need to put a limiter on this, max 1 per item
            if (player.HasBuff(mod.BuffType(Name + "Buff")) < 0 && player.itemTime == 0 && player.itemAnimation > 0 && player.controlUseItem)
            {
            }
            */
        }

        public override void GetWeaponDamage(Player player, ref int damage) {
            damage = Atk;
        }

        public int GetRangedDamage() {
            return SpA;
        }

        public float NatureMultipler(string stat) {
            if (nature == 1 || nature == 7 || nature == 13 || nature == 19 || nature == 25) {
                return 1f;
            } else if (stat == "Atk") {
                if (nature == 2 || nature == 3 || nature == 4 || nature == 5) { return 1.10f; } else if (nature == 6 || nature == 11 || nature == 16 || nature == 21) { return 0.9f; }
            } else if (stat == "Def") {
                if (nature == 6 || nature == 8 || nature == 9 || nature == 10) { return 1.10f; } else if (nature == 2 || nature == 12 || nature == 17 || nature == 22) { return 0.9f; }
            } else if (stat == "SpA") {
                if (nature == 16 || nature == 17 || nature == 18 || nature == 20) { return 1.10f; } else if (nature == 4 || nature == 9 || nature == 14 || nature == 24) { return 0.9f; }
            } else if (stat == "SpD") {
                if (nature == 21 || nature == 22 || nature == 23 || nature == 24) { return 1.10f; } else if (nature == 5 || nature == 10 || nature == 15 || nature == 20) { return 0.9f; }
            } else if (stat == "Spe") {
                if (nature == 11 || nature == 12 || nature == 14 || nature == 15) { return 1.10f; } else if (nature == 3 || nature == 8 || nature == 18 || nature == 23) { return 0.9f; }
            }
            return 1f;
        }

        public override void SaveCustomData(BinaryWriter writer) {
            writer.Write(level);
            writer.Write(experience);
            writer.Write(nature);
            writer.Write(HPIV);
            writer.Write(HPEV);
            writer.Write(AtkIV);
            writer.Write(AtkEV);
            writer.Write(DefIV);
            writer.Write(DefEV);
            writer.Write(SpAIV);
            writer.Write(SpAEV);
            writer.Write(SpDIV);
            writer.Write(SpDEV);
            writer.Write(SpeIV);
            writer.Write(SpeEV);
            writer.Write(originalTrainer);
            writer.Write((UInt16)currentHP);
        }

        public override void LoadCustomData(BinaryReader reader) {
            SetDefaults();
            level = reader.ReadByte();
            experience = reader.ReadInt32();
            nature = reader.ReadByte();
            HPIV = reader.ReadByte();
            HPEV = reader.ReadByte();
            AtkIV = reader.ReadByte();
            AtkEV = reader.ReadByte();
            DefIV = reader.ReadByte();
            DefEV = reader.ReadByte();
            SpAIV = reader.ReadByte();
            SpAEV = reader.ReadByte();
            SpDIV = reader.ReadByte();
            SpDEV = reader.ReadByte();
            SpeIV = reader.ReadByte();
            SpeEV = reader.ReadByte();
            originalTrainer = reader.ReadString();
            try {
                currentHP = reader.ReadUInt16();
            } catch (Exception) {
                currentHP = maxHP;
            }
            SetToolTip();
            /*
            Main.NewText("Netmode is: " + Main.netMode.ToString());
            var netMessage = mod.GetPacket();
            netMessage.Write((byte)PokeModMessageType.SetPokemonWeaponData);
            netMessage.Write(item.whoAmI); //which item this is
            netMessage.Write(level);
            netMessage.Write(experience);
            netMessage.Write(nature);
            netMessage.Write(HPIV);
            netMessage.Write(HPEV);
            netMessage.Write(AtkIV);
            netMessage.Write(AtkEV);
            netMessage.Write(DefIV);
            netMessage.Write(DefEV);
            netMessage.Write(SpAIV);
            netMessage.Write(SpAEV);
            netMessage.Write(SpDIV);
            netMessage.Write(SpDEV);
            netMessage.Write(SpeIV);
            netMessage.Write(SpeEV);
            netMessage.Write(originalTrainer);
            netMessage.Send();*/
        }

        /**
 * Gets and caches the result of the pokemon's first type
 */
        private int typeI = -2;

        public int getTypeI() {
            if (typeI == -2) {
                PokedexEntry val;
                if (Pokedex.pokedex.TryGetValue(id, out val)) {
                    int type;
                    bool found = false;
                    found = PokemonNPC.types.TryGetValue(val.Type_I, out type);
                    if (found) {
                        typeI = type;
                    } else {
                        typeI = -1;
                    }
                }
            }
            return typeI;
        }

        /**
         * Gets and caches the result of the pokemon's second type
         */
        private int typeII = -2;

        public int getTypeII() {
            if (typeII == -2) {
                PokedexEntry val;
                if (Pokedex.pokedex.TryGetValue(id, out val)) {
                    int type;
                    bool found = false;
                    found = PokemonNPC.types.TryGetValue(val.Type_II, out type);
                    if (found) {
                        typeII = type;
                    } else {
                        typeII = -1;
                    }
                }
            }
            return typeI;
        }

        public string getTypeIName() {
            PokedexEntry val;
            if (Pokedex.pokedex.TryGetValue(id, out val)) {
                return val.Type_I;
            } else {
                return "";
            }
        }

        public string getTypeIIName() {
            PokedexEntry val;
            if (Pokedex.pokedex.TryGetValue(id, out val)) {
                return val.Type_II;
            } else {
                return "";
            }
        }

        public string getTypesString() {
            string type1 = getTypeIName();
            string type2 = getTypeIIName();
            if (type1 != "") {
                if (type2 != "") {
                    return type1 + "/" + type2;
                } else {
                    return type1;
                }
            } else {
                return "???";
            }
        }

        public void SetToolTip() {
            if (this.CanRightClick()) {
                item.toolTip = getTypesString() + System.Environment.NewLine + "Level: " + level.ToString() + System.Environment.NewLine + "Experience: " + experience.ToString() + "/" + GetExpForLevel(level + 1).ToString() + System.Environment.NewLine + "Nature: " + GetNatureString() + System.Environment.NewLine + StatLine() + System.Environment.NewLine + "Original Trainer: " + System.Environment.NewLine + currentHP + "/" + maxHP + " HP" + originalTrainer + System.Environment.NewLine + "Right Click to Evolve!";
            } else {
                item.toolTip = getTypesString() + System.Environment.NewLine + "Level: " + level.ToString() + System.Environment.NewLine + "Experience: " + experience.ToString() + "/" + GetExpForLevel(level + 1).ToString() + System.Environment.NewLine + "Nature: " + GetNatureString() + System.Environment.NewLine + StatLine() + System.Environment.NewLine + "Original Trainer: " + originalTrainer + System.Environment.NewLine + currentHP + "/" + maxHP + " HP";
            }
        }

        public int GetExpForLevel(int level) {
            if (EXP == erratic) {
                if (level <= 50) {
                    return (((level * level * level) * (100 - level)) / 50);
                } else if (level > 50 && level <= 68) {
                    return (((level * level * level) * (150 - level)) / 100);
                } else if (level > 68 && level <= 98) {
                    return (((level * level * level) * (1911 - (10 * level))) / 3);
                } else if (level > 98 && level <= 100) {
                    return (((level * level * level) * (160 - level)) / 100);
                }
            } else if (EXP == fast) {
                return ((4 * (level * level * level)) / 5);
            } else if (EXP == medium_fast) {
                return (level * level * level);
            } else if (EXP == medium_slow) {
                return (((6 / 5) * (level * level * level)) - (15 * (level * level)) + (100 * level) - 140);
            } else if (EXP == slow) {
                return ((5 * (level * level * level)) / 4);
            } else if (EXP == fluctuating) {
                if (level <= 15) {
                    return ((level * level * level) * ((((level + 1) / 3) + 24) / 50));
                } else if (level > 15 && level <= 36) {
                    return ((level * level * level) * ((level + 14) / 50));
                } else if (level > 36 && level <= 100) {
                    return ((level * level * level) * (((level / 2) + 32) / 50));
                }
            }
            return 999999999;
        }

        public string GetNatureString() {
            switch (nature) {
                case 1:
                    return "Hardy";

                case 2:
                    return "Lonely";

                case 3:
                    return "Brave";

                case 4:
                    return "Adamant";

                case 5:
                    return "Naughty";

                case 6:
                    return "Bold";

                case 7:
                    return "Docile";

                case 8:
                    return "Relaxed";

                case 9:
                    return "Impish";

                case 10:
                    return "Lax";

                case 11:
                    return "Timid";

                case 12:
                    return "Hasty";

                case 13:
                    return "Serious";

                case 14:
                    return "Jolly";

                case 15:
                    return "Naive";

                case 16:
                    return "Modest";

                case 17:
                    return "Mild";

                case 18:
                    return "Quiet";

                case 19:
                    return "Bashful";

                case 20:
                    return "Rash";

                case 21:
                    return "Calm";

                case 22:
                    return "Gentle";

                case 23:
                    return "Sassy";

                case 24:
                    return "Careful";

                case 25:
                    return "Quirky";

                default:
                    return "Hardy";
            }
        }

        public string StatLine() {
            return "HP: " + maxHP.ToString() + ", IV: " + HPIV.ToString() + ", EV: " + HPEV.ToString() + " Attack: " + Atk.ToString() + ", IV: " + AtkIV.ToString() + ", EV: " + AtkEV.ToString() + " Defense: " + Def.ToString() + ", IV: " + DefIV.ToString() + ", EV: " + DefEV.ToString() + System.Environment.NewLine + "Special Attack: " + SpA.ToString() + ", IV: " + SpAIV.ToString() + ", EV: " + SpAEV.ToString() + " Special Defense: " + SpD.ToString() + ", IV: " + SpDIV.ToString() + ", EV: " + SpDEV.ToString() + " Speed: " + Spe.ToString() + ", IV: " + SpeIV.ToString() + ", EV: " + SpeEV.ToString();
        }

        public override bool CanRightClick() {
            if (EvolveLevel != 0 && EvolveID != 0) {
                PokedexEntry val;
                if (Pokedex.pokedex.TryGetValue((float)EvolveID, out val) && level >= EvolveLevel) {
                    return true;
                }
            }
            return false;
        }

        public override void RightClick(Player player) {
            PokedexEntry val;
            Pokedex.pokedex.TryGetValue((float)EvolveID, out val);
            string evolveName;
            evolveName = val.Pokemon;
            combatTextNum = CombatText.NewText(new Rectangle((int)Main.player[item.owner].position.X, (int)Main.player[item.owner].position.Y, Main.player[item.owner].width, Main.player[item.owner].height), PokemonText, "What? " + item.name + " is evolving!", false, false);
            if (Main.netMode == 2 && combatTextNum != 100) {
                CombatText combatText = Main.combatText[combatTextNum];
                NetMessage.SendData(81, -1, -1, combatText.text, (int)combatText.color.PackedValue, combatText.position.X, combatText.position.Y, 0f, 0, 0, 0);
            }
            combatTextNum = CombatText.NewText(new Rectangle((int)Main.player[item.owner].position.X, (int)Main.player[item.owner].position.Y, Main.player[item.owner].width, Main.player[item.owner].height), PokemonText, "Congratulations! Your " + item.name + " evolved in to " + evolveName + "!", false, false);
            if (Main.netMode == 2 && combatTextNum != 100) {
                CombatText combatText = Main.combatText[combatTextNum];
                NetMessage.SendData(81, -1, -1, combatText.text, (int)combatText.color.PackedValue, combatText.position.X, combatText.position.Y, 0f, 0, 0, 0);
            }
            int itemRef = Item.NewItem((int)Main.player[item.owner].position.X, (int)Main.player[item.owner].position.Y, 1, 1, mod.ItemType(evolveName + "Pokeball"), 1, false, 0, false, false);
            PokemonWeapon newItem;
            newItem = Main.item[itemRef].modItem as PokemonWeapon;
            newItem.SetDefaults();
            newItem.level = this.level;
            newItem.nature = this.nature;
            newItem.experience = this.experience;
            newItem.HPIV = this.HPIV;
            newItem.HPEV = this.HPEV;
            newItem.AtkIV = this.AtkIV;
            newItem.AtkEV = this.AtkEV;
            newItem.DefIV = this.DefIV;
            newItem.DefEV = this.DefEV;
            newItem.SpAIV = this.SpAIV;
            newItem.SpAEV = this.SpAEV;
            newItem.SpDIV = this.SpDIV;
            newItem.SpDEV = this.SpDEV;
            newItem.SpeIV = this.SpeIV;
            newItem.SpeEV = this.SpeEV;
            newItem.currentHP = this.currentHP;
            if (Main.player[item.owner].HasBuff(mod.BuffType(Name + "Buff")) > -1) {
                Main.player[item.owner].DelBuff(Main.player[item.owner].HasBuff(mod.BuffType(Name + "Buff")));
            }
            newItem.SetToolTip();
            this.item.consumable = true;
            this.item.active = false;
        }

        // Add exp points for vanilla monsters
        public void AddExperience(int xp, int psuedoLevel) {
            // see http://bulbapedia.bulbagarden.net/wiki/Experience#Experience_gain_in_battle for formula
            float a = 1f;
            float b = (float)xp;
            float e = 1f;
            // float f = 1f; //used in flat level xp formula, not in scaled xp from gen V
            float L = (float)psuedoLevel;
            float Lp = (float)level;
            float p = 1f;
            float s = 1f;
            float t = (originalTrainer == Main.player[Main.myPlayer].name) ? 1f : 1.5f;
            float v = (level < EvolveLevel) ? 1f : 1.2f;
            experience += (int)((((a * b * L) / (5f * s)) * (((float)Math.Pow(((2f * L) + 10), 2.5f)) / ((float)Math.Pow((L + Lp + 10), 2.5f)))) * t * e * p);
            CheckLevelUp();
            SetToolTip();
        }

        // add exp points calculated from how strong the pokemon defeated was
        public void AddExperience(PokemonNPC npc) {
            if (level < 100) {
                // see http://bulbapedia.bulbagarden.net/wiki/Experience#Experience_gain_in_battle for formula
                float a = 1f;
                float b = (float)npc.EXPV;
                float e = 1f;
                //float f = 1f; //used in flat level xp formula, not in scaled xp from gen V
                float L = (float)npc.level;
                float Lp = (float)level;
                float p = 1f;
                float s = 1f;
                float t = (originalTrainer == Main.player[Main.myPlayer].name) ? 1f : 1.5f;
                float v = (level < EvolveLevel) ? 1f : 1.2f;
                experience += (int)((((a * b * L) / (5f * s)) * (((float)Math.Pow(((2f * L) + 10), 2.5f)) / ((float)Math.Pow((L + Lp + 10), 2.5f)))) * t * e * p);
            }

            //add EV's as well
            List<KeyValuePair<int, string>> list = npc.EV_Worth;
            foreach (KeyValuePair<int, string> kvp in list) {
                if (kvp.Value == "MHP") {
                    if (HPEV + (byte)kvp.Key < HPEV) {
                        HPEV = 255;
                    } else {
                        HPEV += (byte)kvp.Key;
                    }
                } else if (kvp.Value == "Atk") {
                    if (AtkEV + (byte)kvp.Key < AtkEV) {
                        AtkEV = 255;
                    } else {
                        AtkEV += (byte)kvp.Key;
                    }
                } else if (kvp.Value == "Def") {
                    if (DefEV + (byte)kvp.Key < DefEV) {
                        DefEV = 255;
                    } else {
                        DefEV += (byte)kvp.Key;
                    }
                } else if (kvp.Value == "SpA") {
                    if (SpAEV + (byte)kvp.Key < SpAEV) {
                        SpAEV = 255;
                    } else {
                        SpAEV += (byte)kvp.Key;
                    }
                } else if (kvp.Value == "SpD") {
                    if (SpDEV + (byte)kvp.Key < SpDEV) {
                        SpDEV = 255;
                    } else {
                        SpDEV += (byte)kvp.Key;
                    }
                } else if (kvp.Value == "Spe") {
                    if (SpeEV + (byte)kvp.Key < SpeEV) {
                        SpeEV = 255;
                    } else {
                        SpeEV += (byte)kvp.Key;
                    }
                }
            }
            CheckLevelUp();
            SetToolTip();
        }

        public void CheckLevelUp() {
            if (level < 100) {
                while (experience >= GetExpForLevel(level + 1)) {
                    experience -= GetExpForLevel(level + 1);
                    combatTextNum = CombatText.NewText(new Rectangle((int)Main.player[item.owner].position.X, (int)Main.player[item.owner].position.Y, Main.player[item.owner].width, Main.player[item.owner].height), PokemonText, item.name + " grew to level " + (level + 1).ToString() + "!", false, false);
                    if (Main.netMode == 2 && combatTextNum != 100) {
                        CombatText combatText = Main.combatText[combatTextNum];
                        NetMessage.SendData(81, -1, -1, combatText.text, (int)combatText.color.PackedValue, combatText.position.X, combatText.position.Y, 0f, 0, 0, 0);
                    }
                    Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ExpFull"));
                    level += 1;
                    if (level == 100) {
                        break;
                    }
                }
            }
        }
    }
}