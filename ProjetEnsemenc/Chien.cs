public class Chien : AnimauxBons
{
    public Chien ( Potager pot) : base( 20, pot, 3000 )
    {
        this.Nom="Chien";
    }

    public override void Effet(Plante plante)
    {
        // Le chien ne fait rien sur la plante
    }
}