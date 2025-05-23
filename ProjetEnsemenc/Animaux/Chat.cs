public class Chat : AnimauxDestructeurs
{
    public Chat(Potager pot, Simulation simu) : base(7, pot, -1, true, simu)
    {
        this.Nom = "Chat";
    }

    public override void Effet(Plante plante)
    {
        if (plante.Taille == 1)
        {
            plante.EstMorte(); //Le chat écrase les petites plantes
        }
    }


}