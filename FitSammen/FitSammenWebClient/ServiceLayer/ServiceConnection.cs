namespace FitSammenWebClient.ServiceLayer {
    public abstract class ServiceConnection {

        protected readonly HttpClient _httpEnabler;

        public ServiceConnection(HttpClient httpEnabler, string? inBaseUrl) {
            _httpEnabler = httpEnabler;
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
