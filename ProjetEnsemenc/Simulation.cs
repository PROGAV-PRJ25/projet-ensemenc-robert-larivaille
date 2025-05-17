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
    public double Argent { get; set; }
    public int NumeroTour { get; set; }
    public ModeDeJeu mode { get; set; }
    public List<Achats> achatsPossibles = new List<Achats>();
    public List<int> ListeAchats { get; set; } //Nombre de chaque achat qui n'a pas encore été utilisé pour dans l'odre : Arrosage automatique, Bache, Coccinelle, Chien, Epouvantail, Fertilisant, Graine, LampeUV, Pompe, Serre, tuyau d'arrosage, RemedeFusariose, Remede Mildiou, Remede Oidium

    public bool PresenceChien { get; set; }
    public bool PresenceEpouvantail { get; set; } // Indique si un epouvantail est présent sur le jeu (acheté et posé)
    public bool PresenceArrosageAutomatique { get; set; }
    public bool PresenceLampeUV { get; set; }
    public bool PresenceSerre { get; set; }
    


    public Simulation(int hauteur, int largeur)
    {
        Saisons saison = new Saisons(Saison.Printemps);
        Pot = new Potager(hauteur, largeur, saison, saison.TemperatureDeSaison()); //Rentrer params
        mode = ModeDeJeu.Classique;
        Argent = 1000;
        NumeroTour = 1;
        ListeAchats = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        PresenceChien = false;
        PresenceEpouvantail = false;
        PresenceArrosageAutomatique=false;
        PresenceLampeUV=false;
        PresenceSerre=false;
        achatsPossibles.Add(new ArrosageAutomatique());
        achatsPossibles.Add(new Bache());
        achatsPossibles.Add(new AchatCoccinelle());
        achatsPossibles.Add(new AchatChien());
        achatsPossibles.Add(new Epouvantail());
        achatsPossibles.Add(new Fertilisant());
        achatsPossibles.Add(new AchatGraine());
        achatsPossibles.Add(new LampeUV());
        achatsPossibles.Add(new Pompe());
        achatsPossibles.Add(new Serre());
        achatsPossibles.Add(new TuyauArrosage());
        achatsPossibles.Add(new AchatRemedeFusariose());
        achatsPossibles.Add(new AchatRemedeMildiou());
        achatsPossibles.Add(new AchatRemedeOidium());
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

    public void AjouterAchat(int numero, int quantite)
    {
        ListeAchats[numero] += quantite;
    }

    public void RetirerAchat(int numero, int quantite)
    {
        ListeAchats[numero] -= quantite;
    }

    public int DemanderNombreAchats()
    {
        Console.WriteLine("Combien d'unités voulez-vous acheter ?");
        int nombreUnites = -1;
        string reponse = Console.ReadLine()!;
        while ((!Int32.TryParse(reponse, out nombreUnites)) || (nombreUnites < 0))
        {
            Console.WriteLine("Réponse invalide ");
            Console.WriteLine("Entrez le nombre d'unités.");
        }
        return nombreUnites;
    }


    public bool PayerAchat(double prixTotal)
    {
        Console.WriteLine($"Le prix pour cet achat est {prixTotal}");
        Console.WriteLine($"Vous avez un solde de {Argent}");
        if (Argent - prixTotal < 0)
        {
            Console.WriteLine("Vous n'avez pas assez d'argent pour effectuer l'achat.");
            return false;
        }
        else
        {
            Console.WriteLine("Confirmez l'achat : 1.OUI 2.NON. Entrez 1 ou 2");
            int rep = -1;
            string reponse3 = Console.ReadLine()!;
            while ((!Int32.TryParse(reponse3, out rep)) || ((rep != 1) && (rep != 2)))
            {
                Console.WriteLine("Réponse invalide ");
                Console.WriteLine("Confirmez l'achat : 1.OUI 2.NON. Entrez 1 ou 2.");
            }
            if (rep == 1)
            {
                Argent -= prixTotal;
                Console.WriteLine($"Vous avez maintenant un solde de {Argent}");
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void AcheterGraine()
    {
        int numero = 0;
        Console.WriteLine("Vous possédez les graines suivantes :");
        foreach (Graine graine in Pot.SacDeGraines)
        {
            Console.WriteLine($"- {numero}. {graine.Espece} : {graine.Quantite} unités");
            numero++;
        }
        Console.WriteLine("Quel est le numéro de la graine que vous voulez acheter ? ");
        string reponse = Console.ReadLine()!;
        int numeroAAcheter;
        while (!Int32.TryParse(reponse, out numeroAAcheter) || (numeroAAcheter < 0) || (numeroAAcheter >= Pot.SacDeGraines.Count))
        {
            Console.WriteLine("Vous n'avez pas entré un nombre valide. Quel est le numéro de la graine que vous voulez acheter ? ");
        }
        int nombreUnites = DemanderNombreAchats();
        double prixTotal = 0.20 * nombreUnites;
        bool acheter = PayerAchat(prixTotal);
        if (acheter)
        {
            Pot.SacDeGraines[numeroAAcheter].Quantite += nombreUnites;
        }
    }

    public void EffectuerAchat(int numero)
    {
        Achats achatSouhaite = achatsPossibles[numero];
        if (achatSouhaite.Nature == Natures.Remede)
        {
            Console.WriteLine("Pour information, le remède traite tout le potager lorsqu'on l'utilise");
        }
        double prixUnitaire = 0;
        if (achatSouhaite.PrixVariant)
        {
            prixUnitaire = achatSouhaite.Prix * Pot.Hauteur * Pot.Longueur;
            Console.WriteLine($"Le prix de cet achat dépend de la taille du potager. Pour ce potager, le prix par utilisation est {prixUnitaire}");
        }
        else
        {
            prixUnitaire = achatSouhaite.Prix;
            Console.WriteLine($"Le prix par utilisation est {prixUnitaire}");
        }
        int nombreUnites = 1;
        if (achatSouhaite.Nature != Natures.Graine)
        {
            if (achatSouhaite.Nom != Achat.Chien)
            {
                nombreUnites = DemanderNombreAchats();
            }
            else
            {
                Console.WriteLine("Vous ne pouvez acheter qu'un chien. ");
            }
            double prixTotal = prixUnitaire * nombreUnites;
            bool acheter = PayerAchat(prixTotal);
            if (acheter)
            {
                AjouterAchat(numero, nombreUnites);
                if (achatSouhaite.Nom == Achat.Chien)
                {
                    PresenceChien = true;
                    Chien chienA = new Chien(Pot);
                    Pot.ListeAnimaux.Add(chienA);
                    Console.WriteLine(" Vous possédez maintenant un chien");
                }
            }
        }
        else
        {
            AcheterGraine();
        }
    }

    public void Acheter()
    {
        Console.WriteLine($" Vous possédez {Argent} ");
        Console.WriteLine(" Vous pouvez achetez : \n1. un arrosage automatique, \n2. une bache, \n3. des coccinelle, \n4. un chien, \n5. un epouvantail, \n6. du fertilisant, \n7. des graines, \n8. des lampes UV, \n9. une pompe, \n10. une serre,\n11. un tuyau d'arrosage, \n12. du remede anti fusariose, \n13. du remede anti mildiou, \n14. du remede anti Oidium ");
        Console.WriteLine("Entrez les numéros des achats que vous souhaitez faire un par un. Entrez 1000 pour arreter les achats. ");
        string reponse = Console.ReadLine()!;
        int numeroAAcheter = -1;
        while (numeroAAcheter != 1000)
        {
            while ((!Int32.TryParse(reponse, out numeroAAcheter)) || (numeroAAcheter < 0) || ((numeroAAcheter >= 15) && (numeroAAcheter != 1000)))
            {
                Console.WriteLine("Réponse invalide ");
                Console.WriteLine("Entrez les numéros des achats que vous souhaitez faire un par un. Entrez 1000 pour arreter les achats. ");
            }
            if (numeroAAcheter != 1000)
            {
                if (PresenceChien && (numeroAAcheter - 1 == 3))
                {
                    Console.WriteLine("Vous ne pouvez acheter qu'un chien. Effectuez un autre achat.");
                }
                else
                {
                    EffectuerAchat(numeroAAcheter - 1);
                }
            }
        }
        if (ListeAchats[4] != 0) { PresenceEpouvantail = true; }
    }

    public void PoserAchat()
    {
        int numero = 0;
        Console.WriteLine("Vous possédez les achats suivants :");
        foreach (int nombreAchat in ListeAchats)
        {
            if ((nombreAchat != 0) && (numero != 3) && (numero != 6) && (numero != 1) && (numero != 8) && (numero != 10)) //On ne peut pas poser : chien (3), graine (6), bache (1), pompe (8), tuyau d'arrosage (10)
            {
                Console.WriteLine($"- {numero}. {achatsPossibles[numero].Nom} : {nombreAchat} unités");
            }
            numero++;
        }
        Console.WriteLine("Vous ne pouvez utiliser les baches, pompes et tuyau d'arrosage qu'en cas d'intempéries ; ils n'apparaissent pas dans la liste ci-dessus.");
        Console.WriteLine("Quel est le numéro de l'achat que vous voulez utiliser ? ");
        string reponse = Console.ReadLine()!;
        int numeroAAcheter;
        while (!Int32.TryParse(reponse, out numeroAAcheter) || (numeroAAcheter < 0) || (numeroAAcheter >= Pot.SacDeGraines.Count))
        {
            Console.WriteLine("Vous n'avez pas entré un nombre valide. Quel est le numéro de l'achat que vous voulez utiliser ? ");
        }
        if (numeroAAcheter == 5)
        {
            Pot.EffetFertilisant();
            Console.WriteLine("Vous avez choisi le fertilisant, il a amélioré la production maximum de toutes les plantes du potager.");
        }
        if (numeroAAcheter == 0)
        {
            Pot.EffetArrosageAutomatique();
            Console.WriteLine("Vous avez installé un arrosage automatique.");
        }
        //pas fini

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
            if (reponse == 2) Acheter();
            if (reponse == 3) Arroser();
            if (reponse == 4) Console.WriteLine("ça arrive bientôt tkt");
        }
        while (reponse != 5);
        NumeroTour += 1;
    }

}