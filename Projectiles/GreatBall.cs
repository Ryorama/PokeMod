namespace PokeModBlue.Projectiles {

    public class GreatBall : PokeballProjectile {
        public override float rate { get { return 1.5f; } } //override this with a higher rate for other balls, or a method that checks conditions and returns a value etc.

        public override void SetDefaults() {
            base.SetDefaults();
            projectile.name = "GreatBall";
        }
    }
}