using TMPro;

namespace CodeBase.Extensions
{
    public static class TMPTextExtension
    {
        public static void Clear(this TMP_Text tmpText) => 
            tmpText.text = string.Empty;
    }
}