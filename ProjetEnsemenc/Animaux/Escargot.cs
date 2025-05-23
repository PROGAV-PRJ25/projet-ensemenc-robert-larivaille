public class Escargot : AnimauxMangeurs
{
    public Escargot(Potager pot, Simulation simu) : base(12, pot, 8, false, simu)
    {
        this.Nom = "Escargot";
    }
}