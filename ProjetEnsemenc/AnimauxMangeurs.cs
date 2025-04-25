public abstract class AnimauxMangeurs : AnimauxMauvais
{
    public List<Plante> PlantesManges { get; set; }

    public AnimauxMangeurs ( int probaApparition, Potager pot, int duree, bool urgence) : base( probaApparition, pot,duree, urgence)
    {
        PlantesManges = new List<Plante>();
    }
}