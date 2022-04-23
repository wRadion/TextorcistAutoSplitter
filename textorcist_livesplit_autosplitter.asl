state("Textorcist")
{
    /* Those are the only two static and useful pointers I found... */

    /* The first one changes according to vars declared below */
    int menu : "Textorcist.exe", 0x3FAEA8;

    /* The second one changes by 1 when pausing/unpausing in game (+1 when pausing, -1 when unpausing)
       and changes by *a lot* when a boss is loaded or a level is exited */
    int pauseRetry : "Textorcist.exe", 0x3FB120;
}

startup
{
    vars.IsStarted = false;
    vars.IsReset = false;

    /* Possibles values for `menu` */
    vars.CaesarMenu = 231;
    vars.MasterMenu = 239;
    vars.LilithMenu = 241;
    /* This means that it is the same `menu` value for Cardinal and Laurentius */
    vars.CardinalLaurentiusMenu = 242;
    /* Matthew, Enoch Varg and Mother Superior has the same `menu` value */
    vars.MatthewEnochMotherMenu = 244;
    vars.RoomMenu = 245;
    vars.MagdaMenu = 246;
    vars.FuriusMenu = 247;
    /* Title screen, Level/Location selection and Boss Rush has the same `menu` value */
    vars.MenuSelectionBossRushMenu = 248;

    settings.Add("singleBoss", false, "Single Boss");
}

start
{
    if (old.menu != current.menu)
    {
        /* When we go into a boss level from the location selection screen */
        if (old.menu == vars.MenuSelectionBossRushMenu && current.menu != vars.RoomMenu)
        {
            return true;
        }
    }
    else if (vars.IsReset && current.pauseRetry - old.pauseRetry > 1)
    {
        /* This is here so the timer actually restart when Retrying and not just reset */
        vars.IsReset = false;
        return true;
    }
}

reset
{
    if (settings["singleBoss"])
    {
        if (current.pauseRetry - old.pauseRetry > 1)
        {
            /* The `pauseRetry` variable changes right after the `menu` one which causes
               the timer to reset instantly when it starts. This is here to avoid that */
            if (!vars.IsStarted)
            {
                vars.IsStarted = true;
                return false;
            }

            vars.IsReset = true;
            return true;
        }
    }
}
