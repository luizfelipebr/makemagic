using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace makemagic.Utils
{
  /// <summary>
  /// Accessing the external PotterAPI
  /// </summary>
  public class PotterApi
  {
    private readonly string key = "";
    private readonly string uri = "https://www.potterapi.com/v1/";
    private HttpClient httpClient;

    /// <summary>
    /// Initializing the PotterApi object
    /// </summary>
    public PotterApi()
    {
      this.key = Settings.Secret;
      httpClient = HttpClientFactory.Create();
    }

    /// <summary>
    /// To verify if the given house exists into PotterAPI repository
    /// </summary>
    /// <param name="houseId">The ID of the House</param>
    /// <returns>True: if the House exists; False: if the House doesn't exists or if could not connect to the PotterAPI</returns>
    public async Task<bool> AccioHouse(string houseId)
    {
      bool houseExists = false;
      string fullSpell = this.uri + "houses/?key=" + this.key;
      try
      {
        HttpResponseMessage httpResponse = await httpClient.GetAsync(fullSpell);
        if (httpResponse.StatusCode == HttpStatusCode.OK)
        {
          var content = httpResponse.Content;
          var data = await content.ReadAsAsync<List<House>>();
          var house = new House(houseId);
          houseExists = data.Exists(h => h._id == houseId);
        }
      }
      catch (HttpRequestException)
      {
      }
      return houseExists;

    }
  }

  /// <summary>
  /// Model class to store the House ID
  /// </summary>
  public class House
  {
    public string _id { get; set; }
    public House() { }
    public House(string id)
    {
      this._id = id;
    }
  }
}