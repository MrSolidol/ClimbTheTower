using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public static class PhraseDataBase
{
    public enum NPC
    {
        Oldman = 0,
        Girl = 1,
        Brick = 2
    }

    public static List<List<string>> OldManDialogs;
    public static List<List<string>> GirlDialogs;
    public static List<List<string>> BrickDialogs;

    static PhraseDataBase() 
    {
        OldManDialogs = new List<List<string>>()
        {
            new List<string>()
            {
                "0.0",
                "0.1",
                "0.2",
                "0.3",
                "0.4"
            },
            new List<string>()
            {
                "1.0",
                "1.1",
                "1.2",
                "1.3",
                "1.4"
            },
            new List<string>()
            {
                "2.0",
                "2.1",
                "2.2",
                "2.3",
                "2.4",
                "2.5",
                "2.6"
            },
            new List<string>()
            {
                "3.0",
                "3.1",
                "3.2",
                "3.3",
                "3.4",
                "3.5"
            },
            new List<string>()
            {
                "4.0",
                "4.1",
                "4.2",
                "4.3",
                "4.4",
                "4.5",
                "4.6",
                "4.7",
                "4.8"
            }
        };

        GirlDialogs = new List<List<string>>()
        {
            new List<string>()
            {
                "0.0",
                "0.1",
                "0.2",
                "0.3",
                "0.4",
                "0.5"
            },
            new List<string>()
            {
                "1.0",
                "1.1",
                "1.2",
                "1.3",
                "1.4",
                "1.5",
                "1.6"
            },
            new List<string>()
            {
                "2.0",
                "2.1",
                "2.2",
                "2.3",
                "2.4",
                "2.5"
            },
            new List<string>()
            {
                "3.0",
                "3.1",
                "3.2",
                "3.3",
                "3.4",
                "3.5",
                "3.6"
            },
            new List<string>()
            {
                "4.0",
                "4.1",
                "4.2",
                "4.3",
                "4.4",
                "4.5",
                "4.6",
                "4.7"
            }
        };

        BrickDialogs = new List<List<string>>()
        {
            new List<string>()
            {
                "0.0",
                "0.1",
                "0.2",
                "0.3",
                "0.4",
                "0.5"
            },
            new List<string>()
            {
                "1.0",
                "1.1",
                "1.2",
                "1.3",
                "1.4",
                "1.5"
            },
            new List<string>()
            {
                "2.0",
                "2.1",
                "2.2",
                "2.3",
                "2.4"
            },
            new List<string>()
            {
                "3.0",
                "3.1",
                "3.2",
                "3.3",
                "3.4",
                "3.5",
                "3.6"
            },
            new List<string>()
            {
                "4.0",
                "4.1",
                "4.2",
                "4.3",
                "4.4",
                "4.5",
                "4.6",
                "4.7"
            }
        };

    }

    public static List<List<string>> GetDialogs(int number) 
    {
        switch (number) 
        {
            case 0:
                return OldManDialogs;
            case 1:
                return GirlDialogs;
            default:
                return BrickDialogs;
        }
    }
    public static string GetTables(int number)
    {
        switch (number)
        {
            case 0:
                return "OldMan Table";
            case 1:
                return "Girl Table";
            default:
                return "Brick Table";
        }
    }
}
