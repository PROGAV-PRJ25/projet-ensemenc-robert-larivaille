public class RemedeOidium : Remede
{
    public RemedeOidium() : base("Traitement Oidium")
    {
    }

    public override void Agir(Plante plante, Maladie maladie)
    {
        if (maladie.Nom == "Oidium")
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