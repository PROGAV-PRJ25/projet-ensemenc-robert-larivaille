public class Secheresse : Intemperie
{
    public Secheresse(Potager pot) : base(pot)
    {
        this.Numero = 1;
        this.Duree = 8;
    }

    public override void EffetIntemperie()
    {
        foreach (Animaux animal in Pot.ListeAnimaux)
        {
            if (animal.Nom == "Escargot")
            {
                animal.Disparait();
            }
        }
        foreach (Plante plante in Pot.listePlantes)
        {
            plante.NiveauHumidite -= 10;
        }
    }
}