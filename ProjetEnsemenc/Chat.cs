public class Chat : AnimauxDestructeurs
{
    public Chat(Potager pot) : base(40, pot, -1, true)
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