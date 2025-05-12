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
    }

}
