namespace MovieAPIBackend.Models
{
  public class NowPlayingData
  {
    public int page { get; set; }
    public Result[] results { get; set; }
    public Dates dates { get; set; }
    public int total_pages { get; set; }
    public int total_results { get; set; }
  }


  public class Genres
  {
    public Genre[] genres { get; set; }
  }

  public class Genre
  {
    public int id { get; set; }
    public string name { get; set; }
  }


  public class UpcomingData
  {
    public int page { get; set; }
    public Result[] results { get; set; }
    public Dates dates { get; set; }
    public int total_pages { get; set; }
    public int total_results { get; set; }
  }

  public class TopRatedData
  {
    public int page { get; set; }
    public Result[] results { get; set; }
    public int total_results { get; set; }
    public int total_pages { get; set; }
  }

  public class Dates
  {
    public string maximum { get; set; }
    public string minimum { get; set; }
  }

  public class Result
  {
    public string? poster_path { get; set; }
    public bool adult { get; set; }
    public string overview { get; set; }
    public string release_date { get; set; }
    public int[] genre_ids { get; set; }
    public int id { get; set; }
    public string original_title { get; set; }
    public string original_language { get; set; }
    public string title { get; set; }
    public string? backdrop_path { get; set; }
    public float popularity { get; set; }
    public int vote_count { get; set; }
    public bool video { get; set; }
    public float vote_average { get; set; }
  }
}
