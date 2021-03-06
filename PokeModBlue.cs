using PokeModBlue.Items.Weapons;
using PokeModBlue.NPCs.Pokemon;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace PokeModBlue {

    public partial class PokeModBlue : Mod {
        public static byte pokeSpawns = 1;
        public static IDictionary<int, float> originalSpawnPool;
        private double pressedSpawnToggleHotKeyTime;

        public PokeModBlue() {
            Properties = new ModProperties() {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };
        }

        public override void Load() {
            Pokedex.DoNothing();
            RegisterHotKey("Activate/Deactivate Pokemon Spawns", "P");
        }

        public override void HotKeyPressed(string name) {
            if (name == "Activate/Deactivate Pokemon Spawns") {
                if (Math.Abs(Main.time - pressedSpawnToggleHotKeyTime) > 60) {
                    pressedSpawnToggleHotKeyTime = Main.time;
                    if (pokeSpawns == 1) {
                        pokeSpawns = 2;
                        Main.NewText("Only Mod NPCs Spawn");
                    } else if (pokeSpawns == 2) {
                        pokeSpawns = 3;
                        Main.NewText("Only Normal NPCs Spawn");
                    } else if (pokeSpawns == 3) {
                        pokeSpawns = 4;
                        Main.NewText("Only Pokemon Spawn");
                    } else if (pokeSpawns == 4) {
                        pokeSpawns = 1;
                        Main.NewText("All NPCs Spawn");
                    }
                }
            }
        }

        private void PokedexCommand(string[] args) {
            int id;
            if (args.Length == 0 || !Int32.TryParse(args[0], out id)) {
                Main.NewText("Usage: /pokedex [number]");
                Main.NewText("Input the number as the National Pokedex number");
                return;
            } else {
                PokedexEntry entry;
                if (Pokedex.pokedex.TryGetValue((float)id, out entry)) {
                    Main.NewText(entry.Print());
                } else {
                    Main.NewText("No Pokemon found by that ID");
                }
            }
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI) {
            PokeModMessageType msgType = (PokeModMessageType)reader.ReadByte();
            switch (msgType) {
                // This message sent by the player when they load to initialize the custom Pokemon Weapon Data for all players
                case PokeModMessageType.SetPokemonWeaponData:

                    break;

                case PokeModMessageType.SummonPokemon:
                    int npc = NPC.NewNPC(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
                    Main.npc[npc].releaseOwner = reader.ReadByte();
                    Main.npc[npc].life = reader.ReadInt32();

                    PokemonNPC pokemon = Main.npc[npc].modNPC as PokemonNPC;
                    if (pokemon != null) {
                        PokemonWeapon pokemonWeapon = Main.player[Main.npc[npc].releaseOwner].inventory[Main.player[Main.npc[npc].releaseOwner].selectedItem].modItem as PokemonWeapon;
                        if (pokemonWeapon != null) {
                            pokemon.pokemon = pokemonWeapon;
                        }
                    }
                    break;

                default:
                    ErrorLogger.Log("PokeMod: Unknown Message type: " + msgType);
                    break;
            }
        }
    }

    internal enum PokeModMessageType : byte {
        SetPokemonWeaponData,
        SummonPokemon,
    }
}