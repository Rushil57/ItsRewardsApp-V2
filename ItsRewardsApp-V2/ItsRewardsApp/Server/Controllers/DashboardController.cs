using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.Models;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
namespace ItsRewardsApp.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;
        private readonly IDashboard _IDashboard;
        private readonly Authentication _authentication;
        string url = "https://api.insightsc3m.com/rdcapp/index.html?access-token=";

        public DashboardController(IDashboard iDashboard, Authentication authentication,IConfiguration configuration, IMemoryCache memoryCache)
        {
            _IDashboard = iDashboard;
            _authentication = authentication;
            _configuration = configuration;
            _memoryCache=memoryCache;
        }
        public async Task<TobaccoPromotion> Get()
        {
            var token = await GetToken();
            TobaccoPromotion tobaccoPromotion = new TobaccoPromotion();

            Marlboro marlboro = new Marlboro();

            marlboro.Name = "Marlboro";
            marlboro.Link = url + token.AccessToken + "&subscription-key=" + _authentication.NewSubScriptionkey + "&brand=marlboro" + "&lat=&lon=&color=&loyalty_id=";
            tobaccoPromotion.Marlboro = marlboro;

            LandM landM = new LandM();
            landM.Name = "LandM";
            landM.Link = url + token.AccessToken + "&subscription-key=" + _authentication.NewSubScriptionkey + "&brand=lm" + "&lat=&lon=&color=&loyalty_id=";
            tobaccoPromotion.LandM = landM;

            Copenhagen copenhagen = new Copenhagen();
            copenhagen.Name = "Copenhagen";
            copenhagen.Link = url + token.AccessToken + "&subscription-key=" + _authentication.NewSubScriptionkey + "&brand=copenhagen" + "&lat=&lon=&color=&loyalty_id=";
            tobaccoPromotion.Copenhagen = copenhagen;

            Skoal skoal = new Skoal();
            skoal.Name = "Skoal";
            skoal.Link = url + token.AccessToken + "&subscription-key=" + _authentication.NewSubScriptionkey + "&brand=Skoal" + "&lat=&lon=&color=&loyalty_id=";
            tobaccoPromotion.Skoal = skoal;

            Black black = new Black();
            black.Name = "Black";
            black.Link = url + token.AccessToken + "&subscription-key=" + _authentication.NewSubScriptionkey + "&brand=blackandmild" + "&lat=&lon=&color=&loyalty_id=";
            tobaccoPromotion.Black = black;
            tobaccoPromotion.IsLocal = _configuration["IsLocalHost"]; //to check if running environment is local or not
            tobaccoPromotion.ProjectApp = _configuration["ProjectApp"]; //to get the value of image from configuration
  

            return tobaccoPromotion;
        }

        [HttpGet("{AltriaAccountNumber},{cellNumber}")]
        public StoreRetailChoiceDataViewModel Get(string AltriaAccountNumber, string cellNumber)
        {
            var user = _IDashboard.GetStoreNameByAltriaAccountNumber(AltriaAccountNumber, cellNumber);
            return user;
        }

        [HttpGet("{StoreID}")]
        public TobaccoRebate GetStoreById(int StoreID)
        {
            var user = _IDashboard.GetStoreNameByStoreId(StoreID);
            return user;
        }
        private async Task<Token?> GetToken()
        {
            var cacheKey = "accessToken";
            //check if token exists in cache
            if (_memoryCache.TryGetValue(cacheKey, out Token tok))
            {
                return tok;
            }
            else
            {
                HttpClient client = new HttpClient();
                string baseAddress = "https://api.insightsc3m.com/altria/oauth2/v2.0/token";
                var form = new Dictionary<string, string>
                {
                    {"grant_type", _authentication.GrantType},
                    {"client_id", _authentication.ClientId},
                    {"client_secret", _authentication.ClientSecret},
                    {"scope",_authentication.Scope },
                };

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _authentication.SubScriptionkey);

                HttpResponseMessage tokenResponse = await client.PostAsync(baseAddress, new FormUrlEncodedContent(form));
                var jsonContent = await tokenResponse.Content.ReadAsStringAsync();
                tok = JsonConvert.DeserializeObject<Token>(jsonContent);
                _memoryCache.Set(cacheKey, tok, DateTime.Now.AddMinutes(50));     //add token into cachememory
                return tok;
               
            }
        }

        [HttpPost]
        public async Task<StoreRetailChoiceDataViewModel> Post(StoreRetailChoiceDataViewModel storeDetail)
        {
            Random generator = new Random();
            String customNumber = generator.Next(0, 10).ToString("D6");
            storeDetail.SessionID = customNumber;
            var token = await GetToken();
            HttpClient client = new HttpClient();
            string baseAddress = "https://api.insightsc3m.com/agdcchoice/v3/choice/RNP/100?AccountNumber=" + storeDetail.AltriaAccountNumber;
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _authentication.NewTokenGenerateKey);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.AccessToken);
            HttpResponseMessage response = await client.PostAsJsonAsync(baseAddress, storeDetail);
            storeDetail.StatusCode = response.StatusCode.ToString();
            storeDetail.ResponsePhares = response.ReasonPhrase.ToString();
            return storeDetail;
        }
        internal class Token
        {
            [JsonProperty("access_token")]
            public string? AccessToken { get; set; }

            [JsonProperty("token_type")]
            public string? TokenType { get; set; }

            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }

            [JsonProperty("refresh_token")]
            public string? RefreshToken { get; set; }
        }
    }
}
