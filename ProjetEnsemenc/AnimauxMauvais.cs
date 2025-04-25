public abstract class AnimauxMauvais : Animaux
{
    public int Criticite { get; set; }

    public bool Urgence { get; set; }

    public AnimauxMauvais ( int probaApparition, Potager pot, int duree, bool urgence) : base( probaApparition, pot, duree)
    {
        Urgence=urgence;
    }
}