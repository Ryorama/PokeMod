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
	public class PokedexEntryConverter : CustomCreationConverter<IPokedexEntry>
	{
		public override IPokedexEntry Create(Type objectType)
		{
			return new PokedexEntry();
		}
	}
}

