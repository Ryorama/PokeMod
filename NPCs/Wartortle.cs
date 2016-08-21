using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PokeModBlue.NPCs
{
	public class Wartortle : PokemonNPC
	{
		public override float id {get{return 8.0f;}}
		public override int shoot {get{return mod.ProjectileType("WaterGun");}}
        public override byte aiMode { get { return swimming; } }

        public override void SetDefaults()
		{
			base.SetDefaults();
			npc.width = 44;
			npc.height = 50;
			Main.npcFrameCount[npc.type] = 3;
		}
		
		public override float CanSpawn(NPCSpawnInfo spawnInfo)
		{
			return 0f;
		}
	}
}