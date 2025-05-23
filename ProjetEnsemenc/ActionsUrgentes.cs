public class ActionUrgente
{


    public ActionUrgente() { }

    public void GererUrgenceAvecTimeout(object pb, Potager pot, Simulation simu, string[,] grille, int timeoutMs, int tickMs)
    {
        int ticks = timeoutMs / tickMs;
        int duree = 1;
        bool urgenceFinie = false;
        string? saisie = null;
        var saisieTask = Task.Run(() => Console.ReadLine());

        while (!urgenceFinie)
        {
            Console.Clear();
            Console.WriteLine("--- URGENCE ---");
            Console.WriteLine($"Urgence : {pb}");
            if (pb is Grele) Console.WriteLine("⛈️⛈️⛈️⛈️⛈️⛈️");
            if (pb is Inondation) Console.WriteLine("🌊🌊🌊🌊🌊🌊");
            if (pb is Secheresse) Console.WriteLine("☀️☀️☀️☀️☀️☀️");
            simu.MajAffichagePlantes(grille);
            simu.MajAffichageAnimaux(grille);

            // Effets récurrents
            if (pb is Intemperie intemp)
            {
                intemp.EffetIntemperie();
                duree++;
                if (duree > intemp.Duree) urgenceFinie = true;
            }
            else if (pb is AnimauxMauvais ani)
            {
                foreach (Plante plante in pot.ListePlantes)
                    ani.Effet(plante);
                ani.SeDeplacer();
            }

            //Maj potager
            simu.MajAffichagePlantes(grille);
            simu.MajAffichageAnimaux(grille);
            //Affichage
            for (int i = 0; i < pot.Hauteur; i++)
            {
                for (int j = 0; j < pot.Longueur; j++)
                {
                    Console.Write(grille[i, j]);
                }
                Console.WriteLine("");
            }
            Thread.Sleep(200);

            // Affichage des actions possibles
            if (pb is Animaux)
            {
                Console.WriteLine("Vous pouvez : ");
                Console.WriteLine("1 - Faire du bruit (contre : Oiseaux et Rongeurs) \n2 - Poser un épouvantail (Plus aucun oiseau) \n3 - Faire fuir le chat (contre : Chat) \n4 - Adopter un chien (plus aucun rongeur)");
            }
            else if (pb is Intemperie)
            {
                Console.WriteLine("Vous pouvez : ");
                Console.WriteLine("5 - Mettre une bâche (contre : Grêle) \n6 - Installer une pompe (contre : Inondation) \n7 - Utiliser le tuyau d'arrosage (contre : Sécheresse) \n8 - installer l'arrosage automatique (contre : Sécheresse)");
            }
            Console.WriteLine($"Ecrivez un numéro puis appuyez sur entrée (appuyer deux fois si ça ne fonctionne pas).");
            Console.WriteLine($"Vous avez {timeoutMs / 1000.0} secondes pour agir !");

            // Attente de la saisie utilisateur
            for (int t = 0; t < ticks; t++)
            {
                if (saisieTask.IsCompleted)
                {
                    saisie = saisieTask.Result;
                    break;
                }
                Thread.Sleep(tickMs);
            }

            // Si le joueur a répondu
            if (saisieTask.IsCompleted && saisie != null && Convert.ToInt16(saisie) > 0 && Convert.ToInt16(saisie) < 9)
            {
                int choix;
                if (int.TryParse(saisie, out choix))
                {
                    if (pb is Animaux)
                    {
                        if (pb is Animaux)
                        {
                            if (choix == 1) { urgenceFinie = FaireBruit(pb, simu); }
                            else if (choix == 2) { urgenceFinie = PoserEpouvantail(pb, simu); }
                            else if (choix == 3) { urgenceFinie = FaireFuirChat(pb, simu); }
                            else if (choix == 4) { urgenceFinie = AdopterChien(pb, simu); }
                        }
                    }
                    else if (pb is Intemperie)
                    {
                        if (pb is Intemperie)
                        {
                            if (choix == 5) { urgenceFinie = PoserBache(pb, simu); }
                            else if (choix == 6) { urgenceFinie = InstallerPompe(pb, simu); }
                            else if (choix == 7) { urgenceFinie = Arroser(pb, pot, simu); }
                            else if (choix == 8) { urgenceFinie = InstallerArosageAuto(pb, pot, simu); }
                        }
                    }
                }
                if (!urgenceFinie)
                {
                    saisie = null;
                    saisieTask = Task.Run(() => Console.ReadLine());
                }

            }
            else
            {
                Console.WriteLine("Entrée invalide. Veuillez entrer un nombre entre 1 et 8.");
                saisie = null;
                saisieTask = Task.Run(() => Console.ReadLine());
            }
        }

        // Fin de l'urgence
        if (pb is AnimauxMauvais anim)
        {
            anim.X = -1;
            anim.Y = -1;
        }
        simu.mode = ModeDeJeu.Classique;
        Console.WriteLine("-- Fin de l'urgence -- \n <Retour au mode de jeu classique>");
        Thread.Sleep(1000);

        //Pour éviter problème de triple saisie
        while (Console.KeyAvailable)
            Console.ReadKey(true);
    }
    private bool FaireBruit(object Sujet, Simulation simu)
    {
        if (Sujet is Oiseau oiseau)
        {
            oiseau.Disparait();
            Console.WriteLine("L'oiseau a fuit !");
            simu.mode = ModeDeJeu.Classique;
            return true;
        }
        if (Sujet is Rongeur rongeur)
        {
            rongeur.Disparait();
            Console.WriteLine("Le rongeur a fuit !");
            simu.mode = ModeDeJeu.Classique;
            return true;
        }
        else
        {
            Console.WriteLine("Aucun Effet");
            return false;
        }
    }

    private bool PoserEpouvantail(object Sujet, Simulation simu)
    {
        if (Sujet is Oiseau oiseau)
        {
            if (simu.ListeAchats[4] > 0)
            {
                oiseau.Disparait();
                simu.PresenceEpouvantail = true;
                simu.ListeAchats[4]--;
                Console.WriteLine("Vous avez posé un épouvantail, vous n'aurez plus jamais d'oiseau !");
                simu.mode = ModeDeJeu.Classique;
                return true;
            }
            else
            {
                Console.WriteLine("Vous n'avez pas d'épouvantail !");
                return false;
            }
        }
        else
        {
            Console.WriteLine("Aucun Effet");
            return false;
        }
    }
    private bool FaireFuirChat(object Sujet, Simulation simu)
    {
        if (Sujet is Chat chat)
        {
            chat.Disparait();
            Console.WriteLine("Le chat a fuit !");
            simu.mode = ModeDeJeu.Classique;
            return true;
        }
        else
        {
            Console.WriteLine("Aucun Effet");
            return false;
        }
    }
    private bool AdopterChien(object Sujet, Simulation simu)
    {

        if (Sujet is Rongeur rongeur)
        {
            if (simu.ListeAchats[3] > 0)
            {
                rongeur.Disparait();
                Console.WriteLine("Vous avez adopté un chien, vous n'aurez plus jamais de rongeur sur vos terres !");
                simu.PresenceChien = true;
                simu.mode = ModeDeJeu.Classique;
                return true;
            }
            else
            {
                Console.WriteLine("Il n'y avait pas de chien à adopter...");
                return false;
            }
        }
        else
        {
            Console.WriteLine("Aucun Effet");
            return false;
        }
    }
    private bool PoserBache(object Sujet, Simulation simu)
    {
        if ((Sujet is Grele) && simu.ListeAchats[1] > 0)
        {
            simu.PresenceBache = true;
            Console.WriteLine("Vous avez posé une bâche, il n'y aura pas plus de dégâts lors de cette urgence");
            simu.mode = ModeDeJeu.Classique;
            simu.ListeAchats[1]--;
            return true;
        }
        else if (simu.ListeAchats[1] == 0)
        {
            Console.WriteLine("Vous ne possédez pas de bâche, il va falloir attendre que la météo se calme...");
            return false;
        }
        else
        {
            Console.WriteLine("Aucun Effet");
            return false;
        }
    }
    private bool InstallerPompe(object Sujet, Simulation simu)
    {
        if ((Sujet is Inondation) && simu.ListeAchats[8] > 0)
        {
            simu.PresencePompe = true;
            Console.WriteLine("Vous avez posé une pompe pour arrêter l'inondation, il n'y aura pas plus de dégâts lors de cette urgence");
            simu.mode = ModeDeJeu.Classique;
            //On ne perd pas la pompe
            return true;
        }
        else if (simu.ListeAchats[8] == 0)
        {
            Console.WriteLine("Vous ne possédez pas de pompe, il va falloir attendre que la météo se calme...");
            return false;
        }
        else
        {
            Console.WriteLine("Aucun Effet");
            return false;
        }
    }
    private bool Arroser(object Sujet, Potager pot, Simulation simu)
    {
        if ((Sujet is Secheresse) && simu.ListeAchats[10] > 0)
        {
            pot.EffetArroserTuyau();
            Console.WriteLine("Vous avez arrosé vos plantes, mais un arrosage automatique règlerait le problème");
            //On ne perd pas le tuyau d'arrosage
            return true;
        }
        else if (simu.ListeAchats[10] == 0)
        {
            Console.WriteLine("Vous ne possédez pas de tuyau d'arrosage, il va falloir attendre que la météo se calme...");
            return false;
        }
        else
        {
            Console.WriteLine("Aucun Effet");
            return false;
        }
    }
    private bool InstallerArosageAuto(object Sujet, Potager pot, Simulation simu)
    {
        if ((Sujet is Secheresse) && simu.ListeAchats[0] > 0)
        {
            simu.PresenceArrosageAutomatique = true;
            pot.EffetArrosageAutomatique();
            Console.WriteLine("Vous avez installé l'arrosage automatique, vos plantes ne souffriront plus de la sécheresse");
            simu.mode = ModeDeJeu.Classique;
            simu.ListeAchats[0]--;
            return true;
        }
        else if (simu.ListeAchats[0] == 0)
        {
            Console.WriteLine("Vous ne possédez pas d'arrosage automatique, il va falloir attendre que la météo se calme...");
            return false;
        }
        else
        {
            Console.WriteLine("Aucun Effet");
            return false;
        }
    }
}

