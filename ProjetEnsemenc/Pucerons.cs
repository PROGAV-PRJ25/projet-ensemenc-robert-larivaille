public class Pucerons : AnimauxMangeurs
{
    private static int numero=1;
    public Pucerons ( Potager pot) : base( 65, pot, 3, false )
    {
        this.Predateurs.Add("Coccinelle");
        numero++;
        this.Nom="Pucerons" + ToString(numero);
    }

}