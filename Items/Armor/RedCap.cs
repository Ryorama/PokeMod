using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace PokeModBlue.Items.Armor
{
	public class RedCap : ModItem
	{
		public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
		{
			equips.Add(EquipType.Head);
			return true;
		}

		public override void SetDefaults()
		{
			item.name = "Red Cap";
			item.width = 22;
			item.height = 10;
			item.toolTip = "'For those who want to be the very best.'";
			item.value = 10000;
			item.rare = 1;
			item.vanity = true;
		}

		public override bool DrawHead()
		{
			return true;
		}
	}
}
