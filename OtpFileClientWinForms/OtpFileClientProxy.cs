using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace OtpFileClientWinForms
{
    public class OtpFileClientProxy
    {
        private const string RequestUri = "/OtpFileServerWebApi/api/dokumentumok/";


        public IEnumerable<string> GetFolderFiles()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    SetClient(client);

                    var response = client.GetAsync(RequestUri);

                    var content = response.Result.Content.ReadAsStringAsync();

                    if (response.IsFaulted)
                    {
                        throw new OtpApiException
                        {
                            StatusCode = (int)response.Status,
                            Content = content.Result
                        };
                    }

                    return JsonConvert.DeserializeObject<List<string>>(content.Result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OtpFileDownload DownloadFile(string fileName)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    SetClient(client);

                    var response = client.GetAsync($"{RequestUri}{fileName}");

                    var content = response.Result.Content.ReadAsStringAsync();

                    if (response.IsFaulted)
                    {
                        throw new OtpApiException
                        {
                            StatusCode = (int)response.Status,
                            Content = content.Result
                        };
                    }

                    return JsonConvert.DeserializeObject<OtpFileDownload>(content.Result);
                }
            }
            catch (OtpApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new OtpApiException
                {
                    StatusCode = ex.HResult,
                    Content = ex.StackTrace
                };
            }
        }

        public void UploadFile(string fileName, OtpFileUpload otpFileUpload)
        {
            try
            {
                var content = JsonConvert.SerializeObject(otpFileUpload);

                using (var client = new HttpClient())
                {
                    SetClient(client);

                    var response = client.PostAsync($"{RequestUri}{fileName}", new StringContent(content, UTF8Encoding.UTF8, "application/json"));

                    var resultContent = response.Result.Content.ReadAsStringAsync();

                    if (response.IsFaulted || !response.Result.IsSuccessStatusCode)
                    {
                        throw new OtpApiException
                        {
                            StatusCode = (int)response.Result.StatusCode,
                            Content = resultContent.Result
                        };
                    }
                }
            }
            catch (OtpApiException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new OtpApiException
                {
                    StatusCode = ex.HResult,
                    Content = ex.StackTrace
                };
            }
        }

        private void SetClient(HttpClient client)
        {
            client.BaseAddress = new Uri("http://localhost");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
