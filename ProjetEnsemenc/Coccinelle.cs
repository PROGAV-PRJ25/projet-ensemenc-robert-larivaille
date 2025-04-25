public class Coccinelle : AnimauxBons
{
    private static int numero=1;
    public Coccinelle ( Potager pot) : base( 55 , pot, 3)
    {
        numero++;
        this.Nom="Coccinelle" + ToString(numero);
    }
}