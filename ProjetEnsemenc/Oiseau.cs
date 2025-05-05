public class Oiseau : AnimauxMangeurs
{
    public Oiseau ( Potager pot) : base( 70, pot, -1, true)
    {
        this.Predateurs.Add("Chat");
        this.Nom="Oiseau";
    }
}