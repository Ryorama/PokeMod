using Terraria.ModLoader;

namespace PokeModBlue.Items.Weapons {

    public class BulbasaurPokeball : PokemonWeapon {
        public override float id { get { return 1f; } }

        public override void SetDefaults() {
            base.SetDefaults();
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Pokecase", 1);
            recipe.SetResult(ItemLoader.GetItem(item.type));
            recipe.AddRecipe();
        }
    }
}