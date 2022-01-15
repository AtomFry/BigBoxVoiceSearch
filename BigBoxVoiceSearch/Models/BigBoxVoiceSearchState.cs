using System;
using System.Collections.Generic;
using System.Text;

namespace BigBoxVoiceSearch.Models
{
    public enum BigBoxVoiceSearchState
    {
        None,
        Initializing,
        InitializingFailed,
        Inactive,
        Active,
        Recognizing
    }
}
