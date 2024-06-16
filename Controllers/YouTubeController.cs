using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Model;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YouTubeController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetChannelVideos(string? pageToken = null, int maxResults = 50)
        {
            var youTubeService = new YouTubeService(new BaseClientService.Initializer
            {
                ApiKey = "AIzaSyAe3PlvM-9MdR7B73G72gDnsIR7TtRe78o",
                ApplicationName = "SuperHeros"
            });

            var searchRequest = youTubeService.Search.List("snippet");
            searchRequest.ChannelId = "UCbBkUvZOIcn5xWt3c-C3Sbg";
            searchRequest.Order = SearchResource.ListRequest.OrderEnum.Date;
            searchRequest.MaxResults = maxResults;
            searchRequest.PageToken = pageToken;

            var searchResponse = await searchRequest.ExecuteAsync();

            var videoList = searchResponse.Items.Select(item => new VideoDetails
            {
                Title = item.Snippet.Title,
                Link = $"https://www.youtube.com/watch?v={item.Id.VideoId}",
                Thumbnail = item.Snippet.Thumbnails.Medium.Url,
                PublishedAt = item.Snippet.PublishedAtDateTimeOffset
            }).OrderByDescending(video => video.PublishedAt)
            .ToList();

            var response = new YouTubeResponse
            {
                Video = videoList,
                NextPageToken = searchResponse.NextPageToken,
                PrevPageToken = searchResponse.PrevPageToken
            };

            //return Ok(searchResponse); // Complete Response from youtube api
            return Ok(response);
        }
    }
}
