
public class ActionUrgente
{


    public ActionUrgente() { }
    public string? ReadLineWithTimeout(int timeoutMs)
    {
        var task = Task.Run(() => Console.ReadLine());
        if (task.Wait(timeoutMs))
            return task.Result;
        return null; // Timeout
    }
    public void ProposerAction(object Sujet, Potager Pot, Simulation simu)
    {
        if (Sujet is Animaux)
        {
            Console.WriteLine("Vous pouvez : ");
            Console.WriteLine("1 - Faire du bruit (contre : Oiseaux et Rongeurs) \n2 - Poser un épouvantail (Plus aucun oiseau) \n3 - Faire fuir le chat (contre : Chat) \n4 - Adopter un chien (plus aucun rongeur)");
        }
        else if (Sujet is Intemperie)
        {
            Console.WriteLine("Vous pouvez : ");
            Console.WriteLine("5 - Mettre une bâche (contre : Grêle) \n6 - Installer une pompe (contre : Inondation) \n7 - Utiliser le tuyau d'arrosage (contre : Sécheresse) \n8 - installer l'arrosage automatique (contre : Sécheresse)");
        }

        string? saisie = ReadLineWithTimeout(4000);
        if (saisie == null)
        {
            // Si aucune saisie on fait passer le temps
            return;
        }
        int choix;
        if (!int.TryParse(saisie, out choix))
        {
            // Saisie invalide, on ignore et on laisse la boucle continuer
            return;
        }
        if (choix == 1)
        {
            FaireBruit(Sujet, simu);
        }
        else if (choix == 2)
        {
            PoserEpouvantail(Sujet, simu);
        }
        else if (choix == 3)
        {
            FaireFuirChat(Sujet, simu);
        }
        else if (choix == 4)
        {
            AdopterChien(Sujet, simu);
        }
        else if (choix == 5)
        {
            PoserBache(Sujet, simu);
        }
        else if (choix == 6)
        {
            InstallerPompe(Sujet, simu);
        }
        else if (choix == 7)
        {
            Arroser(Sujet, Pot, simu);
        }
        else if (choix == 8)
        {
            InstallerArosageAuto(Sujet, Pot, simu);
        }
    }

    public void FaireBruit(object Sujet, Simulation simu)
    {
        if (Sujet is Oiseau oiseau)
        {
            oiseau.Disparait();
            Console.WriteLine("L'oiseau a fuit !");
            simu.mode = ModeDeJeu.Classique;
        }
        if (Sujet is Rongeur rongeur)
        {
            rongeur.Disparait();
            Console.WriteLine("Le rongeur a fuit !");
            simu.mode = ModeDeJeu.Classique;
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }

    public void PoserEpouvantail(object Sujet, Simulation simu)
    {
        if (Sujet is Oiseau oiseau)
        {
            oiseau.Disparait();
            simu.PresenceEpouvantail = true;
            Console.WriteLine("Vous avez posé un épouvantail, vous n'aurez plus jamais d'oiseau !");
            simu.mode = ModeDeJeu.Classique;
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void FaireFuirChat(object Sujet, Simulation simu)
    {
        if (Sujet is Chat chat)
        {
            chat.Disparait();
            Console.WriteLine("Le chat a fuit !");
            simu.mode = ModeDeJeu.Classique;
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void AdopterChien(object Sujet, Simulation simu)
    {
        if (Sujet is Rongeur rongeur)
        {
            rongeur.Disparait();
            Console.WriteLine("Vous avez adopté un chien, vous n'aurez plus jamais de rongeur sur vos terres !");
            simu.PresenceChien = true;
            simu.mode = ModeDeJeu.Classique;
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void PoserBache(object Sujet, Simulation simu)
    {
        if ((Sujet is Grele) && simu.ListeAchats[1] > 0)
        {
            simu.PresenceBache = true;
            Console.WriteLine("Vous avez posé une bâche, il n'y aura pas plus de dégâts lors de cette urgence");
            simu.mode = ModeDeJeu.Classique;
        }
        else if (simu.ListeAchats[1] == 0)
        {
            Console.WriteLine("Vous ne possédez pas de bâche, il va falloir attendre que la météo se calme...");
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void InstallerPompe(object Sujet, Simulation simu)
    {
        if ((Sujet is Inondation) && simu.ListeAchats[8] > 0)
        {
            simu.PresencePompe = true;
            Console.WriteLine("Vous avez posé une pompe pour arrêter l'inondation, il n'y aura pas plus de dégâts lors de cette urgence");
            simu.mode = ModeDeJeu.Classique;
        }
        else if (simu.ListeAchats[8] == 0)
        {
            Console.WriteLine("Vous ne possédez pas de pompe, il va falloir attendre que la météo se calme...");
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void Arroser(object Sujet, Potager pot, Simulation simu)
    {
        if ((Sujet is Secheresse) && simu.ListeAchats[10] > 0)
        {
            pot.EffetArroserTuyau();
            Console.WriteLine("Vous avez arrosé vos plantes, mais un arrosage automatique règlerait le problème");
        }
        else if (simu.ListeAchats[10] == 0)
        {
            Console.WriteLine("Vous ne possédez pas de tuyau d'arrosage, il va falloir attendre que la météo se calme...");
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
    public void InstallerArosageAuto(object Sujet, Potager pot, Simulation simu)
    {
        if ((Sujet is Secheresse) && simu.ListeAchats[0] > 0)
        {
            simu.PresenceArrosageAutomatique = true;
            pot.EffetArrosageAutomatique();
            Console.WriteLine("Vous avez installé l'arrosage automatique, vos plantes ne souffriront plus de la sécheresse");
            simu.mode = ModeDeJeu.Classique;
        }
        else if (simu.ListeAchats[0] == 0)
        {
            Console.WriteLine("Vous ne possédez pas d'arrosage automatique, il va falloir attendre que la météo se calme...");
        }
        else
        {
            Console.WriteLine("Aucun Effet");
        }
    }
}

