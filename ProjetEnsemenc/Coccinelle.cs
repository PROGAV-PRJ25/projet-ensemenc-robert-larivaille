public class Coccinelle : AnimauxBons
{
    public Coccinelle ( Potager pot) : base( 55 , pot, 3)
    {
        this.Nom="Coccinelle" ;
    }

    public override void Effet(Plante plante)
    {
        // Les coccinelles ne font rien sur la plante, elles chassent les pucerons
    }
}