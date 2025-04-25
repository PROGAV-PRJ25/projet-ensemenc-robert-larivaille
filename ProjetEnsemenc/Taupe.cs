public class Taupe : AnimauxDestructeurs
{
    private static int numero=1;
    public Taupe ( Potager pot) : base(  25, pot, -1, true )
    {
        numero++;
        this.Nom="Taupe" + ToString(numero);
    }
}