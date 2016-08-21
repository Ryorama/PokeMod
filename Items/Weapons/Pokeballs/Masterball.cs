using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PokeModBlue.Items.Weapons.Pokeballs
{
	public class MasterBall : ModItem
	{		
		public override void SetDefaults()
		{
			item.name = "Master Ball";
			item.damage = 1;
			item.thrown = true;
			item.width = 18;
			item.height = 20;
			item.toolTip = "The best Poké Ball with the ultimate level of performance. With it, you will catch any wild Pokémon without fail. ";
			item.useTime = 19;
			item.useAnimation = 19;
			item.useStyle = 1;
			item.noMelee = true;
			item.knockBack = 0.0f;
			item.rare = 7;
			//item.useSound = mod.GetSoundSlot(SoundType.Item, "Sounds/Item/Wooo");
			item.shoot = mod.ProjectileType("MasterBall");
			item.shootSpeed = 7f;
			item.maxStack = 99;
			item.consumable = true;
			//item.useSound = 1;
			//item.noUseGraphic = true;	
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			type = mod.ProjectileType("MasterBall");
			return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
		}
	}
}