public abstract class AnimauxBons : Animaux
{
    public AnimauxBons(int probaApparition, Potager pot, int duree, Simulation simu) : base(probaApparition, pot, duree, simu)
    { }

    public override void Effet(Plante plante)
    {
        plante.AmelioreSante();
    }
}