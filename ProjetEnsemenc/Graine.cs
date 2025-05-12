public class Graine
{
    public string Espece { get; set; }
    public int Quantite { get; set; }

    public Graine(string espece, int quantite)
    {
        Espece = espece;
        Quantite = quantite;
    }
}