public enum ModeDeJeu
{
    Classique,
    Urgence
}

public enum Terrain
{
    Argile,
    Sable,
    Terre,
    Calcaire,
}
public enum Saison
{
    Printemps,
    Ete,
    Automne,
    Hiver,
}
public class Simulation
{
    public Potager Pot { get; set; }
    public int Argent { get; set; }
    public int NumeroTour { get; set; }
    public ModeDeJeu mode { get; set; }
    public List<int> ListeAchats { get; set; } //Nombre de chaque achat qui n'a pas encore été utilisé pour dans l'odre : 

    public bool PresenceChien { get; set; }
    public bool PresenceEpouvantail { get; set; } // Indique si un epouvantail est présent sur le jeu (acheter et posé)

    public Simulation(int hauteur, int largeur)
    {
        Saisons saison = new Saisons(Saison.Printemps);
        Pot = new Potager(hauteur, largeur, saison, saison.TemperatureDeSaison()); //Rentrer params
        mode = ModeDeJeu.Classique;
        Argent = 1000;
        NumeroTour = 1;
        ListeAchats = new List<int>();
        PresenceChien = false;
        PresenceEpouvantail = false;
    }

    public void CreerPlante(string espece, int x, int y)
    {
        Console.WriteLine("Sur quel terrain voulez-vous la planter ? (Argile, Sable, Terre ou Calcaire)");
        string terrain = Console.ReadLine()!;
        Terrain ter = Terrain.Terre; // Par défaut de la terre
        if (terrain.Equals("Argile", StringComparison.OrdinalIgnoreCase)) ter = Terrain.Argile;
        if (terrain.Equals("Sable", StringComparison.OrdinalIgnoreCase)) ter = Terrain.Sable;
        if (terrain.Equals("Terre", StringComparison.OrdinalIgnoreCase)) ter = Terrain.Terre;
        if (terrain.Equals("Calcaire", StringComparison.OrdinalIgnoreCase)) ter = Terrain.Calcaire;
        if (espece == "Artichaut") Pot.ListePlantes.Add(new Artichaut(x, y, Pot, ter));
        if (espece == "Aubergine") Pot.ListePlantes.Add(new Aubergine(x, y, Pot, ter));
        if (espece == "Basilic") Pot.ListePlantes.Add(new Basilic(x, y, Pot, ter));
        if (espece == "Oignon") Pot.ListePlantes.Add(new Oignon(x, y, Pot, ter));
        if (espece == "Olivier") Pot.ListePlantes.Add(new Olivier(x, y, Pot, ter));
        if (espece == "Poivron") Pot.ListePlantes.Add(new Poivron(x, y, Pot, ter));
        if (espece == "Roquette") Pot.ListePlantes.Add(new Roquette(x, y, Pot, ter));
        if (espece == "Thym") Pot.ListePlantes.Add(new Thym(x, y, Pot, ter));
        if (espece == "Tomate") Pot.ListePlantes.Add(new Tomate(x, y, Pot, ter));
    }

    public Recolte AssocierRecoltePlante(Plante plante, Recolte RecAr, Recolte RecAu, Recolte RecB, Recolte RecO, Recolte RecOl, Recolte RecP, Recolte RecR, Recolte RecTh, Recolte RecTo)
    {
        if (plante.Espece == "Artichaut") return RecAr;
        if (plante.Espece == "Aubergine") return RecAu;
        if (plante.Espece == "Basilic") return RecB;
        if (plante.Espece == "Oignon") return RecO;
        if (plante.Espece == "Olivier") return RecOl;
        if (plante.Espece == "Poivron") return RecP;
        if (plante.Espece == "Roquette") return RecR;
        if (plante.Espece == "Thym") return RecTh;
        else return RecTo;
    }

    public void Planter()
    {
        if (Pot.SacDeGraines.Count == 0)
        {
            Console.WriteLine("Vous ne possédez aucune graine donc vous ne pouvez rien planter. ");
        }
        else
        {
            int numero = 0;
            Console.WriteLine("Vous possédez les graines suivantes :");
            foreach (Graine graine in Pot.SacDeGraines)
            {
                if (graine.Quantite != 0)
                {
                    Console.WriteLine($"- {numero}. {graine.Espece} : {graine.Quantite} unités");
                }
                numero++;
            }
            Console.WriteLine("Quel est le numéro de la graine que vous voulez planter ? ");
            string reponse = Console.ReadLine()!;
            int numeroAPlanter;
            while (!Int32.TryParse(reponse, out numeroAPlanter) || (numeroAPlanter < 0) || (numeroAPlanter >= Pot.SacDeGraines.Count))
            {
                Console.WriteLine("Vous n'avez pas entré un nombre valide. Quel est le numéro de la graine que vous voulez planter ? ");
            }
            Console.WriteLine("A quel numéro de ligne voulez-vous la planter ? ");
            string reponseX = Console.ReadLine()!;
            int x;
            while ((!Int32.TryParse(reponseX, out x)) || (x < 0) || (x >= Pot.Hauteur))
            {
                Console.WriteLine("Vous n'avez pas entré un numéro de ligne valide. Quel est le numéro de la ligne où vous voulez planter ? ");
            }
            Console.WriteLine("A quel numéro de colonne voulez-vous la planter ? ");
            string reponseY = Console.ReadLine()!;
            int y;
            while ((!Int32.TryParse(reponseY, out y)) || (y < 0) || (x >= Pot.Longueur))
            {
                Console.WriteLine("Vous n'avez pas entré un numéro de colonne valide. Quel est le numéro de la colonne où vous voulez planter ? ");
            }
            Pot.SacDeGraines[numeroAPlanter].Quantite--;
            CreerPlante(Pot.SacDeGraines[numeroAPlanter].Espece, x, y);
        }

    }

