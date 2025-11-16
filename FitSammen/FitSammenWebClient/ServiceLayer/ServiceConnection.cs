namespace FitSammenWebClient.ServiceLayer {
    public abstract class ServiceConnection {

        private readonly HttpClient _httpEnabler;

        public ServiceConnection(string? inBaseUrl) {
            _httpEnabler = new HttpClient();
            BaseUrl = inBaseUrl;
            UseUrl = BaseUrl;
        }

        public string? BaseUrl { get; init; }
        public string? UseUrl { get; set; }

        public async Task<HttpResponseMessage?> CallServiceGet() {
            HttpResponseMessage? hrm = null;
            if (UseUrl != null) {
                hrm = await _httpEnabler.GetAsync(UseUrl);
            }
            return hrm;
        }

        public async Task<HttpResponseMessage?> CallServicePost(StringContent postJson) {
            HttpResponseMessage? hrm = null;
            if (UseUrl != null) {
                hrm = await _httpEnabler.PostAsync(UseUrl, postJson);
            }
            return hrm;
        }

        public async Task<HttpResponseMessage?> CallServicePut(StringContent postJson) {
            HttpResponseMessage? hrm = null;
            if (UseUrl != null) {
                hrm = await _httpEnabler.PutAsync(UseUrl, postJson);
            }
            return hrm;
        }

        public async Task<HttpResponseMessage?> CallServiceDelete() {
            HttpResponseMessage? hrm = null;
            if (UseUrl != null) {
                hrm = await _httpEnabler.DeleteAsync(UseUrl);
            }
            return hrm;
        }

    }
}
