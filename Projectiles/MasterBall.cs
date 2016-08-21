using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using PokeModBlue.NPCs;

namespace PokeModBlue.Projectiles
{
	public class MasterBall : PokeballProjectile
	{
		public override float rate {get{return 255;}} //override this with a higher rate for other balls, or a method that checks conditions and returns a value etc.
		
		public override void SetDefaults()
		{
			base.SetDefaults();
			projectile.name = "MasterBall";
		}
	}
}