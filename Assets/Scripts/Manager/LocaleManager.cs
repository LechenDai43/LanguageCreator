using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;

public class LocaleManager
{
    // Store all the locales this application has
    public SystemLocale[] locales;
    // The index of current locale (based on locales)
    public int localePointer;


    // Populate the locales and set localePointer to 0
    public void initialize()
    {
        // Get the root path of the application
        string rootPath = Application.dataPath;

        // Read the content table of directory of locale path
        string localeContentTablePath = rootPath + "/Files/Locale/localeList.content";
        StreamReader sr = new StreamReader(localeContentTablePath);
        LocaleList localeList = JsonUtility.FromJson<LocaleList>(sr.ReadToEnd());
        sr.Close();

        // Read the locale files
        int numOfLocales = localeList.getLength();
        locales = new SystemLocale[numOfLocales];
        for (int i = 0; i < numOfLocales; i++)
        {
            string thisLocalePath = rootPath + localeList.getDirectory(i);
            sr = new StreamReader(thisLocalePath);
            string contentOfThisLocaleFile = sr.ReadToEnd();
            sr.Close();
            SystemLocale thisLocale = JsonUtility.FromJson<SystemLocale>(contentOfThisLocaleFile);
            locales[i] = thisLocale;
        }

        // Set the first locale as the default locale
        localePointer = 0;
    }

    // Get the list of all the available locales
    public string[] getAllLocales()
    {
        string[] result = new string[locales.Length];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = locales[i].Tag;
        }
        return result;
    }

    // Change the locale
    public void changeLocale(int key)
    {
        localePointer = key;
    }

    // Get the current locale index
    public int getCurrentLocale()
    {
        return localePointer;
    }

    // Get the text of the initial screen of the current locale
    public InitialScreenLocale getInitialScreenLocale()
    {
        return locales[localePointer].InitialScreenLocale;
    }

    // Directories of the locale files
    [Serializable]
    public class LocaleList
    {
        public string[] Locales;

        public int getLength()
        {
            return Locales.Length;
        }

        public string getDirectory(int i)
        {
            if (i >= 0 && i < this.getLength())
            {
                return Locales[i];
            }
            else
            {
                return null;
            }
        }
    }

    // The text used in the whole application
    [Serializable]
    public class SystemLocale
    {
        // The language tag for this locale
        public string Tag;
        // The text to display on the initial screen
        public InitialScreenLocale InitialScreenLocale;
    }

    // The text to display on the initial screen
    [Serializable]
    public class InitialScreenLocale
    {
        // The text for the "logo" text field
        public string LogoText;
        // The text for the "new" button
        public string NewButton;
        // The text for the "old" button
        public string OldButton;
    }
}
