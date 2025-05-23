using System.Runtime.InteropServices.Marshalling;

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
public enum ChoixOuiNon
{
    Oui,
    Non,
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
    public bool PresenceBache { get; set; }
    public bool PresencePompe { get; set; }
    public bool Grele { get; set; }
    public bool Inondation { get; set; }
    public bool Secheresse { get; set; }

    public ActionUrgente ActionUrgente { get; set; }



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
        PresenceArrosageAutomatique = false;
        PresenceLampeUV = false;
        PresenceSerre = false;
        PresenceBache = false;
        PresencePompe = false;
        Grele = false;
        Inondation = false;
        Secheresse = false;
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
        ActionUrgente = new ActionUrgente();

    }

    //Animaux :  
    public void CreerAnimal(string nom)
    {
        Animaux nouveau;
        if (nom == "Abeille") nouveau = new Abeille(Pot);
        else if (nom == "Chat") nouveau = new Chat(Pot);
        else if (nom == "Chien")
        {
            nouveau = new Chien(Pot);
            PresenceChien = true;
        }
        else if (nom == "Coccinelle") nouveau = new Coccinelle(Pot);
        else if (nom == "Escargot") nouveau = new Escargot(Pot);
        else if (nom == "Oiseau") nouveau = new Oiseau(Pot);
        else if (nom == "Pucerons") nouveau = new Pucerons(Pot);
        else if (nom == "Rongeur") nouveau = new Rongeur(Pot);
        else nouveau = new VersDeTerre(Pot);
        nouveau.Duree += NumeroTour;
        Pot.ListeAnimaux.Add(nouveau);
        nouveau.EstMange();
    }

    public void ApparaitreHasardAnimal()
    {
        Random rng = new Random();
        int[] tableauProbabilites = new int[] { 5, 12, 14, 15, 16, 20 }; // Tableau qui contient les valeurs des probabilités d'apparition des Animaux dans l'ordre du tableau ci-dessous.
        string[] tableauAnimaux = new string[] { "Chien", "Escargot", "Coccinelle", "Abeille", "Pucerons", "VersDeTerre" };
        for (int i = 0; i < 6; i++)
        {
            int tirage = rng.Next(0, 101);
            if (tirage < tableauProbabilites[i])
            {
                string ani = tableauAnimaux[i];
                CreerAnimal(ani);
                if (ani == "VersDeTerre") ani = "Vers de terre";
                Console.WriteLine($"Un nouvel animal est apparu : {ani}");
            }
        }
    }

    public void PoserCoccinelle() //Cas particulier des coccinelles qui peuvent être achetées et posées sur la case souhaitée.
    {
        Console.WriteLine("A quel numéro de ligne voulez-vous poser vos coccinelles ?");
        string reponseX = Console.ReadLine()!;
        int x;
        while (!int.TryParse(reponseX, out x) || (x < 0) || (x >= Pot.Hauteur))
        {
            Console.WriteLine("Vous n'avez pas entré un numéro de ligne valide. Quel est le numéro de la ligne où vous voulez poser vos coccinelles ? ");
            reponseX = Console.ReadLine()!;
        }
        Console.WriteLine("A quel numéro de colonne voulez-vous poser vos coccinelles ? ");
        string reponseY = Console.ReadLine()!;
        int y;
        while (!int.TryParse(reponseY, out y) || (y < 0) || (y >= Pot.Longueur))
        {
            Console.WriteLine("Vous n'avez pas entré un numéro de colonne valide. Quel est le numéro de la colonne où vous voulez poser vos coccinelles ? ");
            reponseY = Console.ReadLine()!;
        }
        Coccinelle c = new Coccinelle(Pot);
        c.X = x;
        c.Y = y;
        Pot.ListeAnimaux.Add(c);
    }

    public void EvolutionAnimaux()
    {
        foreach (Animaux animal in Pot.ListeAnimaux)
        {
            if (animal.Duree == NumeroTour) animal.Disparait();
            if ((animal.Nom != "Pucerons") && (animal.Nom != "VersDeTerre") && (animal.Nom != "Escargot")) animal.SeDeplacer();
            animal.EstMange();
            foreach (Plante plante in Pot.ListePlantes)
            {
                if ((plante.CoorX != -1) && (plante.CoorY != -1) && (plante.CoorX == animal.Y) && (plante.CoorY == animal.X))
                {
                    animal.Effet(plante);
                }
            }
        }
    }

    //Plantes, Graines, Recoltes :
    public void CreerPlante(string espece, int y, int x, Simulation simu)
    {
        Console.WriteLine("Sur quel terrain voulez-vous la planter ? (Argile, Sable, Terre ou Calcaire)");
        string terrain = Console.ReadLine()!;
        Terrain ter;
        while (!Enum.TryParse<Terrain>(terrain, true, out ter))
        {
            Console.WriteLine("Entrée invalide. Veuillez saisir un terrain valide (Argile, Sable, Terre ou Calcaire) :");
            terrain = Console.ReadLine()!;
        }
        if (espece == "Artichaut") Pot.ListePlantes.Add(new Artichaut(y, x, Pot, ter, simu));
        else if (espece == "Aubergine") Pot.ListePlantes.Add(new Aubergine(y, x, Pot, ter, simu));
        if (espece == "Basilic") Pot.ListePlantes.Add(new Basilic(y, x, Pot, ter, simu));
        if (espece == "Oignon") Pot.ListePlantes.Add(new Oignon(y, x, Pot, ter, simu));
        if (espece == "Olivier") Pot.ListePlantes.Add(new Olivier(y, x, Pot, ter, simu));
        if (espece == "Poivron") Pot.ListePlantes.Add(new Poivron(y, x, Pot, ter, simu));
        if (espece == "Roquette") Pot.ListePlantes.Add(new Roquette(y, x, Pot, ter, simu));
        if (espece == "Thym") Pot.ListePlantes.Add(new Thym(y, x, Pot, ter, simu));
        if (espece == "Tomate") Pot.ListePlantes.Add(new Tomate(y, x, Pot, ter, simu));
    }

    public Recolte AssocierRecoltePlante(Plante plante, Recolte RecAr, Recolte RecAu, Recolte RecB, Recolte RecO, Recolte RecOl, Recolte RecP, Recolte RecR, Recolte RecTh, Recolte RecTo)
    {
        if (plante.Espece == "Artichaut") return RecAr;
        else if (plante.Espece == "Aubergine") return RecAu;
        else if (plante.Espece == "Basilic") return RecB;
        else if (plante.Espece == "Oignon") return RecO;
        else if (plante.Espece == "Olivier") return RecOl;
        else if (plante.Espece == "Poivron") return RecP;
        else if (plante.Espece == "Roquette") return RecR;
        else if (plante.Espece == "Thym") return RecTh;
        else return RecTo;
    }
    public Plante AssocierGrainePlante(Graine graine)
    {
        switch (graine.Espece)
        {
            case "Artichaut": return new Artichaut(); //Instances créés juste pour accéder aux propriétés
            case "Aubergine": return new Aubergine();
            case "Basilic": return new Basilic();
            case "Oignon": return new Oignon();
            case "Olivier": return new Olivier();
            case "Poivron": return new Poivron();
            case "Roquette": return new Roquette();
            case "Thym": return new Thym();
            case "Tomate": return new Tomate();
            default: return null;
        }
    }

    public void Planter(Simulation simu)
    {
        bool presenceGraine = false;
        foreach (Graine graine in Pot.SacDeGraines)
        {
            Plante plante = AssocierGrainePlante(graine);
            if (plante != null && plante.SaisondeSemis == Pot.Saison.Nom)
            {
                if (graine.Quantite != 0)
                {
                    presenceGraine = true;
                }
            }
        }
        if (!presenceGraine)
        {
            Console.WriteLine("Vous ne possédez aucune graine que vous pouvez planter en cette saison. ");
        }
        else
        {
            int numero = 0;
            Console.WriteLine("Vous possédez et pouvez planter les graines suivantes en cette saison :");
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
            int numeroAPlanter = -1;
            bool saisieInt = false;
            bool grainePossible = false;
            while (!saisieInt || !grainePossible)
            {
                if (!int.TryParse(reponse, out numeroAPlanter) || numeroAPlanter < 0 || numeroAPlanter >= Pot.SacDeGraines.Count)
                {
                    Console.WriteLine("Vous n'avez pas entré un nombre valide. Quel est le numéro de la graine que vous voulez planter ? ");
                    reponse = Console.ReadLine()!;
                    saisieInt = false;
                }
                else saisieInt = true;
                if (Pot.SacDeGraines[numeroAPlanter].Quantite == 0)
                {
                    Console.WriteLine("Vous ne possédez pas cette graine, choisissez une graine de la liste :");
                    reponse = Console.ReadLine()!;
                    grainePossible = false;
                }
                else grainePossible = true;

            }
            Console.WriteLine("A quel numéro de colonne voulez-vous la planter ? ");
            string reponseX = Console.ReadLine()!;
            int x;
            while (!int.TryParse(reponseX, out x) || (x < 0) || (x >= Pot.Longueur))
            {
                Console.WriteLine("Vous n'avez pas entré un numéro de colonne valide. Quel est le numéro de la ligne où vous voulez planter ? ");
                reponseX = Console.ReadLine()!;
            }
            Console.WriteLine("A quel numéro de ligne voulez-vous la planter ? ");
            string reponseY = Console.ReadLine()!;
            int y;
            while (!int.TryParse(reponseY, out y) || (y < 0) || (y >= Pot.Hauteur))
            {
                Console.WriteLine("Vous n'avez pas entré un numéro de ligne valide. Quel est le numéro de la colonne où vous voulez planter ? ");
                reponseY = Console.ReadLine()!;
            }
            Pot.SacDeGraines[numeroAPlanter].Quantite--;
            CreerPlante(Pot.SacDeGraines[numeroAPlanter].Espece, y, x, simu);
        }

    }

    public void Arroser()
    {
        Console.WriteLine("---");
        Console.WriteLine("Voici l'état d'humidité de vos plantes : ");
        int numero = 0;
        foreach (Plante plante in Pot.ListePlantes)
        {
            if (plante.CoorX != -1 && plante.CoorY != -1)
            {
                Console.WriteLine($"- {numero}. {plante.Espece} : niveau actuel : {plante.NiveauHumidite} | niveau optimal : {plante.SeuilHumidite} | diminution de l'humidité par tour : {plante.BesoinEau} ");
                numero++;
            }
        }
        Console.WriteLine("---");
        Console.WriteLine("Arroser une plante augmente son niveau d'humidité actuel de 10. ");
        Console.WriteLine("Entrez les numéros des plantes à arroser un par un. Entrez 1000 pour arrêter l'arrosage. ");

        int numeroAArroser = -1;
        bool continuer = true;
        while (continuer)
        {
            string reponse = Console.ReadLine()!;
            if (Int32.TryParse(reponse, out numeroAArroser))
            {
                if (numeroAArroser == 1000)
                    continuer = false;
                else if (numeroAArroser >= 0 && numeroAArroser < Pot.ListePlantes.Count)
                {
                    Pot.ListePlantes[numeroAArroser].NiveauHumidite += 10;
                }
                else
                {
                    Console.WriteLine("Numéro invalide. Essayez encore.");
                }
            }
            else
            {
                Console.WriteLine("Réponse invalide. Entrez un numéro valide ou 1000 pour arrêter.");
            }
        }
    }

    public void MajBesoinEau()
    {
        foreach (Plante plante in Pot.ListePlantes)
        {
            if (plante.NiveauHumidite <= 0)
            {
                plante.NiveauHumidite = 0;
            }
            else
                plante.NiveauHumidite -= plante.BesoinEau;
        }
    }

    public void VerifierEsperanceDeVie(Plante plante)
    {
        if (plante.EsperanceDeVie < NumeroTour - plante.TourPlantation)
            plante.EstMorte();
    }


    // Achats : 
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
            Console.WriteLine("Confirmez l'achat : Entrez Oui ou Non");
            string reponse = Console.ReadLine()!;
            ChoixOuiNon choix;
            while (!Enum.TryParse<ChoixOuiNon>(reponse, true, out choix))
            {
                Console.WriteLine("Entrée invalide. Veuillez saisir un choix valide : Oui, Non");
                reponse = Console.ReadLine()!;
            }
            if (choix == ChoixOuiNon.Oui)
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
        Console.WriteLine("Vous pouvez acheter les graines suivantes :");
        foreach (Graine graine in Pot.SacDeGraines)
        {
            Console.WriteLine($"- {numero}. {graine.Espece} : {graine.Quantite} unités");
            numero++;
        }
        Console.WriteLine($"- {numero}. Retour à la liste des achats");
        Console.WriteLine("Quel est le numéro de la graine que vous voulez acheter ? ");
        string reponse = Console.ReadLine()!;
        int numeroAAcheter;
        while (!Int32.TryParse(reponse, out numeroAAcheter) || (numeroAAcheter < 0) || (numeroAAcheter > Pot.SacDeGraines.Count))
        {
            Console.WriteLine("Vous n'avez pas entré un nombre valide. Quel est le numéro de la graine que vous voulez acheter ? ");
            reponse = Console.ReadLine()!;
        }
        if (numeroAAcheter == numero)
        {
            Acheter();
            return;
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
            Console.WriteLine($"Le prix par item est {prixUnitaire}");
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
            bool achatConfirme = PayerAchat(prixTotal);
            if (achatConfirme)
            {
                AjouterAchat(numero, nombreUnites);
                if (achatSouhaite.Nom == Achat.Chien)
                {
                    PresenceChien = true;
                    CreerAnimal("Chien");
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
                reponse = Console.ReadLine()!;
            }
            if (numeroAAcheter != 1000)
            {
                if (PresenceChien && (numeroAAcheter - 1 == 3))
                {
                    Console.WriteLine("Vous ne pouvez acheter qu'un chien. Effectuez un autre achat.");
                    return;
                }
                else
                {
                    EffectuerAchat(numeroAAcheter - 1);
                    break;
                }
                numeroAAcheter = -1;
            }
        }
        if (ListeAchats[4] != 0) { PresenceEpouvantail = true; }
    }

    public void PoserAchat()
    {
        bool presenceAchat = false;
        for (int i = 0; i < ListeAchats.Count(); i++)
        {
            //On ignore les items que l'on ne peut pas poser
            if ((i != 1) && (i != 3) && (i != 6) && (i != 8) && (i != 10))
            {
                if (ListeAchats[i] != 0)
                {
                    presenceAchat = true;
                }
            }
        }
        if (!presenceAchat)
        {
            Console.WriteLine("Vous n'avez aucun item à poser.");
        }
        else
        {
            int numero = 0;
            Console.WriteLine("Vous possédez les items suivants :");
            foreach (int nombreAchat in ListeAchats)
            {
                if ((nombreAchat != 0) && (numero != 3) && (numero != 6) && (numero != 1) && (numero != 8) && (numero != 10)) //On ne peut pas poser : chien (3), graine (6), bache (1), pompe (8), tuyau d'arrosage (10)
                {
                    Console.WriteLine($"- {numero}. {achatsPossibles[numero].Nom} : {nombreAchat} unités");
                }
                numero++;
            }
            Console.WriteLine("Vous ne pouvez utiliser les baches, pompes et tuyau d'arrosage qu'en cas d'intempéries ; ils n'apparaissent pas dans la liste ci-dessus.");
            Console.WriteLine("Même chose pour le chien et l'épouvantail qui ne sont utilisable qu'en cas d'animaux urgent à faire fuir");
            Console.WriteLine("Pour planter une graine, référez vous à l'action (1) du menu principal");
            Console.WriteLine("Quel est le numéro de l'achat que vous voulez utiliser ? ");
            string reponse = Console.ReadLine()!;
            int numeroAPoser;
            while (!Int32.TryParse(reponse, out numeroAPoser) || (numeroAPoser < 0) || (numeroAPoser >= ListeAchats.Count))
            {
                Console.WriteLine("Vous n'avez pas entré un nombre valide. Quel est le numéro de l'achat que vous voulez utiliser ? ");
                reponse = Console.ReadLine()!;
            }
            if (numeroAPoser == 0)
            {
                Pot.EffetArrosageAutomatique();
                Console.WriteLine("Vous avez installé un arrosage automatique.");
            }
            else if (numeroAPoser == 2)
            {
                PoserCoccinelle();
                Console.WriteLine("Vous avez posé des coccinelles.");
            }
            else if (numeroAPoser == 4)
            {
                PresenceEpouvantail = true;
                Console.WriteLine("Vous avez posé un épouvantail.");
            }
            else if (numeroAPoser == 5)
            {
                Pot.EffetFertilisant();
                Console.WriteLine("Vous avez choisi le fertilisant, il a amélioré la production maximum de toutes les plantes du potager.");
            }
            else if (numeroAPoser == 7)
            {
                PresenceLampeUV = true;
                Console.WriteLine("Vous avez posé des lampes UV.");
            }
            else if (numeroAPoser == 9)
            {
                PresenceSerre = true;
                Console.WriteLine("Vous avez posé une serre.");
            }
            else if ((numeroAPoser == 11) || (numeroAPoser == 12) || (numeroAPoser == 13))
            {
                //
                Console.WriteLine("Vous avez posé un  remède.");
                Pot.EffetPoserRemede(numeroAPoser); //Le Console.WriteLine pour dire qu'on a utilisé un remède est dans la méthode Pot.EffetPoserRemede.
            }
        }
    }

    public void ImpactAchatPose() //Effectue l'impact pour les achats déjà posé
    {
        if (PresenceLampeUV) Pot.EffetLampeUV();
        if (PresenceArrosageAutomatique) Pot.EffetArrosageAutomatique();
        if (PresenceSerre) Pot.EffetSerre();
    }


    // Initialisation, actions et affichage : 
    public void InitialisationPotager(Potager Pot, string[,] grille)
    {
        for (int i = 0; i < Pot.Hauteur; i++)
        {
            for (int j = 0; j < Pot.Longueur; j++)
            {
                grille[i, j] = " 🔳 ";
            }
        }
    }

    public void MajConditionsPotager()
    {
        Console.WriteLine("---");
        Console.WriteLine("-- Statuts du potager --");
        Console.WriteLine($"Saison : {Pot.Saison.Nom}, Température : {Pot.Temperature}");
        Console.WriteLine("---");

    }


    public void MajAffichagePlantes(string[,] grille)

    {
        InitialisationPotager(Pot, grille);
        foreach (Plante plante in Pot.ListePlantes)
        {
            if (plante.CoorX != -1 && plante.CoorY != -1)
            {
                if ((plante.Espece == "Artichaut") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " atc";
                if ((plante.Espece == "Artichaut") && (plante.Taille == 2)) grille[plante.CoorY, plante.CoorX] = " ATC";
                if ((plante.Espece == "Artichaut") && (plante.Taille == 3)) grille[plante.CoorY, plante.CoorX] = "🌲";
                if ((plante.Espece == "Artichaut") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = "🥦";

                if ((plante.Espece == "Aubergine") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " aub";
                if ((plante.Espece == "Aubergine") && (plante.Taille == 2)) grille[plante.CoorY, plante.CoorX] = " AUB";
                if ((plante.Espece == "Aubergine") && (plante.Taille == 3)) grille[plante.CoorY, plante.CoorX] = "🌾";
                if ((plante.Espece == "Aubergine") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = "🍆";

                if ((plante.Espece == "Basilic") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " bsl";
                if ((plante.Espece == "Basilic") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = "🪴";

                if ((plante.Espece == "Oignon") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " ogn";
                if ((plante.Espece == "Oignon") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = "🧅";

                if ((plante.Espece == "Olivier") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " olv";
                if ((plante.Espece == "Olivier") && (plante.Taille == 2)) grille[plante.CoorY, plante.CoorX] = " OLV";
                if ((plante.Espece == "Olivier") && (plante.Taille == 3)) grille[plante.CoorY, plante.CoorX] = "🌿";
                if ((plante.Espece == "Olivier") && (plante.Taille == 4)) grille[plante.CoorY, plante.CoorX] = "🌳";
                if ((plante.Espece == "Olivier") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = "🫒";

                if ((plante.Espece == "Poivron") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " pvr";
                if ((plante.Espece == "Poivron") && (plante.Taille == 2)) grille[plante.CoorY, plante.CoorX] = " PVR";
                if ((plante.Espece == "Poivron") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = "🫑";

                if ((plante.Espece == "Roquette") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " rqt";
                if ((plante.Espece == "Roquette") && (plante.Taille == 2)) grille[plante.CoorY, plante.CoorX] = " RQT";
                if ((plante.Espece == "Roquette") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = "🥬";

                if ((plante.Espece == "Thym") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " thy";
                if ((plante.Espece == "Thym") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = "🌱";

                if ((plante.Espece == "Tomate") && (plante.Taille == 1)) grille[plante.CoorY, plante.CoorX] = " tmt";
                if ((plante.Espece == "Tomate") && (plante.Taille == 2)) grille[plante.CoorY, plante.CoorX] = " TMT";
                if ((plante.Espece == "Tomate") && (plante.Taille == plante.TailleMax)) grille[plante.CoorY, plante.CoorX] = "🍅";
            }
        }
    }

    public List<string> MajAffichageAnimaux(string[,] grille)
    {
        List<string> listeAnimauxAfficher = new List<string>();
        listeAnimauxAfficher.Add("Certains animaux ne sont pas visibles sur la grille car ils sont sur des plantes ou d'autres animaux : ");
        string emoji = "";
        foreach (Animaux animal in Pot.ListeAnimaux)
        {
            if ((animal.X != -1) && (animal.Y != -1))
            {
                if (animal.Nom == "Chien") emoji = "🐕";
                if (animal.Nom == "Abeille") emoji = "🐝";
                if (animal.Nom == "Chat") emoji = "🐈";
                if (animal.Nom == "Coccinelle") emoji = "🐞";
                if (animal.Nom == "Escargot") emoji = "🐌";
                if (animal.Nom == "Oiseau") emoji = "‍🐦";
                if (animal.Nom == "Pucerons") emoji = "🦗";
                if (animal.Nom == "Rongeur") emoji = "🐀";
                if (animal.Nom == "VersDeTerre") emoji = "🪱 ";
                if (grille[animal.X, animal.Y] == " 🔳 ")
                {
                    grille[animal.X, animal.Y] = " " + emoji + " ";
                }
                else // Cette partie permet de ne pas surcharger l'affichage.
                {
                    listeAnimauxAfficher.Add($"- {emoji} | ligne : {animal.X}, colonne : {animal.Y}");
                }
            }
        }
        return listeAnimauxAfficher;
    }


    public void ChoisirActionsTour(ref bool jeuEnCours, ref string[,] grille, Simulation simu)
    {
        int reponse;
        do
        {
            ApparaitreHasardAnimal();
            MajAffichagePlantes(grille);
            List<string> ajoutAffichage = MajAffichageAnimaux(grille);
            AffichageComplet(grille);
            if (ajoutAffichage.Count >= 2)
            {
                foreach (string message in ajoutAffichage)
                {
                    Console.WriteLine(message);
                }
            }
            Console.WriteLine("Choisissez une action du menu principal :");
            string rep = Console.ReadLine()!;
            while (!int.TryParse(rep, out reponse))
            {
                Console.WriteLine("Vous n'avez pas entré un nombre valide. Que voulez-vous faire ? ");
                rep = Console.ReadLine()!;
            }
            if (reponse == 1) Planter(simu);
            if (reponse == 2) Acheter();
            if (reponse == 3) Arroser();
            if (reponse == 4) PoserAchat();
            if (reponse == 6) AfficherWiki();
        }
        while (reponse != 5 && reponse != 7);
        if (reponse == 5) NumeroTour += 1;
        if (reponse == 7) jeuEnCours = false;
    }

    public void AfficherWiki()
    {
        Console.WriteLine("Bienvenue dans le Wiki, tu trouveras ici toutes les informations nécessaires pour prendre soin de ton super potager !");
        Console.WriteLine(@"
╔═════════════════════════════════════════════════════════╦════════════╦════════════╦════════════╦════════════╗
║ Plantes Vivaces                                         ║ Artichaut  ║ Aubergine  ║  Olivier   ║ Thym       ║
╠═════════════════════════════════════════════════════════╬════════════╬════════════╬════════════╬════════════╣
║ Terrain préféré                                         ║ Terre      ║ Terre      ║ Terre      ║ Calcaire   ║
║ Saison de semis                                         ║ Printemps  ║ Printemps  ║ Automne    ║ Printemps  ║
║ Saison de récolte                                       ║ Automne    ║ Été        ║ Automne    ║ Été        ║
║ Espacement (1 = 50 cm)                                  ║ 3          ║ 1          ║ 14         ║ 1          ║
║ Quota Croissance (somme taille max des plantes autour)  ║ 10         ║ 20         ║ 8          ║ 30         ║
║ Taille maximale de la plante                            ║ 4          ║ 4          ║ 5          ║ 2          ║
║ Temps de croissance (tours)                             ║ 3          ║ 3          ║ 12         ║ 3          ║
║ Humidité préférée                                       ║ 60%        ║ 50%        ║ 40%        ║ 50%        ║
║ Luminosité préférée                                     ║ 90%        ║ 80%        ║ 85%        ║ 90%        ║
║ Température préférée                                    ║ 15-25°C    ║ 20-28°C    ║ 20-30°C    ║ 15-25°C    ║
╠═════════════════════════════════════════════════════════╬════════════╬════════════╬════════════╬════════════╣
║ Maladies que la plante peut attraper                    ║ Mildiou    ║ Mildiou    ║ Mildiou    ║ Oidium     ║
║ Probabilité d’attraper ces maladies                     ║ 15%        ║ 30%        ║ 40%        ║ 15%        ║
╠═════════════════════════════════════════════════════════╬════════════╬════════════╬════════════╬════════════╣
║ Espérance de vie (nb de tours)                          ║ 55         ║ 40         ║ 200        ║ 44         ║
║ Quantité produite par plant                             ║ 5          ║ 2          ║ 5000       ║ 20         ║
║ Récoltes possibles par saison                           ║ 2          ║ 3          ║ 1          ║ 2          ║
╚═════════════════════════════════════════════════════════╩════════════╩════════════╩════════════╩════════════╝
");

        Console.WriteLine(@"
╔═════════════════════════════════════════════════════════╦════════════╦═════════╦══════════╦══════════╦══════════╗
║ Plantes Annuelles                                       ║ Basilic    ║ Oignon  ║ Poivron  ║ Roquette ║ Tomate   ║
╠═════════════════════════════════════════════════════════╬════════════╬═════════╬══════════╬══════════╬══════════╣
║ Terrain préféré                                         ║ Terre      ║ Terre   ║ Terre    ║ Terre    ║ Terre    ║
║ Saison de semis                                         ║ Printemps  ║ Automne ║ Hiver    ║ Été      ║ Printemps║
║ Saison de récolte                                       ║ Été        ║ Été     ║ été      ║ Automne  ║ Été      ║
║ Espacement (1 = 50 cm)                                  ║ 0          ║ 0       ║ 1        ║ 0        ║ 1        ║
║ Quota Croissance (somme taille max des plantes autour)  ║ 18         ║ 20      ║ 15       ║ 25       ║ 30       ║
║ Taille maximale de la plante                            ║ 2          ║ 2       ║ 3        ║ 3        ║ 3        ║
║ Temps de croissance (tours)                             ║ 3          ║ 3       ║ 2        ║ 2        ║ 4        ║
║ Besoins en eau                                          ║ 5          ║ 5       ║ 10       ║ 5        ║ 10       ║
║ Humidité préférée                                       ║ 60%        ║ 70%     ║ 80%      ║ 65%      ║ 80%      ║
║ Luminosité préférée                                     ║ 85%        ║ 90%     ║ 90%      ║ 70%      ║ 90%      ║
║ Zone de température préférée                            ║ 20-25°C    ║ 5-38°C  ║ 20-28°C  ║ 10-20°C  ║ 15-30°C  ║
╠═════════════════════════════════════════════════════════╬════════════╬═════════╬══════════╬══════════╬══════════╣
║ Maladies que la plante peut attraper                    ║ Fusariose, ║ Mildiou ║ Mildiou, ║ Mildiou  ║ Mildiou, ║
║                                                         ║ Mildiou,   ║         ║ Oidium   ║          ║ Oidium   ║
║                                                         ║ Oidium     ║         ║          ║          ║          ║
╠═════════════════════════════════════════════════════════╬════════════╬═════════╬══════════╬══════════╬══════════╣
║ Probabilités d’attraper ces maladies                    ║ 35%,10%,20%║ 20%     ║ 35%,25%  ║ 30%      ║ 50%,20%  ║
╠═════════════════════════════════════════════════════════╬════════════╬═════════╬══════════╬══════════╬══════════╣
║ Espérance de vie (nb de tours)                          ║ 12         ║ 12      ║ 12       ║ 12       ║ 12       ║
║ Quantité produite par plant                             ║ 20         ║ 1       ║ 6        ║ 10       ║ 30       ║
║ Récoltes possibles par saison                           ║ 3          ║ 1       ║ 3        ║ 3        ║ 3        ║
╚═════════════════════════════════════════════════════════╩════════════╩═════════╩══════════╩══════════╩══════════╝
");


        Console.WriteLine("");
        Console.WriteLine("Entre 1000 pour revenir au menu principal.");

        bool continuer = true;
        int saisie;
        while (continuer)
        {
            string reponse = Console.ReadLine()!;
            if (Int32.TryParse(reponse, out saisie))
            {
                if (saisie == 1000)
                    continuer = false;
            }
            else
            {
                Console.WriteLine("Réponse invalide. Entrez un numéro valide ou 1000 pour arrêter.");
            }
        }

    }

    public void AffichageComplet(string[,] grille)
    {
        //Console.Clear();
        Console.WriteLine("--- Statuts du potager ---");
        Console.WriteLine($"Saison : {Pot.Saison.Nom}, Température : {Pot.Temperature}");
        Console.WriteLine();

        // Construire les lignes du potager (gauche)
        List<string> lignesPotager = new List<string>();
        for (int i = 0; i < Pot.Hauteur; i++)
        {
            string ligne = "";
            for (int j = 0; j < Pot.Longueur; j++)
            {
                ligne += grille[i, j];
            }
            lignesPotager.Add(ligne);
            lignesPotager.Add("");
        }

        // Construire les lignes de droite (récoltes, statuts, menu)
        List<string> lignesDroite = new List<string>();
        lignesDroite.Add("-- Récoltes : --");
        foreach (Recolte recolte in Pot.Inventaire)
        {
            if (recolte.Quantite != 0)
                lignesDroite.Add($"- {recolte.Espece} : {recolte.Quantite}");
        }
        lignesDroite.Add("");
        lignesDroite.Add("-- Statuts des plantes : --");
        foreach (Plante plante in Pot.ListePlantes)
        {
            if (plante.CoorX != -1 && plante.CoorY != -1)
            {
                lignesDroite.Add($"Statuts {plante.Espece} : Taille :{plante.Taille}, Santé {plante.Sante} ");
                lignesDroite.Add($"  | Humidité {plante.NiveauHumidite}, Luminosité {plante.NiveauLuminosite}, Température : {plante.NiveauTemperature}");
            }
        }
        lignesDroite.Add("");
        lignesDroite.Add("-- Menu Principal --");
        lignesDroite.Add("(1) Planter une graine");
        lignesDroite.Add("(2) Faire un Achat");
        lignesDroite.Add("(3) Arroser");
        lignesDroite.Add("(4) Poser un item de votre inventaire");
        lignesDroite.Add("(5) Avancer dans le temps");
        lignesDroite.Add("(6) Afficher le Wiki");
        lignesDroite.Add("(7) Quitter le jeu");

        int largeurAffichage = Pot.Longueur * 4;
        int maxLignes = Math.Max(lignesPotager.Count, lignesDroite.Count);

        // 1. Afficher potager + droite, ligne par ligne, tant qu'il y a des lignes de potager
        for (int i = 0; i < lignesPotager.Count; i++)
        {
            string gauche = lignesPotager[i];
            string droite = i < lignesDroite.Count ? lignesDroite[i] : "";
            Console.WriteLine(string.Format("{0,-" + largeurAffichage + "}   {1}", gauche, droite));
        }
        // 2. Si la partie droite est plus longue, continuer à l'afficher seule
        for (int i = lignesPotager.Count; i < lignesDroite.Count; i++)
        {
            Console.WriteLine($"{new string(' ', largeurAffichage)}   {lignesDroite[i]}");
        }
    }

    public void TirerAuSortIntemperie(Simulation simu, Grele grele, Inondation inondation, Secheresse secheresse, Saison saison, string[,] grille)
    {
        Random rng = new Random();
        int probaGrele = 10;
        int probaInondation = 10;
        int probaSecheresse = 0;
        if (saison == Saison.Hiver)
            probaGrele = 20;
        if (saison == Saison.Ete)
            probaSecheresse = 25;
        if (saison == Saison.Automne || saison == Saison.Printemps)
            probaInondation = 20;
        if (simu.PresenceArrosageAutomatique)
            probaSecheresse = 0;  //Les plantes ne sont plus affectées => plus d'urgence
        int tirageGrele = rng.Next(0, 101);
        if (tirageGrele < probaGrele) // Inférieur stricte pour si proba de 0
        {
            Grele = true;
            simu.mode = ModeDeJeu.Urgence;
            AffichageUrgence(ref grille, grele, simu.ActionUrgente, simu.Pot, simu);
        }
        int tirageInondation = rng.Next(0, 101);
        if (tirageInondation < probaInondation)
        {
            Inondation = true;
            simu.mode = ModeDeJeu.Urgence;
            AffichageUrgence(ref grille, inondation, simu.ActionUrgente, simu.Pot, simu);
        }
        int tirageSecheresse = rng.Next(0, 101);
        if (tirageSecheresse < probaSecheresse)
        {
            Secheresse = true;
            simu.mode = ModeDeJeu.Urgence;
            AffichageUrgence(ref grille, secheresse, simu.ActionUrgente, simu.Pot, simu);
        }
    }

    public void TirerAuSortAnimauxUrgents(Simulation simu, ref Oiseau oiseau, ref Chat chat, ref Rongeur rongeur, string[,] grille)
    {
        Random rng = new Random();
        int probaOiseau = 20;
        int probaChat = 7;
        int probaRongeur = 10;
        if (simu.PresenceEpouvantail)
            probaOiseau = 0;
        if (simu.PresenceChien)
            probaRongeur = 0;
        int tirageOiseau = rng.Next(0, 101);
        if (tirageOiseau < probaOiseau)
        {
            oiseau.X = rng.Next(0, Pot.Hauteur);
            oiseau.Y = rng.Next(0, Pot.Longueur);
            simu.mode = ModeDeJeu.Urgence;
            AffichageUrgence(ref grille, oiseau, simu.ActionUrgente, simu.Pot, simu);
        }
        int tirageChat = rng.Next(0, 101);
        if (tirageChat < probaChat)
        {
            chat.X = rng.Next(0, Pot.Hauteur);
            chat.Y = rng.Next(0, Pot.Longueur);
            simu.mode = ModeDeJeu.Urgence;
            AffichageUrgence(ref grille, chat, simu.ActionUrgente, simu.Pot, simu);
        }
        int tirageRongeur = rng.Next(0, 101);
        if (tirageRongeur <= probaRongeur)
        {
            rongeur.X = rng.Next(0, Pot.Hauteur);
            rongeur.Y = rng.Next(0, Pot.Longueur);
            simu.mode = ModeDeJeu.Urgence;
            AffichageUrgence(ref grille, rongeur, simu.ActionUrgente, simu.Pot, simu);
        }
    }

    public void AffichageUrgence(ref string[,] grille, object pb, ActionUrgente actionUrgente, Potager pot, Simulation simu)
    {
        int duree = 1;
        Console.Clear();
        Console.WriteLine("--- URGENCE ---");
        Console.WriteLine($"Urgence : {pb}");
        if (pb is Grele) Console.WriteLine("⛈️⛈️⛈️⛈️⛈️⛈️");
        if (pb is Inondation) Console.WriteLine("🌊🌊🌊🌊🌊🌊");
        if (pb is Secheresse) Console.WriteLine("☀️☀️☀️☀️☀️☀️");
        Console.WriteLine();

        bool dureeEffectuee = false;
        while (simu.mode == ModeDeJeu.Urgence && !dureeEffectuee)
        {
            MajAffichagePlantes(grille);
            MajAffichageAnimaux(grille); //On ne récupère pas la liste renvoyée car en urgence on n'indique pas le positionnement des animaux non visibles.
            foreach (Animaux animal in Pot.ListeAnimaux) // Comme l'animal d'urgence se déplace il faut vérifier si des animaux sont mangés.
            {
                animal.EstMange();
            }

            List<string> lignesPotager = new List<string>();
            for (int i = 0; i < Pot.Hauteur; i++)
            {
                string ligne = "";
                for (int j = 0; j < Pot.Longueur; j++)
                {
                    ligne += grille[i, j];
                }
                lignesPotager.Add(ligne);
                lignesPotager.Add("");
            }
            for (int i = 0; i < lignesPotager.Count; i++)
            {
                Console.WriteLine(lignesPotager[i]);
            }
            if (pb is Intemperie intemp)
            {
                intemp.EffetIntemperie();
                duree++;
                if (duree >= intemp.Duree) { dureeEffectuee = true; }
            }
            if (pb is AnimauxMauvais ani)
            {
                foreach (Plante plante in Pot.ListePlantes)
                {
                    ani.Effet(plante);
                }
                ani.SeDeplacer();
            }
            actionUrgente.ProposerAction(pb, pot, simu);
        }
        if (pb is AnimauxMauvais anim)
        {
            anim.X = -1;
            anim.Y = -1;
        }
        simu.mode = ModeDeJeu.Classique;
        Console.WriteLine("-- Fin de l'urgence -- \n <Retour au mode de jeu classique>");
    }

    public void Simuler(Potager pot, Simulation simu)
    {
        bool jeuEnCours = true;
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

        //Ajout à l'inventaire
        Pot.Inventaire.Add(RecArtichaut);
        Pot.Inventaire.Add(RecAubergine);
        Pot.Inventaire.Add(RecBasilic);
        Pot.Inventaire.Add(RecOignon);
        Pot.Inventaire.Add(RecOlivier);
        Pot.Inventaire.Add(RecPoivron);
        Pot.Inventaire.Add(RecRoquette);
        Pot.Inventaire.Add(RecThym);
        Pot.Inventaire.Add(RecTomate);

        // Création des intempéries et animaux pour le mode urgence
        Grele grele = new Grele(Pot);
        Inondation inondation = new Inondation(Pot);
        Secheresse secheresse = new Secheresse(Pot);
        ActionUrgente = new ActionUrgente();
        Oiseau oiseau = new Oiseau(Pot);
        oiseau.X = -1;
        oiseau.Y = -1;
        Pot.ListeAnimaux.Add(oiseau);
        Chat chat = new Chat(Pot);
        chat.X = -1;
        chat.Y = -1;
        Pot.ListeAnimaux.Add(chat);
        Rongeur rongeur = new Rongeur(Pot);
        rongeur.X = -1;
        rongeur.Y = -1;
        Pot.ListeAnimaux.Add(rongeur);

        string[,] GrillePotager = new string[pot.Hauteur, pot.Longueur];
        InitialisationPotager(pot, GrillePotager);

        while (jeuEnCours)
        {
            if (simu.mode == ModeDeJeu.Classique)
            {
                EvolutionAnimaux();
                MajAffichagePlantes(GrillePotager);
                List<string> ajoutAffichage = MajAffichageAnimaux(GrillePotager);
                MajBesoinEau();

                foreach (Plante plante in pot.ListePlantes)
                {
                    plante.MettreAJourPlantesAutour();
                    VerifierEsperanceDeVie(plante);
                    ImpactAchatPose();
                    plante.ImpactConditions();
                    plante.ProbabiliteTomberMalade();
                    plante.Contamination();
                    if (NumeroTour % plante.TempsCroissance == 0)
                        plante.Grandir();
                    if (pot.Saison.Nom == plante.SaisondeRecolte && plante.NbRecolte < plante.NbRecoltePossible)
                        plante.DonnerRecolte(pot, AssocierRecoltePlante(plante, RecArtichaut, RecAubergine, RecBasilic, RecOignon, RecOlivier, RecPoivron, RecRoquette, RecThym, RecTomate));
                    Console.WriteLine(plante);

                }
                if (NumeroTour % 12 == 1)
                {
                    pot.Saison.Nom = Saison.Printemps;
                    Pot.Saison.ChangerBesoinEau();
                    Pot.Saison.ChangerTemperature();
                }
                if (NumeroTour % 12 == 4)
                {
                    pot.Saison.Nom = Saison.Ete;
                    Pot.Saison.ChangerBesoinEau();
                    Pot.Saison.ChangerTemperature();
                }
                if (NumeroTour % 12 == 7)
                {
                    pot.Saison.Nom = Saison.Automne;
                    Pot.Saison.ChangerBesoinEau();
                    Pot.Saison.ChangerTemperature();
                }
                if (NumeroTour % 12 == 10)
                {
                    pot.Saison.Nom = Saison.Hiver;
                    Pot.Saison.ChangerBesoinEau();
                    Pot.Saison.ChangerTemperature();
                }

                ChoisirActionsTour(ref jeuEnCours, ref GrillePotager, simu);
                if (jeuEnCours)
                {
                    Random rng = new Random();
                    if (rng.Next(1, 3) == 1)  //Pour éviter d'avoir les 2 urgences.
                        TirerAuSortAnimauxUrgents(simu, ref oiseau, ref chat, ref rongeur, GrillePotager);
                    else
                        TirerAuSortIntemperie(simu, grele, inondation, secheresse, Pot.Saison.Nom, GrillePotager);
                }
            }

        }
        Console.WriteLine("-- FIN DE LA PARTIE --");
        Console.WriteLine(" Merci d'avoir joué ! A très vite ;)");
    }


}