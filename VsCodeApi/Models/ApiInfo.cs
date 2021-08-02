using System;
using System.ComponentModel.DataAnnotations;

namespace VsCodeApi.Models
{
    public class ApiInfo : IComparable<ApiInfo>
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public string DocUrl { get; set; }
        [Required]
        public int Upvote { get; set; } = 0;

        public int CompareTo(ApiInfo other)
        {
            var c = -Upvote.CompareTo(other.Upvote);
            return (c != 0) ? c : Id.CompareTo(other.Id);
        }

        public static void MemSet(ApiInfo Destination, ApiInfo Source)
        {
            Destination.Name = Source.Name;
            Destination.Url = Source.Url;
            Destination.DocUrl = Source.DocUrl;
            Destination.Description = Source.Description;
            Destination.Upvote = Source.Upvote;
        }
    }
}
