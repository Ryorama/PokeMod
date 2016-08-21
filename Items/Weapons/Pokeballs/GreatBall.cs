using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PokeModBlue.Items.Weapons.Pokeballs
{
	public class GreatBall : ModItem
	{		
		public override void SetDefaults()
		{
			item.name = "Great Ball";
			item.damage = 1;
			item.thrown = true;
			item.width = 18;
			item.height = 20;
			item.toolTip = "A good, high-performance Poké Ball that provides a higher Pokémon catch rate than a standard Poké Ball can. ";
			item.useTime = 19;
			item.useAnimation = 19;
			item.useStyle = 1;
			item.noMelee = true;
			item.knockBack = 0.0f;
			item.value = Item.sellPrice(0, 0, 0, 120);
			item.rare = 1;
			//item.useSound = mod.GetSoundSlot(SoundType.Item, "Sounds/Item/Wooo");
			item.shoot = mod.ProjectileType("GreatBall");
			item.shootSpeed = 7f;
			item.maxStack = 99;
			item.consumable = true;
			//item.useSound = 1;
			//item.noUseGraphic = true;	
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			type = mod.ProjectileType("GreatBall");
			return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
		}
	}
}