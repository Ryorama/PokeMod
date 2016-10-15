using PokeModBlue.Items.Weapons;
using Terraria;
using Terraria.ModLoader;

namespace PokeModBlue.Items.Usable {
    public class Potion : ModItem {
		
		public override void SetDefaults()
		{
			item.name = "Potion";
            item.width = 30;
            item.height = 42;
            item.useSound = 3;
            item.useStyle = 2;
            item.useTurn = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.maxStack = 30;
            item.consumable = true;
            item.potion = true;
            item.toolTip = "A spray-type medicine for treating wounds. It can be used to restore 20 HP to an injured Pokémon.";
            item.value = Item.sellPrice(0, 0, 0, 60);
            item.rare = 0;
		}
		
        public override bool UseItem(Player player) {
            for (int i = 0; i < Main.player[player.whoAmI].inventory.Length; i++) {
                PokemonWeapon pokemonWeapon = Main.player[player.whoAmI].inventory[i].modItem as PokemonWeapon;
                if (pokemonWeapon != null) {
                    if (pokemonWeapon.npc != null) {
                        pokemonWeapon.npc.HealEffect(20);
                        pokemonWeapon.npc.life += 20;
                        if (pokemonWeapon.npc.life > pokemonWeapon.maxHP) {
                            pokemonWeapon.npc.life = pokemonWeapon.maxHP;
                        }
                        pokemonWeapon.currentHP = pokemonWeapon.npc.life;
                        pokemonWeapon.SetToolTip();
                        return true;
                    }
                }
            }
            return false;
        }
    }
}