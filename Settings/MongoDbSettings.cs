namespace SuperHeroAPI.Settings;

public class MongoDbSettings
{
    public string Host { get; set; }
    public int Port { get; set; }

    public string connectionString => $"mongodb://{Host}:{Port}";
}