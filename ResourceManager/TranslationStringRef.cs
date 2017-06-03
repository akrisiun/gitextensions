using System.Diagnostics;

namespace ResourceManager
{
    /// <summary>Provides translated text.</summary>
    [DebuggerDisplay("{String}")]
    public class TranslationStringRef
    {
        /// <summary>Creates a new <see cref="TranslationStringRef"/> with the specified <paramref name="text"/>.</summary>
        public TranslationStringRef(string text)
        {
            String = new TranslationString(text);
        }

        public TranslationString String { get; set; }
    }
}
