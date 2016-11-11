using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace PokeModBlue.Items.Weapons.Pokeballs {

    public class UltraBall : ModItem {

        public override void SetDefaults() {
            item.name = "Ultra Ball";
            item.damage = 1;
            item.thrown = true;
            item.width = 18;
            item.height = 20;
            item.toolTip = "An ultra-high performance Poké Ball that provides a higher success rate for catching Pokémon than a Great Ball. ";
            item.useTime = 19;
            item.useAnimation = 19;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 0.0f;
            item.value = Item.sellPrice(0, 0, 0, 240);
            item.rare = 2;
            //item.useSound = mod.GetSoundSlot(SoundType.Item, "Sounds/Item/Wooo");
            item.shoot = mod.ProjectileType("UltraBall");
            item.shootSpeed = 7f;
            item.maxStack = 99;
            item.consumable = true;
            //item.useSound = 1;
            //item.noUseGraphic = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
            type = mod.ProjectileType("UltraBall");
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}