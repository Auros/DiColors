using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DiColors.Services
{
    /*public class ProfileManager
    {
        private bool _loaded;
        private readonly Dictionary<string, Config> _loadedProfiles = new Dictionary<string, Config>();

        internal async Task LoadProfiles()
        {
            await Task.Run(() =>
            {
                if (_loaded)
                {
                    return;
                }
                _loadedProfiles.Clear();
                Directory.CreateDirectory(Constants.FOLDERDIR);
                Directory.CreateDirectory(Constants.PROFILEDIR);
                var info = new DirectoryInfo(Constants.PROFILEDIR);
                var files = info.EnumerateFiles().Where(x => x.Extension.EndsWith("json"));
                for (int i = 0; i < files.Count(); i++)
                {
                    var file = files.ElementAt(i);
                    var fileText = File.ReadAllText(file.FullName);
                    var config = JsonConvert.DeserializeObject<Config>(fileText);
                    _loadedProfiles.Add(file.FullName, config);
                }
                _loaded = true;
            });
        }

        internal async Task<Config[]> GetAllProfiles()
        {
            await LoadProfiles();
            return _loadedProfiles.Values.ToArray();
        }

        internal async Task<Config> AddProfile(Config config)
        {
            await LoadProfiles();
            var path = Path.Combine(Constants.PROFILEDIR, config.Name);
            return await Task.Run(() =>
            {
                var json = JsonConvert.SerializeObject(config);
                var newConfig = JsonConvert.DeserializeObject<Config>(json);
                File.WriteAllText(path, json);
                _loadedProfiles.Add(path, newConfig);
                return newConfig;
            });
        }

        internal async Task DeleteProfile(Config config)
        {
            await LoadProfiles();
            await Task.Run(() =>
            {
                var conf = _loadedProfiles.FirstOrDefault(c => c.Value.Name == config.Name);
                if (!conf.Equals(default(KeyValuePair<string, Config>)))
                {
                    File.Delete(conf.Key);
                    _loadedProfiles.Remove(conf.Key);
                }
            });
        }

        internal async Task SaveProfile(Config config)
        {
            await LoadProfiles();
            await Task.Run(() =>
            {
                var conf = _loadedProfiles.FirstOrDefault(c => c.Value.Name == config.Name);
                if (!conf.Equals(default(KeyValuePair<string, Config>)))
                {
                    File.WriteAllText(conf.Key, JsonConvert.SerializeObject(config));
                }
            });
        }
    }*/
}