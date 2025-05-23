public abstract class AnimauxDestructeurs : AnimauxMauvais
{
    public AnimauxDestructeurs(int probaApparition, Potager pot, int duree, bool urgence, Simulation simu) : base(probaApparition, pot, duree, urgence, simu)
    { }

    public override void Effet(Plante plante)
    {
        plante.EstMorte();
    }

}