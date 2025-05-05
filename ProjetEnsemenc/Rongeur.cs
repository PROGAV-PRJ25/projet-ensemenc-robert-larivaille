public class Rongeur : AnimauxMangeurs
{
    public Rongeur ( Potager pot) : base( 35, pot , -1 , true)
    {
        this.Predateurs.Add("Chat");
        this.Nom="Rongeur";
    }

}