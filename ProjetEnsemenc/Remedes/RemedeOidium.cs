public class RemedeOidium : Remede
{
    public RemedeOidium() : base("Traitement Oidium")
    {
    }

    public override void Agir(Maladie maladie, Plante plante)
    {
        if (maladie.Nom == "Oidium")
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