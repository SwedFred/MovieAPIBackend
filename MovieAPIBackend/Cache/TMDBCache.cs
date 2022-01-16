using MovieAPIBackend.Models;
using System.Collections.Concurrent;
using System.Text.Json;

namespace MovieAPIBackend.Cache
{
  public class TMDBCache
  {
    public ConcurrentDictionary<string, Genres> movieGenres = new ConcurrentDictionary<string, Genres>();

    private readonly ILogger<TMDBCache> _logger;
    private string Url { get; set; } = "";
    private string Key { get; set; }
    private List<string> SupportedLanguages;
    

    public TMDBCache(ILogger<TMDBCache> logger, IConfiguration configuration)
    {
      _logger = logger;
      Key = configuration.GetSection("Settings").GetValue<string>("TMDBKey");
      Url = configuration.GetSection("Settings").GetValue<string>("TMDBUrl");
      var languages = configuration.GetSection("Settings").GetSection("SupportedLanguages").GetChildren().Select(x => x.Value).ToList();
      if (languages == null || !languages.Any())
        SupportedLanguages = new List<string>() { "en-US" };
      else
        SupportedLanguages = languages;
      PopulateMovieGenres();
    }


    private async void PopulateMovieGenres()
    {
      ConcurrentDictionary<string, Dictionary<string, string>> genres = new ConcurrentDictionary<string, Dictionary<string, string>>();
      foreach (var lang in SupportedLanguages)
      {
        using (var httpClient = new HttpClient())
        {
          var url = $"{Url}genre/movie/list?api_key={Key}" + (lang.Length > 0 ? $"&language={lang}" : "");

          var response = httpClient.GetAsync(url).Result;

          if (!response.IsSuccessStatusCode)
          {
            _logger.LogError("failure, /genre/movie/list, " + response.ReasonPhrase);
          }
          else
          {
            var result = await response.Content.ReadAsStringAsync();
            var genre = JsonSerializer.Deserialize<Genres>(result);
            if (genre != null)
              movieGenres.TryAdd(lang, genre);
          }
        }
      }
    }
  }
}
