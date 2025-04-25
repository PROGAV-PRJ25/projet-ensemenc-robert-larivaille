public class Oiseau : AnimauxMangeurs
{
    private static int numero=1;
    public Oiseau ( Potager pot) : base( 70, pot, -1, true)
    {
        this.Predateurs.Add("Oiseau");
        numero++;
        this.Nom="Oiseau" + ToString(numero);
    }
}