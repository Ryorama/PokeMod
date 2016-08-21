using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ModLoader;

namespace PokeModBlue.Sounds.Custom
{
	public class ExpFull : ModSound
	{
		public override void PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
		{
			soundInstance = sound.CreateInstance();
			soundInstance.Volume = volume;
			soundInstance.Pan = pan;
			soundInstance.Pitch = 1.0f;
			Main.PlaySoundInstance(soundInstance);
		}
	}
}
