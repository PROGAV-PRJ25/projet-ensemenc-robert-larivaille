public class Coccinelle : AnimauxBons
{
    public Coccinelle(Potager pot, Simulation simu) : base(14, pot, 3, simu)
    {
        this.Nom = "Coccinelle";
    }

    public override void Effet(Plante plante)
    {
        // Les coccinelles ne font rien sur la plante, elles chassent les pucerons
    }
}