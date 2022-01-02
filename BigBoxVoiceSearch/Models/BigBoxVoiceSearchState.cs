using System;
using System.Collections.Generic;
using System.Text;

namespace BigBoxVoiceSearch.Models
{
    public enum BigBoxVoiceSearchState
    {
        None,
        Initializing,
        InitializeFailed,
        Inactive,
        Active,
        Recognizing
    }
}
