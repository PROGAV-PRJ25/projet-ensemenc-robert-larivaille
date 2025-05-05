public class RemedeChampignon : Remede
{
    public RemedeChampignon() : base("Fongicide")
    {
    }

    public override void Agir(Plante plante, Maladie maladie)
    {
        if (maladie.Nom == "Champignon")
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