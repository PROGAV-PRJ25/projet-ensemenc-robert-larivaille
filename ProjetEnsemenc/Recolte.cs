public class Recolte
{
    public string Espece { get; set; }
    public int Quantite { get; set; }

    public Recolte(string espece, int quantite)
    {
        Espece = espece;
        Quantite = quantite;
    }
}