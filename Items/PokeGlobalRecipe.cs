using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PokeModRed.Items
{
    class PokeGlobalRecipe : GlobalRecipe
    {
        public override void OnCraft(Item item, Recipe recipe)
        {
            item.SetDefaults();
        }
    }
}