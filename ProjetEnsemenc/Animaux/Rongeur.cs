public class Rongeur : AnimauxMangeurs
{
    public Rongeur(Potager pot, Simulation simu) : base(10, pot, -1, true, simu)
    {
        this.Predateurs.Add("Chat");
        this.Nom = "Rongeur";
    }

}