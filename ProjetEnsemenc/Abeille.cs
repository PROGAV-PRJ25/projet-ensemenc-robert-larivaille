public class Abeille : AnimauxBons
{
    private static int numero=1;
    public Abeille ( Potager pot) : base( 60, pot, 3000 )
    {
        numero++;
        this.Nom="Abeille" + ToString(numero);
    }
}