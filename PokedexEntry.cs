namespace PokeModBlue {

    public class PokedexEntry : IPokedexEntry {
        public float Per { get; set; }
        public float Nat { get; set; }
        public int HP { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int SpA { get; set; }
        public int SpD { get; set; }
        public int Spe { get; set; }
        public int Total { get; set; }
        public string Type_I { get; set; }
        public string Type_II { get; set; }
        public string Tier { get; set; }
        public string Ability_I { get; set; }
        public string Ability_II { get; set; }
        public string Hidden_Ability { get; set; }
        public string Mass { get; set; }
        public int LK_GK { get; set; }
        public string EV_Worth { get; set; }
        public int EXPV { get; set; }
        public string Color { get; set; }
        public int? Hatch { get; set; }
        public string Gender { get; set; }
        public string Egg_Group_I { get; set; }
        public string Egg_Group_II { get; set; }
        public int Catch { get; set; }
        public int EXP { get; set; }
        public int? EvolveLevel { get; set; }
        public float? EvolveID { get; set; }
        public float? Joh { get; set; }
        public float? Hoe { get; set; }
        public float? Sin { get; set; }
        public float? Un { get; set; }
        public string Pokemon { get; set; }

        public string LevelingRate() {
            if (EXP == 600000) { return "Erratic"; }
            if (EXP == 800000) { return "Fast"; }
            if (EXP == 1000000) { return "Medium Fast"; }
            if (EXP == 1059860) { return "Medium Slow"; }
            if (EXP == 1250000) { return "Slow"; }
            if (EXP == 1640000) { return "Fluctuating"; }
            return "";
        }

        public string Print() {
            return "Name: " + Pokemon + ", National Pokedex Number: " + Per.ToString() + ", Regional Pokedex Number: " + Nat + ", Base HP: " + HP.ToString() + ", Base Attack: " + Atk.ToString() + ", Base Defense: " + Def.ToString() + ", Base Sp.Attack: "
            + SpA.ToString() + ", Base Sp.Def: " + SpD.ToString() + ", Base Speed: " + Spe.ToString() + ", Total Base Stats: " + Total.ToString() + ", Type I: " + Type_I + ", Type II: " + Type_II + ", Tier: " + Tier + ", Ability I: "
            + Ability_I + ", Ability II: " + Ability_II + ", Hidden Ability: " + Hidden_Ability + ", Mass: " + Mass + ", Low Kick/Grass Knot damage: " + LK_GK.ToString() + ", EV Worth: " + EV_Worth + ", Exp Value: " + EXPV.ToString()
            + ", Pokedex Color: " + Color + ", Hatch: " + Hatch.ToString() + ", Gender" + Gender + ", Egg Group I: " + Egg_Group_I + ", Egg Group II: " + Egg_Group_II + ", Catch Rate: " + Catch.ToString() + ", Leveling Rate: " + LevelingRate();
        }
    }
}