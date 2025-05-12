public abstract class AnimauxDestructeurs : AnimauxMauvais
{
    public AnimauxDestructeurs(int probaApparition, Potager pot, int duree, bool urgence) : base(probaApparition, pot, duree, urgence)
    { }

    public override void Effet(Plante plante)
    {
        plante.EstMorte();
    }

}