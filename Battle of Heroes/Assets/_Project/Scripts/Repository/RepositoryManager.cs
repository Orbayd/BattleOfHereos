using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;


public class RepositoryManager
{
    private string FileName => "UserDbo";

    private string DataPath => Application.persistentDataPath;

    public UserDbo Dbo { get; private set; }

    private HeroConfig _config;

    public RepositoryManager(HeroConfig config)
    {
        _config = config;
    }

    public void Load()
    {
        if (!File.Exists(GetFilePath()))
        {
            FirstPlay();
            return;
        }
        var output = File.ReadAllText(GetFilePath());
        Dbo = JsonConvert.DeserializeObject<UserDbo>(output);

        foreach (var herodbo in Dbo.Heroes)
        {
            herodbo.HeroData = _config.Heroes.First(x => x.Name == herodbo.Id);
        }
    }

    private void FirstPlay()
    {
        Dbo = new UserDbo();
        Dbo.Heroes ??= new List<HeroDbo>();

        foreach (var data in _config.Heroes)
        {
            var herodbo = new HeroDbo();
            herodbo.HeroData = data;
            herodbo.Id = data.Name;
            Dbo.Heroes.Add(herodbo);
        }
        Save();
    }



    public void Save()
    {
        var output = JsonConvert.SerializeObject(Dbo);
        
        File.WriteAllText(GetFilePath(), output);
    }

    private string GetFilePath()
    {
        return Path.Combine(DataPath, FileName+".json");
    }
}
