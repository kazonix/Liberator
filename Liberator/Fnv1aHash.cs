namespace Liberator;

internal static class Fnv1aHash
{

    internal static uint Compute(string text)
    {
        uint num = 0U;
        if (text != null)
        {
            num = 2166136261U;
            for (int i = 0; i < text.Length; i++)
            {
                num = ((uint)text[i] ^ num) * 16777619U;
            }
        }
        return num;
    }
}
