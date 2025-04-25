public class Escargot : AnimauxMangeurs
{
    private static int numero=1;
    public Escargot ( Potager pot) : base( 50 , pot, 8, false)
    {
        numero++;
        this.Nom="Escargot" + ToString(numero);
    }
}