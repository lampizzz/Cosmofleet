using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class LanguageSwitcher : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown languageDropdown;

    private void Start()
    {
        // Подписка на изменение языка
        languageDropdown.onValueChanged.AddListener(ChangeLanguage);

        // Установка текущего языка в Dropdown
        languageDropdown.value = GetCurrentLanguageIndex();
    }

    private void ChangeLanguage(int index)
    {
        string localeCode = index == 0 ? "en" : "ru"; // 0 - English, 1 - Russian
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(localeCode);
    }

    private int GetCurrentLanguageIndex()
    {
        // Возвращаем индекс текущего языка
        return LocalizationSettings.SelectedLocale.Identifier.Code == "en" ? 0 : 1;
    }
}