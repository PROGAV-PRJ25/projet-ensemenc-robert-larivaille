public abstract class Saisons
{
    public Potager Pot { get; set; }
    public string Nom { get; set; }
    public List<int> Luminosite { get; set; }
    public List<int> Temperature { get; set; }

    public Saisons(string nom)
    {
        Nom = nom;
        Luminosite = new List<int>();
        Temperature = new List<int>();
    }

    public void ChangerBesoinEau()
    {
        if ((Nom == "Hiver") || (Nom == "Automne"))
        {
            foreach (Plante plante in Pot.ListePlantes)
            {
                plante.BesoinEau += 2;
            }
        }
        else
        {
            foreach (Plante plante in Pot.ListePlantes)
            {
                plante.BesoinEau -= 2;
            }
        }
    }

}