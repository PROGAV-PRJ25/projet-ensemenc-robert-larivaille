public class Potager
{
    public Saisons Saison { get; set; }
    public int Temperature { get; set; }
    public int Hauteur { get; set; }
    public int Longueur { get; set; }
    public List<Plante> ListePlantes { get; set; }
    public List<Animaux> ListeAnimaux { get; set; }
    public List<Graine> SacDeGraines { get; set; }
    public List<Recolte> Inventaire { get; set; }

    public Potager(int hauteur, int longueur, Saisons saison, int temperature)
    {
        ListePlantes = new List<Plante>();
        ListeAnimaux = new List<Animaux>();
        SacDeGraines = new List<Graine>();
        Inventaire = new List<Recolte>();
        Hauteur = hauteur;
        Longueur = longueur;
        Temperature = temperature;
        Saison = saison;
        SacDeGraines.Add(new Graine("Artichaut", 0));
        SacDeGraines.Add(new Graine("Aubergine", 0));
        SacDeGraines.Add(new Graine("Basilic", 0));
        SacDeGraines.Add(new Graine("Oignon", 0));
        SacDeGraines.Add(new Graine("Olivier", 0));
        SacDeGraines.Add(new Graine("Poivron", 0));
        SacDeGraines.Add(new Graine("Roquette", 0));
        SacDeGraines.Add(new Graine("Thym", 0));
        SacDeGraines.Add(new Graine("Tomate", 0));
    }

    public void EffetArrosageAutomatique()
    {
        foreach (Plante plante in ListePlantes)
        {
            plante.NiveauHumidite = plante.SeuilHumidite;
        }
    }

    public void EffetFertilisant()
    {
        foreach (Plante plante in ListePlantes)
        {
            plante.Fertilise();
        }
    }

    public void EffetArroserTuyau()
    {
        foreach (Plante plante in ListePlantes)
        {
            plante.NiveauHumidite += 5;
        }
    }

    public void EffetLampeUV()
    {
        foreach (Plante plante in ListePlantes)
        {
            plante.NiveauLuminosite=plante.SeuilLuminosite;
        }
    }

    public void EffetSerre()
    {
        foreach (Plante plante in ListePlantes)
        {
            int tempCible = (plante.TemperatureCible[1] + plante.TemperatureCible[0]) / 2;
            plante.NiveauLuminosite=plante.SeuilLuminosite;
            plante.NiveauTemperature = tempCible;
            plante.NiveauHumidite = plante.SeuilHumidite;
        }
    }
}
