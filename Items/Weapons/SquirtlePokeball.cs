using Terraria.ModLoader;

namespace PokeModBlue.Items.Weapons {

    public class SquirtlePokeball : PokemonWeapon {
        public override float id { get { return 7f; } }

        public override void SetDefaults() {
            base.SetDefaults();
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Pokecase", 1);
            recipe.SetResult(mod.ItemType(Name + "Pokeball"));
            recipe.AddRecipe();
        }
    }
}