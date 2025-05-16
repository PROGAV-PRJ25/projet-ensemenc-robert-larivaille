public class RemedeMildiou : Remede
{
    public RemedeMildiou() : base("Traitement Mildiou")
    {
    }

    public override void Agir(Maladie maladie, Plante plante)
    {
        if (maladie.Nom == "Mildiou")
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