public class RemedeFusariose : Remede
{
    public RemedeFusariose() : base("Traitement Fusariose")
    {

    }

    public override void Agir(Maladie maladie, Plante plante)
    {
        if (maladie.Nom == "Fusariose")
        {
            plante.EstMaladeDe.Remove(maladie);
            plante.Sante = 100;
        }
        else
        {
            Console.WriteLine($"{Nom} n'a aucun effet sur {maladie.Nom}.");
        }
    }
}