    public void Arroser()
    {
        Console.WriteLine("Voici l'état d'humidité de vos plantes : ");
        int numero = 0;
        foreach (Plante plante in Pot.ListePlantes)
        {
            Console.WriteLine($"- {numero}. {plante} : niveau actuel : {plante.NiveauHumidite} | niveau optimal : {plante.SeuilHumidite} | diminution de l'humidité par tour : {plante.BesoinEau} ");
            numero++;
        }
        Console.WriteLine("Arroser une plante augmente son niveau d'humidité actuel de 10. ");
        Console.WriteLine("Entrez les numéros des plantes à arroser un par un. Entrez 1000 pour arreter l'arrosage. ");
        string reponse = Console.ReadLine()!;
        int numeroAArroser = -1;
        while (numeroAArroser != 1000)
        {
            while ((!Int32.TryParse(reponse, out numeroAArroser)) || (numeroAArroser < 0) || ((numeroAArroser >= Pot.ListePlantes.Count) && (numeroAArroser != 1000)))
            {
                Console.WriteLine("Réponse invalide ");
                Console.WriteLine("Entrez les numéros des plantes à arroser un par un. Entrez 1000 pour arreter l'arrosage. ");
            }
            if (numeroAArroser != 1000)
            {
                Pot.ListePlantes[numeroAArroser].NiveauHumidite += 10;
            }
        }

    }

    public void VerifierEsperanceDeVie(Plante plante)
    {
        if (plante.EsperanceDeVie > NumeroTour)
            plante.EstMorte();
    }
    public void Simuler(Potager pot)
    {
        // Création des récoltes
        Recolte RecArtichaut = new Recolte("Artichaut", 0);
        Recolte RecAubergine = new Recolte("Aubergine", 0);
        Recolte RecBasilic = new Recolte("Basilic", 0);
        Recolte RecOignon = new Recolte("Oignon", 0);
        Recolte RecOlivier = new Recolte("Olive", 0);
        Recolte RecPoivron = new Recolte("Poivron", 0);
        Recolte RecRoquette = new Recolte("Roquette", 0);
        Recolte RecThym = new Recolte("Thym", 0);
        Recolte RecTomate = new Recolte("Tomate", 0);

        foreach (Plante plante in pot.ListePlantes)
        {
            plante.MettreAJourPlantesAutour();
            VerifierEsperanceDeVie(plante);
            plante.ImpactConditions();
            plante.Contamination();
            if (NumeroTour % plante.TempsCroissance == 0) { plante.Grandir(); }
            plante.DonnerRecolte(pot, AssocierRecoltePlante(plante, RecArtichaut, RecAubergine, RecBasilic, RecOignon, RecOlivier, RecPoivron, RecRoquette, RecThym, RecTomate));
            Console.WriteLine(plante);

        }
        if (NumeroTour == 1) { pot.Saison.Nom = Saison.Printemps; }
        if (NumeroTour == 4) { pot.Saison.Nom = Saison.Ete; }
        if (NumeroTour == 7) { pot.Saison.Nom = Saison.Automne; }
        if (NumeroTour == 10) { pot.Saison.Nom = Saison.Hiver; }
        Pot.Saison.ChangerBesoinEau();
        Pot.Saison.ChangerTemperature();

        int reponse;
        do
        {
            Console.WriteLine("Que voulez-vous faire ?");
            Console.WriteLine("(1) Planter une graine \n(2) Faire un Achat \n(3) Arroser \n(4) Poser un item de votre inventaire \n(5) Avancer dans le temps");
            reponse = Convert.ToInt16(Console.ReadLine()!);
            if (reponse == 1) Planter();
            if (reponse == 2) Console.WriteLine("ça arrive bientôt tkt");
            if (reponse == 3) Arroser();
            if (reponse == 4) Console.WriteLine("ça arrive bientôt tkt");
        }
        while (reponse != 5);
        NumeroTour += 1;
    }

}