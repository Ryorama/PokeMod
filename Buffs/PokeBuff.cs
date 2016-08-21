using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace PokeModBlue.Buffs
{
	public abstract class PokeBuff : ModBuff
	{
		public virtual float id {get; protected set;}
		
		public new string Name
		{
			get{
				PokedexEntry val;
				if (Pokedex.pokedex.TryGetValue(id, out val))
				{
					return val.Pokemon;
				} else {
					return "";
				}
			}
		}
		
		public override void SetDefaults()
		{
			Main.buffName[Type] = Name;
			Main.buffTip[Type] = Name +" will fight for you";
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
		}
	}
}