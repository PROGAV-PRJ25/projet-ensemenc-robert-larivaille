public abstract class AnimauxMauvais : Animaux
{

    public bool Urgence { get; set; }

    public AnimauxMauvais(int probaApparition, Potager pot, int duree, bool urgence, Simulation simu) : base(probaApparition, pot, duree, simu)
    {
        Urgence = urgence;
    }

}