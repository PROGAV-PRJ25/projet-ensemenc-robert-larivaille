public class Rongeur : AnimauxMangeurs
{
    private static int numero=1;
    public Rongeur ( Potager pot) : base( 35, pot , -1 , true)
    {
        numero++;
        this.Nom="Rongeur" + ToString(numero);
    }

}