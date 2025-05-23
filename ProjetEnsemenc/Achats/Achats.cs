public enum Achat
{
    ArrosageAutomatique,
    Bache,
    Coccinelle,
    Chien,
    Epouvantail,
    Fertilisant,
    Graine,
    LampeUV,
    Pompe,
    Serre,
    TuyauArrosage,
    RemedeFusariose,
    RemedeMildiou,
    RemedeOidium
}

public enum Natures
{
    Animal,
    Graine,
    Objet,
    Remede
}


public class Achats
{
    public Achat Nom { get; set; }
    public Natures Nature { get; set; }
    protected int Numero { get; set; }
    public double Prix { get; set; }
    public bool PrixVariant { get; set; } //Pour certain achat le prix varie selon la taille du potager, pour ceux-ci le prix ci-dessus est donné par case unitaire.

    public Achats(Achat nom, Natures nature, int numero, double prix, bool prixVariant)
    {
        Nom = nom;
        Nature = nature;
        Numero = numero;
        Prix = prix;
        PrixVariant = prixVariant;
    }
}