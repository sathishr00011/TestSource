using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manifest.Contracts
{
    public enum ManifestType
    {
        AppStore,
        Tibco,
        OMS,
        InStore
    }

    public enum ManifestArguments
    {
        Version,
        Regions,
        Template,
        Output,
        Tag,
        SearchDirectoryPath
    }
}
