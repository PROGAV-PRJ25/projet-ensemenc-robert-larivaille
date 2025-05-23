public class Abeille : AnimauxBons
{
    public Abeille(Potager pot, Simulation simu) : base(15, pot, 3000, simu)
    {
        this.Nom = "Abeille";
    }
}