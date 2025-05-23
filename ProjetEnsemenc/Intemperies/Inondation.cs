public class Inondation : Intemperie
{
    public Inondation(Potager pot) : base(pot)
    {
        this.Numero = 2;
        this.Duree = 6;
    }

    public override void EffetIntemperie()
    {
        foreach (Animaux animal in Pot.ListeAnimaux)
        {
            if ((animal.Nom == "Chien") || (animal.Nom == "Escargot")) animal.Disparait();
        }
        foreach (Plante plante in Pot.ListePlantes)
        {
            plante.NiveauHumidite += 10;
        }
    }
}