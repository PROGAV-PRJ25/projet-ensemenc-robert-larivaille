public class Chat : AnimauxDestructeurs
{
    private static int numero=1;
    public Chat ( Potager pot) : base(  40, pot, -1, true )
    {
        numero++;
        this.Nom="Chat" + ToString(numero);
    }
}