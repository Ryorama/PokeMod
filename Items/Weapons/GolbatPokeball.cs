using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PokeModBlue.Items.Weapons {

	public class GolbatPokeball : PokemonWeapon
	{
		public override float id {get{return 42f;}}
		
		public override void SetDefaults()
		{
			base.SetDefaults();
		}
	}
}
