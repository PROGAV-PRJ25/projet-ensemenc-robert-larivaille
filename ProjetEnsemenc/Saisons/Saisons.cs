public class Saisons
{
    public Potager? Pot { get; set; }

    public Saison Nom { get; set; }
    public List<int> Luminosite { get; set; }
    public List<int> Temperature { get; set; }

    public Saisons(Saison nom)
    {
        Nom = nom;
        Luminosite = new List<int>();
        Temperature = new List<int>();
        ChangerBesoinEau();
        ChangerTemperature();
    }

    public void ChangerBesoinEau()
    {
        if (Pot != null && Pot.ListePlantes.Count() != 0)
        {
            if ((Nom == Saison.Hiver) || (Nom == Saison.Automne))
            {
                foreach (Plante plante in Pot.ListePlantes)
                {
                    plante.BesoinEau -= 2;
                }
            }
            else
            {
                foreach (Plante plante in Pot.ListePlantes)
                {
                    plante.BesoinEau += 2;
                }
            }
        }
    }
    public void ChangerTemperature()
    {
        Temperature.Clear();
        Luminosite.Clear();
        if (Nom == Saison.Printemps)
        {
            Temperature.Add(5);
            Temperature.Add(20);
            Luminosite.Add(50);
            Luminosite.Add(80);
        }
        if (Nom == Saison.Ete)
        {
            Temperature.Add(20);
            Temperature.Add(35);
            Luminosite.Add(90);
            Luminosite.Add(100);
        }
        if (Nom == Saison.Automne)
        {
            Temperature.Add(10);
            Temperature.Add(20);
            Luminosite.Add(50);
            Luminosite.Add(75);
        }
        if (Nom == Saison.Hiver)
        {
            Temperature.Add(0);
            Temperature.Add(10);
            Luminosite.Add(20);
            Luminosite.Add(40);
        }
    }

    public int TemperatureDeSaison()
    {
        int temp;
        Random rng = new Random();
        temp = rng.Next(Temperature[Temperature.Count - 2], Temperature[Temperature.Count - 1] + 1);
        return temp;
    }

}