using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PokeModBlue.NPCs.Pokemon {

	public class Gyarados : PokemonNPC
	{
		public override float id {get{return 130f;}}
		
		public override void SetDefaults()
		{
			base.SetDefaults();
			npc.width = 120;
			npc.height = 102;
			Main.npcFrameCount[npc.type] = 3;
            drawOffsetY = 10;
        }
	}
}
