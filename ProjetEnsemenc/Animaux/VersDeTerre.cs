public class VersDeTerre : AnimauxBons
{
    public VersDeTerre(Potager pot, Simulation simu) : base(20, pot, 3000, simu)
    {
        this.Predateurs.Add("Oiseau");
        this.Nom = "VersDeTerre";
    }

    public override void Effet(Plante plante)
    {
        plante.AmelioreTerrain();
    }
}