public class Chien : AnimauxBons
{
    private static int numero=1;
    public Chien ( Potager pot) : base( 20, pot, 3000 )
    {
        numero++;
        this.Nom="Chien" + ToString(numero);
    }

}