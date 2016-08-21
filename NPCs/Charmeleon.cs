using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PokeModBlue.NPCs
{
	public class Charmeleon : PokemonNPC
	{
		public override float id {get{return 5.0f;}}
		public override int shoot {get{return mod.ProjectileType("Ember");}}
		
		public override void SetDefaults()
		{
			base.SetDefaults();
            //npc.width = 46;
            //npc.height = 46;
            npc.width = 18;
            npc.height = 40;
			Main.npcFrameCount[npc.type] = 3;
		}

		public override void CreateDust()
		{
			Lighting.AddLight((int)(npc.Center.X / 16f), (int)(npc.Center.Y / 16f), 0.8f, 0.8f, 0.8f);
		}
		
		public override float CanSpawn(NPCSpawnInfo spawnInfo)
		{
			return 0f;
		}
	}
}