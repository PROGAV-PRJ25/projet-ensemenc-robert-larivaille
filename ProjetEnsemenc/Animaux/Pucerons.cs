public class Pucerons : AnimauxMangeurs
{
    public Pucerons ( Potager pot) : base( 16, pot, 3, false )
    {
        this.Predateurs.Add("Coccinelle");
        this.Nom="Pucerons" ;
    }

}