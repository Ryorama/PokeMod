using Terraria;
using Terraria.ModLoader;

namespace PokeModBlue.Items.Accessories {

    public class TrainerCard : ModItem {

        public override void SetDefaults() {
            item.name = "Trainer Card";
            item.width = 40;
            item.height = 32;
            item.toolTip = "This ID card displays information about a Trainer.";
            item.value = 0;
            item.rare = 2;
        }

        public override void UpdateInventory(Player player) {
            PokePlayer pokePlayer = player.GetModPlayer(mod, "PokePlayer") as PokePlayer;
            if (pokePlayer != null) {
                pokePlayer.trainerCard = true;
            }
        }

        /*
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
        */
    }
}