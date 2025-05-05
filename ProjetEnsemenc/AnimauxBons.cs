public abstract class AnimauxBons : Animaux
{
    public AnimauxBons (int probaApparition, Potager pot, int duree) : base( probaApparition, pot, duree)
    {}

    public override void Effet(Plante plante)
    {
        plante.AmelioreSante();
    }
}