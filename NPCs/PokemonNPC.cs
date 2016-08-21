using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using PokeModBlue.Items.Weapons;
using PokeModBlue.Projectiles.Minions.PokemonProjectiles;

namespace PokeModBlue.NPCs
{
	public abstract class PokemonNPC : ModNPC
	{	
		//properties that need to be synchronized
		// once and then on demand
		public byte capture = 0; //current capture status, 0 if not currently undergoing attempted capture
		public float ballRate = 0; // rate of ball attempting capture currently, 0 if not undergoing attempted captured
		
		// once
		public byte nature;
		public byte HPIV;
		public byte AtkIV;
		public byte DefIV;
		public byte SpAIV;
		public byte SpDIV;
		public byte SpeIV;
		
		// once and then on level up
		public byte level = 1;
		public byte HPEV;
		public byte AtkEV;
		public byte DefEV;
		public byte SpAEV;
		public byte SpDEV;
		public byte SpeEV;
		
		// internals
		public PokemonWeapon pokemon; //does this need to be synced?
		private bool set = true;
        public float movSpeed;
        int combatTextNum;
        //private bool netUpdate = true;

        //internals that are the same for all Pokemon of certain species or can be derived from synchronized properties
        public static Color PokemonText = new Color(255, 255, 255, 255);
        public virtual float id {get; protected set;}
		public virtual float idleAccel {get{return 0.978f;}}
		public virtual float spacingMult {get{return 1f;}}
		public virtual float viewDist {get{return 450f;}}
		public virtual float chaseDist {get{return 75f;}}
		public virtual float chaseAccel {get{return 6f;}}
		public virtual float inertia {get{return 40f;}}
		public virtual float shootCool {get{return 270f;}}
		public virtual float shootSpeed { get { return 12f; } }
        public virtual int shoot {get{return -1;}}
        public virtual float speed { get { return (float)Spe / (float)level / 10f; } }
        public virtual byte aiMode { get { return running; } }

        public int maxHP
		{
			get {
				return ((((2*baseHP)+HPIV+(HPEV/4))*level)/100)+level+10;
			}
		}
		public int Atk
		{
			get {
				return (int)((float)(((((2*baseAtk)+AtkIV+(AtkEV/4))*level)/100)+level+5)*NatureMultipler("Atk"));
			}
		}
		
		public int Def
		{
			get {
				return (int)((float)(((((2*baseDef)+DefIV+(DefEV/4))*level)/100)+level+5)*NatureMultipler("Def"));
			}
		}
		
		public int SpA
		{
			get {
				return (int)((float)(((((2*baseSpA)+SpAIV+(SpAEV/4))*level)/100)+level+5)*NatureMultipler("SpA"));
			}
		}
		
		public int SpD
		{
			get {
				return (int)((float)(((((2*baseSpD)+SpDIV+(SpDEV/4))*level)/100)+level+5)*NatureMultipler("SpD"));
			}
		}
		
		public int Spe
		{
			get {
				return (int)((float)(((((2*baseSpe)+SpeIV+(SpeEV/4))*level)/100)+level+5)*NatureMultipler("Spe"));
			}
		}
		
		// constants
		public const byte Hardy = 1;
		public const byte Lonely = 2;
		public const byte Brave = 3;
		public const byte Adamant = 4;
		public const byte Naughty = 5;
		public const byte Bold = 6;
		public const byte Docile = 7;
		public const byte Relaxed = 8;
		public const byte Impish = 9;
		public const byte Lax = 10;
		public const byte Timid = 11;
		public const byte Hasty = 12;
		public const byte Serious = 13;
		public const byte Jolly = 14;
		public const byte Naive = 15;
		public const byte Modest = 16;
		public const byte Mild = 17;
		public const byte Quiet = 18;
		public const byte Bashful = 19;
		public const byte Rash = 20;
		public const byte Calm = 21;
		public const byte Gentle = 22;
		public const byte Sassy = 23;
		public const byte Careful = 24;
		public const byte Quirky = 25;
		
		public const int erratic = 600000;
		public const int fast = 800000;
		public const int medium_fast = 1000000;
		public const int medium_slow = 1059860;
		public const int slow = 1250000;
		public const int fluctuating = 1640000;
		
		public const byte running = 1;
        public const byte flying = 2;
        public const byte swimming = 3;
        public static float spacing = 10.0f;

        public int catchRate {
			get{
				PokedexEntry val;
				if (Pokedex.pokedex.TryGetValue(id, out val))
				{
					return val.Catch;
				} else {
					return 0;
				}
			}
		}
		
