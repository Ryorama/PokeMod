using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PokeModBlue.NPCs.Pokemon
{
	public class Squirtle : PokemonNPC
	{
		public override float id {get{return 7.0f;}}
		public override int shoot {get{return mod.ProjectileType("WaterGun");}}
        public override byte aiMode { get { return swimming; } }

        public override void SetDefaults()
		{
			base.SetDefaults();
			npc.width = 46;
			npc.height = 40;
			Main.npcFrameCount[npc.type] = 3;
		}
		
		public override float CanSpawn(NPCSpawnInfo spawnInfo)
		{
			return 0f;
		}
	}
}