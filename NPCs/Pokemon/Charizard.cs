using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PokeModBlue.NPCs.Pokemon
{
	public class Charizard : PokemonNPC
	{
		public override float id {get{return 6.0f;}}
		public override int shoot {get{return mod.ProjectileType("Ember");}}
		
		public override void SetDefaults()
		{
			base.SetDefaults();
			npc.width = 58;
			npc.height = 56;
			Main.npcFrameCount[npc.type] = 3;
		}

		public override void CreateDust()
		{
			Lighting.AddLight((int)(npc.Center.X / 16f), (int)(npc.Center.Y / 16f), 1.0f, 1.0f, 1.0f);
		}
		
		public override float CanSpawn(NPCSpawnInfo spawnInfo)
		{
			return 0f;
		}
	}
}