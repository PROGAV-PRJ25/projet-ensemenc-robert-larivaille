public class Taupe : AnimauxDestructeurs
{
    public Taupe ( Potager pot) : base(  25, pot, -1, true )
    {
        this.Nom="Taupe";
    }

    public override void Effet(Plante plante)
    {
        //fait un trou 
    }
}