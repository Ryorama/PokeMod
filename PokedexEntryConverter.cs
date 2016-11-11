using Newtonsoft.Json.Converters;
using System;

namespace PokeModBlue {

    public class PokedexEntryConverter : CustomCreationConverter<IPokedexEntry> {

        public override IPokedexEntry Create(Type objectType) {
            return new PokedexEntry();
        }
    }
}