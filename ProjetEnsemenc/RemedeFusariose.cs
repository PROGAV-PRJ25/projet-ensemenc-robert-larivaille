public class RemedeFusariose : Remede
{
    public RemedeFusariose() : base("Traitement Fusariose")
    {
    }

    public override void Agir(Plante plante, Maladie maladie)
    {
        if (maladie.Nom == "Fusariose")
        {
            plante.estMaladeDe.Remove(maladie);
            plante.Sante = 100;
        }
        else
        {
            Console.WriteLine($"{Nom} n'a aucun effet sur {maladie.Nom}.");
        }
    }
}