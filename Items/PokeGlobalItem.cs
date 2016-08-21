using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PokeModBlue.Items.Weapons;

namespace PokeModBlue.Items
{
    public class PokeGlobalItem : GlobalItem
    {
        private double time;
        private static Item[] singleSlotArray;

        public PokeGlobalItem()
        {
            singleSlotArray = new Item[1];
        }

        public override void PostDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (time != Main.time)
            {
                time = Main.time;

                // These 2 lines might not be needed, not sure why there are here.
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

                DrawUpdateExtraAccessories(spriteBatch);
            }
            time = Main.time;
            base.PostDrawInInventory(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }

        private void DrawUpdateExtraAccessories(SpriteBatch spriteBatch)
        {
            if (Main.playerInventory && Main.EquipPage == 2)
            {
                Point value = new Point(Main.mouseX, Main.mouseY);
                Rectangle r = new Rectangle(0, 0, (int)((float)Main.inventoryBackTexture.Width * Main.inventoryScale), (int)((float)Main.inventoryBackTexture.Height * Main.inventoryScale));

                PokePlayer pp = Main.player[Main.myPlayer].GetModPlayer<PokePlayer>(mod);
                for (int i = 0; i < 6; i++)
                {
                    Main.inventoryScale = 0.85f;
                    Item accItem = pp.ExtraAccessories[i];

                    int mH = 0;
                    if (Main.mapEnabled)
                    {
                        if (!Main.mapFullscreen && Main.mapStyle == 1)
                        {
                            mH = 256;
                        }
                        if (mH + 600 > Main.screenHeight)
                        {
                            mH = Main.screenHeight - 600;
                        }
                    }

                    int num17 = Main.screenWidth - 92 - (47 * 3);
                    int num18 = mH + 174;
                    if (Main.netMode == 1) num17 -= 47;
                    r.X = num17;
                    r.Y = num18 + (0 + i) * 47;


                    if (r.Contains(value))
                    {
                        Main.player[Main.myPlayer].mouseInterface = true;
                        Main.armorHide = true;
                        singleSlotArray[0] = accItem;
                        ItemSlot.Handle(singleSlotArray, ItemSlot.Context.EquipPet, 0);
                        accItem = singleSlotArray[0];
                    }
                    singleSlotArray[0] = accItem;
                    ItemSlot.Draw(spriteBatch, singleSlotArray, 13, 0, new Vector2(r.X, r.Y));
                    accItem = singleSlotArray[0];

                    pp.ExtraAccessories[i] = accItem;
                }
            }
        }
    }
}