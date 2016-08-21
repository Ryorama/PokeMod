using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PokeModBlue.Items
{
	public class Pokecase : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "Pokecase";
			item.width = 36;
			item.height = 28;
			item.maxStack = 1;
			AddTooltip("Choose your starter Pokemon!");
			item.value = 100;
			item.rare = 1;
		}
	}
}