		public int baseHP {
			get{
				PokedexEntry val;
				if (Pokedex.pokedex.TryGetValue(id, out val))
				{
					return val.HP;
				} else {
					return 0;
				}
			}
		}
		public int baseAtk {
			get{
				PokedexEntry val;
				if (Pokedex.pokedex.TryGetValue(id, out val))
				{
					return val.Atk;
				} else {
					return 0;
				}
			}
		}
		public int baseDef {
			get{
				PokedexEntry val;
				if (Pokedex.pokedex.TryGetValue(id, out val))
				{
					return val.Def;
				} else {
					return 0;
				}
			}
		}
		public int baseSpA {
			get{
				PokedexEntry val;
				if (Pokedex.pokedex.TryGetValue(id, out val))
				{
					return val.SpA;
				} else {
					return 0;
				}
			}
		}
		public int baseSpD {
			get{
				PokedexEntry val;
				if (Pokedex.pokedex.TryGetValue(id, out val))
				{
					return val.SpD;
				} else {
					return 0;
				}
			}
		}
		public int baseSpe {
			get{
				PokedexEntry val;
				if (Pokedex.pokedex.TryGetValue(id, out val))
				{
					return val.Spe;
				} else {
					return 0;
				}
			}
		}
		public int EXP {
			get{
				PokedexEntry val;
				if (Pokedex.pokedex.TryGetValue(id, out val))
				{
					return val.EXP;
				} else {
					return 0;
				}
			}
		}
		public int EXPV {
			get{
				PokedexEntry val;
				if (Pokedex.pokedex.TryGetValue(id, out val))
				{
					return val.EXPV;
				} else {
					return 0;
				}
			}
		}
		public List<KeyValuePair<int, string>> EV_Worth {
			get{
				PokedexEntry val;
				List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
				if (Pokedex.pokedex.TryGetValue(id, out val))
				{
					string str = val.EV_Worth;
					for (int i=0; i< str.Length; i++)
					{
						if (Char.IsDigit(str[i]))
						{
							string stat ="";
							stat += str[i+2];
							stat += str[i+3];
							stat += str[i+4];
							list.Add(new KeyValuePair<int, string>(str[i]-'0', stat));
						}
					}
				}
				return list;
			}
		}
		public string Name
		{
			get{
				PokedexEntry val;
				if (Pokedex.pokedex.TryGetValue(id, out val))
				{
					return val.Pokemon;
				} else {
					return "";
				}
			}
		}		
		
		public override void SetDefaults()
		{
			npc.name = Name;
			Random rnd = new Random();
			nature = (byte)rnd.Next(1,25);
			HPIV = (byte)rnd.Next(0,31);
			AtkIV = (byte)rnd.Next(0,31);
			DefIV = (byte)rnd.Next(0,31);
			SpAIV = (byte)rnd.Next(0,31);
			SpDIV = (byte)rnd.Next(0,31);
			SpeIV = (byte)rnd.Next(0,31);
			HPEV = 0;
            AtkEV = 0;
            DefEV = 0;
            SpAEV = 0;
            SpDEV = 0;
            SpeEV = 0;
			int spawnLevel = 2;
			int spawnFactor = 2;
			if (NPC.downedBoss1){spawnLevel+=spawnFactor;}
			if (NPC.downedBoss2){spawnLevel+=spawnFactor;}
			if (NPC.downedBoss3){spawnLevel+=spawnFactor;}
			if (NPC.downedQueenBee){spawnLevel+=spawnFactor;}
			if (Main.hardMode){spawnLevel+=spawnFactor;}
			if (NPC.downedMechBoss1){spawnLevel+=spawnFactor;}
			if (NPC.downedMechBoss2){spawnLevel+=spawnFactor;}
			if (NPC.downedMechBoss3){spawnLevel+=spawnFactor;}
			if (NPC.downedPlantBoss){spawnLevel+=spawnFactor;}
			if (NPC.downedGolemBoss){spawnLevel+=spawnFactor;}
			if (NPC.downedAncientCultist){spawnLevel+=spawnFactor;}			
			level = (byte)rnd.Next(spawnLevel,spawnLevel+spawnLevel/2);
			npc.displayName = npc.name;
			npc.displayName = "Lvl " +level.ToString() +" " +npc.name; //WORKS BUT MAKES DEBUGGING WITH /npc DIFFICULT AS YOU NEED TO TYPE /npc Lvl # Caterpie TO GET A CATERPIE
			npc.friendly = true;
            npc.damage = Atk;
			npc.defense = Def;
			npc.lifeMax = maxHP;
			npc.life = maxHP;
			npc.knockBackResist = 1.0f;
			Main.npcCatchable[mod.NPCType(npc.name)] = true;
			npc.soundHit = mod.GetSoundSlot(SoundType.NPCHit, "Sounds/NPCHit/NormalDamage");
            if (ModLoader.GetMod("PokeModBlueSounds") != null)
            {
                npc.soundKilled = ModLoader.GetMod("PokeModBlueSounds").GetSoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/id" + ((int)id).ToString());
            }
            set = false;

        }
		
