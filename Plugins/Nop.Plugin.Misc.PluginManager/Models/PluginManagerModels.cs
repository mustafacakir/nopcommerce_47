using System.Text.Json.Serialization;

namespace Nop.Plugin.Misc.PluginManager.Models;

public class PluginDescriptorRaw
{
    [JsonPropertyName("Group")]
    public string Group { get; set; }

    [JsonPropertyName("FriendlyName")]
    public string FriendlyName { get; set; }

    [JsonPropertyName("SystemName")]
    public string SystemName { get; set; }

    [JsonPropertyName("Version")]
    public string Version { get; set; }

    [JsonPropertyName("SupportedVersions")]
    public List<string> SupportedVersions { get; set; }

    [JsonPropertyName("Author")]
    public string Author { get; set; }

    [JsonPropertyName("DisplayOrder")]
    public int DisplayOrder { get; set; }

    [JsonPropertyName("FileName")]
    public string FileName { get; set; }

    [JsonPropertyName("Description")]
    public string Description { get; set; }

    [JsonPropertyName("LimitedToStores")]
    public List<int> LimitedToStores { get; set; } = new();
}

public class PluginStoreEntry
{
    public string SystemName { get; set; }
    public string FriendlyName { get; set; }
    public string Group { get; set; }
    public List<int> LimitedToStores { get; set; } = new();
    public string FilePath { get; set; }
}

public class PluginManagerIndexModel
{
    public List<PluginStoreEntry> Plugins { get; set; } = new();
    public List<StoreEntry> Stores { get; set; } = new();
}

public class StoreEntry
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class SavePluginRequest
{
    public string FilePath { get; set; }
    public List<int> StoreIds { get; set; } = new();
}
