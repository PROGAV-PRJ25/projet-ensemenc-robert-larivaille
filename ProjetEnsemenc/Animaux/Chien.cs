public class Chien : AnimauxBons
{
    public Chien(Potager pot, Simulation simu) : base(5, pot, 3000, simu)
    {
        this.Nom = "Chien";
    }

    public override void Effet(Plante plante)
    {
        // Le chien ne fait rien sur la plante
    }
}