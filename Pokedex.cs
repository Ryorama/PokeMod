using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Terraria.Social;
using Terraria.Utilities;

namespace PokeModBlue
{
	public static class Pokedex
	{
		public static Dictionary<float, PokedexEntry> pokedex = new Dictionary<float, PokedexEntry>();

		static Pokedex()
		{
			List<PokedexEntry> _pokedex = JsonConvert.DeserializeObject<List<PokedexEntry>>(PokedexJSON._pokedexJSON, new PokedexEntryConverter());
			foreach (PokedexEntry _pokedexEntry in _pokedex)
			{
				pokedex.Add(_pokedexEntry.Nat, _pokedexEntry);
				/*
				using (StreamWriter writer = new StreamWriter("\\Weapons\\" +_pokedexEntry.Pokemon +"Pokeball.cs"))
				{
					writer.WriteLine("using System;");
					writer.WriteLine("using Microsoft.Xna.Framework;");
					writer.WriteLine("using Terraria;");
					writer.WriteLine("using Terraria.ID;");
					writer.WriteLine("using Terraria.ModLoader;");
					writer.WriteLine("");
					writer.WriteLine("namespace PokeModBlue.Items.Weapons {");
					writer.WriteLine("");
					writer.WriteLine("	public class " +_pokedexEntry.Pokemon +"Pokeball : PokemonWeapon");
					writer.WriteLine("	{");
					writer.WriteLine("		public override float id {get{return " +_pokedexEntry.Nat +"f;}}");
					writer.WriteLine("		");
					writer.WriteLine("		public override void SetDefaults()");
					writer.WriteLine("		{");
					writer.WriteLine("			base.SetDefaults();");
					writer.WriteLine("		}");
					writer.WriteLine("	}");
					writer.WriteLine("}");
				}
				using (StreamWriter writer = new StreamWriter("\\Buffs\\" +_pokedexEntry.Pokemon +"Buff.cs"))
				{
					writer.WriteLine("using System;");
					writer.WriteLine("using Terraria;");
					writer.WriteLine("using Terraria.ModLoader;");
					writer.WriteLine("");
					writer.WriteLine("namespace PokeModBlue.Buffs {");
					writer.WriteLine("");
					writer.WriteLine("	public class " +_pokedexEntry.Pokemon +"Buff" +" : PokeBuff");
					writer.WriteLine("	{");
					writer.WriteLine("		public override float id {get{return " +_pokedexEntry.Nat +"f;}}");
					writer.WriteLine("	}");
					writer.WriteLine("}");
				}
				using (StreamWriter writer = new StreamWriter("\\NPCs\\" +_pokedexEntry.Pokemon +".cs"))
				{
					writer.WriteLine("using System;");
					writer.WriteLine("using Microsoft.Xna.Framework;");
					writer.WriteLine("using Terraria;");
					writer.WriteLine("using Terraria.ID;");
					writer.WriteLine("using Terraria.ModLoader;");
					writer.WriteLine("");
					writer.WriteLine("namespace PokeModBlue.NPCs.Pokemon {");
					writer.WriteLine("");
					writer.WriteLine("	public class " +_pokedexEntry.Pokemon +" : PokemonNPC");
					writer.WriteLine("	{");
					writer.WriteLine("		public override float id {get{return " +_pokedexEntry.Nat +"f;}}");
					writer.WriteLine("		");
					writer.WriteLine("		public override void SetDefaults()");
					writer.WriteLine("		{");
					writer.WriteLine("			base.SetDefaults();");
					writer.WriteLine("			npc.width = 20;");
					writer.WriteLine("			npc.height = 20;");
					writer.WriteLine("			Main.npcFrameCount[npc.type] = 3;");
					writer.WriteLine("		}");
					writer.WriteLine("	}");
					writer.WriteLine("}");
				}
				using (StreamWriter writer = new StreamWriter("\\Sounds\\Item\\" +"id" +_pokedexEntry.Nat +".cs"))
				{
					writer.WriteLine("using Microsoft.Xna.Framework.Audio;");
					writer.WriteLine("using Terraria;");
					writer.WriteLine("using Terraria.ModLoader;");
					writer.WriteLine("");
					writer.WriteLine("namespace PokeModBlue.Sounds.Item");
					writer.WriteLine("{");
					writer.WriteLine("	public class " +"id" +_pokedexEntry.Nat +" : ModSound");
					writer.WriteLine("	{");
					writer.WriteLine("		public override void PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)");
					writer.WriteLine("		{");
					writer.WriteLine("			soundInstance = sound.CreateInstance();");
					writer.WriteLine("			soundInstance.Volume = volume;");
					writer.WriteLine("			soundInstance.Pan = pan;");
					writer.WriteLine("			soundInstance.Pitch = 1.0f;");
					writer.WriteLine("			Main.PlaySoundInstance(soundInstance);");
					writer.WriteLine("		}");
					writer.WriteLine("	}");
					writer.WriteLine("}");
				}
				using (StreamWriter writer = new StreamWriter("\\Sounds\\NPCKilled\\" +"id" +_pokedexEntry.Nat +".cs"))
				{
					writer.WriteLine("using Microsoft.Xna.Framework.Audio;");
					writer.WriteLine("using Terraria;");
					writer.WriteLine("using Terraria.ModLoader;");
					writer.WriteLine("");
					writer.WriteLine("namespace PokeModBlue.Sounds.NPCKilled");
					writer.WriteLine("{");
					writer.WriteLine("	public class " +"id" +_pokedexEntry.Nat +" : ModSound");
					writer.WriteLine("	{");
					writer.WriteLine("		public override void PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)");
					writer.WriteLine("		{");
					writer.WriteLine("			soundInstance = sound.CreateInstance();");
					writer.WriteLine("			soundInstance.Volume = volume;");
					writer.WriteLine("			soundInstance.Pan = pan;");
					writer.WriteLine("			soundInstance.Pitch = 0.8f;");
					writer.WriteLine("			Main.PlaySoundInstance(soundInstance);");
					writer.WriteLine("		}");
					writer.WriteLine("	}");
					writer.WriteLine("}");
				}
				*/
			}
		}
		
		public static void DoNothing()
		{
			
		}
	}
}