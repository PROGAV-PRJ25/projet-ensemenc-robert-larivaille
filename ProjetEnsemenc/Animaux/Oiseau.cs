public class Oiseau : AnimauxMangeurs
{
    public Oiseau(Potager pot, Simulation simu) : base(20, pot, -1, true, simu)
    {
        this.Predateurs.Add("Chat");
        this.Nom = "Oiseau";
    }
}