        /*
		public override void SendExtraAI(BinaryWriter writer)
		{

		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{

		}
		*/

		public override void AI()
		{
            if (!set && npc.releaseOwner != 255)
            {
                //Main.NewText("Go " + npc.name + "!");
                combatTextNum = CombatText.NewText(new Rectangle((int)Main.player[npc.releaseOwner].position.X, (int)Main.player[npc.releaseOwner].position.Y, npc.width, npc.height), PokemonText, "Go " + npc.name + "!", false, false);
                if (Main.netMode == 2 && combatTextNum != 100)
                {
                    CombatText combatText = Main.combatText[combatTextNum];
                    NetMessage.SendData(81, -1, -1, combatText.text, (int)combatText.color.PackedValue, combatText.position.X, combatText.position.Y, 0f, 0, 0, 0);
                }
                pokemon = Main.player[npc.releaseOwner].inventory[Main.player[npc.releaseOwner].selectedItem].modItem as PokemonWeapon;
                level = pokemon.level;
                nature = pokemon.nature;
                HPIV = pokemon.HPIV;
                AtkIV = pokemon.AtkIV;
                DefIV = pokemon.DefIV;
                SpAIV = pokemon.SpAIV;
                SpDIV = pokemon.SpDIV;
                SpeIV = pokemon.SpeIV;
                HPEV = pokemon.HPEV;
                AtkEV = pokemon.AtkEV;
                DefEV = pokemon.DefEV;
                SpAEV = pokemon.SpAEV;
                SpDEV = pokemon.SpDEV;
                SpeEV = pokemon.SpeEV;
                npc.damage = Atk;
                npc.defense = Def;
                npc.lifeMax = maxHP;
                npc.life = maxHP;
                Main.player[npc.releaseOwner].AddBuff(mod.BuffType(Name+"Buff"), 3600);
                npc.displayName = "Lvl " + level.ToString() + " " + npc.name;
                set = true;
            }
			if (!set && npc.releaseOwner == 255)
			{
				npc.friendly = false;
				set = true;
			}

            if (npc.wet)
            {
                if (aiMode == swimming)
                {
                    movSpeed = speed;
                } else
                {
                    movSpeed = speed / 2f;
                }
            }
            else
            {
				if (aiMode == swimming)
                {
                    movSpeed = speed / 2f;
                } else
                {
                    movSpeed = speed;
                }
            }

            // check on currently on the ground
            bool grounded = false;

            if (npc.velocity.Y == 0.0f)
            {
                int num103 = (int)(npc.position.X + (float)(npc.width / 2)) / 16;
                int num104 = (int)(npc.position.Y + (float)npc.height) / 16 + 1;
                if (WorldGen.SolidTile(num103, num104))
                {
                    grounded = true;
                }
            }

            float distance = 9999999;
            Vector2 targetPos = npc.position;
            Vector2 direction;

            //find target
            if (npc.friendly)
            {
                // if the distance from the player is too great, just instant teleport to the player
                if ((Main.player[npc.releaseOwner].Center - npc.Center).Length() > 1000f)
                {
                    npc.Center = Main.player[npc.releaseOwner].Center;
                    if (aiMode == flying || (aiMode == swimming && npc.wet))
                    {
                        npc.position.Y -= npc.height;
                    }
                }

                // look for a non friendly npc within sight range and set them as the target
                for (int k = 0; k < 200; k++)
                {
                    NPC otherNPC = Main.npc[k];
                    direction = otherNPC.Center - npc.Center;
                    float otherDistance = direction.Length();
                    if (otherNPC.CanBeChasedBy(this, false) && otherDistance < distance && otherDistance < viewDist && Collision.CanHitLine(otherNPC.position, otherNPC.width, otherNPC.height, npc.position, npc.width, npc.height))
                    {
                        distance = otherDistance;
                        targetPos = otherNPC.position;
                        FaceTarget(otherNPC);
                    }
                }
            }
            else //not friendly
            {
                // look for a player or friendly npc within sight range and set them as the target
                // don't have a target? then get one
                if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
                {
                    npc.TargetClosest(true);
                }
                // now check again, if you have one set the location etc.
                if (!(npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active))
                {
                    targetPos = Main.player[npc.target].position;
                    targetPos.Y = targetPos.Y + (Main.player[npc.target].height / 2);
                }
            }


            //attack target
            if (targetPos != npc.position) // if there is a target
            {
                // get the distance between this and it's target
                direction = targetPos - npc.Center;
                // if the distance is less than 'viewDist', the distance this sees enemies from
                if (direction.Length() < viewDist)
                {
                    // then move toward the target
                    //direction.Normalize();
                    //npc.velocity = (npc.velocity * inertia + direction * chaseAccel) / (inertia + 1);
                    if (npc.direction > 0)
                    {
                        npc.velocity.X += movSpeed;
                    }
                    if (npc.direction < 0)
                    {
                        npc.velocity.X -= movSpeed;
                    }

                    if (aiMode == flying || (aiMode == swimming && npc.wet))
                    {
                        if (npc.directionY > 0)
                        {
                            npc.velocity.Y += movSpeed;
                        }
                        if (npc.directionY < 0)
                        {
                            npc.velocity.Y -= movSpeed;
                        }
                    }

                    // jump at targets you can't reach 
                    if (targetPos.Y < npc.position.Y && grounded)
                    {
                        npc.velocity.Y -= movSpeed;
                    }
                }
                else
                {
                    //otherwise slowly come to a halt
                    npc.velocity *= (float)Math.Pow(0.97, 40.0 / inertia);
                }
                /*
                if in melee range
                    use melee attack
                else
                    if spA > atK then
                        use a ranged attack
                        if less than medium distance from owner //means the ranged pokemon will attempt to run away from target while shooting at them, without getting to far to engage, and without just running off on owner
                            maintain ranged attack distance to target
                        endif
                    else
                        move towards target
                    endif
                endif      
                */
            } else
            {
                //idle movement, this is only when there is no valid target to attack
                if (npc.friendly)
                {
                    direction = Main.player[npc.releaseOwner].Center - npc.Center;
                    Vector2 targPos = Main.player[npc.releaseOwner].Center;
                    if (aiMode == flying || (aiMode == swimming && npc.wet))
                    {
                        targPos.Y -= npc.height*2;
                    }
                    FaceTarget(targPos, Main.player[npc.releaseOwner].width, Main.player[npc.releaseOwner].height);
                    distance = direction.Length();

                    if (distance > chaseDist) //TODO here is the issue with flying, once it is within chase distance of player it stops hovering and falls due to gravity
                    {
                        // then walk, swim, dig, fly to player, if there is no line of sight to player, it is permissible to turn off collision
                        // TEMPORARY NO MOVEMENT MODES, BASIC LEFT RIGHT WALKING, NO COLLISION TURNING OFF
                        //direction.Normalize();
                        //npc.velocity = (npc.velocity * inertia + direction * chaseAccel) / (inertia + 1);
                        if (npc.direction > 0)
                        {
                            npc.velocity.X += movSpeed;
                        }
                        if (npc.direction < 0)
                        {
                            npc.velocity.X -= movSpeed;
                        }
                        // flying should keep moving and maintain height regardless of distance
                        if (aiMode == flying || (aiMode == swimming && npc.wet))
                        {
                            if (npc.directionY > 0)
                            {
                                npc.velocity.Y += movSpeed;
                            } //TODO could make this a else if so it can't move up and down in the one cycle thereby hovering, better for rubber banding
                            if (npc.directionY < 0)
                            {
                                npc.velocity.Y -= movSpeed;
                            }
                        }
                    } else
                    {
                        // flying should keep moving and maintain height regardless of distance
                        if (aiMode == flying || (aiMode == swimming && npc.wet))
                        {
                            if (npc.directionY > 0)
                            {
                                npc.velocity.Y += movSpeed;
                            } //TODO could make this a else if so it can't move up and down in the one cycle thereby hovering, better for rubber banding
                            if (npc.directionY < 0)
                            {
                                npc.velocity.Y -= movSpeed;
                            }
                        }
                    }

                }
                else // not friendly
                {
                    // walk, swim, dig, fly around
                    // NOT USED YET AS ALL NPCS CAN SEE ANYONE IF THEY EXIST, NOT JUST IN A CERTAIN RANGE
                }
            }

            // climb up stairs code
            bool flag = false;
            bool flag2 = false;
            Collision.StepUp(ref npc.position, ref npc.velocity, npc.width, npc.height, ref npc.stepSpeed, ref npc.gfxOffY, 1, false, 0);
            if (npc.velocity.Y == 0f)
            {
                if (npc.velocity.X < 0f || npc.velocity.X > 0f)
                {
                    int num102 = (int)(npc.position.X + (float)(npc.width / 2)) / 16;
                    int j3 = (int)(npc.position.Y + (float)(npc.height / 2)) / 16 + 1;
                    if (flag)
                    {
                        num102--;
                    }
                    if (flag2)
                    {
                        num102++;
                    }
                    WorldGen.SolidTile(num102, j3);
                }
                int num103 = (int)(npc.position.X + (float)(npc.width / 2)) / 16;
                int num104 = (int)(npc.position.Y + (float)npc.height) / 16 + 1;
                if (WorldGen.SolidTile(num103, num104) || Main.tile[num103, num104].halfBrick() || Main.tile[num103, num104].slope() > 0 || npc.type == 200)
                {
                    if (npc.type == 200)
                    {
                        npc.velocity.Y = -3.1f;
                    }
                    else
                    {
                        try
                        {
                            num103 = (int)(npc.position.X + (float)(npc.width / 2)) / 16;
                            num104 = (int)(npc.position.Y + (float)(npc.height / 2)) / 16;
                            if (flag)
                            {
                                num103--;
                            }
                            if (flag2)
                            {
                                num103++;
                            }
                            num103 += (int)npc.velocity.X;
                            if (!WorldGen.SolidTile(num103, num104 - 1) && !WorldGen.SolidTile(num103, num104 - 2))
                            {
                                npc.velocity.Y = -5.1f;
                            }
                            else if (!WorldGen.SolidTile(num103, num104 - 2))
                            {
                                npc.velocity.Y = -7.1f;
                            }
                            else if (WorldGen.SolidTile(num103, num104 - 5))
                            {
                                npc.velocity.Y = -11.1f;
                            }
                            else if (WorldGen.SolidTile(num103, num104 - 4))
                            {
                                npc.velocity.Y = -10.1f;
                            }
                            else
                            {
                                npc.velocity.Y = -9.1f;
                            }
                        }
                        catch
                        {
                            npc.velocity.Y = -9.1f;
                        }
                    }
                }
            }

            npc.rotation = npc.velocity.X * 0.05f;
            if (npc.velocity.X > 0f)
            {
                npc.spriteDirection = (npc.direction = 1);
            }
            else if (npc.velocity.X < 0f)
            {
                npc.spriteDirection = (npc.direction = -1);
            }

            if (capture > 0)
            {
                Capture();
            }
            CreateDust();
        }
		
