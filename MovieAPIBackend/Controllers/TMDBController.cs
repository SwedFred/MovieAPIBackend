using Microsoft.AspNetCore.Mvc;
using MovieAPIBackend.Cache;
using MovieAPIBackend.Models;
using System.Collections.Concurrent;
using System.Text.Json;

namespace MovieAPIBackend.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class TMDBController : ControllerBase
  {
    private readonly ILogger<TMDBController> _logger;
    private string Url { get; set; } = "";
    private string Key { get; set; }
    private TMDBCache cache;

    public TMDBController(ILogger<TMDBController> logger, IConfiguration configuration, TMDBCache tMDBCache)
    {
      _logger = logger;
      Key = configuration.GetSection("Settings").GetValue<string>("TMDBKey");
      Url = configuration.GetSection("Settings").GetValue<string>("TMDBUrl");
      cache = tMDBCache;
    }


    /// <summary>
    /// Gets a list of the currently top rated movies
    /// </summary>
    /// <response code="204">Invalid API key</response>
    [HttpGet]
    [Route("/api/movies/toprated")]
    public async Task<TopRatedData> GetTopRatedMovies(string? language = "", int? page = 1, string? region = "")
    {
      // language must have min length 1, default eu-US, pattern ([a-z]{2})-([A-Z]{2})
      // page is minimum 1, maximum 1000, default 1
      // region mus be uppercase ^[A-Z]{2}$
      using (var httpClient = new HttpClient())
      {
        var url = $"{Url}movie/top_rated?api_key={Key}" + (language.Length > 0 ? $"&language={language}" : "") + (page > 1 ? $"&page={page}" : "") + (region.Length > 0 ? $"$region={region}" : "");

        var response = httpClient.GetAsync(url).Result;

        if (!response.IsSuccessStatusCode)
        {
          _logger.LogError("failure, /api/movies/toprated, " + response.ReasonPhrase);
          return null;
        }

        var result = await response.Content.ReadAsStringAsync();
        var value = JsonSerializer.Deserialize<TopRatedData>(result);
        return value;
      }
    }

    /// <summary>
    /// Gets a list of upcoming movies
    /// </summary>
    /// <response code="204">Invalid API key</response>
    [HttpGet]
    [Route("/api/movies/upcomming")]
    public async Task<UpcomingData> GetUpcommingMovies(string? language = "", int? page = 1, string? region = "")
    {
      // language must have min length 1, default eu-US, pattern ([a-z]{2})-([A-Z]{2})
      // page is minimum 1, maximum 1000, default 1
      // region mus be uppercase ^[A-Z]{2}$
      using (var httpClient = new HttpClient())
      {
        var url = $"{Url}movie/upcoming?api_key={Key}" + (language.Length > 0 ? $"&language={language}" : "") + (page > 1 ? $"&page={page}" : "") + (region.Length > 0 ? $"$region={region}" : "");

        var response = httpClient.GetAsync(url).Result;

        if (!response.IsSuccessStatusCode)
        {
          _logger.LogError("failure, /api/movies/upcoming, " + response.ReasonPhrase);
          return null;
        }

        var result = await response.Content.ReadAsStringAsync();
        var value = JsonSerializer.Deserialize<UpcomingData>(result);
        return value;
      }
    }

    /// <summary>
    /// Gets a list of movies played at theaters right now
    /// </summary>
    /// <response code="204">Invalid API key</response>
    [HttpGet]
    [Route("/api/movies/nowplaying")]
    public async Task<NowPlayingData> GetNowPlayingMovies(string? language = "", int? page = 1, string? region = "")
    {
      // language must have min length 1, default eu-US, pattern ([a-z]{2})-([A-Z]{2})
      // page is minimum 1, maximum 1000, default 1
      // region mus be uppercase ^[A-Z]{2}$
      using (var httpClient = new HttpClient())
      {
        var url = $"{Url}movie/now_playing?api_key={Key}" + (language.Length > 0 ? $"&language={language}" : "") + (page > 1 ? $"&page={page}" : "") + (region.Length > 0 ? $"$region={region}" : "");

        var response = httpClient.GetAsync(url).Result;

        if (!response.IsSuccessStatusCode)
        {
          _logger.LogError("failure, /api/movies/nowplaying, " + response.ReasonPhrase);
          return null;
        }

        var result = await response.Content.ReadAsStringAsync();
        var value = JsonSerializer.Deserialize<NowPlayingData>(result);
        return value;
      }
    }
  }
}