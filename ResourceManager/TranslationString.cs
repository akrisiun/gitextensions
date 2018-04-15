using System;
using System.Diagnostics;

namespace ResourceManager
{
    /// <summary>Provides translated text.</summary>
    [DebuggerDisplay("{Text}")]
    [CLSCompliant(false)]
    public class TranslationString3 : TranslationString
    {
        /// <summary>Creates a new <see cref="TranslationString"/> with the specified <paramref name="text"/>.</summary>
        public TranslationString3(string text) : base(text)
        {
            // Text = text;
        }

        /// <summary>Gets the translated text.</summary>
        //  public string Text { get; private set; }

        /// <summary>Returns <see cref="Text"/> value.</summary>
        public override string ToString() { return Text; }
    }
}


namespace ResourceManager.Texts
{
}

namespace GitCommands.Texts
{
}