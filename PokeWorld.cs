using PokeModBlue.Items.Weapons;
using Terraria;
using Terraria.ModLoader;

namespace PokeModBlue {

    public class PokeWorld : ModWorld {
        public PokemonWeapon pokemon;

        // used to debug what information the server knows, essentially nothing, only the item owner actually saves and loads the stats of the item
        // so there is no central authoritative server for item information, so I cannot ask the server to tell everyone the stats of the pokemon, and only the server is allowed to send info
        public override void PostUpdate() {
            if (Main.netMode == 2) {
                for (int i = 0; i < Main.player.Length - 1; i++) {
                    if (Main.player[i] != null) {
                        pokemon = Main.player[i].inventory[Main.player[i].selectedItem].modItem as PokemonWeapon;
                        ErrorLogger.Log(Main.player[i].name + " " + pokemon.ToString() + " " + pokemon.item.toolTip);
                    }
                }
            }
        }
    }
}