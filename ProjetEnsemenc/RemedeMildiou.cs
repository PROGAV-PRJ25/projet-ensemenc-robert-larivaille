public class RemedeMildiou : Remede
{
    public RemedeMildiou() : base("Traitement Mildiou")
    {
    }

    public override void Agir(Plante plante, Maladie maladie)
    {
        if (maladie.Nom == "Mildiou")
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