public abstract class AnimauxMangeurs : AnimauxMauvais
{
    public List<Plante> PlantesManges { get; set; }

    public AnimauxMangeurs(int probaApparition, Potager pot, int duree, bool urgence, Simulation simu) : base(probaApparition, pot, duree, urgence, simu)
    {
        PlantesManges = new List<Plante>();
    }

    public override void Effet(Plante plante)
    {
        plante.EstMange();
    }
}