        public void Capture()
        {
            // see http://bulbapedia.bulbagarden.net/wiki/Catch_rate for forumla
            float bonusStatus = 1.0f;
            int a = (int)((((3f * (float)npc.lifeMax - (2f * (float)npc.life)) * (float)catchRate * ballRate) / (3f * (float)npc.lifeMax)) * bonusStatus);
            if (a > 255 || ballRate == 255) { a = 255; }
            int b = (int)((double)1048560 / Math.Sqrt(Math.Sqrt((double)16711680 / (double)a)));
            Random rnd = new Random();
            for (int i=0; i < 3; i++)
            {
                if (rnd.Next(0, 65535) >= b)
                {
                    if (i==0)
                    {
                        //Main.NewText("Oh no! The Pokémon broke free!");
                        combatTextNum = CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), PokemonText, "Oh no! The Pokémon broke free!", false, false);
                        if (Main.netMode == 2 && combatTextNum != 100)
                        {
                            CombatText combatText = Main.combatText[combatTextNum];
                            NetMessage.SendData(81, -1, -1, combatText.text, (int)combatText.color.PackedValue, combatText.position.X, combatText.position.Y, 0f, 0, 0, 0);
                        }
                        capture = 0;
                        return;
                    } else if (i==1)
                    {
                        //Main.NewText("Aww! It appeared to be caught!");
                        combatTextNum = CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), PokemonText, "Aww! It appeared to be caught!", false, false);
                        if (Main.netMode == 2 && combatTextNum != 100)
                        {
                            CombatText combatText = Main.combatText[combatTextNum];
                            NetMessage.SendData(81, -1, -1, combatText.text, (int)combatText.color.PackedValue, combatText.position.X, combatText.position.Y, 0f, 0, 0, 0);
                        }
                        capture = 0;
                        return;
                    } else if (i==2)
                    {
                        //Main.NewText("Aargh! Almost had it!");
                        combatTextNum = CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), PokemonText, "Aargh! Almost had it!", false, false);
                        if (Main.netMode == 2 && combatTextNum != 100)
                        {
                            CombatText combatText = Main.combatText[combatTextNum];
                            NetMessage.SendData(81, -1, -1, combatText.text, (int)combatText.color.PackedValue, combatText.position.X, combatText.position.Y, 0f, 0, 0, 0);
                        }
                        capture = 0;
                        return;
                    } else if (i == 3)
                    {
                        //Main.NewText("Gah! It was so close, too!");
                        combatTextNum = CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), PokemonText, "Gah! It was so close, too!", false, false);
                        if (Main.netMode == 2 && combatTextNum != 100)
                        {
                            CombatText combatText = Main.combatText[combatTextNum];
                            NetMessage.SendData(81, -1, -1, combatText.text, (int)combatText.color.PackedValue, combatText.position.X, combatText.position.Y, 0f, 0, 0, 0);
                        }
                        capture = 0;
                        return;
                    }
                }
            }
            //Main.NewText("Gotcha! " +npc.name +" was caught!");
            combatTextNum = CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), PokemonText, "Gotcha! " + npc.name + " was caught!", false, false);
            if (Main.netMode == 2 && combatTextNum != 100)
            {
                CombatText combatText = Main.combatText[combatTextNum];
                NetMessage.SendData(81, -1, -1, combatText.text, (int)combatText.color.PackedValue, combatText.position.X, combatText.position.Y, 0f, 0, 0, 0);
            }
            int itemRef = Item.NewItem((int)npc.position.X, (int)npc.position.Y, 1, 1, mod.ItemType(Name+"Pokeball"), 1, false, 0, false, false);
            PokemonWeapon newItem;
            newItem = Main.item[itemRef].modItem as PokemonWeapon;
            newItem.SetDefaults();
            newItem.level = this.level;
            newItem.nature = this.nature;
            newItem.experience = 0;
            newItem.HPIV = this.HPIV;
            newItem.HPEV = this.HPEV;
            newItem.AtkIV = this.AtkIV;
            newItem.AtkEV = this.AtkEV;
            newItem.DefIV = this.DefIV;
            newItem.DefEV = this.DefEV;
            newItem.SpAIV = this.SpAIV;
            newItem.SpAEV = this.SpAEV;
            newItem.SpDIV = this.SpDIV;
            newItem.SpDEV = this.SpDEV;
            newItem.SpeIV = this.SpeIV;
            newItem.SpeEV = this.SpeEV;
            newItem.SetToolTip();
            capture = 0;
            npc.active = false;
        }

		public override bool CheckDead()
		{
			if (set && npc.releaseOwner != 255 && Main.player[npc.releaseOwner].HasBuff(mod.BuffType(Name+"Buff")) > -1)
			{
				//Main.NewText(npc.name +" has fainted!");
                combatTextNum = CombatText.NewText(new Rectangle((int)Main.player[npc.releaseOwner].position.X, (int)Main.player[npc.releaseOwner].position.Y, npc.width, npc.height), PokemonText, npc.name + " has fainted!", false, false);
                if (Main.netMode == 2 && combatTextNum != 100)
                {
                    CombatText combatText = Main.combatText[combatTextNum];
                    NetMessage.SendData(81, -1, -1, combatText.text, (int)combatText.color.PackedValue, combatText.position.X, combatText.position.Y, 0f, 0, 0, 0);
                }
                Main.player[npc.releaseOwner].DelBuff(Main.player[npc.releaseOwner].HasBuff(mod.BuffType(Name+"Buff")));
			}
			return true;
		}

        public override bool CheckActive()
		{
			if (set && npc.active && npc.releaseOwner != 255 && Main.player[npc.releaseOwner].HasBuff(mod.BuffType(Name+"Buff")) < 0)
			{
				//Main.NewText(npc.name +", ok! Come back!");
                combatTextNum = CombatText.NewText(new Rectangle((int)Main.player[npc.releaseOwner].position.X, (int)Main.player[npc.releaseOwner].position.Y, npc.width, npc.height), PokemonText, npc.name + ", ok! Come back!", false, false);
                if (Main.netMode == 2 && combatTextNum != 100)
                {
                    CombatText combatText = Main.combatText[combatTextNum];
                    NetMessage.SendData(81, -1, -1, combatText.text, (int)combatText.color.PackedValue, combatText.position.X, combatText.position.Y, 0f, 0, 0, 0);
                }
				npc.life = 0;
				npc.active = false;
				npc.netUpdate = true;
			}
			if (npc.friendly)
			{
				return false; //doesn't count against max npc limit near players (as they are player summoned and shouldn't reduce total enemy npcs)
			}
			return true;
		}
		
		public override void FindFrame(int frameHeight)
		{
			if (Math.Abs(npc.velocity.X) > 1.0f)
			{
				npc.frameCounter+=1f;
				npc.frame.Y = frameHeight * ((int)(npc.frameCounter/7) % Main.npcFrameCount[npc.type]);
			} else {
				npc.frameCounter = 0;
				npc.frame.Y = 0;
			}
			
		}
		
		public virtual void CreateDust()
		{
		}
		
		public int GetRangedDamage()
		{
			return SpA;
		}
		
		public float GetKnockback()
		{
			return 1.0f;
		}

        public void FaceTarget(NPC target)
        {
            npc.targetRect = new Rectangle((int)target.position.X, (int)target.position.Y, (int)target.width, (int)target.height);
            npc.direction = 1;
            if ((float)(npc.targetRect.X + npc.targetRect.Width / 2) < npc.position.X + (float)(npc.width / 2))
            {
                npc.direction = -1;
            }
            npc.directionY = 1;
            if ((float)(npc.targetRect.Y + npc.targetRect.Height / 2) < npc.position.Y + (float)(npc.height / 2))
            {
                npc.directionY = -1;
            }
            if (npc.confused)
			{
				npc.direction *= -1;
			}
            if ((npc.direction != npc.oldDirection || npc.directionY != npc.oldDirectionY || npc.target != npc.oldTarget) && !npc.collideX && !npc.collideY)
            {
                npc.netUpdate = true;
            }
        }

        public void FaceTarget(Vector2 pos, float width, float height)
        {
            npc.targetRect = new Rectangle((int)pos.X, (int)pos.Y, (int)width, (int)height);
            npc.direction = 1;
            if ((float)(npc.targetRect.X + npc.targetRect.Width / 2) < npc.position.X + (float)(npc.width / 2))
            {
                npc.direction = -1;
            }
            npc.directionY = 1;
            if ((float)(npc.targetRect.Y + npc.targetRect.Height / 2) < npc.position.Y + (float)(npc.height / 2))
            {
                npc.directionY = -1;
            }
            if (npc.confused)
            {
                npc.direction *= -1;
            }
            if ((npc.direction != npc.oldDirection || npc.directionY != npc.oldDirectionY || npc.target != npc.oldTarget) && !npc.collideX && !npc.collideY)
            {
                npc.netUpdate = true;
            }
        }

        public float NatureMultipler(string stat)
		{
			if (nature == 1 || nature == 7 || nature == 13 || nature == 19 || nature == 25){
				return 1f;
			} else if (stat == "Atk") {
				if (nature == 2 || nature == 3 || nature == 4 || nature == 5) {return 1.10f;}
				else if (nature == 6 || nature == 11 || nature == 16 || nature == 21) {return 0.9f;}
			} else if (stat == "Def") {
				if (nature == 6 || nature == 8 || nature == 9 || nature == 10) {return 1.10f;}
				else if (nature == 2 || nature == 12 || nature == 17 || nature == 22) {return 0.9f;}
			} else if (stat == "SpA") {
				if (nature == 16 || nature == 17 || nature == 18 || nature == 20) {return 1.10f;}
				else if (nature == 4 || nature == 9 || nature == 14 || nature == 24) {return 0.9f;}
			} else if (stat == "SpD") {
				if (nature == 21 || nature == 22 || nature == 23 || nature == 24) {return 1.10f;}
				else if (nature == 5 || nature == 10 || nature == 15 || nature == 20) {return 0.9f;}
			} else if (stat == "Spe") {
				if (nature == 11 || nature == 12 || nature == 14 || nature == 15) {return 1.10f;}
				else if (nature == 3 || nature == 8 || nature == 18 || nature == 23) {return 0.9f;}
			}
			return 1f;
		}
		
		public int GetExpForLevel(int level)
		{
			if (EXP == erratic)
			{
				if (level <= 50)
				{
					return (((level * level * level) * (100 - level)) / 50);
				} else if (level > 50 && level <= 68)
				{
					return (((level * level * level) * (150 - level)) / 100);
				} else if (level > 68 && level <= 98) 
				{
					return (((level * level * level) * (1911 - (10 * level))) / 3);
				} else if (level > 98 && level <= 100)
				{
					return (((level * level * level) * (160 - level)) / 100);
				}
			} else if (EXP == fast)
			{
				return (  ( 4 * ( level * level * level) ) / 5 );
			} else if (EXP == medium_fast)
			{
				return ( level * level * level);
			} else if (EXP == medium_slow)
			{
				return (((6 / 5)*(level * level * level)) - (15 * (level * level)) + (100*level) - 140);
			} else if (EXP == slow)
			{
				return ((5 * (level * level * level)) / 4);
			} else if (EXP == fluctuating)
			{
				if (level <= 15)
				{
					return ((level * level * level) * ((((level+1)/3)+24)/50));
				} else if (level > 15 && level <= 36)
				{
					return ((level * level * level) * ((level+14)/50));
				} else if (level > 36 && level <= 100) 
				{
					return ((level * level * level) * (((level/2)+32)/50));
				}
			}
			return 999999999;
		}
		
		public string GetNatureString()
		{
			switch (nature)
			{
				case 1:
					return "Hardy";
				case 2:
					return "Lonely";
				case 3:
					return "Brave";
				case 4:
					return "Adamant";
				case 5:
					return "Naughty";
				case 6:
					return "Bold";
				case 7:
					return "Docile";
				case 8:
					return "Relaxed";
				case 9:
					return "Impish";
				case 10:
					return "Lax";
				case 11:
					return "Timid";
				case 12:
					return "Hasty";
				case 13:
					return "Serious";
				case 14:
					return "Jolly";
				case 15:
					return "Naive";
				case 16:
					return "Modest";
				case 17:
					return "Mild";
				case 18:
					return "Quiet";
				case 19:
					return "Bashful";
				case 20:
					return "Rash";
				case 21:
					return "Calm";
				case 22:
					return "Gentle";
				case 23:
					return "Sassy";
				case 24:
					return "Careful";
				case 25:
					return "Quirky";
				default:
					return "Hardy";
			}
		}
		
		public string StatLine()
		{
			return "HP: " +maxHP.ToString() +", IV: " +HPIV.ToString() +", EV: " +HPEV.ToString() + " Attack: " +Atk.ToString() +", IV: " +AtkIV.ToString() +", EV: " +AtkEV.ToString() +" Defense: " +Def.ToString() +", IV: " +DefIV.ToString() +", EV: " +DefEV.ToString() +System.Environment.NewLine +"Special Attack: " +SpA.ToString() +", IV: " +SpAIV.ToString() +", EV: " +SpAEV.ToString() + " Special Defense: " +SpD.ToString() +", IV: " +SpDIV.ToString() +", EV: " +SpDEV.ToString() + " Speed: " +Spe.ToString() +", IV: " +SpeIV.ToString() +", EV: " +SpeEV.ToString();
		}
		
		// Acts as a multiplier to reduce or increase all Pokemon spawns
		public override float CanSpawn(NPCSpawnInfo spawnInfo)
		{
            if (PokeModBlue.pokeSpawns == 3)
            {
                return 0f;
            } else if (catchRate > 235)
			{
				return ((float)catchRate/255f)/16f;
			} else if (catchRate <= 235 && catchRate >= 190)
			{
				if (NPC.downedBoss1)
				{
					return ((float)catchRate/255f)/16f;
				} else {
					return 0f;
				}
			} else if (catchRate < 190 && catchRate >= 150)
			{
				if (NPC.downedBoss2)
				{
					return ((float)catchRate/255f)/16f;
				} else {
					return 0f;
				}
			} else if (catchRate < 150 && catchRate >= 100)
			{
				if (NPC.downedBoss3)
				{
					return ((float)catchRate/255f)/16f;
				} else {
					return 0f;
				}
			} else if (catchRate < 100 && catchRate >= 80)
			{
				if (Main.hardMode)
				{
					return ((float)catchRate/255f)/16f;
				} else {
					return 0f;
				}
			} else if (catchRate < 80 && catchRate >= 60)
			{
				if (NPC.downedMechBoss1)
				{
					return ((float)catchRate/255f)/16f;
				} else {
					return 0f;
				}
			} else if (catchRate < 60 && catchRate >= 40)
			{
				if (NPC.downedMechBoss2)
				{
					return ((float)catchRate/255f)/16f;
				} else {
					return 0f;
				}
			} else if (catchRate < 40 && catchRate >= 30)
			{
				if (NPC.downedMechBoss3)
				{
					return ((float)catchRate/255f)/16f;
				} else {
					return 0f;
				}
			} else if (catchRate < 30 && catchRate >= 20)
			{
				if (NPC.downedPlantBoss)
				{
					return ((float)catchRate/255f)/16f;
				} else {
					return 0f;
				}
			} else if (catchRate < 20 && catchRate >= 10)
			{
				if (NPC.downedAncientCultist)
				{
					return ((float)catchRate/255f)/16f;
				} else {
					return 0f;
				}
			} else {
				return 0f;
			}
		}
	}
}