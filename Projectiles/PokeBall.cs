using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using PokeModBlue.NPCs;

namespace PokeModBlue.Projectiles
{
	public class PokeBall : PokeballProjectile
	{
		public override float rate {get{return 1.0f;}} //override this with a higher rate for other balls, or a method that checks conditions and returns a value etc.
		
		public override void SetDefaults()
		{
			base.SetDefaults();
			projectile.name = "PokeBall";
		}
    }
}