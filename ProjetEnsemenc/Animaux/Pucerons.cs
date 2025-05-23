public class Pucerons : AnimauxMangeurs
{
    public Pucerons(Potager pot, Simulation simu) : base(16, pot, 3, false, simu)
    {
        this.Predateurs.Add("Coccinelle");
        this.Nom = "Pucerons";
    }

}